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

interface Ileavegrade {
  leavegradecode_list:any
}
@Component({
  selector: 'app-hrm-mst-addleavegrade',
  templateUrl: './hrm-mst-addleavegrade.component.html',
  styleUrls: ['./hrm-mst-addleavegrade.component.scss']
})

export class HrmMstAddleavegradeComponent {
  leavegradecode_list: any[] = [];
  reactiveFormadd!: FormGroup;
  responsedata:any
  selection = new SelectionModel<Ileavegrade>(true, []);

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService,public NgxSpinnerService:NgxSpinnerService) {
    this.reactiveFormadd = new FormGroup({
      leavegrade_code :new FormControl(''),
      leavegrade_name :new FormControl(''),
      Total :new FormControl(''),
      available :new FormControl(''),
      limitper_month:new FormControl(''),
  });
}

ngOnInit(): void {
  var url = 'LeaveGrade/Getleavegradecodesummary'
  this.service.get(url).subscribe((result: any) => {
    this.responsedata = result;
    this.leavegradecode_list = this.responsedata.leavegradecode_list;
  });
}

submit(){
  const selectedData = this.selection.selected; // Get the selected items
  if (selectedData.length === 0) {
    this.ToastrService.warning("Select Atleast one leave grade to Added");
    return;
  } 
  
  for (const data of selectedData) {
    this.leavegradecode_list.push(data);
}

  debugger;
  var params={ 
    leavegradecode_list : this.leavegradecode_list,
    leavegrade_code : this.reactiveFormadd.value.leavegrade_code,
    leavegrade_name : this.reactiveFormadd.value.leavegrade_name,
    total_leavecount : this.reactiveFormadd.value.Total,
    available_leavecount : this.reactiveFormadd.value.available,
    leave_limit : this.reactiveFormadd.value.limitper_month,
     
  }
  console.log(params)
  
  var url = 'LeaveGrade/LeaveGradeSubmit'
  this.NgxSpinnerService.show();
    this.service.postparams(url,params).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      if (result.status == false) {
        this.ToastrService.warning(result.message)
        this.router.navigate(['/hrm/HrmMstLeaveGrade']);

     }
     else{
      this.ToastrService.success(result.message)
      this.router.navigate(['/hrm/HrmMstLeaveGrade']);
     }

    });

}



isAllSelected() {
  const numSelected = this.selection.selected.length;
  const numRows = this.leavegradecode_list.length;
  return numSelected === numRows;
}
masterToggle() {
  this.isAllSelected() ?
    this.selection.clear() :
    this.leavegradecode_list.forEach((row: Ileavegrade) => this.selection.select(row));
}

}




