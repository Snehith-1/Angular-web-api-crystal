import { Component, OnInit, Output } from '@angular/core';
import {  EventEmitter,  HostListener, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-hrm-trn-employeeonboard-view-completed',
  templateUrl: './hrm-trn-employeeonboard-view-completed.component.html',
  styleUrls: ['./hrm-trn-employeeonboard-view-completed.component.scss']
})
export class HrmTrnEmployeeonboardViewCompletedComponent {
  page: string = 'pending';
  ShowContent1= false;
  ShowContent = false ;
  employee_gid: any;
  employee_details: any;
  lstab: any;
  OnboardingTaskList: any;
  HRDocumentList: any;
  stsform: FormGroup | any;
  taskstatuslist: any;
  cbotask_status: any;
  lblremarks: any;
  txttask_remarks: any;
  taskinitiate_gid: any;
  lbltaskinitiate_gid: any;
  lbltask_name: any;
  View360Form! : FormGroup;
  statusEntityFormData = {
    cbotask_status: '',
    txttask_remarks: ''  
  };
  employeegid!: string | null;
  sample: boolean | undefined;
  
  constructor(public router:Router,private route: ActivatedRoute,private SocketService: SocketService,private NgxSpinnerService: NgxSpinnerService,private ToastrService: ToastrService,private FormBuilder: FormBuilder ) {
    this.createStatusForm();
    
}
createStatusForm(){
  this.View360Form = this.FormBuilder.group({
    txttask_remarks: ['', [Validators.required,Validators.pattern("^(?!\s*$).+")]],
    cbotask_status: ['',[Validators.required]],
  });
}

  backbutton(){
    this.ShowContent = false;
    this.ShowContent1 = true;
  if ( this.lstab== 'pending') {
    const url = `/hrm/SysMstEmployeeView?lstab=${this.lstab}`;
    this.router.navigateByUrl(url);
  } 
  else if( this.lstab== 'mytask-completed' || this.lstab== 'mytask-pending') {
    const url = `/hrm/SysTrnMyTask?lstab=${this.lstab}`;
    this.router.navigateByUrl(url);
  } 
  else if( this.lstab== 'taskmanagement-completed' || this.lstab== 'taskmanagement-new' || this.lstab== 'taskmanagement-pending') {
    const url = `/hrm/SysTrnTaskManagement?lstab=${this.lstab}`;
    this.router.navigateByUrl(url);
  } 
  else {
    this.router.navigate(['/hrm/SysMstEmployeePendingSummary']);
  }
  
    // this.router.navigate(['/system/SysMstEmployeePendingSummary']);
  }

  ngOnInit(): void 
  
  {

    const employee_gid = this.route.snapshot.paramMap.get('employee_gid');
    // console.log(termsconditions_gid)
    this.employeegid = employee_gid;

    const secretKey = 'storyboarderp';

    // const deencryptedParam = AES.decrypt(this.employeegid, secretKey).toString(enc.Utf8);
    // console.log(deencryptedParam)
    // this.GetEditEmployeeSummary(deencryptedParam)

    if (this.taskinitiate_gid) {
      this.sample = true;
    } else {
      this.sample = false;
    }

    console.log('Value of sample:', this.sample);


  this.route.queryParams.subscribe(params => {
  this.employee_gid = params['employee_gid'];
  this.lstab = params['lstab'];
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

   this.ShowContent1 = this.taskinitiate_gid;
   this.ShowContent = this.employee_gid;
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



