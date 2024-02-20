import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { environment } from 'src/environments/environment.development';
import { ExcelService } from 'src/app/Service/excel.service';
interface IPayrunReport {
  branch_name: string;
  department_name: string;
  month: string;
  year: string;
  branch_gid: string;
  department_gid: string;
  salary_gid:string;
}
@Component({
  selector: 'app-pay-rpt-employeesalaryreport',
  templateUrl: './pay-rpt-employeesalaryreport.component.html',
  styleUrls: ['./pay-rpt-employeesalaryreport.component.scss']
})
export class PayRptEmployeesalaryreportComponent {
  reactiveForm!: FormGroup;
  responsedata: any;
  dept_name: any;
  departmentlist: any[] = [];
  branch_name: any;
  department_name: any;
  branchlist: any[] = [];
  payrun_list: any[] = [];
  monthlist: any[] = [];
  addtionOptions: any[][] = [
    ['componentOptions1:any[] = []'], ['componentOptions2:any[] = []'], ['componentOptions3:any[] = []'], ['componentOptions4:any[] = []'],['componentOptions5:any[] = []'],
    ['componentOptions6:any[] = []'], ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'], ['componentOptions9:any[] = []'], ['componentOptions10:any[] = []'],
    ['componentOptions11:any[] = []'],['componentOptions12:any[] = []'],['componentOptions1:any[] = []'],['componentOptions2:any[] = []'],['componentOptions3:any[] = []'],   
    ['componentOptions4:any[] = []'], ['componentOptions5:any[] = []'], ['componentOptions6:any[] = []'], ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],
    ['componentOptions9:any[] = []'],  ['componentOptions10:any[] = []'],['componentOptions11:any[] = []'], ['componentOptions12:any[] = []'],['componentOptions1:any[] = []'],
    ['componentOptions2:any[] = []'], ['componentOptions3:any[] = []'], ['componentOptions4:any[] = []'],['componentOptions5:any[] = []'],['componentOptions6:any[] = []'],
    ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],['componentOptions9:any[] = []'],['componentOptions10:any[] = []'],['componentOptions11:any[] = []'],
    ['componentOptions12:any[] = []']  ];
  deductionOptions: any[][] = [
    ['componentOptions1:any[] = []'], ['componentOptions2:any[] = []'], ['componentOptions3:any[] = []'], ['componentOptions4:any[] = []'],['componentOptions5:any[] = []'],
    ['componentOptions6:any[] = []'], ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'], ['componentOptions9:any[] = []'], ['componentOptions10:any[] = []'],
    ['componentOptions11:any[] = []'],['componentOptions12:any[] = []'],['componentOptions1:any[] = []'],['componentOptions2:any[] = []'],['componentOptions3:any[] = []'],   
    ['componentOptions4:any[] = []'], ['componentOptions5:any[] = []'], ['componentOptions6:any[] = []'], ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],
    ['componentOptions9:any[] = []'],  ['componentOptions10:any[] = []'],['componentOptions11:any[] = []'], ['componentOptions12:any[] = []'],['componentOptions1:any[] = []'],
    ['componentOptions2:any[] = []'], ['componentOptions3:any[] = []'], ['componentOptions4:any[] = []'],['componentOptions5:any[] = []'],['componentOptions6:any[] = []'],
    ['componentOptions7:any[] = []'], ['componentOptions8:any[] = []'],['componentOptions9:any[] = []'],['componentOptions10:any[] = []'],['componentOptions11:any[] = []'],
    ['componentOptions12:any[] = []']  
  ];
  payrunother_list: any[][] = [
    ['componentOptions1:any[] = []'], 
   ];

  
  PayrunReport!: IPayrunReport;
  branch_gid: any;
  salary_gid:any;
  department_gid: any;
  month: any; 
  company_code: any;
  payrunadd_list:any;
  year: any;
  payrundeduction_list: any;
  constructor(private formBuilder: FormBuilder,  private excelService : ExcelService, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
  this.PayrunReport = {} as IPayrunReport;
  }

  ngOnInit(): void {
    
    this.reactiveForm = new FormGroup({

         branch_name : new FormControl(''),
         department_name : new FormControl(''),     
         month: new FormControl(''),
          year: new FormControl(''),
          branch_gid: new FormControl(''),
          department_gid: new FormControl(''),
      });

    var api='PayRptPayrunSummary/GetBranchDtl'
    this.service.get(api).subscribe((result:any)=>{
    this.branchlist = result.GetBranchDtl;
    //console.log(this.branchlist)
   });

   var api='PayRptPayrunSummary/GetDepartmentDtl'
   debugger;
   this.service.get(api).subscribe((result:any)=>{
   this.departmentlist = result.GetDepartmentDtl;
   //console.log(this.branchlist)
  });
  // this.summary();
 
}
// summary(){
//   const params = {
//     branch_gid: "",
//     department_gid: "",
//     month :"selectmonth",
//     year:""
//   };

