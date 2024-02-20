import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';
import { SelectionModel } from '@angular/cdk/collections';
import { NgxSpinnerService } from 'ngx-spinner';

export class IEmployee {
  employeeattendace_list: string[] = [];
  employee_gid:any;
  
}

@Component({
  selector: 'app-hrm-trn-attendancenonrollformanufacturing',
  templateUrl: './hrm-trn-attendancenonrollformanufacturing.component.html',
  styleUrls: ['./hrm-trn-attendancenonrollformanufacturing.component.scss']
})
export class HrmTrnAttendancenonrollformanufacturingComponent  {
  constructor(private renderer: Renderer2, private el: ElementRef, public service: SocketService,
     private ToastrService: ToastrService, private route: Router, private formBuilder: FormBuilder,
     public NgxSpinnerService:NgxSpinnerService,) { 
  }
 
  railwayHours: any;
  railwayMinutes: any;
  railwaySeconds: any;



  reactiveForm!: FormGroup;
  selection = new SelectionModel<IEmployee>(true, []);
  department_list: any[] = [];
  designation_list: any[] = [];
  country_list: any[] = [];
  country_list2: any[] = [];
  AttentypeList:any[]=[];
  entity_list:any[]=[];
  branchList:any[]=[];
  departmentList:any[]=[];
  employeeattendace_list:any[]=[];
  shiftList:any[]=[];
  employeeattendace_list1:any[]=[];
  login_time :any;
  Attentype:any;
  branch_name:any;
  department_name:any;
  shift_name:any;
  railwayTime: any;
  punchinTimeControl: any;
  ngOnInit(): void {
    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    
    this.updateRailwayTime();
    flatpickr('.date-picker', options);
    this.punchinTimeControl = new FormControl();
    this.reactiveForm = new FormGroup({
      date: new FormControl(''),
      branch_name: new FormControl(''),
      department_name: new FormControl(''),
      Attentype: new FormControl(''),
      punchin_time1: new FormControl(''),
      punchout_time1: new FormControl(''),
      employeeattendace_list:  this.formBuilder.array([]),

      
    });
    var url = 'HrmTrnAttendanceroll/GetBranch';
    this.service.get(url).subscribe((result: any) => {
    this.branchList = result.GetBranch1;     
    this.branchList = [{ branch_name: 'All', branch_gid: 'all' }, ...this.branchList];
    
   
        });
        this.AttentypeList = [
          { Attentype_name: 'All', Attentype_gid: 'all' },
          { Attentype_name: 'Present', Attentype_gid: 'present' },
          { Attentype_name: 'Absent', Attentype_gid: 'absent' }
        ];
        
  }
  onBranchChange(branch_gid: any) {
    
    const branchValue = this.reactiveForm.get('branch_name')!.value;
  
    if (branchValue === 'all') {
      this.departmentList = [{ department_name: 'All', department_gid: 'all' }];
    } else {
      var url1 = 'HrmTrnAttendanceroll/GetDepartment';
    let param: { branch_gid: any } = {
      branch_gid: branch_gid
  };
    this.service.getparams(url1, param).subscribe((result: any) => {
      
      this.departmentList = [{ department_name: 'All', department_gid: 'all' }];
      this.departmentList.push(...result.GetDepartment1); // Add the data to the existing array 
    });
    var url2 = 'HrmTrnAttendanceroll/GetShift';
    this.service.getparams(url2, param).subscribe((result: any) => {
      
      this.shiftList = [{ shift_name: 'All', shift_gid: 'all' }];
      this.shiftList.push(...result.shiftList); // Add the data to the existing array 
    });
    
    }
 
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.employeeattendace_list.length;
    return numSelected === numRows;
  }
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.employeeattendace_list.forEach((row: IEmployee) => this.selection.select(row));
  }
  search(){
     Object.keys(this.reactiveForm.controls).forEach(controlName => {
  if (controlName.startsWith('login_time_') || controlName.startsWith('logout_time_') || controlName.startsWith('lunch_in_')) {
    this.reactiveForm.removeControl(controlName);
  }
});
    var url = 'HrmTrnAttendanceroll/GetEmployeedtlSummary';
    let param = {
      date:this.reactiveForm.value.date,
    };
    this.NgxSpinnerService.show();
    this.service.getparams(url, param).subscribe((result: any) => {
      this.employeeattendace_list = result.employeeattendace_list1;
      this.NgxSpinnerService.hide;

      this.NgxSpinnerService.show();
      for (let i = 0; i < this.employeeattendace_list.length; i++) {
        const apiDate = new Date(this.employeeattendace_list[i].login_time);
        const hours = apiDate.getHours().toString().padStart(2, '0');
        const minutes = apiDate.getMinutes().toString().padStart(2, '0');
        const seconds = apiDate.getSeconds().toString().padStart(2, '0');
        const formattedTime = `${hours}:${minutes}:${seconds}`;
        this.reactiveForm.addControl(`login_time_${i}`, new FormControl(formattedTime));
        this.employeeattendace_list[i].login_time=formattedTime;

        const apiDate1 = new Date(this.employeeattendace_list[i].lunch_in);
        const hours1 = apiDate1.getHours().toString().padStart(2, '0');
        const minutes1 = apiDate1.getMinutes().toString().padStart(2, '0');
        const seconds1 = apiDate1.getSeconds().toString().padStart(2, '0');
        const formattedTime1 = `${hours1}:${minutes1}:${seconds1}`; // Include seconds in the formatted time
        
        this.reactiveForm.addControl(`lunch_in_${i}`, new FormControl(formattedTime1));
        this.employeeattendace_list[i].lunch_in=formattedTime1;


        const apiDate2= new Date(this.employeeattendace_list[i].logout_time);
        const hours2= apiDate2.getHours().toString().padStart(2, '0');
        const minutes2 = apiDate2.getMinutes().toString().padStart(2, '0');
        const seconds2 = apiDate2.getSeconds().toString().padStart(2, '0');
        const formattedTime2 = `${hours2}:${minutes2}:${seconds2}`;
        
        this.reactiveForm.addControl(`logout_time_${i}`, new FormControl(formattedTime2));
        this.employeeattendace_list[i].logout_time=formattedTime2;
      }
      this.NgxSpinnerService.hide(); 
  
    });
    console.log(this.reactiveForm.value)
    
  }
  logintime(event :any ,n: number){
    this.employeeattendace_list[n].login_time=event.target.value;
  }
  logouttime(event :any ,n: number){
    this.employeeattendace_list[n].logout_time=event.target.value;
  }
  
  clearAction(data :any){
    Object.keys(this.reactiveForm.controls).forEach(controlName => {
  if (controlName.startsWith('login_time_') || controlName.startsWith('logout_time_') || controlName.startsWith('lunch_in_')) {
    this.reactiveForm.removeControl(controlName);
  }
});
    
     window.scrollTo({ top: 0, behavior: 'smooth' });
     let param={
     employee_gid:data.employee_gid,
    date:this.reactiveForm.value.date,
     }
      var url='HrmTrnAttendanceroll/cleardtl';
     window.scrollTo({ top: 0, behavior: 'smooth' });
    this.service.postparams(url,param).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.ToastrService.success(result.message)
        this.search()
      }
    });

  }
  updateAction(data :any){
    Object.keys(this.reactiveForm.controls).forEach(controlName => {
  if (controlName.startsWith('login_time_') || controlName.startsWith('logout_time_') || controlName.startsWith('lunch_in_')) {
    this.reactiveForm.removeControl(controlName);
  }
});
  
  let param={
    login_time:data.login_time,
    logout_time :data.logout_time,
    employee_gid:data.employee_gid,
    date:this.reactiveForm.value.date,

  }
    var url='HrmTrnAttendanceroll/updatedtl';
     window.scrollTo({ top: 0, behavior: 'smooth' });
    this.service.postparams(url,param).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.ToastrService.success(result.message)
        this.search()
      }
    });


  }
  punchin(){
    Object.keys(this.reactiveForm.controls).forEach(controlName => {
      if (controlName.startsWith('login_time_') ) {
        this.reactiveForm.removeControl(controlName);
      }
    });
    window.scrollTo({ top: 0, behavior: 'smooth' });
    const selectedData = this.selection.selected; 
    if (selectedData.length === 0) {
      this.ToastrService.warning("Select Atleast one Employee to punchin");
      return;
    } 
    if (this.reactiveForm.value.punchin_time1 == "") {
      this.ToastrService.warning("Select Punch in Time");
      return;
    }
    
    for (const data of selectedData) {
      this.employeeattendace_list1.push(data);
 }

    var url = 'HrmTrnAttendanceroll/Updatepunchindtl';
      const param = {        
        date: this.reactiveForm.value.date, 
        punchin_time: this.reactiveForm.value.punchin_time1,
        update_lists:this.employeeattendace_list1,
         
      };
      debugger;
      console.log(param)
      this.NgxSpinnerService.show();
      this.service.postparams(url, param).subscribe((result: any) => {   
        this.employeeattendace_list1 = [];
        this.NgxSpinnerService.hide();
        this.selection.clear();
        if (result.status === false) {
          this.ToastrService.warning(result.message);
        } else {
          this.ToastrService.success(result.message);
        }
       
        this.search()
      });
  }
  punchout(){
    Object.keys(this.reactiveForm.controls).forEach(controlName => {
      if (controlName.startsWith('logout_time_') ) {
        this.reactiveForm.removeControl(controlName);
      }
    });
    window.scrollTo({ top: 0, behavior: 'smooth' });
    const selectedData = this.selection.selected; 
    if (selectedData.length === 0) {
      this.ToastrService.warning("Select Atleast one Employee to punchout");
      return;
    } 
    if (this.reactiveForm.value.punchout_time1 == "") {
      this.ToastrService.warning("Select Punch out Time");
      return;
    }
    
    for (const data of selectedData) {
      this.employeeattendace_list1.push(data);
 }

    var url = 'HrmTrnAttendanceroll/Updatepunchoutdtl';
      const param = {        
        date: this.reactiveForm.value.date, 
        punchout_time: this.reactiveForm.value.punchout_time1,
        update_lists:this.employeeattendace_list1,
         
      };
      debugger;
      console.log(param)
      this.NgxSpinnerService.show();
      this.service.postparams(url, param).subscribe((result: any) => {   
        this.employeeattendace_list1 = [];
        this.NgxSpinnerService.hide();
        this.selection.clear();
        if (result.status === false) {
          this.ToastrService.warning(result.message);
        } else {
          this.ToastrService.success(result.message);
        }
       
        this.search()
      });
  }
  updateRailwayTime(event?: Event): void {
    // If there's an event (user input), update the corresponding value
    if (event) {
      const target = event.target as HTMLInputElement;
      const inputArray = target.value.split(':');
  
      if (inputArray.length === 3) {
        const hours = parseInt(inputArray[0], 10);
        const minutes = parseInt(inputArray[1], 10);
        const isPM = target.value.toLowerCase().includes('pm');
  
        // Convert to railway time
        if (isPM && hours < 12) {
          this.railwayHours = (hours + 12).toString().padStart(2, '0');
        } else if (!isPM && hours === 12) {
          // Handle midnight (12:00 AM) case
          this.railwayHours = '00';
        } else {
          this.railwayHours = hours.toString().padStart(2, '0');
        }
  
        this.railwayMinutes = minutes.toString().padStart(2, '0');
        this.railwaySeconds = inputArray[2];
      }
    }
  }
  

  formattedRailwayTime(): string {
    return `${this.railwayHours || '00'}-${this.railwayMinutes || '00'}-${this.railwaySeconds || '00'}`;
  }
  

}
