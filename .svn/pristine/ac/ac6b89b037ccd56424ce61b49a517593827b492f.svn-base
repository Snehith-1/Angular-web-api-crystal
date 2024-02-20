import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormControl, FormGroup, Validators,FormBuilder } from '@angular/forms';
@Component({
  selector: 'app-hrm-mst-designation',
  templateUrl: './hrm-mst-designation.component.html',
  styleUrls: ['./hrm-mst-designation.component.scss']
})
export class HrmMstDesignationComponent {
  designationdetail: any;
  designation_type: any;
  designationTypeedit: any;
  rbo_status:any;
  designation_list:any;
  txtremarks:any;
  EditForm!: FormGroup; 
  StatusForm!: FormGroup;
  AddForm!: FormGroup; 
  designation_gid  = "";
  adddesignationFormData  = { 
    txtdesignation: '',

  };
  editdesignationFormData = {
    txteditdesignation: '',

  };
  statusdesignationFormData = {
    txtdesignation: '',
    txtremarks: '',
    rbo_status: ''  
  };
  parametervalue: any;
 

 
  constructor(public router:Router, private ToastrService: ToastrService,private NgxSpinnerService: NgxSpinnerService, private service: SocketService,public FormBuilder: FormBuilder)
  {
    this.createForm(); 
    this.createEditForm();
    this.createStatusForm();


     
  }



  createForm() {
    this.AddForm = this.FormBuilder.group({
      txtdesignation : ['', [Validators.required,Validators.pattern("^(?!\s*$).+")]]

    });
  }

  createEditForm() {
    this.EditForm = this.FormBuilder.group({
      txteditdesignation: ['', [Validators.required,Validators.pattern("^(?!\s*$).+")]]

    });
  }
  createStatusForm(){
    this.StatusForm = this.FormBuilder.group({
      // txtremarks: ['', Validators.required],
      txtremarks: new FormControl(null,

        [

          Validators.required,

          Validators.pattern(/^(?!\s*$).+/)

        ]),
      rbo_status: [''] 
    });
  }
  clearForm() {
    this.AddForm.reset(); 
  }

// Summary
  ngOnInit() {
      var url = 'SystemMaster/GetDesignation';
      this.NgxSpinnerService.show();
      this.service.get(url).subscribe((result: any) => {
      
        if(result.designation_list != null){
          $('#Designationsummary').DataTable().destroy();
          this.designationdetail= result.designation_list;  
          this.NgxSpinnerService.hide();
          setTimeout(()=>{   
            $('#Designationsummary').DataTable();
          }, 1);
        }
        else{
          this.designationdetail= result.designation_list;
          setTimeout(() => {
            var table = $('#Designationsummary').DataTable();
          }, 1);
          this.NgxSpinnerService.hide();
          $('#Designationsummary').DataTable().destroy();  
          this.NgxSpinnerService.hide();
          $('#Designationsummary').DataTable();
        }
       

  });
  

  }
  
  opendesignationopup(){
    this.clearForm();
  }

 // Add Designation
 adddesignation(){
  if (this.AddForm.valid) {
    var params = {

      designation_type: this.adddesignationFormData.txtdesignation
  }

  this.NgxSpinnerService.show();

  var url = 'SystemMaster/CreateDesignation';
  this.NgxSpinnerService.hide();  
  this.service.post(url,params).subscribe((result: any) => {
    console.log(result)

    if (result.status == true) {
      this.ToastrService.success(result.message)
      this.ngOnInit();
    }
    else {
      this.ToastrService.warning(result.message)
      this.NgxSpinnerService.hide();
        }
  
   
  })

    
  }
}
// Edit Designation
  editdesignation(designation_gid:any){
    this.designation_gid = designation_gid;
    this.EditForm.reset();
    var param = {
      designation_gid: designation_gid
  }
  this.NgxSpinnerService.show();
  var url = 'SystemMaster/EditDesignation';
  this.service.getparams(url,param).subscribe((result:any) => {
    this.editdesignationFormData.txteditdesignation = result.designation_type;
    this.NgxSpinnerService.hide();
  });

  }
//Update the Editable 
updatedesignation(){
   
  
   
  var url = 'SystemMaster/UpdateDesignation';
  var params = {
    designation_gid: this.designation_gid,
    designation_type: this.editdesignationFormData.txteditdesignation
   
  }
  this.NgxSpinnerService.show();
  this.service.post(url,params).subscribe((result:any) => {    
  if(result.status == true){
    this.NgxSpinnerService.hide();
    this.ToastrService.success(result.message);
    this.ngOnInit();
    }
    
    else {
      this.ToastrService.warning(result.message);
      this.NgxSpinnerService.hide();
     
    } 
   
  })
  }

  //Status icon change
  Status_update(designation_gid:any){
    this.StatusForm.reset();
    this.NgxSpinnerService.show();
    this.designation_gid = designation_gid
  var params = {
    designation_gid: designation_gid
}
var url = 'SystemMaster/EditDesignation';
this.service.getparams(url, params).subscribe((result:any) => {
  this.designation_gid = designation_gid;
  this.statusdesignationFormData.txtdesignation = result.designation_type;

 
  this.statusdesignationFormData.rbo_status = result.status_log;
});
this.NgxSpinnerService.show();
var url ='SystemMaster/GetActiveLog'
    this.service.getparams(url, params).subscribe((result:any) => {
      this.designation_list = result.designation_list;
      this.NgxSpinnerService.hide(); 
    });



}

// Status editable update
update_status(){
 
  var params = {
    designation_gid :this.designation_gid,
    designation_type: this.statusdesignationFormData.txtdesignation,
    remarks: this.statusdesignationFormData.txtremarks,
    status_log:this.statusdesignationFormData.rbo_status
}
this.NgxSpinnerService.show();
var url = 'SystemMaster/DesignationStatusUpdate';
this.service.post(url, params).subscribe((result:any) => {
  if(result.status == true){
    this.NgxSpinnerService.hide();
    this.ToastrService.success(result.message);
    this.ngOnInit();
      }
      else {
      this.ToastrService.warning(result.message);
      this.NgxSpinnerService.hide();
     
      } 
     
      
    })
    
};

//delete
delete(designation_gid:any){
  this.parametervalue = designation_gid 

}
ondelete(){
  this.NgxSpinnerService.show();
  var params = {
    designation_gid: this.parametervalue 
}
  var url = 'SystemMaster/DeleteDesignation';
  this.service.getparams(url, params).subscribe((result:any) => {
    if(result.status == true){
      this.NgxSpinnerService.hide();
      this.ToastrService.success("Designation Deleted Successfully");
      this.ngOnInit();
        }
        else {
          this.ToastrService.warning("Error Occurred While Deleting Designation");
           this.NgxSpinnerService.hide();
     
        }   
      
      });
}


}