//   const url2 = 'PayRptPayrunSummary/GetPayrunSummary';

//   this.service.getparams(url2, params).subscribe((result) => {
//     this.responsedata = result;
//     this.payrun_list = this.responsedata.payrunlist;
//     this.getaddtion()
//     this.getdeduction()
//     this.getother()
//   });

// }

GetpayrunSummary() {
  const selectedBranch = this.reactiveForm.value.branch_name || 'null';
  const selectedDepartment = this.reactiveForm.value.department_name || 'null';
  const selectmonth = this.reactiveForm.value.month || 'null';
  const selectyear = this.reactiveForm.value.year || 'null';



    for (const control of Object.keys(this.reactiveForm.controls)) {
      this.reactiveForm.controls[control].markAsTouched();
    }
    const params = {
      branch_gid: selectedBranch,
      department_gid: selectedDepartment,
      month:selectmonth,
      year:selectyear
    };

    const url2 = 'PayRptPayrunSummary/Getpayrunsummary';

    this.service.getparams(url2, params).subscribe((result) => {
      this.responsedata = result;
      this.payrun_list = this.responsedata.payrunlist;
      setTimeout(() => {
        $('#payrun_list').DataTable();
      },1);

      this.getaddtion()
      this.getdeduction()
      this.getother()
    });
  
}
getaddtion(){
   for(let i=0;i<this.payrun_list.length;i++){
      const data=this.payrun_list[i];
       this.getfuncAdd(data.salary_gid,i)
      }
    }
 getfuncAdd(salary_gid :string,n :number){
      var url = 'PayRptPayrunSummary/additionalsummary'
      let param: { salary_gid: any } = {
        salary_gid: salary_gid
    };
      
    this.service.getparams(url,param).subscribe((result: any) => {
      this.addtionOptions[n] = result.addsummary; 
      debugger;
      console.log(this.addtionOptions)  
  });   
      }

      getdeduction(){
        for(let i=0;i<this.payrun_list.length;i++){
          const data=this.payrun_list[i];
           this.getfuncdeduct(data.salary_gid,i)
          }
        }
        getfuncdeduct(salary_gid :string,n :number){
          var url = 'PayRptPayrunSummary/deductsummary'
          let param: { salary_gid: any } = {
            salary_gid: salary_gid
        };
          
        this.service.getparams(url,param).subscribe((result: any) => {
          this.deductionOptions[n] = result.addsummary; 
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

              PrintPDF(salary_gid: string,month:string,year:string) {
                this.company_code = localStorage.getItem('c_code'),
                window.location.href = "http://" + environment.host + "/Print/EMS_print/pay_rpt_payempolyeeslip.aspx?salarygid=" +salary_gid + "&companycode=" + this.company_code+ "&month=" + month+ "&year=" + year
              }
    
    openModalpdf(){   
    }

    exportExcel(){
      
      const Employeesalaryreport = this.payrun_list.map(item => ({
        BranchName: item.branch_name || '', 
        Department: item.department || '',
        EmployeeCode: item.user_code || '',
        EmployeeName: item.employee_name || '',
        LeaveTaken: item.leave_taken || '', 
        LOPDays: item.lop || '',
        TotalDays: item.month_workingdays || '',
        WorkingDays: item.actual_month_workingdays || '',
        PublicHolidays: item.public_holidays || '', 
        BasicSalary: item.basic_salary || '',
        EarnedBasicSalary: item.earned_basic_salary || '',
        AdditionalComponentName: item.salarycomponent_name || '', 
        AdditionalEarnedAmount: item.earned_amount || '',
        GrossSalary: item.gross_salary || '',
        DeductionComponentName: item.salarycomponent_name || '',
        DeductionalEarnedAmount: item.earned_amount || '', 
        EarnedGrossSalary: item.earned_gross_salary || '',
        NetSalary: item.net_salary || '',
        EarnedNetSalary: item.earned_net_salary || '',
        
      }));
    
            
            this.excelService.exportAsExcelFile(Employeesalaryreport, 'employee_report');
        
          }
    
    }




  


    
    



