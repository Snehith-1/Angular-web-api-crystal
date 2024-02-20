import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
@Component({
  selector: 'app-hrm-mst-leavetype',
  templateUrl: './hrm-mst-leavetype.component.html',
  styleUrls: ['./hrm-mst-leavetype.component.scss']
})
export class HrmMstLeavetypeComponent {
  Leavetype_list: any[] = [];
  consider_list: any[] = [];
  responsedata: any;
  reactiveFormadd!: FormGroup;



  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    }
   
    ngOnInit(): void {

      this.reactiveFormadd = new FormGroup({
        leave_code: new FormControl(''),
        leave_name: new FormControl(''),
        Status_flag: new FormControl(''),
        weekoff_consider: new FormControl(''),
        holiday_consider: new FormControl(''),
        carry_forward: new FormControl(''),
        Accured_type: new FormControl(''),
        negative_leave: new FormControl(''),
        Consider_as: new FormControl(''),
        Leave_Days: new FormControl(''),
      
      });
    

      //// Summary Grid//////
  var url = 'LeaveTypeGrade/LeavetypeSummary'
     
  this.service.get(url).subscribe((result: any) => {
  this.responsedata = result;
  this.Leavetype_list = this.responsedata.Leavetype_list;
    setTimeout(() => {
      $('#Leavetype_list').DataTable();
      }, );
});

this.consider_list = [
  { label: 'Leave with pay', value: 'leave_with_pay' },
  { label: 'Leave without pay', value: 'leave_without_pay' }
];
 }

 public onsubmit(): void {
  debugger
  if (this.reactiveFormadd.value.leave_code != null && this.reactiveFormadd.value.leave_name != '')
    if (this.reactiveFormadd.value.Status_flag != null && this.reactiveFormadd.value.weekoff_consider != '')
      if (this.reactiveFormadd.value.holiday_consider != null && this.reactiveFormadd.value.carry_forward != '') 
      if (this.reactiveFormadd.value.Accured_type != null && this.reactiveFormadd.value.negative_leave != '') 
      if (this.reactiveFormadd.value.Consider_as != null && this.reactiveFormadd.value.Leave_Days != '') 
      {
        for (const control of Object.keys(this.reactiveFormadd.controls)) {
          this.reactiveFormadd.controls[control].markAsTouched();
        }
        debugger;
        this.reactiveFormadd.value;
        let param = {
          leavetype_code: this.reactiveFormadd.value.leave_code,
          leavetype_name: this.reactiveFormadd.value.leave_name,
          leavetype_status: this.reactiveFormadd.value.Status_flag,
          consider_as: this.reactiveFormadd.value.Consider_as,
          weekoff_applicable: this.reactiveFormadd.value.weekoff_consider,
          holiday_applicable: this.reactiveFormadd.value.holiday_consider,
          carryforward: this.reactiveFormadd.value.carry_forward,
          accrud: this.reactiveFormadd.value.Accured_type,
          leave_days: this.reactiveFormadd.value.Leave_Days,
        }
            var url1 = 'LeaveTypeGrade/PostAddleave'

        this.service.postparams(url1, param).subscribe((result: any) => {
          debugger;
          if (result.status == false) {
            this.ToastrService.warning(result.message)
            // this.ComponentgroupSummary();
          }
          else {
            this.reactiveFormadd.get("leave_code")?.setValue(null);
            this.reactiveFormadd.get("leave_name")?.setValue(null);
            this.reactiveFormadd.get("Status_flag")?.setValue(null);
            this.reactiveFormadd.get("weekoff_consider")?.setValue(null);
            this.reactiveFormadd.get("holiday_consider")?.setValue(null);
            this.reactiveFormadd.get("carry_forward")?.setValue(null);
            this.reactiveFormadd.get("Accured_type")?.setValue(null);
            this.reactiveFormadd.get("negative_leave")?.setValue(null);
            this.reactiveFormadd.get("Consider_as")?.setValue(null);
            this.reactiveFormadd.get("Leave_Days")?.setValue(null);

            this.ToastrService.success("Leave Type Added successfully")
            window.location.reload();
          }
        });
      }
      else {
        this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
      }
}
}
