import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from '../../ems.utilities/services/socket.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AES, enc } from 'crypto-js';

@Component({
  selector: 'app-hrm-trn-leavebalanceedit',
  templateUrl: './hrm-trn-leavebalanceedit.component.html',
  styleUrls: ['./hrm-trn-leavebalanceedit.component.scss']
})
export class HrmTrnLeavebalanceeditComponent {

  leaveform!: FormGroup | any;
  selectedleavegrade: any;
  leavegrade_list: any;
  leavegradelist : any;
  leavebalance_list: any[] = [];
  leavegrade : any;
  responsedata: any;
  result: any;
  leave_grade :any[] = [];
  leavegrade_gid : any;
  employeegid : any;
  employee_gid : any;
  branch_gid:any;
  employeegid1 : any;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: SocketService,
    private ToastrService: ToastrService,
  )
{
  this.leaveform = new FormGroup({
    
    leavegrade_name: new FormControl('', Validators.required),
    leavetype_name: new FormControl('', Validators.required),
    available_leavecount: new FormControl('', Validators.required),
    confirmed_date:new FormControl('', Validators.required),
    total_leavecount:new FormControl('', Validators.required),
    leave_limit:new FormControl('', Validators.required),
    leavegradelist: this.formBuilder.array([])


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



  var api = 'Leaveopening/Getleavegradeopeningdropdown';
  this.service.get(api).subscribe((result: any) => {
    this.responsedata = result;
    this.leavegrade_list = this.responsedata.leave_list;
  });

  
}

get leavegradecontrol() {
  return this.leaveform.get('leavegrade_name');
}

Oneditleaveopening() {
  
  let leavegrade_gid = this.leaveform.get("leavegrade_name")?.value;
  
  console.log(leavegrade_gid)

  let param = {

  leavegrade_gid : leavegrade_gid

}

var url = 'Leaveopening/editleaveopening';

   this.service.getparams(url,param).subscribe((result:any)=>{

     this.responsedata=result;

     this.leavegradelist = this.responsedata.leaveopeningbalance_list;

     for (let i = 0; i < this.leavegradelist.length; i++) {
      this.leaveform.addControl(`total_leavecount${i}`, new FormControl(this.leavegradelist[i].total_leavecount));
      this.leaveform.addControl(`leave_limit${i}`, new FormControl(this.leavegradelist[i].leave_limit));
    }
  });
}

submit() {
  for (const control of Object.keys(this.leaveform.controls)) {
    this.leaveform.controls[control].markAsTouched();
  }
  let flag = 0; 
  // Loop through each row in custodianadd_list
  for (let i = 0; i < this.leavegradelist.length; i++) {
    debugger;
    const row = this.leavegradelist.at(i);
     const employee_gid = this.employeegid1;
     const formData = {
      flag: i === 0 ? 1 : 0, 
      employee_gid: employee_gid,
      branch_gid: row.branch_gid,
      employee_name: row.employee_name,
      leavegrade_gid: row.leavegrade_gid,
      leavegrade_code: row.leavegrade_code,
      leavegrade_name:row.leavegrade_name,
      attendance_startdate: row.attendance_startdate,
      attendance_enddate: row.attendance_enddate,
      total_leavecount: row.total_leavecount,
      available_leavecount: row.available_leavecount,
      leave_limit: row.leave_limit,
    };

    var url1 = 'Leaveopening/Postleavebalance';
    this.service.post(url1,formData).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message);
      } else {
        this.ToastrService.success(result.message);
      }
    });
  }
}

redirecttolist(){
  this.router.navigate(['/hrm/HrmTrnProbationperiod']);
}
}


