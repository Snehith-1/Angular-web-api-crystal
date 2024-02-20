import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CountryISO, SearchCountryField } from "ngx-intl-tel-input";
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AES, enc } from 'crypto-js';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';

@Component({
  selector: 'app-pay-mst-form16employeedetail',
  templateUrl: './pay-mst-form16employeedetail.component.html',
  styleUrls: ['./pay-mst-form16employeedetail.component.scss']
})

export class PayMstForm16employeedetailComponent {
  personalinfoForm!: FormGroup;
  quartersinfoForm!: FormGroup;
  incometaxForm!: FormGroup;
  incomeForm!: FormGroup;

  tdsinfoForm!: FormGroup;
  // file:any;
  // SearchCountryField = SearchCountryField;
  // CountryISO = CountryISO;
  // preferredCountries: CountryISO[] = [CountryISO.India, CountryISO.India];
  employee_gid: any;
  assessment_gid: any;
  responsedata: any;
  personalinfo: any;
  quartersinfo: any;
  finyear_list: any;
  incometax: any;
  incomeinfo : any;

  totalamount : number = 0;
  grosssalary_amount: number = 0;
  perquisites_amount: number = 0;
  profitof_salary: number = 0;
  lessallowancetotal: number = 0;
  pfamount1: number = 0;
  pfamount2: number = 0;
  pfamount3: number = 0;
  component1: number = 0;
  component2: number = 0;
  component3: number = 0;
  balanceamount: number = 0;
  entertainment_allowance: number = 0;
  taxon_emp: number = 0;
  aggreagateofab: number = 0;
  incomecharge_headsalaries: number = 0;
  grosstotal_income: number = 0;
  employeeincome_total: number = 0;
  employeeincome_rs1: number = 0;
  employeeincome_rs2: number = 0;
  employeeincome_rs3: number = 0;

  total_amt_paid_credited: number = 0;
  q1_amt_paid_credited: number = 0;
  q2_amt_paid_credited: any;
  q3_amt_paid_credited: any;
  q4_amt_paid_credited: number = 0;
  total_amt_tax_deducted: number = 0;
  q1_amt_tax_deducted: number = 0;
  q2_amt_tax_deducted: number = 0;
  q3_amt_tax_deducted: number = 0;
  q4_amt_tax_deducted: number = 0;
  total_amt_tax_deposited: number = 0;
  q1_amt_tax_deposited: number = 0;
  q2_amt_tax_deposited: number = 0;
  q3_amt_tax_deposited: number = 0;
  q4_amt_tax_deposited: number = 0;

  section80C_i_value: number = 0;
  section80C_ii_value: number = 0;
  section80C_iii_value: number = 0;
  section80C_iv_value: number = 0;
  section80C_v_value: number = 0;
  section80C_vi_value: number = 0;
  section80C_vii_value: number = 0;
  section80C_vii_gross_total: number = 0;
  section80CCC_gross_total: number = 0;
  section80CCD_gross_total: number = 0;
  aggregate3sec_gross_total: number = 0;
  other_section1_deductable: number = 0;
  other_section2_deductable: number = 0;
  other_section3_deductable: number = 0;
  other_section4_deductable: number = 0;
  other_section5_deductable: number = 0;
  aggregate4Asec_deductible_total: number = 0;
  tds_total_income: number =0;

