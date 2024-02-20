import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupName, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';


@Component({
  selector: 'app-hrm-mst-addholidaygrade',
  templateUrl: './hrm-mst-addholidaygrade.component.html',
  styleUrls: ['./hrm-mst-addholidaygrade.component.scss']
})
export class HrmMstAddholidaygradeComponent {  
  Holidaygradecode_list: any[] = [];
  reactiveFormadd!: FormGroup;
  responsedata: any;
  parameterValue:any;

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService,public NgxSpinnerService:NgxSpinnerService) {
    this.reactiveFormadd = new FormGroup({
      Holiday_date :new FormControl(''),
      holiday_name :new FormControl(''),
      holiday_type:new FormControl(''),
      holiday_remarks:new FormControl(''),
      // weekdays_list: this.formBuilder.array([])
    });
  }
  ngOnInit(): void {
    // var url = 'HolidayGradeManagement/Addholidaysummary'
    // this.service.get(url).subscribe((result: any) => {
  
    //   this.responsedata = result;
    //   this.Holidaygradecode_list = this.responsedata.holidaygrade1_list;
    // });
    this.holidaygradesummary()

    const options: Options = {
          dateFormat: 'd-m-Y',    
        };
        flatpickr('.date-picker', options);
    }

    holidaygradesummary(){

    var url = 'HolidayGradeManagement/Addholidaysummary'
    this.service.get(url).subscribe((result: any) => {  
      this.responsedata = result;
      this.Holidaygradecode_list = this.responsedata.holidaygrade1_list;
    });
  }
    Addholiday(){
      debugger;
    var params={ 
      Holiday_date : this.reactiveFormadd.value.Holiday_date,
      holiday_name : this.reactiveFormadd.value.holiday_name,
      holiday_type : this.reactiveFormadd.value.holiday_type,
      holiday_remarks : this.reactiveFormadd.value.holiday_remarks,
    }
    console.log(params)
  
    var url = 'HolidayGradeManagement/AddHolidayGradesubmit'
      this.service.postparams(url,params).subscribe((result: any) => {
        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.holidaygradesummary()  
          this.router.navigate(['/hrm/HrmMstAddHolidaygrademanagement']);
       }
       else{
        this.ToastrService.success(result.message)
        this.holidaygradesummary()  
        this.router.navigate(['/hrm/HrmMstAddHolidaygrademanagement']);    
        window.location.reload();
        }

      });      

}
openModaldelete(parameter: string) {
  this.parameterValue = parameter
}

ondelete() {
  console.log(this.parameterValue);
  var url3 = 'HolidayGradeManagement/Deleteholiday'
  this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
    if (result.status == false) {
      this.ToastrService.warning(result.message)

    }
    else {
      this.ToastrService.success(result.message)
    }
    window.location.reload();

  });
}
}

