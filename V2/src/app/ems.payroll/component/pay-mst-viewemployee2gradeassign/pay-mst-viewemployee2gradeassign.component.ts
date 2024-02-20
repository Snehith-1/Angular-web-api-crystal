import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
@Component({
  selector: 'app-pay-mst-viewemployee2gradeassign',
  templateUrl: './pay-mst-viewemployee2gradeassign.component.html',
  styleUrls: ['./pay-mst-viewemployee2gradeassign.component.scss']
})
export class PayMstViewemployee2gradeassignComponent {
  reactiveFormadd!: FormGroup;
  details_list: any[] = []
  addition_list: any[] = []
  deduction_list: any[] = []
  salarygradetemplate_gid: any;
  responsedata: any;
  selection: any;
  reactiveForm!: FormGroup;
  employee2salarygradetemplate_gid: any;
  constructor(
    private renderer: Renderer2,
    private el: ElementRef,
    public service: SocketService,
    private ToastrService: ToastrService,
    private route: Router,
    private router: ActivatedRoute,
    private formBuilder: FormBuilder,) { }

    ngOnInit(): void {
    const employee2salarygradetemplate_gid = this.router.snapshot.paramMap.get('employee2salarygradetemplate_gid');
   
    this.employee2salarygradetemplate_gid = employee2salarygradetemplate_gid;

    const secretKey = 'storyboarderp';

    const deencryptedParam = AES.decrypt(this.employee2salarygradetemplate_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.GetEditEmployeetosalarygrade(deencryptedParam)
    this.reactiveForm = this.formBuilder.group({
     
    });
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
  GetEditEmployeetosalarygrade(employee2salarygradetemplate_gid :any){
    this.Addtictionsummary(employee2salarygradetemplate_gid);
    this.deductionsummary(employee2salarygradetemplate_gid);
    this.detailssummary(employee2salarygradetemplate_gid); 
    this.employee2salarygradetemplate_gid=employee2salarygradetemplate_gid;

  }

  Addtictionsummary(employee2salarygradetemplate_gid: string){
    var url = 'PayMstEmployeesalarytemplate/EditAddtictionsummary'
    let param: { employee2salarygradetemplate_gid: any , salarygradetype :any } = {
      employee2salarygradetemplate_gid: employee2salarygradetemplate_gid,
      salarygradetype : "Addition"
    };
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.addition_list = this.responsedata.Editsummaryaddition_list;
     
      this.addition_list = this.responsedata.Editsummaryaddition_list;
   
   
      setTimeout(() => {
        $('#addition_list').DataTable();
      }, 1);
  
  
    });
  
  }
  deductionsummary(employee2salarygradetemplate_gid: string){
    var url = 'PayMstEmployeesalarytemplate/Editdeductionsummary'
    let param: { employee2salarygradetemplate_gid: any , salarygradetype :any } = {
      employee2salarygradetemplate_gid: employee2salarygradetemplate_gid,
      salarygradetype : "Deduction"
    };
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;   
      this.deduction_list = this.responsedata.Editsummarydeduction_list;

      this.deduction_list = this.responsedata.Editsummarydeduction_list;
     
      debugger; 
      setTimeout(() => {
        $('#deduction_list').DataTable();
      }, 1);
    });
  
  }
  detailssummary(employee2salarygradetemplate_gid: string){
    var url1 = 'PayMstEmployeesalarytemplate/EditDetailssummary';
    let param: { employee2salarygradetemplate_gid: any } = {
      employee2salarygradetemplate_gid: employee2salarygradetemplate_gid
    };
    this.service.getparams(url1, param).subscribe((result: any) => {
      this.details_list[0] = result.edittemplate_list[0];
      const basic_salary = this.details_list[0].basic_salary;
      const net_salary = this.details_list[0].net_salary;
      const gross_salary = this.details_list[0].gross_salary;
      const ctc = this.details_list[0].ctc;
      const template_name = this.details_list[0].template_name;
      debugger;
      this.reactiveFormadd.patchValue({
        basic_salary: basic_salary, 
        template_name: template_name, 
        gross_salary: gross_salary, 
        net_salary: net_salary,
        ctc: ctc,

      }); 
    });
  
  }
 
  }

