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


export class IEmployee {
  employeegrade_list: string[] = [];
  employee_gid:any;
}
@Component({
  selector: 'app-pay-mst-employee2gradeassign',
  templateUrl: './pay-mst-employee2gradeassign.component.html',
  styleUrls: ['./pay-mst-employee2gradeassign.component.scss']
})
export class PayMstEmployee2gradeassignComponent {


  
  employeegrade_list: any[] = [];
  select_list: any[] = [];
  reactiveForm!: FormGroup;
  selection = new SelectionModel<IEmployee>(true, []);
  monthyear: any;
  month:any;
  year:any;
  

  
  constructor(    
    private renderer: Renderer2,
    private el: ElementRef,
    public service: SocketService,
    private ToastrService: ToastrService,
    private route: Router,
    private router: ActivatedRoute,
    private formBuilder: FormBuilder
       )
   { }
   ngOnInit(): void {
    debugger;
  
    this.Getemployeegrade();
    this.reactiveForm = this.formBuilder.group({
     
    });
  }
  Getemployeegrade() {
    var url = 'PayMstEmployeesalarytemplate/GetEmployeegradeassignsummary';
    this.service.get(url).subscribe((result: any) => {
      this.employeegrade_list = result.Getgradeassign_list;
    });
  }
  submit() {
    debugger;
    const selectedData = this.selection.selected; // Get the selected items
    if (selectedData.length === 0) {
      this.ToastrService.warning("Select At least one Employee grade");
      return;
    } 
    const param = selectedData.map(data => data.employee_gid).join('+');
    console.log(param);
    this.selection.clear();
    const secretKey = 'storyboarderp';
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.route.navigate(['/payroll/PayMstEmployeegradeconfirm',encryptedParam])
  }
  
  
   isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.employeegrade_list.length;
    return numSelected === numRows;
  }
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.employeegrade_list.forEach((row: IEmployee) => this.selection.select(row));
  }
}