  constructor(private fb: FormBuilder, private ToastrService: ToastrService,
    private route: ActivatedRoute, private router: Router, private service: SocketService,
    public NgxSpinnerService: NgxSpinnerService) {
      this.personalinfoForm = new FormGroup({
        employee_gid: new FormControl(''),
        first_name: new FormControl(''),
        last_name: new FormControl(''),
        active_flag: new FormControl(''),
        dob: new FormControl(''),
        phone: new FormControl(''),
        email_id: new FormControl(''),
        blood_group: new FormControl(''),
        pan_number: new FormControl(''),
        uan_number: new FormControl(''),
        address_line1: new FormControl(''),
        address_line2: new FormControl(''),
        state: new FormControl(''),
        country: new FormControl(''),
        city: new FormControl(''),
        postal_code: new FormControl(''),
      });

      this.incometaxForm = new FormGroup({
        finyear_start : new FormControl(''),
        finyear_gid : new FormControl(''),
        document_type: new FormControl(''),
        finyear_range: new FormControl(''),
        document_title : new FormControl(''),
        remarks: new FormControl('') 
      })

      this.quartersinfoForm = new FormGroup({
        employee_gid: new FormControl(''),
        assessment_gid: new FormControl(''),
        q1_rpt_original_statement: new FormControl(''),
        q1_amt_paid_credited: new FormControl(''),
        q1_amt_tax_deducted: new FormControl(''),
        q1_amt_tax_deposited: new FormControl(''),
        q2_rpt_original_statement: new FormControl(''),
        q2_amt_paid_credited: new FormControl(''),
        q2_amt_tax_deducted: new FormControl(''),
        q2_amt_tax_deposited: new FormControl(''),
        q3_rpt_original_statement: new FormControl(''),
        q3_amt_paid_credited: new FormControl(''),
        q3_amt_tax_deducted: new FormControl(''),
        q3_amt_tax_deposited: new FormControl(''),
        q4_rpt_original_statement: new FormControl(''),
        q4_amt_paid_credited: new FormControl(''),
        q4_amt_tax_deducted: new FormControl(''),
        q4_amt_tax_deposited: new FormControl(''),
        total_amt_paid_credited: new FormControl(''),
        total_amt_tax_deducted: new FormControl(''),
        total_amt_tax_deposited: new FormControl(''),
      });

      this.tdsinfoForm = new FormGroup ({
        section80C_i_name:  new FormControl(''),
        section80C_ii_name: new FormControl(''),
        section80C_iii_name: new FormControl(''),
        section80C_iv_name: new FormControl(''),
        section80C_v_name: new FormControl(''),
        section80C_vi_name: new FormControl(''),
        section80C_vii_name: new FormControl(''),
        section80C_i_value: new FormControl(''),
        section80C_ii_value: new FormControl(''),
        section80C_iii_value: new FormControl(''),        
        section80C_iv_value: new FormControl(''),        
        section80C_v_value: new FormControl(''),        
        section80C_vi_value: new FormControl(''),        
        section80C_vii_value: new FormControl(''),
        section80C_vii_gross_total: new FormControl(''),
        section80C_vii_deductable_total: new FormControl(''),
        section80CCC_gross_total: new FormControl(''),
        section80CCC_deductable_total: new FormControl(''),
        section80CCD_gross_total: new FormControl(''),
        section80CCD_deductable_total: new FormControl(''),
        aggregate3sec_gross_total: new FormControl(''),
        aggregate3sec_deductable_total: new FormControl(''),
        other_section1_deductable: new FormControl(''),
        other_section2_deductable: new FormControl(''),
        other_section3_deductable: new FormControl(''),
        other_section4_deductable: new FormControl(''),
        other_section5_deductable: new FormControl(''),
        aggregate4Asec_deductible_total: new FormControl(''),
        tds_total_income: new FormControl(''),
      })

      this.incomeForm = new FormGroup({
        grosssalary_amount : new FormControl(''),
        perquisites_amount :  new FormControl(''),
        profitof_salary : new FormControl(''),
        totalamount : new FormControl(''),
        component1 : new FormControl(''),
        pfamount1 : new FormControl(''),
        component2 : new FormControl(''),
        pfamount2: new FormControl(''),
        component3 : new FormControl(''),
        pfamount3 : new FormControl(''),
        balanceamount : new FormControl(''),
        entertainment_allowance : new FormControl(''),
        taxon_emp : new FormControl(''),
        aggreagateofab : new FormControl(''),
        incomecharge_headsalaries : new FormControl(''),
        employee_income1 : new FormControl(''),
        employeeincome_rs1  : new FormControl(''),
        employee_income2 : new FormControl(''),
        employeeincome_rs2 : new FormControl(''),
        employee_income3 : new FormControl(''),
        employeeincome_rs3 : new FormControl(''),
        employeeincome_total : new FormControl(''),
        grosstotal_income : new FormControl(''),
        assessment_gid : new FormControl(''),
        employee_gid : new FormControl(''),
        lessallowancetotal :  new FormControl(''),
      })
 
  }

  ngOnInit() {
    const employee_gid = this.route.snapshot.paramMap.get('employee_gidassessment_gid');
    this.employee_gid = employee_gid;

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.employee_gid, secretKey).toString(enc.Utf8);   

