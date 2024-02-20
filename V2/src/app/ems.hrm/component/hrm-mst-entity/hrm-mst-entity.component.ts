import { Component, ElementRef, OnInit, } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-hrm-mst-entity',
  templateUrl: './hrm-mst-entity.component.html',
  styleUrls: ['./hrm-mst-entity.component.scss']
})
export class HrmMstEntityComponent {
  addEntityFormData  = { 
    txtEntity_name: '',
    txtEntity_code: '',

  };
  editEntityFormData = {
    txtedientity_name: '',
    txteditentity_code: '',

  };
  statusEntityFormData = {
    txtentity_name: '',
    rbo_status: '',
    txtremarks: ''  
  };
  txtEntity_name:any
  txtEntity_code:any
  Entity_data :any; 
  responsedata:any;
  params:any;
  entity_gid: any;
  result:any
  txteditaml_name: any;
  txteditentity_code: any;
  txteditlms_code: any;
  txteditbureau_code: any;
  txtedientity_name: any;
  rbo_status: any;
  txtentity_name: any;
  txtremarks: any;
  entityinactivelog_data: any; 
  EntityAddForm : FormGroup | any;
  EntityEditForm : FormGroup | any;
  EntityStatusForm : FormGroup | any;
  parametervalue: any;

  constructor(private SocketService: SocketService,private el: ElementRef,private NgxSpinnerService: NgxSpinnerService,private ToastrService: ToastrService,private FormBuilder: FormBuilder) {
    this.createForm(); 
    this.createEditForm();
    this.createStatusForm();
  }
  createForm(){
    this.EntityAddForm = this.FormBuilder.group({
      txtEntity_name: ['', [Validators.required,Validators.pattern("^(?!\s*$).+")]],
      txtEntity_code: [''],
    });
  }
  createEditForm(){
    this.EntityEditForm = this.FormBuilder.group({
      txtedientity_name: ['', [Validators.required,Validators.pattern("^(?!\s*$).+")]],
      txteditentity_code: [''],
    });
  }
  createStatusForm(){
    this.EntityStatusForm = this.FormBuilder.group({
      // txtremarks: ['', [Validators.required,Validators.pattern("^(?!\s*$).+")]],
      txtremarks: new FormControl(null,

        [

          Validators.required,

          Validators.pattern(/^(?!\s*$).+/)

        ]),
      rbo_status: [''] 
    });
  }
  clearForm() {
    this.EntityAddForm.reset(); 
  }

  ngOnInit() {
    this.NgxSpinnerService.show();
    var url= 'SystemMaster/GetEntity';
    this.SocketService.get(url).subscribe((result:any)=>{
      if(result.application_list != null){
        $('#Entitysummary').DataTable().destroy();
        this.Entity_data = result.application_list;  
        this.NgxSpinnerService.hide();
        setTimeout(()=>{   
          $('#Entitysummary').DataTable();
        }, 1);
      }
      else{
        this.Entity_data = result.application_list; 
        setTimeout(()=>{   
          $('#Entitysummary').DataTable();
        }, 1);
        this.NgxSpinnerService.hide();
        $('#Entitysummary').DataTable().destroy();
      } 
    });
    
  }
  openEntitypopup(){
    this.EntityAddForm.reset(); 
  }
  addEntity(){
    if (this.EntityAddForm.valid) {
      var params = {
        entity_name:this.addEntityFormData.txtEntity_name,
        entity_code:this.addEntityFormData.txtEntity_code,
      }
      this.NgxSpinnerService.show();
      var url = 'SystemMaster/CreateEntity';
      this.SocketService.post(url, params).subscribe((result:any) => {
        if(result.status == true){
          this.NgxSpinnerService.hide();
          this.ToastrService.success(result.message);
        }
        else {
          this.ToastrService.warning(result.message);
          this.NgxSpinnerService.hide();
        }
        this.ngOnInit();
      })
    }
    
  }

