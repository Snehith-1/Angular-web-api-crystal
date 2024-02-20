import { Component, OnInit, OnDestroy, ChangeDetectorRef, Renderer2, ElementRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
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
interface IPayrunReport {
  
}

@Component({
  selector: 'app-pay-trn-payrunview',
  templateUrl: './pay-trn-payrunview.component.html',
  styleUrls: ['./pay-trn-payrunview.component.scss']
})
export class PayTrnPayrunviewComponent {
  reactiveForm!: FormGroup;
  responsedata: any;
  dept_name: any;
  departmentlist: any[] = [];
  branch_name: any;
  branchlist: any[] = [];
  payrun_list: any[] = [];
  selection = new SelectionModel<IPayrunReport>(true, []);
  addtionOptions: any[][] = [
    ['componentOptions1:any[] = []'], ['componentOptions2:any[] = []'], ['componentOptions3:any[] = []'], ['componentOptions4:any[] = []'],['componentOptions5:any[] = []'],
    ['componentOptions6:any[] = []'], ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'], ['componentOptions9:any[] = []'], ['componentOptions10:any[] = []'],
    ['componentOptions11:any[] = []'],['componentOptions12:any[] = []'],['componentOptions1:any[] = []'],['componentOptions2:any[] = []'],['componentOptions3:any[] = []'],   
    ['componentOptions4:any[] = []'], ['componentOptions5:any[] = []'], ['componentOptions6:any[] = []'], ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],
    ['componentOptions9:any[] = []'],  ['componentOptions10:any[] = []'],['componentOptions11:any[] = []'], ['componentOptions12:any[] = []'],['componentOptions1:any[] = []'],
    ['componentOptions2:any[] = []'], ['componentOptions3:any[] = []'], ['componentOptions4:any[] = []'],['componentOptions5:any[] = []'],['componentOptions6:any[] = []'],
    ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],['componentOptions9:any[] = []'],['componentOptions10:any[] = []'],['componentOptions11:any[] = []'],
    ['componentOptions12:any[] = []'],['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],['componentOptions9:any[] = []'],['componentOptions10:any[] = []'],['componentOptions11:any[] = []'],
    ['componentOptions12:any[] = []'],    ['componentOptions9:any[] = []'],  ['componentOptions10:any[] = []'],['componentOptions11:any[] = []'], ['componentOptions12:any[] = []'],['componentOptions1:any[] = []'],
    ['componentOptions2:any[] = []'], ['componentOptions3:any[] = []'], ['componentOptions4:any[] = []'],['componentOptions5:any[] = []'],['componentOptions6:any[] = []'],
    ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],['componentOptions9:any[] = []'],['componentOptions10:any[] = []'],['componentOptions11:any[] = []'],
    ['componentOptions12:any[] = []'],['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],['componentOptions9:any[] = []'],['componentOptions10:any[] = []'],['componentOptions11:any[] = []'],
    ['componentOptions12:any[] = []']    ];
  deductionOptions: any[][] = [
    ['componentOptions1:any[] = []'], ['componentOptions2:any[] = []'], ['componentOptions3:any[] = []'], ['componentOptions4:any[] = []'],['componentOptions5:any[] = []'],
    ['componentOptions6:any[] = []'], ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'], ['componentOptions9:any[] = []'], ['componentOptions10:any[] = []'],
    ['componentOptions11:any[] = []'],['componentOptions12:any[] = []'],['componentOptions1:any[] = []'],['componentOptions2:any[] = []'],['componentOptions3:any[] = []'],   
    ['componentOptions4:any[] = []'], ['componentOptions5:any[] = []'], ['componentOptions6:any[] = []'], ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],
    ['componentOptions9:any[] = []'],  ['componentOptions10:any[] = []'],['componentOptions11:any[] = []'], ['componentOptions12:any[] = []'],['componentOptions1:any[] = []'],
    ['componentOptions2:any[] = []'], ['componentOptions3:any[] = []'], ['componentOptions4:any[] = []'],['componentOptions5:any[] = []'],['componentOptions6:any[] = []'],
    ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],['componentOptions9:any[] = []'],['componentOptions10:any[] = []'],['componentOptions11:any[] = []'],
    ['componentOptions12:any[] = []'],['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],['componentOptions9:any[] = []'],['componentOptions10:any[] = []'],['componentOptions11:any[] = []'],
    ['componentOptions12:any[] = []'],  ['componentOptions2:any[] = []'], ['componentOptions3:any[] = []'], ['componentOptions4:any[] = []'],['componentOptions5:any[] = []'],['componentOptions6:any[] = []'],
    ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],['componentOptions9:any[] = []'],['componentOptions10:any[] = []'],['componentOptions11:any[] = []'],
    ['componentOptions12:any[] = []'],['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],['componentOptions9:any[] = []'],['componentOptions10:any[] = []'],['componentOptions11:any[] = []'],
    ['componentOptions12:any[] = []']   
  ];
  payrunother_list: any[][] = [
    ['componentOptions1:any[] = []'], 
   ];

  
  PayrunReport!: IPayrunReport;
  branch_gid: any;
  salary_gid:any;
  monthyear: any;
  month:any;
  year:any;
  working_days: any;
  department_gid: any;
  company_code: any;
  payrunadd_list:any;
  payrundeduction_list: any;
  constructor(private formBuilder: FormBuilder,
     private route: ActivatedRoute,
      private router: Router,
       private ToastrService: ToastrService,
        public service: SocketService) {
  this.PayrunReport = {} as IPayrunReport;
  }

  ngOnInit(): void {
    const monthyear = this.route.snapshot.paramMap.get('monthyear');
    this.monthyear = monthyear;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.monthyear, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam);
    debugger;
    const [month, year,working_days] = deencryptedParam.split('+');
    this.month = month;
    this.year = year;
    this.working_days = working_days;

 
  this.summary(this.month,this.year);

}
summary(month :any,year :any){
  const param = {
    month :month,
    year:year,
  };

  const url = 'PayTrnSalaryManagement/Getpayrunsummary';

  this.service.getparams(url, param).subscribe((result: any) => {
    this.payrun_list = result.payrunviewlist;
    this.getaddtion()
    this.getother()
    setTimeout(() => {
      $('#payrun_list').DataTable();
    },1);
  });

}


