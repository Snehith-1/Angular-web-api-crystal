import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupName, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { SelectionModel } from '@angular/cdk/collections';

interface Iholidaygrade {
  Holidaygradeassign_list:any
}
@Component({
  selector: 'app-hrm-trn-addholidayasign',
  templateUrl: './hrm-trn-addholidayasign.component.html',
  styleUrls: ['./hrm-trn-addholidayasign.component.scss']
})

export class HrmTrnAddholidayasignComponent {
  Holidaygradeassign_list: any[] = [];
  reactiveFormadd!: FormGroup;
  responsedata: any;
  selection = new SelectionModel<Iholidaygrade>(true, []);

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService,public NgxSpinnerService:NgxSpinnerService) {
    this.reactiveFormadd = new FormGroup({
      holiday_grade :new FormControl(''),
      holidaygrade_name :new FormControl(''),     
      Holidaygradeassign_list: this.formBuilder.array([])
    });
  }
  ngOnInit(): void {
    // var url = 'HolidayGradeManagement/Addholidaysummary'
    // this.service.get(url).subscribe((result: any) => {
  
    //   this.responsedata = result;
    //   this.Holidaygradeassign_list = this.responsedata.holidaygrade1_list;
    // });
    this.holidayassignsummary()
    const options: Options = {
          dateFormat: 'd-m-Y',    
        };
        flatpickr('.date-picker', options);
    }
    holidayassignsummary(){
    var url = 'HolidayGradeManagement/Addholidaysummary'
    this.service.get(url).subscribe((result: any) => {
  
      this.responsedata = result;
      this.Holidaygradeassign_list = this.responsedata.holidaygrade1_list;
    });
  }
    submit(){
      const selectedData = this.selection.selected; // Get the selected items
      if (selectedData.length === 0) {
        this.ToastrService.warning("Select Atleast one leave grade to Added");
        return;
      } 
      
      for (const data of selectedData) {
        this.Holidaygradeassign_list.push(data);
    }
    debugger;
    var params={ 
      holidaygrade_list : this.Holidaygradeassign_list,
      holidaygrade_code : this.reactiveFormadd.value.holiday_grade,
      holidaygrade_name : this.reactiveFormadd.value.holidaygrade_name,       
    }

    console.log(params)
  
    var url = 'HolidayGradeManagement/HolidayAssignSubmit'
      this.service.postparams(url,params).subscribe((result: any) => {
        debugger;
        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.holidayassignsummary()
          this.router.navigate(['/hrm/HrmMstHolidaygradeManagement']);
       }
       else{
        this.ToastrService.success(result.message)
        this.router.navigate(['/hrm/HrmMstHolidaygradeManagement']);
        window.location.reload();

       }
  
      });

    }

    

isAllSelected() {
  const numSelected = this.selection.selected.length;
  const numRows = this.Holidaygradeassign_list.length;
  return numSelected === numRows;
}
masterToggle() {
  this.isAllSelected() ?
    this.selection.clear() :
    this.Holidaygradeassign_list.forEach((row: Iholidaygrade) => this.selection.select(row));
}


}
