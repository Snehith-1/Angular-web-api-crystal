import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-hrm-mst-addshifttype',
  templateUrl: './hrm-mst-addshifttype.component.html',
  styleUrls: ['./hrm-mst-addshifttype.component.scss']
})
export class HrmMstAddshifttypeComponent {
  reactiveFormadd!: FormGroup;
  weekdays_list: any[] = [];
  responsedata: any;

  constructor(private formBuilder: FormBuilder,
    private ToastrService: ToastrService,
     public service: SocketService,
     private router: Router,
     public NgxSpinnerService:NgxSpinnerService,) {
      this.reactiveFormadd = new FormGroup({
        shift_name :new FormControl(''),
        email_list :new FormControl(''),
        grace_time:new FormControl(''),
        login_scheduler:new FormControl(''),
        entrycutoff_time: new FormControl(''),
        overnight_flag:  new FormControl(''),
        inovernight_flag: new FormControl(''),
        outovernight_flag: new FormControl(''),
        logout_schedular :new FormControl(''),
        existcutoff_time: new FormControl(''),
        logout_overnight_flag :new FormControl(''),
        logout_inovernight_flag: new FormControl(''),
        logout_outovernight_flag: new FormControl(''),
        logintime: new FormControl(''),
        logouttime: new FormControl(''),
        Ot_cutoff: new FormControl(''),
        weekdays_list: this.formBuilder.array([])
      });
   
 }
  ngOnInit(): void {
    var url = 'ShiftType/GetWeekdaysummary'
    this.service.get(url).subscribe((result: any) => {
  
      this.responsedata = result;
      this.weekdays_list = this.responsedata.weekday_list;
      console.log(this.weekdays_list); 
      this.triggerGetOptions();

      this.weekdays_list.forEach((item) => {
        item.logintime = ('');
        item.logouttime = ('');
        item.Ot_cutoff = ('');

      });
    });
  }
  triggerGetOptions(): void {
    for (let i = 0; i < this.weekdays_list.length; i++) {
      const data = this.weekdays_list[i];
      //this.ge(data.componentgroup_name, i);
    }
  }
  submit() {
    debugger;
    var params={ 
      weekday_list : this.weekdays_list,
      login_scheduler : this.reactiveFormadd.value.login_scheduler,
      entrycutoff_time : this.reactiveFormadd.value.entrycutoff_time,
      overnight_flag : this.reactiveFormadd.value.overnight_flag,
      inovernight_flag : this.reactiveFormadd.value.inovernight_flag,
      outovernight_flag : this.reactiveFormadd.value.outovernight_flag,

      logout_schedular : this.reactiveFormadd.value.logout_schedular,
      grace_time : this.reactiveFormadd.value.grace_time,
      email_list : this.reactiveFormadd.value.email_list,
      
      shift_name : this.reactiveFormadd.value.shift_name,
      logintime : this.reactiveFormadd.value.logintime,
      logouttime : this.reactiveFormadd.value.logouttime,
      Ot_cutoff : this.reactiveFormadd.value.Ot_cutoff,  
    }
    console.log(params)
  
    var url = 'ShiftType/Shiftweekdaystime'
    this.NgxSpinnerService.show();
      this.service.postparams(url,params).subscribe((result: any) => {
        this.NgxSpinnerService.hide();
        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.router.navigate(['/hrm/HrmMstShiftTypeSummary']);
  
       }
       else{
        this.ToastrService.success(result.message)
        this.router.navigate(['/hrm/HrmMstShiftTypeSummary']);
       }
  
      });
  
  }


  

}
