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
@Component({
  selector: 'app-pay-mst-employeegradeconfirm',
  templateUrl: './pay-mst-employeegradeconfirm.component.html',
  styleUrls: ['./pay-mst-employeegradeconfirm.component.scss']
})
export class PayMstEmployeegradeconfirmComponent {
  reactiveFormadd!: FormGroup;
  details_list: any[] = []
  template_list: any[] = []
  addition_list: any[] = []
  deduction_list: any[] = []
  calculate_addition: any[] = []
  calculate_deduction: any[] = []
  salarygradetemplate_gid: any;
  basic_salary1:any;
  componentOptions: any[][] = [
    ['componentOptions1:any[] = []'],
    ['componentOptions2:any[] = []'],
    ['componentOptions3:any[] = []'],   
    ['componentOptions4:any[] = []'],
    ['componentOptions5:any[] = []'],
    ['componentOptions6:any[] = []'],  
  ];
  componentOptions1: any[][] = [
    ['componentOption1:any[] = []'],
    ['componentOption2:any[] = []'],
    ['componentOption3:any[] = []'],   
    ['componentOption4:any[] = []'],
    ['componentOption5:any[] = []'],
    ['componentOption6:any[] = []'],  
  ];
  responsedata: any;
  selection: any;
  reactiveForm!: FormGroup;
  employee_gids: any;
  employee_lists: any[] = [];
  month:any;
  year:any;
  template_name:any;
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
    const employee_gids = this.router.snapshot.paramMap.get('employee_gids');
    this.employee_gids = employee_gids;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.employee_gids, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam);

    // Split the deencryptedParam using '+' and insert into employee_lists
    this.employee_lists = deencryptedParam.split('+');
    
    // Create an array of objects with the "employee_gid" property
    this.employee_lists = this.employee_lists.map(value => ({ "employee_gid": value }));
    
    // Log the resulting array of objects
    console.log(this.employee_lists);
    


    this.GettemplateName();
    
    this.reactiveFormadd = new FormGroup({
      employee_contribution_0:new FormControl(''),
      employee_contribution_1:new FormControl(''),
      employee_contribution_2:new FormControl(''),
      employee_contribution_3:new FormControl(''),
      employee_contribution_4:new FormControl(''),
      employee_contribution_5:new FormControl(''),
      employee_contribution_6:new FormControl(''),
      employee_contribution_7:new FormControl(''),
      employee_contribution_8:new FormControl(''),
      employee_contribution_9:new FormControl(''),


      employer_contribution_0:new FormControl(''),
      employer_contribution_1:new FormControl(''),
      employer_contribution_2:new FormControl(''),
      employer_contribution_3:new FormControl(''),
      employer_contribution_4:new FormControl(''),
      employer_contribution_5:new FormControl(''),
      employer_contribution_6:new FormControl(''),
      employer_contribution_7:new FormControl(''),
      employer_contribution_8:new FormControl(''),
      employer_contribution_9:new FormControl(''),

      demployee_contribution_0: new FormControl(''),
      demployee_contribution_1: new FormControl(''),
      demployee_contribution_2: new FormControl(''),
      demployee_contribution_3: new FormControl(''),
      demployee_contribution_4: new FormControl(''),
      demployee_contribution_5: new FormControl(''),

      demployer_contribution_0: new FormControl(''),
      demployer_contribution_1: new FormControl(''),
      demployer_contribution_2: new FormControl(''),
      demployer_contribution_3: new FormControl(''),
      demployer_contribution_4: new FormControl(''),
      demployer_contribution_5: new FormControl(''),



      componentgroup_gid :new FormControl(''),
      componentgroup_code :new FormControl(''),
      componentgroup_name:new FormControl(''),
      display_name:new FormControl(''),
      statutory: new FormControl(''),
      template_name: new FormControl(''),
      component_name: new FormControl(''),
      basic_salary: new FormControl(''),
      salarycomponent_name: new FormControl(''),
      salarygradetemplate_gid: new FormControl(''),
      employee_contribution :new FormControl(''),
      employer_contribution: new FormControl(''),
      demployee_contribution :new FormControl(''),
      demployer_contribution: new FormControl(''),
      gross_salary: new FormControl(''),
      grand_salary: new FormControl(''),
      ctc: new FormControl(''),
      net_salary: new FormControl(''),
      addition_list: this.formBuilder.array([]),
      deduction_list: this.formBuilder.array([])
      

    });
  

   
    

  }

 
 


  GettemplateName(){
    var url = 'PayMstEmployeesalarytemplate/GettemplateName'
    this.service.get(url).subscribe((result: any) => {
  
      this.responsedata = result;
      this.template_list = this.responsedata.template_list;
    });

  }

  Addtictionsummary(salarygradetemplate_gid: string){
    var url = 'PayMstEmployeesalarytemplate/Addtictionsummary'
    let param: { salarygradetemplate_gid: any , salarygradetype :any } = {
      salarygradetemplate_gid: salarygradetemplate_gid,
      salarygradetype : "Addition"
    };
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.addition_list = this.responsedata.summaryaddition_list;
      this.triggerGetOptions();
      this.addition_list = this.responsedata.summaryaddition_list;
 
   
      setTimeout(() => {
        $('#addition_list').DataTable();
      }, 1);
  
  
    });

  }


  deductionsummary(salarygradetemplate_gid: string){
    var url = 'PayMstEmployeesalarytemplate/deductionsummary'
    let param: { salarygradetemplate_gid: any , salarygradetype :any } = {
      salarygradetemplate_gid: salarygradetemplate_gid,
      salarygradetype : "Deduction"
    };
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;   
      this.deduction_list = this.responsedata.summarydeduction_list;
      this.triggerGetOptions1();
      this.deduction_list = this.responsedata.summarydeduction_list;
      debugger; 
      setTimeout(() => {
        $('#deduction_list').DataTable();
      }, 1);
    });

  }
  fecthdetails(salarygradetemplate_gid: string): void {
    this.Addtictionsummary(salarygradetemplate_gid);
    this.deductionsummary(salarygradetemplate_gid);
    this.detailssummary(salarygradetemplate_gid); 
    this.salarygradetemplate_gid=salarygradetemplate_gid;
  }


  triggerGetOptions(): void {
    for (let i = 0; i < this.addition_list.length; i++) {
      const data = this.addition_list[i];
      this.getoptions(data.componentgroup_name, i);
    }
  }
  triggerGetOptions1(): void {
    for (let i = 0; i < this.deduction_list.length; i++) {
      const data = this.deduction_list[i];
      this.getdeduction(data.componentgroup_name, i);
    }
  }
  onKeyPress(event: any) {
    // Get the pressed key
    const key = event.key;
   
    // Check if the key is a number or a dot (for decimal point)
    if (!/^[0-9.]$/.test(key)) {
        // If not a number or dot, prevent the default action (key input)
        event.preventDefault();
    }
  }
  detailssummary(salarygradetemplate_gid: string){
    var url1 = 'PayMstEmployeesalarytemplate/Detailssummary';
    let param: { salarygradetemplate_gid: any } = {
      salarygradetemplate_gid: salarygradetemplate_gid
    };
    this.service.getparams(url1, param).subscribe((result: any) => {
      this.details_list[0] = result.template_list[0];
      const basic_salary = this.details_list[0].basic_salary;
      const net_salary = this.details_list[0].net_salary;
      const gross_salary = this.details_list[0].gross_salary;
      this.reactiveFormadd.patchValue({
        basic_salary: basic_salary,       
      }); 
    });
  
  } 
  calculateaddition1(salarycomponent_gid: string, n: number): void { 
   
    var url1 = 'PayTrnSalaryGrade/Getcomponentamount';
    let param: { salarycomponent_gid: any } = {
      salarycomponent_gid: salarycomponent_gid
    };
    this.service.getparams(url1, param).subscribe((result: any) => {
      this.calculate_addition[0] = result.Getcomponentamount[0];
      const component_flag_employer = this.calculate_addition[0].component_flag_employer;
      const component_flag = this.calculate_addition[0].component_flag;
      const component_percentage = this.calculate_addition[0].component_percentage;
      const component_amount = this.calculate_addition[0].component_amount;
      const component_percentage_employer = this.calculate_addition[0].component_percentage_employer;
      const component_amount_employer = this.calculate_addition[0].component_amount_employer;
      const newBasicSalary = this.reactiveFormadd.get('basic_salary')?.value;
  
      if (component_flag == "Y") {
        this.addition_list[n].employee_contribution =Number((newBasicSalary) * (component_percentage / 100)).toFixed(2)
        this.reactiveFormadd.addControl(`employee_contribution_${n}`, new FormControl(this.addition_list[n].employee_contribution));
       } 
      else {
        this.addition_list[n].employee_contribution = Number(component_amount).toFixed(2)
        this.reactiveFormadd.addControl(`employee_contribution_${n}`, new FormControl(this.addition_list[n].employee_contribution));
        }
  
      if (component_flag_employer == "Y") {
  
        this.addition_list[n].employer_contribution = Number((newBasicSalary) * (component_percentage_employer / 100)).toFixed(2)
        this.reactiveFormadd.addControl(`employer_contribution_${n}`, new FormControl(this.addition_list[n].employer_contribution));
       } else {
  
        this.addition_list[n].employer_contribution = Number(component_amount_employer).toFixed(2)
        this.reactiveFormadd.addControl(`employer_contribution_${n}`, new FormControl(this.addition_list[n].employer_contribution));
         }

     });

  }
  
  getoptions(componentgroup_name: any,n:number) {
    var url1 = 'PayTrnSalaryGrade/Getcomponentname';
    let param: { componentgroup_name: any } = {
        componentgroup_name: componentgroup_name
    };
    this.service.getparams(url1,param).subscribe((result: any) => {
        this.componentOptions[n] = result.GetComponentname;
        debugger;   
    });
}

 
getdeduction(componentgroup_name: any,n:number) {
  var url1 = 'PayTrnSalaryGrade/Getcomponentname';
  let param: { componentgroup_name: any } = {
      componentgroup_name: componentgroup_name
  };
  debugger;
  const salary=  this.reactiveFormadd.value.basic_salary;
  if(componentgroup_name=="ESI" && salary >=21000 ){
    this.reactiveFormadd.addControl(`demployer_contribution_${n}`, new FormControl(0));
    this.reactiveFormadd.addControl(`demployee_contribution_${n}`, new FormControl(0));
    this.componentOptions1[n] = [];

  }
  else{
  this.service.getparams(url1,param).subscribe((result: any) => {
      this.componentOptions1[n] = result.GetComponentname;
     
     
  });}
}

  
submit() {
  debugger;
  console.log(this.addition_list)
  console.log(this.deduction_list)
  console.log(this.employee_lists)

  var params={ 
    summaryaddition_list : this.addition_list,
    summarydeduction_list: this.deduction_list,
    employee_lists: this.employee_lists,
    newBasicSalary : this.reactiveFormadd.value.basic_salary,
    template_name : this.reactiveFormadd.value.template_name,
    gross_salary : this.reactiveFormadd.value.gross_salary,
    ctc : this.reactiveFormadd.value.ctc,
    net_salary : this.reactiveFormadd.value.net_salary,
    salarygradetemplate_gid : this.salarygradetemplate_gid,
  }
  console.log(params)
 
  var url = 'PayMstEmployeesalarytemplate/PostSalaryGradetoemployee'
  this.NgxSpinnerService.show();
    this.service.postparams(url,params).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      if (result.status == false) {
        this.ToastrService.warning(result.message)
        this.route.navigate(['/payroll/PayMstEmployeetemplatesummary']);
     }
     else{
      this.ToastrService.success(result.message)
      this.route.navigate(['/payroll/PayMstEmployeetemplatesummary']);
     }
    });

}
calculatededuction(salarycomponent_gid: string, n: number): void { 
  debugger;
  var url1 = 'PayTrnSalaryGrade/Getcomponentamount';
  let param: { salarycomponent_gid: any } = {
    salarycomponent_gid: salarycomponent_gid
  };
  this.service.getparams(url1, param).subscribe((result: any) => {
    this.calculate_deduction[0] = result.Getcomponentamount[0];
    const component_flag_employer = this.calculate_deduction[0].component_flag_employer;
    const component_flag = this.calculate_deduction[0].component_flag;
    const component_percentage = this.calculate_deduction[0].component_percentage;
    const component_amount = this.calculate_deduction[0].component_amount;
    const component_percentage_employer = this.calculate_deduction[0].component_percentage_employer;
    const component_amount_employer = this.calculate_deduction[0].component_amount_employer;
    const componentgroup_name = this.calculate_deduction[0].componentgroup_name;
    if(componentgroup_name=="ESI"){
      this.basic_salary1 = this.reactiveFormadd.get('gross_salary')?.value;

    }
    else{
      this.basic_salary1 = this.reactiveFormadd.get('basic_salary')?.value;

    }
    
    debugger;
    if (component_flag == "Y") {
      this.deduction_list[n].demployee_contribution =Number( (this.basic_salary1) * (component_percentage / 100)).toFixed(2);
      this.reactiveFormadd.addControl(`demployee_contribution_${n}`, new FormControl(this.deduction_list[n].demployee_contribution));
     } 
    else {
      this.deduction_list[n].demployee_contribution = Number(component_amount).toFixed(2);
      this.reactiveFormadd.addControl(`demployee_contribution_${n}`, new FormControl(this.deduction_list[n].demployee_contribution));
      }

    if (component_flag_employer == "Y") {

      this.deduction_list[n].demployer_contribution = Number((this.basic_salary1) * (component_percentage_employer / 100)).toFixed(2);
      this.reactiveFormadd.addControl(`demployer_contribution_${n}`, new FormControl(this.deduction_list[n].demployer_contribution));
     } else {

      this.deduction_list[n].demployer_contribution = Number(component_amount_employer).toFixed(2);
      this.reactiveFormadd.addControl(`demployer_contribution_${n}`, new FormControl(this.deduction_list[n].demployer_contribution));
       }
   });

}

  onBasicSalaryChange(){
   
    let totalAddEmployeeContribution = 0;
    let totalAddEmployerContribution = 0;
    let totalDedEmployeeContribution = 0;
    let totalDedEmployerContribution = 0;

    for (let i = 0; i < this.addition_list.length; i++) {
      totalAddEmployeeContribution += parseFloat(this.addition_list[i].employee_contribution || 0);
    }

    for (let i = 0; i < this.addition_list.length; i++) {
      totalAddEmployerContribution += parseFloat(this.addition_list[i].employer_contribution || 0);
    }

    for (let i = 0; i < this.deduction_list.length; i++) {
      totalDedEmployeeContribution += parseFloat(this.deduction_list[i].demployee_contribution || 0);
    }

    for (let i = 0; i < this.deduction_list.length; i++) {
      totalDedEmployerContribution += parseFloat(this.deduction_list[i].demployer_contribution || 0);
    }


    const newBasicSalary = parseFloat(this.reactiveFormadd.get('basic_salary')?.value || 0);
    let basic_salary1: number = Number(newBasicSalary.toFixed(2));
    let totalAddEmployeeContribution1: number = Number(totalAddEmployeeContribution.toFixed(2));
    let totalAddEmployerContribution1: number = Number(totalAddEmployerContribution.toFixed(2));
    let totalDedEmployerContribution1: number = Number(totalDedEmployerContribution.toFixed(2));
    let totalDedEmployeeContribution1: number = Number(totalDedEmployeeContribution.toFixed(2));
    


    let grossSalary: number = Number(basic_salary1) + Number(totalAddEmployeeContribution1);
    let grand_salary: number = Number(grossSalary) + Number(totalAddEmployerContribution1);
    let ctc: number = Number(grand_salary) + Number(totalDedEmployerContribution1);
    let net_salary: number = Number(grand_salary) - Number(totalDedEmployeeContribution1);
    

    this.reactiveFormadd.patchValue({
      basic_salary: basic_salary1.toFixed(2),
      gross_salary: grossSalary.toFixed(2),
      grand_salary: grand_salary.toFixed(2),
      net_salary: net_salary.toFixed(2),
      ctc: ctc.toFixed(2),
    });
}
  

}
