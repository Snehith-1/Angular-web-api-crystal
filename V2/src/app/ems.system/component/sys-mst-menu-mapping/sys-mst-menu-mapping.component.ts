import { AfterViewInit, Component, ElementRef, ɵdevModeEqual, } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
// import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

// import $ from 'jquery'; 
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-sys-mst-menu-mapping',
  templateUrl:'./sys-mst-menu-mapping.component.html',
  styleUrls: ['./sys-mst-menu-mapping.component.scss']
})
export class SysMstMenuMappingComponent   { 
  menusummary_list:any = [];
  levelonemenu_list: any;
  dplevelone:any;
  getThirdevel:any;
  getFourthevel:any;
  dplevelfour:any;
  levelfourmenu_list: any;
  levelthreemenu_list: any;
  leveltwomenu_list: any;
  dpleveltwo: any;
  dplevelthree: any;
  dpoptions = [
    { entity_gid: 'asp012', entity_name: 'Nizar' },
    { entity_gid: 'asp013', entity_name: 'Yesu' },
    { entity_gid: 'asp014', entity_name: 'bala' },
    { entity_gid: 'asp015', entity_name: 'Sherin' },
    { entity_gid: 'asp016', entity_name: 'sundar' },
    { entity_gid: 'asp017', entity_name: 'Raji' },
  ];
  menuinactivelog_list: any;
  txtmodule_name: any;
  txtremarks: String | null = null;
  rbo_status: any;
  txtmodule_gid: any;
  menu_gid: any;
  Remarksvalidation: any;
  constructor(private SocketService: SocketService,private el: ElementRef,private NgxSpinnerService: NgxSpinnerService,private ToastrService: ToastrService) {
    // this.Remarksvalidation = new FormGroup ({
    //   name : new FormControl('',Validators.required)
    // })
  
  }
 
  ngOnInit() {
    this.NgxSpinnerService.show();
    var url= 'SystemMaster/GetMenuMappingSummary';
    this.SocketService.get(url).subscribe((result:any)=>{
      if(result.menusummary_list != null){
        $('#menumapping').DataTable().destroy();
        this.menusummary_list = result.menusummary_list;  
        this.NgxSpinnerService.hide();
        setTimeout(()=>{   
          $('#menumapping').DataTable();
        }, 1);
      }
      else{
        setTimeout(()=>{   
          $('#menumapping').DataTable();
        }, 1);
        this.menusummary_list = result.menusummary_list; 
        this.NgxSpinnerService.hide();
        $('#menumapping').DataTable().destroy();
      } 
    });

    var url = 'SystemMaster/GetFirstLevelMenu';
    this.SocketService.get(url).subscribe((result: any) => {
      this.levelonemenu_list = result.menu_list;
    });

  }
  Status_update(menu_gid:any){ 
    this.menu_gid = menu_gid;
    var params = {
      menu_gid: menu_gid

  }

  var url = 'SystemMaster/GetMenuMappingEdit';

  this.SocketService.getparams(url,params).subscribe((result:any) => {
        this.txtmodule_name =result.module_name,
        this.txtmodule_gid = result.module_gid,
        this.rbo_status = result.Status 

  });

  var url ='SystemMaster/GetMenuMappingInactivateview'
  this.SocketService.getparams(url, params).subscribe((result:any) => {
    this.menuinactivelog_list = result.menusummary_list;
  });

  }
  update_status(){
    this.NgxSpinnerService.show();
    var params = { 
      module_name:this.txtmodule_name, 
      remarks:this.txtremarks, 
      rbo_status:this.rbo_status,
      menu_gid : this.menu_gid 
  }
  var url = 'SystemMaster/GetMenuMappingInactivate';
  this.SocketService.post(url, params).subscribe((result:any) => {
    console.log(result.status)
    if(result.status == true){
      this.ToastrService.success(result.message)
      this.NgxSpinnerService.hide();
        }
        
        else {

          this.ToastrService.info(result.message)
          this.NgxSpinnerService.hide();
        }  
        this.ngOnInit()
        this.txtremarks = '';
      })

  };

  onChangeFirstlevel(){
    var params = {
      module_gid_parent:this.dplevelone
  }
    var url = 'SystemMaster/GetSecondLevelMenu';
    this.SocketService.getparams(url,params).subscribe((result: any) => {
      this.leveltwomenu_list = result.menu_list;
    });
  }
  onChangeSecoundlevel(){
    var params = {
          module_gid_parent: this.dpleveltwo
      }
        var url = 'SystemMaster/GetThirdLevelMenu';
        this.SocketService.getparams(url,params).subscribe((result: any) => {
          this.levelthreemenu_list = result.menu_list;
        });
  }

onChangeThirdlevel(){
  var params = {
        module_gid_parent: this.dplevelthree
    }
      var url = 'SystemMaster/GetFourthLevelMenu';
      this.SocketService.getparams(url,params).subscribe((result: any) => {
        this.levelfourmenu_list = result.menu_list;
      });
}

AddSubmit() {
  var params = {
    module_gid: this.dplevelfour,
    module_name: this.dplevelfour
  };
  this.NgxSpinnerService.show();
  var url = 'SystemMaster/PostMenudAdd';
  this.SocketService.post(url, params).subscribe((result: any) => {
    if (result.status == true) {
      this.ToastrService.success(result.message)
      this.NgxSpinnerService.hide();
      this.ngOnInit();
      
    this.dplevelone = null;
    this.dpleveltwo = null;
    this.dplevelthree = null;
    this.dplevelfour = null;

    }
    else {
      this.ToastrService.warning(result.message)
      this.NgxSpinnerService.hide();
    }

    
}
)}



  
}