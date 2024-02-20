import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AES, enc } from 'crypto-js';

@Component({
  selector: 'app-hrm-trn-probationleaveupdate',
  templateUrl: './hrm-trn-probationleaveupdate.component.html',
  styleUrls: ['./hrm-trn-probationleaveupdate.component.scss']
})
export class HrmTrnProbationleaveupdateComponent {
  
  leaveform!: FormGroup | any;
  selectedleavegrade: any;
  leavegrade_list: any;
  leavegradelist : any;
  leavegrade : any;
  jobtype_list: any;
  jobtype : any;
  responsedata: any;
  result: any;
  probationform : any;
  leave_grade :any[] = [];
  leavegrade_gid : any;
  employeegid : any;
  employee_gid : any;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: SocketService,
    private ToastrService: ToastrService,
  )
{
  this.probationform = new FormGroup({
    
    leavegrade_name: new FormControl('', Validators.required),
    jobtype_name:new FormControl('', Validators.required),
    confirmed_date:new FormControl('', Validators.required),
    user_confirmed:new FormControl('', Validators.required),


  });
}

ngOnDestroy(): void {

}

ngOnInit(): void {

  const employee_gid = this.route.snapshot.paramMap.get('employee_gid');
  this.employeegid = employee_gid

  const secretKey = 'storyboarderp';

  const deencryptedParam = AES.decrypt(this.employeegid, secretKey).toString(enc.Utf8);
  console.log(deencryptedParam)



  var api = 'Probationperiod/Getleavegradedropdown';
  this.service.get(api).subscribe((result: any) => {
    this.responsedata = result;
    this.leavegrade_list = this.responsedata.Getleavegradedropdown;
  });

  var api = 'Probationperiod/Getjobtypedropdown';
  this.service.get(api).subscribe((result: any) => {
    this.responsedata = result;
    this.jobtype_list = this.responsedata.Getjobtypedropdown;
  });
}

get leavegradecontrol() {
  return this.probationform.get('leavegrade_name');
}
get jobtypecontrol() {
  return this.probationform.get('jobtype_name');
}

OnGetleavegradeSummary() {
  
  let leavegrade_gid = this.probationform.get("leavegrade_name")?.value;
  
  console.log(leavegrade_gid)

  let param = {

  leavegrade_gid : leavegrade_gid

}

var url = 'Probationperiod/GetleavegradeSummary';

   this.service.getparams(url,param).subscribe((result:any)=>{

     this.responsedata=result;

     this.leavegradelist = this.responsedata.leavegrade_list;

     for (let i = 0; i < this.leavegrade_list.length; i++) {
      this.probationform.addControl(`leavetype_gid${i}`, new FormControl(this.leavegrade_list[i].leavetype_gid));
      this.probationform.addControl(`total_leavecount${i}`, new FormControl(this.leavegrade_list[i].total_leavecount));
      this.probationform.addControl(`leave_limit${i}`, new FormControl(this.leavegrade_list[i].leave_limit));
    }
  });
}

updated() {
  console.log(this.probationform.value)
  console.log(this.probationform.invalid)
   const api = 'Probationperiod/Updatedleavegrade';
   this.service.post(api,this.probationform.value).subscribe((result: any) => {
        this.responsedata = result;
         this.router.navigate(['/hrm/hrm-trn-probationperiod']);
       }
     , (error: any) => {
       if (error.status === 401)
       this.router.navigate(['pages/401'])
       else if (error.status === 404)
      this.router.navigate(['pages/404'])
    });
  
}

}
