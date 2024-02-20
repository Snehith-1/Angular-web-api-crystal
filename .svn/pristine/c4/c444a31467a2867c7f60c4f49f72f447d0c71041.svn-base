import { Component, OnInit, OnDestroy, ChangeDetectorRef, Renderer2, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { SelectionModel } from '@angular/cdk/collections';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { Subscription, Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { NgbTimepickerModule, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ActivatedRoute, Router } from '@angular/router';

export class IUnassignEmployee {
  leavegrade_gid:any;
  unassign_employeelist:any;
}


@Component({
  selector: 'app-hrm-trn-leavegradeunassign2employee',
  templateUrl: './hrm-trn-leavegradeunassign2employee.component.html',
  styleUrls: ['./hrm-trn-leavegradeunassign2employee.component.scss']
})
export class HrmTrnLeavegradeunassign2employeeComponent {

  responsedata:any;
  unassignemployee_list:any[] = [];
  selection = new SelectionModel<IUnassignEmployee>(true, []);
  parameterValue: any;
  leave_name:any;
  leave_code:any;
  leavegrade_gid: any;
  Leaveunassign_type: any;
  pick:Array<any> = [];
  CurObj: IUnassignEmployee = new IUnassignEmployee();
 

  constructor(    
    private renderer: Renderer2,
    private el: ElementRef,
    public service: SocketService,
    private ToastrService: ToastrService,
    private route: Router,
    private router: ActivatedRoute,
    private formBuilder: FormBuilder,
    
       )
  {}
  ngOnInit(): void { 


    const leavegrade_gid = this.router.snapshot.paramMap.get('leavegrade_gid');
    this.leavegrade_gid = leavegrade_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.leavegrade_gid, secretKey).toString(enc.Utf8);    
    this.leavegrade_gid = deencryptedParam
   

var url = 'LeaveGrade/UnassignEmployeeSummary'
let param={
  leavegrade_gid:this.leavegrade_gid
}
this.service.getparams(url,param).subscribe((result: any) => {
this.responsedata = result;
this.unassignemployee_list = this.responsedata.unassign_employeelist;
  setTimeout(() => {
    $('#unassignemployee_list').DataTable();
    }, );
});

var url = 'LeaveGrade/Leavegradeunassign'
let param1={
leavegrade_gid:this.leavegrade_gid
}
this.service.getparams(url,param1).subscribe((result: any) => {
this.responsedata = result;  
this.Leaveunassign_type = this.responsedata.Leaveunassign_type;
this.leave_name = this.Leaveunassign_type[0].leavegrade_name;
this.leave_code = this.Leaveunassign_type[0].leavegrade_code;
});

}

isAllSelected() {
  const numSelected = this.selection.selected.length;
  const numRows = this.unassignemployee_list.length;
  return numSelected === numRows;
}
masterToggle() {
  this.isAllSelected() ?
    this.selection.clear() :
    this.unassignemployee_list.forEach((row: IUnassignEmployee) => this.selection.select(row));
}


// validate(){
//   debugger;
  
//   console.log(this.parameterValue);
//   var url3 = 'LeaveGrade/DeleteUnassignemployee'
//   this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
//     if (result.status == false) {
//       this.ToastrService.warning(result.message)
//     }
//     else {
//       this.ToastrService.success(result.message)
     
//     }

//   });
// }

validate(){
  debugger;
    this.pick = this.selection.selected  
    this.CurObj.unassign_employeelist = this.pick
    this.CurObj.leavegrade_gid=this.leavegrade_gid
      if (this.CurObj.unassign_employeelist.length === 0) {
      this.ToastrService.warning("Select Atleast one Employee to unassign");
      return;
    } 
 
      var url = 'LeaveGrade/DeleteUnassignemployee';  
      this.service.post(url, this.CurObj).subscribe((result: any) => {
        if (result.status === false) {
          this.ToastrService.warning(result.message);
          
        } else {
          this.ToastrService.success(result.message);
          this.route.navigate(['/hrm/HrmMstLeaveGrade'])
          
        }
      });
    
   
    this.selection.clear();

}



}