  editEntity(entity_gid:any){
    this.entity_gid = entity_gid;
    this.EntityEditForm.reset();
        var params = {
          entity_gid: entity_gid
      }
      this.NgxSpinnerService.show();
      var url = 'SystemMaster/EditEntity';
      this.SocketService.getparams(url, params).subscribe((result:any) => {
        this.editEntityFormData.txtedientity_name = result.entity_name,
        this.editEntityFormData.txteditentity_code = result.entity_code,
        this.NgxSpinnerService.hide();    
      });
  
  }

  update(){
  var url = 'SystemMaster/UpdateEntity';
  var params = {
    entity_name: this.editEntityFormData.txtedientity_name,
    entity_code:this.editEntityFormData.txteditentity_code,
    entity_gid: this.entity_gid
  }
  this.NgxSpinnerService.show();
  this.SocketService.post(url, params).subscribe((result:any) => {    
  if(result.status == true){
    this.ToastrService.success(result.message);
    this.NgxSpinnerService.hide();
    this.ngOnInit();
    }
    else {
      this.ToastrService.warning(result.message);
      this.NgxSpinnerService.hide();
    }   
  })
  }
  Status_update(entity_gid:any){
    this.entity_gid = entity_gid
    this.EntityStatusForm.reset();
  var params = {
    entity_gid: entity_gid
}
this.NgxSpinnerService.show();
var url = 'SystemMaster/EditEntity';
this.SocketService.getparams(url, params).subscribe((result:any) => {
  this.entity_gid =result.entity_gid,
  this.statusEntityFormData.txtentity_name = result.entity_name,
  this.statusEntityFormData.rbo_status = result.Status
  this.NgxSpinnerService.hide(); 
});
var params ={
  entity_gid:entity_gid
}
this.NgxSpinnerService.show();
var url ='SystemMaster/EntityInactiveLogview'
this.SocketService.getparams(url, params).subscribe((result:any) => {
  this.entityinactivelog_data = result.application_list;
  this.NgxSpinnerService.hide(); 
});
}

update_status(){
  var params = {
      entity_gid:this.entity_gid,
      entity_name:this.statusEntityFormData.txtentity_name,
      remarks:this.statusEntityFormData.txtremarks,
      rbo_status:this.statusEntityFormData.rbo_status
}
this.NgxSpinnerService.show();
var url = 'SystemMaster/InactiveEntity';
this.SocketService.post(url, params).subscribe((result:any) => {
  if(result.status == true){
    this.ToastrService.success(result.message);
    this.NgxSpinnerService.hide();
      }
      else {
      this.ToastrService.warning(result.message);
      this.NgxSpinnerService.hide();
      }   
      this.ngOnInit();
    })
};
delete(parameter:any){

  this.parametervalue = parameter
  
   
  
    }

    
  
    ondelete(){
  
      this.NgxSpinnerService.show();
  
      var url = 'SystemMaster/DeleteEntity';
  
      let params = {
  
        entity_gid : this.parametervalue
  
      }
  
      this.SocketService.getparams(url, params).subscribe((result:any) => {
  
        if(result.status == true){
  
          this.NgxSpinnerService.hide();
  
          this.ToastrService.success(result.message);
  
          this.ngOnInit();
  
        }
  
        else {
  
          this.ToastrService.warning(result.message);
  
          this.NgxSpinnerService.hide();
  
          this.ngOnInit();
  
        }  
  
      })
  
    }
// delete(entity_gid:any){
//   var params = {
//     entity_gid: entity_gid
// }
// this.NgxSpinnerService.show();
//   var url = 'SystemMaster/DeleteEntity';
//   this.SocketService.getparams(url, params).subscribe((result:any) => {
//     if(result.status == true){
//       this.ToastrService.success(result.message);
//       this.NgxSpinnerService.hide();
//         }
//         else {
//           this.ToastrService.warning(result.message);
//           this.NgxSpinnerService.hide();
//         } 
//         this.ngOnInit();  
//       })
// }
  
  
}



