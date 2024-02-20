import { AfterViewInit, Component, ElementRef, Injectable, OnInit, } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
@Component({
  selector: 'app-hrm-mst-baselocation',
  templateUrl: './hrm-mst-baselocation.component.html',
  styleUrls: ['./hrm-mst-baselocation.component.scss']
})
export class HrmMstBaselocationComponent {
  baselocation_list:any = [];
  txtbase_location: any;
  parametervalue:any;
  txteditlms_code: any;
  txteditbase_location: any;
  baselocation_gid: any;
  rbo_status: any;
  txtremarks: any;
  txtaddbase_location:any;
  baselocationinactivelog_list: any;
  BaseLocationAddForm : FormGroup | any;
  BaseLocationEditForm : FormGroup | any;
  BaseLocationStatusForm : FormGroup | any;
  addBaseLocationFormData  = { 
    txtaddbase_location: '',

  };
  editBaseLocationFormData = {
    txteditbase_location: '',

  };
  statusBaseLocationFormData = {
    txtbase_location: '',
    rbo_status: '',
    txtremarks: ''  
  };


  constructor(private SocketService: SocketService,private el: ElementRef,private NgxSpinnerService: NgxSpinnerService,private ToastrService: ToastrService,public router:Router,private FormBuilder: FormBuilder) {

    this.createForm(); 
    this.createEditForm();
    this.createStatusForm();
  }


  createForm(){
    this.BaseLocationAddForm = this.FormBuilder.group({
      txtaddbase_location: ['', [Validators.required,Validators.pattern("^(?!\s*$).+")]],

    });
  }
  createEditForm(){
    this.BaseLocationEditForm = this.FormBuilder.group({
      txteditbase_location: ['', [Validators.required,Validators.pattern("^(?!\s*$).+")]],

    });
  }
  createStatusForm(){
    this.BaseLocationStatusForm = this.FormBuilder.group({
      txtremarks: new FormControl(null,

        [

          Validators.required,

          Validators.pattern(/^(?!\s*$).+/)

        ]),
      // txtremarks: ['', [Validators.required,Validators.pattern("^(?!\s*$).+")]],
      rbo_status: [''] 
    });
  }
  clearForm() {
    this.BaseLocationAddForm.reset(); 
  }
  ngOnInit() {
    this.NgxSpinnerService.show();
    var url= 'HrmMaster/GetBaseLocation';
    this.SocketService.get(url).subscribe((result:any)=>{
      if(result.master_list != null){
        $('#locationsummary').DataTable().destroy();
        this.baselocation_list = result.master_list;  
        this.NgxSpinnerService.hide();
        setTimeout(()=>{   
          $('#locationsummary').DataTable();
        }, 1);
      }
      else{
        this.baselocation_list = result.master_list; 
        setTimeout(()=>{   
          $('#locationsummary').DataTable();
        }, 1);
        this.NgxSpinnerService.hide();
        $('#locationsummary').DataTable().destroy()
      } 
    });
  }
  openBaselocationpopup(){
    this.BaseLocationAddForm.reset(); 
  }
  addBaselocation(){
    if (this.BaseLocationAddForm.valid) {}
    var params = {
      baselocation_name: this.addBaseLocationFormData.txtaddbase_location,

    }
    this.NgxSpinnerService.show();
    var url = 'HrmMaster/CreateBaseLocation';
    this.SocketService.post(url, params).subscribe((result:any) => {
      if(result.status == true){
        this.ToastrService.success(result.message);
        this.NgxSpinnerService.hide();
        this.ngOnInit();
      }
      else {
        this.ToastrService.warning(result.message);
        this.NgxSpinnerService.hide();
        this.ngOnInit();
      }
      
    })
  }

  editbaselocation(baselocation_gid:any){
    this.baselocation_gid = baselocation_gid;
    this.BaseLocationEditForm.reset();
        var params = {
          baselocation_gid: baselocation_gid
      }
      this.NgxSpinnerService.show();
      var url = 'HrmMaster/EditBaseLocation';
      this.SocketService.getparams(url, params).subscribe((result:any) => {
            this.editBaseLocationFormData.txteditbase_location = result.baselocation_name;

            this.NgxSpinnerService.hide();
      });
  }

  update(){
    //Update the values in database 
  
    this.NgxSpinnerService.show();
  var url = 'HrmMaster/UpdateBaseLocation';
  var params = {
    baselocation_name: this.editBaseLocationFormData.txteditbase_location,
    baselocation_gid: this.baselocation_gid
  }
  this.NgxSpinnerService.show();
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
  }

  
  Status_update(baselocation_gid:any){
    this.baselocation_gid = baselocation_gid
    this.BaseLocationStatusForm.reset();
  var params = {
    baselocation_gid: baselocation_gid
}
this.NgxSpinnerService.show();
var url = 'HrmMaster/EditBaseLocation';
this.SocketService.getparams(url, params).subscribe((result:any) => {
  this.baselocation_gid = result.baselocation_gid
  this.statusBaseLocationFormData.txtbase_location = result.baselocation_name;
  this.statusBaseLocationFormData.rbo_status = result.Status;
  this.NgxSpinnerService.hide();
});
var params ={
  baselocation_gid:baselocation_gid
}
var url ='HrmMaster/BaseLocationInactiveLogview'
this.SocketService.getparams(url, params).subscribe((result:any) => {
  this.baselocationinactivelog_list = result.master_list;
});
}
// openupdateBaselocationpopup()
// {
//   this.txtremarks.reset();
// }

update_status(){
  var params = {
    baselocation_gid : this.baselocation_gid,
    remarks: this.statusBaseLocationFormData.txtremarks,
    rbo_status:this.statusBaseLocationFormData.rbo_status
}
this.NgxSpinnerService.show();
var url = 'HrmMaster/InactiveBaseLocation';
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
  var url = 'HrmMaster/DeleteBaseLocation';
let params = {
  baselocation_gid:this. parametervalue
}
  this.SocketService.getparams(url, params).subscribe((result:any) => {
    if(result.status == true){
      this.ToastrService.success(" Base Location Deleted Successfully");
      this.NgxSpinnerService.hide();
      this.ngOnInit();
        }
        else {
        this.ToastrService.warning("Error Occurred While Deleting Base Location");
      this.NgxSpinnerService.hide();
        }   
        
      })
}
  
  
}