getaddtion(){
   for(let i=0;i<this.payrun_list.length;i++){
      const data=this.payrun_list[i];
       this.getfuncAdd(data.salary_gid,i)
       this.getfuncdeduct(data.salary_gid,i)
      }
    }
 getfuncAdd(salary_gid :string,n :number){
      var url = 'PayTrnSalaryManagement/Additionalsubsummary'
      let param: { salary_gid: any } = {
        salary_gid: salary_gid
       };
      
    this.service.getparams(url,param).subscribe((result: any) => {
      this.addtionOptions[n] = result.addsummary1; 
      debugger;
      console.log(this.addtionOptions)  
      });   
      }

 getfuncdeduct(salary_gid :string,n :number){
          var url = 'PayTrnSalaryManagement/Deductsubsummary'
          let param: { salary_gid: any } = {
            salary_gid: salary_gid
           };
          
        this.service.getparams(url,param).subscribe((result: any) => {
          this.deductionOptions[n] = result.addsummary1; 
          debugger;
          console.log(this.deductionOptions)  
          });   
  }

  getother(){
    for(let i=0;i<this.payrun_list.length;i++){
        const data=this.payrun_list[i];
        this.getfuncother (data.salary_gid,i)
          }
        }
  getfuncother(salary_gid :string,n :number){
     var url = 'PayRptPayrunSummary/othersummary'
     let param: { salary_gid: any } = {
     salary_gid: salary_gid
            };
              
    this.service.getparams(url,param).subscribe((result: any) => {
    this.payrunother_list[n] = result.addsummary; 
    debugger;
    console.log(this.payrunother_list)  
       });   
     }


isAllSelected() {
  const numSelected = this.selection.selected.length;
  const numRows = this.payrun_list.length;
  return numSelected === numRows;
   }
masterToggle() {
  this.isAllSelected() ?
  this.selection.clear() :
  this.payrun_list.forEach((row: IPayrunReport) => this.selection.select(row));
  }

        
}