    const [month,year] = deencryptedParam.split('+');
    this.assessment_gid = month;
    this.employee_gid = year;
    
    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    
    flatpickr('.date-picker', options);

    var api1 = 'PayMstAssessmentSummary/Getfinyeardropdown';
    this.service.get(api1).subscribe((result: any) => {
      this.finyear_list = result.Getfinyeardropdown;
 
    });
    
    this.GetPersonaldata();
    this.getincometaxsummary();
    this.GetQuatersdata();
    this.GetIncometaxinfo();
  }

  formatDate(dateString: string): string {
    const [year, month, day] = dateString.split('-');
    return `${day}-${month}-${year}`;
  }

  // onChange2(event: any) {
  //   this.file = event.target.files[0];
  // }

  GetPersonaldata() {
    var api = 'PayMstAssessmentSummary/GetPersonaldata';

    let param = {
      employee_gid: this.employee_gid
    }

    this.service.getparams(api, param).subscribe((result: any) => {
    
      this.responsedata = result;
      this.personalinfo = result;

      const dob = this.personalinfo.employee_dob;
      const formattedDob = this.formatDate(dob);

      this.personalinfoForm.get("first_name")?.setValue(this.personalinfo.user_firstname);
      this.personalinfoForm.get("last_name")?.setValue(this.personalinfo.user_lastname);
      this.personalinfoForm.get("dob")?.setValue(formattedDob);
      this.personalinfoForm.get("email_id")?.setValue(this.personalinfo.employee_emailid);
      this.personalinfoForm.get("active_flag")?.setValue(this.personalinfo.employee_gender);
      this.personalinfoForm.get("pan_number")?.setValue(this.personalinfo.pan_no);
      this.personalinfoForm.get("uan_number")?.setValue(this.personalinfo.uan_no);
      this.personalinfoForm.get("blood_group")?.setValue(this.personalinfo.bloodgroup);
      this.personalinfoForm.get("phone")?.setValue(this.personalinfo.employee_mobileno);      
      this.personalinfoForm.get("address_line1")?.setValue(this.personalinfo.address1);
      this.personalinfoForm.get("address_line2")?.setValue(this.personalinfo.address2);
      this.personalinfoForm.get("city")?.setValue(this.personalinfo.city);
      this.personalinfoForm.get("state")?.setValue(this.personalinfo.state);
      this.personalinfoForm.get("postal_code")?.setValue(this.personalinfo.postal_code);
      this.personalinfoForm.get("country")?.setValue(this.personalinfo.country_gid);
    });
  }

  updatepersonaldetails() {
    var api = 'PayMstAssessmentSummary/UpdatePersonalInfo';
    this.service.post(api, this.personalinfoForm.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
      }
      else {
        this.ToastrService.warning(result.message)
      }
    });
  }

  getincometaxsummary() {
    var api = 'PayMstAssessmentSummary/Getincometaxsummary';
      this.service.get(api).subscribe((result:any) => {
        $('#incometax').DataTable().destroy();
        this.incometax =result.incometaxsummary_lists;
        setTimeout(()=>{  
          $('#incometax').DataTable();
        }, 1);
      });
  }

  submit() {
    const api = 'PayMstAssessmentSummary/PostIncometax'
    this.service.post(api, this.incometaxForm.value).subscribe((result: any) => {
      this.responsedata = result;
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.router.navigate(['/payroll/PayMstAssessmentsummary']);
        this.ToastrService.success(result.message)
      }         
    });
  }

  submitquartersdetails() {
    var api = 'PayMstAssessmentSummary/PostQuartersInfo';
    this.quartersinfoForm.get("employee_gid")?.setValue(this.employee_gid);
    this.quartersinfoForm.get("assessment_gid")?.setValue(this.assessment_gid);
    
    this.service.post(api, this.quartersinfoForm.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
      }
      else {
        this.ToastrService.warning(result.message)
      }
    });
  }

  submitincomedetails() {
   
 
    var api = 'PayMstAssessmentSummary/PostIncome';
    this.incomeForm.get("employee_gid")?.setValue(this.employee_gid);
    this.incomeForm.get("assessment_gid")?.setValue(this.assessment_gid);
 
   
   
    this.service.post(api, this.incomeForm.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
      }
      else {
        this.ToastrService.warning(result.message)
      }
    });
  }

  GetIncometaxinfo() {
    debugger;
 
    var api = 'PayMstAssessmentSummary/GetIncomedata';
 
    let param = {
      employee_gid: this.employee_gid,
      assessment_gid: this.assessment_gid
    }
 
    this.service.getparams(api, param).subscribe((result: any) => {
   
      this.responsedata = result;
      this.incomeinfo = result;
 
      this.incomeForm.get("employee_gid")?.setValue(this.incomeinfo.employee_gid);
      this.incomeForm.get("assessment_gid")?.setValue(this.incomeinfo.assessment_gid);
      this.incomeForm.get("grosssalary_amount")?.setValue(this.incomeinfo.grosssalary_amount);
      this.incomeForm.get("perquisites_amount")?.setValue(this.incomeinfo.perquisites_amount);
      this.incomeForm.get("profitof_salary")?.setValue(this.incomeinfo.profitof_salary);
      this.incomeForm.get("totalamount")?.setValue(this.incomeinfo.totalamount);
      this.incomeForm.get("component1")?.setValue(this.incomeinfo.component1);
      this.incomeForm.get("pfamount1")?.setValue(this.incomeinfo.pfamount1);
      this.incomeForm.get("component2")?.setValue(this.incomeinfo.component2);
      this.incomeForm.get("pfamount2")?.setValue(this.incomeinfo.pfamount2);      
      this.incomeForm.get("component3")?.setValue(this.incomeinfo.component3);
      this.incomeForm.get("balanceamount")?.setValue(this.incomeinfo.balanceamount);
      this.incomeForm.get("taxon_emp")?.setValue(this.incomeinfo.taxon_emp);
      this.incomeForm.get("aggreagateofab")?.setValue(this.incomeinfo.aggreagateofab);
      this.incomeForm.get("incomecharge_headsalaries")?.setValue(this.incomeinfo.incomecharge_headsalaries);
      this.incomeForm.get("employee_income1")?.setValue(this.incomeinfo.employee_income1);
      this.incomeForm.get("employeeincome_rs1")?.setValue(this.incomeinfo.employeeincome_rs1);
      this.incomeForm.get("employee_income2")?.setValue(this.incomeinfo.employee_income2);
      this.incomeForm.get("employeeincome_rs2")?.setValue(this.incomeinfo.employeeincome_rs2);
      this.incomeForm.get("employee_income3")?.setValue(this.incomeinfo.employee_income3);
      this.incomeForm.get("employeeincome_rs3")?.setValue(this.incomeinfo.employeeincome_rs3);
      this.incomeForm.get("employeeincome_total")?.setValue(this.incomeinfo.employeeincome_total);
      this.incomeForm.get("grosstotal_income")?.setValue(this.incomeinfo.grosstotal_income);
      this.incomeForm.get("lessallowancetotal")?.setValue(this.incomeinfo.lessallowancetotal);
 
    });
  }

  grosssalarytotal(){
    this.totalamount = ((+this.grosssalary_amount) + (+this.perquisites_amount) + (+this.profitof_salary))
  }
   
  allowancesalarytotal(){
    this.lessallowancetotal = ((+this.pfamount1) + (+this.pfamount2)  + (+this.pfamount3))
  }
   
  balanceamounttotal(){
    this.balanceamount = this.totalamount - this.lessallowancetotal
  }
   
  aggreagateof_ab(){
    this.aggreagateofab = ((+this.entertainment_allowance) + (+this.taxon_emp))
  }
   
  incomechargeabletotal(){
    this.incomecharge_headsalaries = this.balanceamount - this.aggreagateofab
  }
   
  otherincometotal(){
    this.employeeincome_total = ((+this.employeeincome_rs1) + (+this.employeeincome_rs2) + (+this.employeeincome_rs3))
  }
   
  grosstotal(){
    this.grosstotal_income = this.incomecharge_headsalaries - this.employeeincome_total
  }

  amountpaidcalc() {
    this.total_amt_paid_credited = ((+this.q1_amt_paid_credited) + (+this.q2_amt_paid_credited) + (+this.q3_amt_paid_credited) + (+this.q4_amt_paid_credited))
  }

  amounttaxdeductioncalc() {
    this.total_amt_tax_deducted = ((+this.q1_amt_tax_deducted) + (+this.q2_amt_tax_deducted) + (+this.q3_amt_tax_deducted) + (+this.q4_amt_tax_deducted))
  }

  amounttaxdepositcalc() {
    this.total_amt_tax_deposited = ((+this.q1_amt_tax_deposited) + (+this.q2_amt_tax_deposited) + (+this.q3_amt_tax_deposited) + (+this.q4_amt_tax_deposited))
  }

  GetQuatersdata() {
    var api = 'PayMstAssessmentSummary/GetQuatersdata';

    let param = {
      employee_gid: this.employee_gid
    }

    this.service.getparams(api, param).subscribe((result: any) => {    
      this.responsedata = result;
      this.quartersinfo = result;
      this.quartersinfoForm.get("q1_rpt_original_statement")?.setValue(this.quartersinfo.tdsquarter1_receiptno);
      this.quartersinfoForm.get("q1_amt_paid_credited")?.setValue(this.quartersinfo.tdsquarter1_paidcredited);
      this.quartersinfoForm.get("q1_amt_tax_deducted")?.setValue(this.quartersinfo.tdsquarter1_amount_deducted);
      this.quartersinfoForm.get("q1_amt_tax_deposited")?.setValue(this.quartersinfo.tdsquarter1_amount_deposited);
      this.quartersinfoForm.get("q2_rpt_original_statement")?.setValue(this.quartersinfo.tdsquarter2_receiptno);
      this.quartersinfoForm.get("q2_amt_paid_credited")?.setValue(this.quartersinfo.tdsquarter2_paidcredited);
      this.quartersinfoForm.get("q2_amt_tax_deducted")?.setValue(this.quartersinfo.tdsquarter2_amount_deducted);
      this.quartersinfoForm.get("q2_amt_tax_deposited")?.setValue(this.quartersinfo.tdsquarter2_amount_deposited);      
      this.quartersinfoForm.get("q3_rpt_original_statement")?.setValue(this.quartersinfo.tdsquarter3_receiptno);
      this.quartersinfoForm.get("q3_amt_paid_credited")?.setValue(this.quartersinfo.tdsquarter3_paidcredited);
      this.quartersinfoForm.get("q3_amt_tax_deducted")?.setValue(this.quartersinfo.tdsquarter3_amount_deducted);
      this.quartersinfoForm.get("q3_amt_tax_deposited")?.setValue(this.quartersinfo.tdsquarter3_amount_deposited);
      this.quartersinfoForm.get("q4_rpt_original_statement")?.setValue(this.quartersinfo.tdsquarter4_receiptno);
      this.quartersinfoForm.get("q4_amt_paid_credited")?.setValue(this.quartersinfo.tdsquarter4_paidcredited);
      this.quartersinfoForm.get("q4_amt_tax_deducted")?.setValue(this.quartersinfo.tdsquarter4_amount_deducted);
      this.quartersinfoForm.get("q4_amt_tax_deposited")?.setValue(this.quartersinfo.tdsquarter4_amount_deposited);
      this.quartersinfoForm.get("total_amt_paid_credited")?.setValue(this.quartersinfo.totalamount_paidcredited);
      this.quartersinfoForm.get("total_amt_tax_deducted")?.setValue(this.quartersinfo.tdsquarter_totalamount_deducted);
      this.quartersinfoForm.get("total_amt_tax_deposited")?.setValue(this.quartersinfo.tdsquarter_totalamount_deposited);
    });
  }

  section80cgrosscalc() {
    this.section80C_vii_gross_total = ((+this.section80C_i_value) + (+this.section80C_ii_value) + (+this.section80C_iii_value) + (+this.section80C_iv_value) + (+this.section80C_v_value) + (+this.section80C_vi_value) + (+this.section80C_vii_value))
  }

  aggregate3sectionsgrosscalc() {
    this.aggregate3sec_gross_total = (+this.section80C_vii_gross_total) + (+this.section80CCC_gross_total) + (+this.section80CCD_gross_total);
  }

  aggregate4Asectiondeductiblescalc() {
    this.aggregate4Asec_deductible_total = (+this.other_section1_deductable) + (+this.other_section2_deductable) + (+this.other_section3_deductable) + (+this.other_section4_deductable) + (+this.other_section5_deductable);
  }

  back() {
    this.router.navigate(['/payroll/PayMstAssessmentsummary'])
  }
}
