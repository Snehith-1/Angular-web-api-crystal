import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AES, enc } from 'crypto-js';
@Component({
  selector: 'app-hrm-trn-employeeboardviewpending',
  templateUrl: './hrm-trn-employeeboardviewpending.component.html',
  styleUrls: ['./hrm-trn-employeeboardviewpending.component.scss']
})
export class HrmTrnEmployeeboardviewpendingComponent {
  employee_gid: any;
  employee_details: any;
  lstab: any;
  OnboardingTaskList: any;
  HRDocumentList: any;
  lbltaskinitiate_gid: any;
  cbotask_status: any;
  txttask_remarks: any;
  StatusForm : FormGroup | any;
  statusFormData = {
    cbotask_status: '',
    txttask_remarks: ''  
  };
  taskinitiate_gid: any;
  param: any;
  values: any;
  
  constructor(public router:Router,private route: ActivatedRoute,private SocketService: SocketService,private ToastrService: ToastrService,private NgxSpinnerService: NgxSpinnerService,private FormBuilder: FormBuilder) {
    this.createStatusForm();
  }
  createStatusForm(){
    this.StatusForm = this.FormBuilder.group({
      txttask_remarks: new FormControl(null,[Validators.required,Validators.pattern(/^(?!\s*$).+/)]),
      cbotask_status: ['', [Validators.required]],
    });

  }
  backbutton(){
    this.router.navigate(['/hrm/HrmtrnEmployeeonboard']);
  }


  ngOnInit(): void 
  {
 
    
  this.route.queryParams.subscribe(params => {
  this.employee_gid = params['employee_gid']; 
  this.lstab = params['lstab']; 
  });

  this.route.queryParams.subscribe(params => {
    this.taskinitiate_gid = params['taskinitiate_gid'];
    });

  


  if ( this.lstab== 'pending') {
      var url = 'EmployeeOnboard/EmployeePendingEditView';
  } 
  else if( this.lstab== 'pending') {
    var url = 'EmployeeOnboard/SysMstTeamMaster';
  } 
  else if( this.lstab== 'pending') {
    var url = 'EmployeeOnboard/EmployeePendingEditView';
  } 
  else {
    var url = 'EmployeeOnboard/EmployeeEditView';
  }
  var params = {
  employee_gid: this.employee_gid,
  }; 
  this.SocketService.getparams(url,params).subscribe((result: any) => { 
    this.employee_details  = result; 
  });

  var url = 'EmployeeOnboard/GetTaskOnboardView';
  this.SocketService.getparams(url,params).subscribe((result: any) => { 
      this.OnboardingTaskList  = result.MdlTaskViewInfo; 
    });
  
  var url = 'EmployeeOnboard/GetHRDoclist';
  this.SocketService.getparams(url,params).subscribe((result: any) => { 
      this.HRDocumentList  = result.hrdoc; 
   });
} 
Update_Status(taskinitiate_gid:any){
  debugger
  this.NgxSpinnerService.show();
    var params = {
      taskinitiate_gid :taskinitiate_gid,
      task_status : this.statusFormData.cbotask_status,
      task_remarks : this.statusFormData.txttask_remarks
    }
    var url = 'EmployeeOnboard/PostMyTaskStatusUpdate';
    this.SocketService.post(url, params).subscribe((result: any) => {
      if(result.status == true){
        this.ToastrService.success(result.message);
        this.ngOnInit();
      }
      else{
        this.ToastrService.warning(result.message);
      }
  });
  this.NgxSpinnerService.hide();
}

  // DownloadDocument(val1, val2, val3){
  //   if (val3 == 'N') {
  //     DownloaddocumentService.Downloaddocument(val1, val2);
  // }
  // else {
  //     DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
  // } 
  // }

 
}




