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
import { NgxSpinnerService } from 'ngx-spinner';

export class IEmployee {
  employeeleave_list: string[] = [];
  employee_gid:any;
  
}
@Component({
  selector: 'app-pay-mst-leavegenerateview',
  templateUrl: './pay-mst-leavegenerateview.component.html',
  styleUrls: ['./pay-mst-leavegenerateview.component.scss']
})
export class PayMstLeavegenerateviewComponent {

  
  select_list: any[] = [];
  employeeleave_list :any[] = [];
  employeeleavelist :any[] = [];

  employeedtl_list:any[] = [];
  reactiveForm!: FormGroup;
  selection = new SelectionModel<IEmployee>(true, []);
  monthyear: any;
  month:any;
  year:any;
  working_days:any;
  LeaveGeneratingFor:any
  date: any;
  responsedata: any;
  

  constructor(    
    private renderer: Renderer2,
    private el: ElementRef,
    public service: SocketService,
    private ToastrService: ToastrService,
    private route: Router,
    private router: ActivatedRoute,
    private formBuilder: FormBuilder,
    public NgxSpinnerService:NgxSpinnerService,
    
   
       )
   { }
   ngOnInit(): void {
    debugger;
    const monthyear = this.router.snapshot.paramMap.get('monthyear');
    this.monthyear = monthyear;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.monthyear, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam);
    const [month, year, working_days] = deencryptedParam.split('+');
    this.month = month;
    this.year = year;
    this.working_days = working_days;
    this.LeaveGeneratingFor=month+' '+year;
  
    this.Getemployeeleave(month, year);

    this.reactiveForm = new FormGroup({
      // adjusted_lop_0: new FormControl(''),
      // adjusted_lop_1: new FormControl(''),
      // adjusted_lop_2: new FormControl(''),
      // adjusted_lop_3: new FormControl(''),
      // actual_lop_0: new FormControl(''),
      // actual_lop_1: new FormControl(''),
      // actual_lop_2: new FormControl(''),
      // actual_lop_3: new FormControl(''),
      // leave_0: new FormControl(''),
      // leave_1: new FormControl(''),
      // leave_2: new FormControl(''),
      // leave_3: new FormControl(''),
      // leave_4: new FormControl(''),
      holidaycount: new FormControl(''),
      leavecount: new FormControl(''),
      absent: new FormControl(''),
      late: new FormControl(''),
      weekoff_days: new FormControl(''),
      actual_lop: new FormControl(''),
      adjusted_lop: new FormControl(''),
      permission: new FormControl(''),
      salary_days: new FormControl(''),
      lop: new FormControl(''),
      total_days: new FormControl(''),
      employeeleave_list:  this.formBuilder.array([]),
    });

       const currentDate = new Date();
       this.date = currentDate.toDateString();
  }
  Getemployeeleave(month: any,year: any) {     
    var url = 'PayTrnSalaryManagement/GetManageLeave';
    let param = {
      month: month,
      year: year,
    };
    this.NgxSpinnerService.show();
    this.service.getparams(url, param).subscribe((result: any) => {
      this.employeeleave_list = result.employeeleavelist;
      this.NgxSpinnerService.hide(); 
      setTimeout(() => {
        $('#employeeleave_list').DataTable();
      },1);
      debugger;
      for (let i = 0; i < this.employeeleave_list.length; i++) {
        this.reactiveForm.addControl(`holidaycount_${i}`, new FormControl(this.employeeleave_list[i].holidaycount));
        this.reactiveForm.addControl(`leavecount_${i}`, new FormControl(this.employeeleave_list[i].leavecount));
        this.reactiveForm.addControl(`absent_${i}`, new FormControl(this.employeeleave_list[i].absent));
        this.reactiveForm.addControl(`late_${i}`, new FormControl(this.employeeleave_list[i].late));
        this.reactiveForm.addControl(`weekoff_days_${i}`, new FormControl(this.employeeleave_list[i].weekoff_days));
        this.reactiveForm.addControl(`actual_lop_${i}`, new FormControl(this.employeeleave_list[i].actual_lop));
        this.reactiveForm.addControl(`adjusted_lop_${i}`, new FormControl(this.employeeleave_list[i].adjusted_lop));
        this.reactiveForm.addControl(`permission_${i}`, new FormControl(this.employeeleave_list[i].permission));
        this.reactiveForm.addControl(`salary_days_${i}`, new FormControl(this.employeeleave_list[i].salary_days));
        this.reactiveForm.addControl(`month_workingdays_${i}`, new FormControl(this.employeeleave_list[i].month_workingdays));
    
      }

    });
   
  }

  update(){
     
    const selectedData = this.selection.selected; // Get the selected items
    if (selectedData.length === 0) {
      this.ToastrService.warning("Select Atleast one Employee to payrun");
      return;
    } 
    
    for (const data of selectedData) {
      this.employeedtl_list.push(data);
 }

    var url = 'PayTrnSalaryManagement/Updatemonthlypayrun';
      const param = {        
        month: this.month, 
        year: this.year,
        employeeleave_list:this.employeedtl_list
         
      };
      debugger;
      console.log(param)
      this.NgxSpinnerService.show();
      this.service.postparams(url, param).subscribe((result: any) => {   
        this.NgxSpinnerService.hide();
        if (result.status === false) {
          this.ToastrService.warning(result.message);
        } else {
          this.ToastrService.success(result.message);
          this.route.navigate(['/payroll/PayTrnSalaryManagement'])
        }
      });
    

  }
  onKeyPress(event: any) {
    // Get the pressed key
    const key = event.key;
  
    if (!/^[0-9.]$/.test(key)) {
        // If not a number or dot, prevent the default action (key input)
        event.preventDefault();
    }
  }


  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.employeeleave_list.length;
    return numSelected === numRows;
  }
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.employeeleave_list.forEach((row: IEmployee) => this.selection.select(row));
  }
 
}
