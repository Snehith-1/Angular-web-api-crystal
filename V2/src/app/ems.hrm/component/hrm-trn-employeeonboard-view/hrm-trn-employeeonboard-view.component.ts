import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { AES, enc } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-hrm-trn-employeeonboard-view',
  templateUrl: './hrm-trn-employeeonboard-view.component.html',
  styleUrls: ['./hrm-trn-employeeonboard-view.component.scss']
})
export class HrmTrnEmployeeonboardViewComponent {
  employee_gid: any;
  employee_details: any;
  lstab: any;
  OnboardingTaskList: any;
  HRDocumentList: any;
  param: any;
  values: any;
  
  constructor(public router:Router,private route: ActivatedRoute,private SocketService: SocketService,private ToastrService: ToastrService) {}

  // backbutton(){
  // if ( this.lstab== 'pending') {
  //   const url = `/system/SysMstEmployeeView?lstab=${this.lstab}`;
  //   this.router.navigateByUrl(url);
  // } 
  // else if( this.lstab== 'mytask-completed' || this.lstab== 'mytask-pending') {
  //   const url = `/system/SysTrnMyTask?lstab=${this.lstab}`;
  //   this.router.navigateByUrl(url);
  // } 
  // else if( this.lstab== 'taskmanagement-completed' || this.lstab== 'taskmanagement-new' || this.lstab== 'taskmanagement-pending') {
  //   const url = `/system/SysTrnTaskManagement?lstab=${this.lstab}`;
  //   this.router.navigateByUrl(url);
  // } 
  // else {
  //   this.router.navigate(['/system/SysMstEmployeePendingSummary']);
  // }
  
  //   // this.router.navigate(['/system/SysMstEmployeePendingSummary']);
  // }
  backbutton(){
    this.router.navigate(['/hrm/HrmtrnEmployeeonboard']);
  }


  ngOnInit(): void 
  {
 
    // this.route.queryParams.subscribe(params => {
    //   this.param= params['hash'];
    // })
    // console.log(this.param)
    // var replace = ' '
    // var str = this.param
    // var check = str.replace(new RegExp(replace, 'g'), "+")
    // const secretKey = 'storyboarderp';
    // const deencryptedParam = AES.decrypt(this.param, secretKey).toString(enc.Utf8);
    // console.log(deencryptedParam);
    // this.values = deencryptedParam.split('&')
    // console.log(this.values);
    // this.employee_gid = this.values[0]
    // this.lstab = this.values[1]

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
} 

  downloadFile(file_path:string , file_name:string): void {
    var params= {
      file_path: file_path,
      file_name: file_name
    }
    this.SocketService.downloadFile(params).subscribe((data:any) => {
      if(data != null){
        this.SocketService.filedownload(data);
      }
      else{
        this.ToastrService.warning("Error in file download")  
      }
      
    });
  }
  downloadAllFiles(file_list:any){
    for(let i=0;i<file_list.length; i=i+1){
      this.downloadFile(file_list[i].hrdoc_path, file_list[i].hrdoc_name);
    }
  }
 
}



