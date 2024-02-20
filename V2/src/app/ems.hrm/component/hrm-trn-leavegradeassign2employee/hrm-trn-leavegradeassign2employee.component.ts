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


export class IAssignEmployee {
  leavegrade_gid:any;
  assign_employeelist:any;
}
@Component({
  selector: 'app-hrm-trn-leavegradeassign2employee',
  templateUrl: './hrm-trn-leavegradeassign2employee.component.html',
  styleUrls: ['./hrm-trn-leavegradeassign2employee.component.scss']
})
export class HrmTrnLeavegradeassign2employeeComponent {
  responsedata:any;
  assignemployee_list:any[] = [];
  assignemployee_list1:any[] = [];
  selection = new SelectionModel<IAssignEmployee>(true, []);
  leave_name:any;
  leave_code:any;
  leavegrade_gid: any;
  Leaveassign_type: any;
  pick:Array<any> = [];
  CurObj: IAssignEmployee = new IAssignEmployee();

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
    debugger;

    //// Summary Grid//////
    const leavegrade_gid = this.router.snapshot.paramMap.get('leavegrade_gid');
    this.leavegrade_gid = leavegrade_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.leavegrade_gid, secretKey).toString(enc.Utf8);    
    this.leavegrade_gid = deencryptedParam
    
   
      var url = 'LeaveGrade/AssignEmployeeSummary'
      this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
      this.assignemployee_list = this.responsedata.assign_employeelist;
        setTimeout(() => {
          $('#assignemployee_list').DataTable();
          }, );
      
      });

      var url = 'LeaveGrade/Leavegradeassign'
      let param={
      leavegrade_gid:this.leavegrade_gid
      }
   this.service.getparams(url,param).subscribe((result: any) => {
   this.responsedata = result;  
   this.Leaveassign_type = this.responsedata.Leaveassign_type;
   this.leave_name = this.Leaveassign_type[0].leavegrade_name;
   this.leave_code = this.Leaveassign_type[0].leavegrade_code;
 });
      
    }


isAllSelected() {
  const numSelected = this.selection.selected.length;
  const numRows = this.assignemployee_list.length;
  return numSelected === numRows;
}
masterToggle() {
  this.isAllSelected() ?
    this.selection.clear() :
    this.assignemployee_list.forEach((row: IAssignEmployee) => this.selection.select(row));
}

validate(){
  debugger;
    this.pick = this.selection.selected  
    this.CurObj.assign_employeelist = this.pick
    this.CurObj.leavegrade_gid=this.leavegrade_gid
      if (this.CurObj.assign_employeelist.length === 0) {
      this.ToastrService.warning("Select Atleast one Employee to assign");
      return;
    } 

    debugger
 
      var url = 'LeaveGrade/Postforunassign';  
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
