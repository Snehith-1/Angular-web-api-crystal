import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';


interface IProfileManagement {

  // Personal Details
  first_name: string;  
  last_name: string;  
  gender: string;  
  dateof_birth: string;  
  mobile: string;  
  personal_no: string;  
  qualification: string;  
  blood_group: string;  
  experience: string;

  firstedit_name: string;
  lastedit_name: string;
  gender_edit: string;
  martial_edit: string;
  dateof_birthedit: string;
  mobile_edit: string;
  personal_noedit: string;
  qualification_edit: string;
  blood_groupedit: string;
  experience_edit: string;
  company_edit: string;
  employee_dateedit: string;
 
  //personal details


  // Change Password
  curr_pwd: string;  
  new_pwd: string;  
  conf_pwd: string; 

  curredit_pwd: string;
  newedit_pwd: string;
  confedit_pwd: string;
  
  // Work Experience
  empl_prevcomp: string;
  empl_code: string;
  prev_occp: string;
  department: string;
  date_ofjoining: string;
  date_ofreleiving: string;
  work_period: string;
  HR_name: string;
  reason: string;
  report: string;
  rmrks: string;

  // Nomination
  name: string;
  dateofbirth: string;
  age: string;
  mobile_no: string;
  relt_employee: string;
  resign_employee: string;
  residing_addr: string;
  nominee_for: string;

  // Statutory
  provident_no: string;
  date_ofjoinPF: string;
  employee_no: string;

  // Emergency Contact
  contact_person: string;
  cont_addr: string;
  cont_no: string;
  cont_emailid: string;
  remarks: string;

  // Dependent
  name_user: string;
  relationship: string;
  date_ofbirth: string;

  // Education
  inst_name: string;
  deg_dip: string;
  field_ofstudy: string;
  date_ofcompletion: string;
  addn_notes: string;
}

@Component({
  selector: 'app-hrm-trn-profile',
  templateUrl: './hrm-trn-profile.component.html',
  styleUrls: ['./hrm-trn-profile.component.scss']
})

export class HrmTrnProfileComponent {
  blood_groupedit: any;
  file!: File;
  
  reactiveForm1!: FormGroup;
  reactiveForm2!: FormGroup;
  reactiveForm3!: FormGroup;
  reactiveForm4!: FormGroup;
  reactiveForm5!: FormGroup;
  reactiveForm6!: FormGroup;

  reactiveForm8Edit!: FormGroup;
  reactiveForm7Edit!: FormGroup;

  profilemanagement!: IProfileManagement;
 
  emp_name: any;
  emp_code: any;
  emp_mobilenumber: any;
  emp_department: any;
  emp_designation: any;
  emp_branch: any;
  emp_Gender: any;
  emp_Joiningdate: any;
  emp_email: any;
  emp_address: any;
  relationshipwith_employee_list: any;
  policies_list: any[] = [];
  // bloodgroup_list: any;
  nominationdtllist: any;
  emergencylist: any;
  dependentdtllist: any;
  educationdtllist: any;
  employeename_list: any;
  selectedBranch: any;
  selectedEmployee: any;
  employeeedit_list: any;

  responsedata: any;
  parameterValue1: any;
  parameterValue2: any;
  formdata = { }

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private route: ActivatedRoute, private router: Router,) {
    this.profilemanagement = {} as IProfileManagement;

    // Personal Details
    this.reactiveForm7Edit = new FormGroup({
      firstedit_name: new FormControl(this.profilemanagement.firstedit_name, [Validators.required,]),
      lastedit_name: new FormControl(this.profilemanagement.lastedit_name, []),
      dateof_birthedit: new FormControl(this.profilemanagement.dateof_birthedit, []),
      gender_edit: new FormControl(this.profilemanagement.gender_edit, []),
      martial_edit: new FormControl(this.profilemanagement.martial_edit, []),
      mobile_edit: new FormControl(this.profilemanagement.mobile_edit, [Validators.required, Validators.pattern('[0-9]{10}$'), Validators.maxLength(10)]),
      personal_noedit: new FormControl(this.profilemanagement.personal_noedit, [Validators.required, Validators.pattern('[0-9]{10}$'), Validators.maxLength(10)]),
      blood_groupedit: new FormControl(this.profilemanagement.blood_groupedit, []),
      qualification_edit: new FormControl(this.profilemanagement.qualification_edit, []),
      experience_edit: new FormControl(this.profilemanagement.experience_edit, []),
      company_edit: new FormControl(this.profilemanagement.company_edit, []),
      employee_dateedit: new FormControl(this.profilemanagement.employee_dateedit, []),
      
      
    });

    // Change Password
    this.reactiveForm8Edit = new FormGroup({
      curredit_pwd: new FormControl(this.profilemanagement.curredit_pwd, [Validators.required,]),
      newedit_pwd: new FormControl(this.profilemanagement.newedit_pwd, [Validators.required,]),
      confedit_pwd: new FormControl(this.profilemanagement.confedit_pwd, [Validators.required,]),
    });

    // Work Experience
    this.reactiveForm6 = new FormGroup({
      empl_prevcomp: new FormControl(this.profilemanagement.empl_prevcomp, [Validators.required,]),
      empl_code: new FormControl(this.profilemanagement.empl_code, [Validators.required,]),
      prev_occp: new FormControl(this.profilemanagement.prev_occp, [Validators.required,]),
      department: new FormControl(this.profilemanagement.department, []),
      date_ofjoining: new FormControl(this.profilemanagement.date_ofjoining, [Validators.required,]),
      date_ofreleiving: new FormControl(this.profilemanagement.date_ofreleiving, []),
      work_period: new FormControl(this.profilemanagement.work_period, [Validators.required,]),
      HR_name: new FormControl(this.profilemanagement.HR_name, []),
      reason: new FormControl(this.profilemanagement.reason, [Validators.required,]),
      report: new FormControl(this.profilemanagement.report, []),
      rmrks: new FormControl(this.profilemanagement.rmrks, [Validators.required,]),
    });

    // Nomination
    this.reactiveForm5 = new FormGroup({
      name: new FormControl(this.profilemanagement.name, [Validators.required,]),
      dateofbirth: new FormControl(this.profilemanagement.dateofbirth, [Validators.required,]),
      age: new FormControl(this.profilemanagement.age, [Validators.required,]),
      mobile_no: new FormControl(this.profilemanagement.mobile_no, [Validators.pattern('[0-9]{10}$'), Validators.maxLength(10)]),
      relt_employee: new FormControl(this.profilemanagement.relt_employee, [Validators.required,]),
      resign_employee: new FormControl(this.profilemanagement.resign_employee, []),
      residing_addr: new FormControl(this.profilemanagement.residing_addr, []),
      nominee_for: new FormControl(this.profilemanagement.nominee_for, [Validators.required,]),
    });

    // Statutory
    this.reactiveForm1 = new FormGroup({
      provident_no: new FormControl(this.profilemanagement.provident_no, [Validators.required,]),
      date_ofjoinPF: new FormControl(this.profilemanagement.date_ofjoinPF, [Validators.required,]),
      employee_no: new FormControl(this.profilemanagement.employee_no, [Validators.required,]),
    });

    // Emergency Contact
    this.reactiveForm3 = new FormGroup({
      contact_person: new FormControl(this.profilemanagement.contact_person, [Validators.required,]),
      cont_addr: new FormControl(this.profilemanagement.cont_addr, [Validators.required,]),
      cont_no: new FormControl(this.profilemanagement.cont_no, [Validators.required, Validators.pattern('[0-9]{10}$'), Validators.maxLength(10)]),
      cont_emailid: new FormControl(this.profilemanagement.cont_emailid, [Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]),
      remarks: new FormControl(this.profilemanagement.remarks, []),
    });

    // Dependent
    this.reactiveForm2 = new FormGroup({
      name_user: new FormControl(this.profilemanagement.name_user, [Validators.required,]),
      relationship: new FormControl(this.profilemanagement.relationship, [Validators.required,]),
      date_ofbirth: new FormControl(this.profilemanagement.date_ofbirth, [Validators.required,]),
    });

    // Education
    this.reactiveForm4 = new FormGroup({
      inst_name: new FormControl(this.profilemanagement.inst_name, [Validators.required,]),
      deg_dip: new FormControl(this.profilemanagement.deg_dip, [Validators.required,]),
      field_ofstudy: new FormControl(this.profilemanagement.field_ofstudy, [Validators.required,]),
      date_ofcompletion: new FormControl(this.profilemanagement.date_ofcompletion, [Validators.required,]),
      addn_notes: new FormControl(this.profilemanagement.addn_notes, []),
    });
  }

  back() {
    this.router.navigate(['/hrm/HrmMemberDashboard'])
  }

  // Personal Details
  get firstedit_name() {
    return this.reactiveForm7Edit.get('firstedit_name')!;
  }
  get lastedit_name() {
    return this.reactiveForm7Edit.get('lastedit_name')!;
  }
  get dateof_birthedit() {
    return this.reactiveForm7Edit.get('dateof_birthedit')!;
  }
  get gender_edit() {
    return this.reactiveForm7Edit.get('gender_edit')!;
  }
  get martial_edit() {
    return this.reactiveForm7Edit.get('martial_edit')!;
  }
  
  get mobile_edit() {
    return this.reactiveForm7Edit.get('mobile_edit')!;
  }
  get personal_noedit() {
    return this.reactiveForm7Edit.get('personal_noedit')!;
  }
 
  //  get blood_groupedit() {
  //    return this.reactiveForm7Edit.get('blood_groupedit')!;
  //  }
  get qualification_edit() {
    return this.reactiveForm7Edit.get('qualification_edit')!;
  }
  get experience_edit() {
    return this.reactiveForm7Edit.get('experience_edit')!;
  }
  get company_edit() {
    return this.reactiveForm7Edit.get('company_edit')!;
  }
  get employee_dateedit() {
    return this.reactiveForm7Edit.get('employee_dateedit')!;
  }
  
 

  ngOnInit(): void {
    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    flatpickr('.date-picker', options);  

    var url = 'HrmTrnProfileManagement/GetNominationSummary'
    this.service.get(url).subscribe((result: any) => {

      this.responsedata = result;
      this.nominationdtllist = this.responsedata.nominationlist;
    });

    var url = 'HrmTrnProfileManagement/employeeList'
    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
      this.employeename_list = this.responsedata.employeenamelist;
      this.emp_name = this.employeename_list[0].Name;
      this.emp_code = this.employeename_list[0].UserCode;
      this.emp_mobilenumber = this.employeename_list[0].employeemobileNo;
      this.emp_department = this.employeename_list[0].Department;
      this.emp_designation = this.employeename_list[0].Designation;
      this.emp_branch = this.employeename_list[0].Branch;
      this.emp_address = this.employeename_list[0].Address;
      this.emp_Gender = this.employeename_list[0].Gender;
      this.emp_Joiningdate = this.employeename_list[0].Joiningdate;
      this.emp_email = this.employeename_list[0].email;
    });

    var url = 'HrmTrnProfileManagement/GetEmergencyContactSummary'
    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
      this.emergencylist = this.responsedata.emergencycontactlist;
    });

    var url = 'HrmTrnProfileManagement/GetDependentSummary'
    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
      this.dependentdtllist = this.responsedata.dependentlist;
    });
   

    var url = 'HrmTrnProfileManagement/GetEducationSummary'
    this.service.get(url).subscribe((result: any) => {
      this.responsedata = result;
      this.educationdtllist = this.responsedata.educationlist;
    });

    var url = 'HrmTrnProfileManagement/GetCompanyPolicies';
    this.service.get(url).subscribe((result: any) => {
      this.policies_list = result.CompanyPolicy;
    });

    // var url = 'HrmTrnProfileManagement/GetBloodGroup';
    // this.service.get(url).subscribe((result: any) => {
    //   this.bloodgroup_list = result.bloodgroup_list;
    // });

    var url = 'HrmTrnProfileManagement/Getrelationshipwithemployee';
    this.service.get(url).subscribe((result: any) => {
      this.relationshipwith_employee_list = result.relationshipwith_employee_list;
    });
     this.GetEditEmployee();
  }

  preprocessPolicyDesc(policyDesc: string): string {
    // Replace the tab character with a line break
    return policyDesc.replace(/\t/g, '<br>');
  }

  GetEditEmployee() {

    var url = 'HrmTrnProfileManagement/GetEditEmployee'
   
    this.service.get(url).subscribe((result: any) => {
    this.responsedata=result;
      this.employeeedit_list = result.GetEditEmployee;
      console.log(this.employeeedit_list)
      console.log(this.employeeedit_list[0].branch_gid)
      debugger;
      this.selectedBranch = this.employeeedit_list[0].branch_gid;
      this.selectedEmployee = this.employeeedit_list[0].employee_gid;
      this.reactiveForm7Edit.get("firstedit_name")?.setValue(this.employeeedit_list[0].user_firstname);
      this.reactiveForm7Edit.get("lastedit_name")?.setValue(this.employeeedit_list[0].user_lastname);
      this.reactiveForm7Edit.get("dateof_birthedit")?.setValue(this.employeeedit_list[0].employee_dob);
      this.reactiveForm7Edit.get("blood_groupedit")?.setValue(this.employeeedit_list[0].bloodgroup_name);
      this.reactiveForm7Edit.get("gender_edit")?.setValue(this.employeeedit_list[0].employee_gender);
      this.reactiveForm7Edit.get("martial_edit")?.setValue(this.employeeedit_list[0].marital_status);
      this.reactiveForm7Edit.get("mobile_edit")?.setValue(this.employeeedit_list[0].employee_mobileno);
      this.reactiveForm7Edit.get("personal_noedit")?.setValue(this.employeeedit_list[0].employee_personalno);
      this.reactiveForm7Edit.get("qualification_edit")?.setValue(this.employeeedit_list[0].employee_qualification);
      this.reactiveForm7Edit.get("experience_edit")?.setValue(this.employeeedit_list[0].employee_experience);
      this.reactiveForm7Edit.get("company_edit")?.setValue(this.employeeedit_list[0].employee_emailid);
      this.reactiveForm7Edit.get("employee_dateedit")?.setValue(this.employeeedit_list[0].employee_joingdate);
      
      this.reactiveForm7Edit.get("employee_gid")?.setValue(this.employeeedit_list[0].employee_gid);
    
     
    });
  }

  public detailsupdate(): void {
    debugger;
    let param= {
      firstedit_name: this.reactiveForm7Edit.value.firstedit_name,
      lastedit_name: this.reactiveForm7Edit.value.lastedit_name,
      gender_edit: this.reactiveForm7Edit.value.gender_edit,
      martial_edit: this.reactiveForm7Edit.value.martial_edit,
      dateof_birthedit: this.reactiveForm7Edit.value.dateof_birthedit,
      blood_groupedit: this.reactiveForm7Edit.value.blood_groupedit,
       mobile_edit: this.reactiveForm7Edit.value.mobile_edit,
       personal_noedit: this.reactiveForm7Edit.value.personal_noedit,
       qualification_edit: this.reactiveForm7Edit.value.qualification_edit,
       experience_edit: this.reactiveForm7Edit.value.experience_edit,
       company_edit: this.reactiveForm7Edit.value.company_edit,
       employee_dateedit: this.reactiveForm7Edit.value.employee_dateedit,
       
    }
      
      
      var url22 = 'HrmTrnProfileManagement/UpdatePersonalDetails'
      this.service.postparams(url22, param).pipe().subscribe((result: { status: boolean; message: string | undefined; }) => {
        this.responsedata = result;
        if (result.status == false) {
          this.ToastrService.warning(result.message)
        }
        else {
          this.ToastrService.success(result.message)
        }
        window.location.reload();
      });
      
    }
    
  
  

  // Change Password
  get curredit_pwd() {
    return this.reactiveForm8Edit.get('curredit_pwd')!;
  }
  get newedit_pwd() {
    return this.reactiveForm8Edit.get('newedit_pwd')!;
  }
  get confedit_pwd() {
    return this.reactiveForm8Edit.get('confedit_pwd')!;
  }

  openModaledit(parameter: string) {
    this.parameterValue1 = parameter
    this.reactiveForm8Edit.get("curredit_pwd")?.setValue(this.parameterValue1.curr_pwd);
    this.reactiveForm8Edit.get("newedit_pwd")?.setValue(this.parameterValue1.new_pwd);
    this.reactiveForm8Edit.get("confedit_pwd")?.setValue(this.parameterValue1.conf_pwd);
  }

  public passwordupdate(): void {
    debugger;
     
      
      
      var url21 = 'HrmTrnProfileManagement/UpdatePassword'
      this.service.postparams(url21, this.reactiveForm8Edit.value).pipe().subscribe((result: { status: boolean; message: string | undefined; }) => {
        this.responsedata = result;
        if (result.status == false) {
          this.ToastrService.warning(result.message)
        }
        else {
          this.ToastrService.success(result.message)
        }
        window.location.reload();
      });
   
  }

  // Work Experience
  get empl_prevcomp() {
    return this.reactiveForm6.get('empl_prevcomp')!;
  }
  get empl_code() {
    return this.reactiveForm6.get('empl_code')!;
  }
  get prev_occp() {
    return this.reactiveForm6.get('prev_occp')!;
  }
  get department() {
    return this.reactiveForm6.get('department')!;
  }
  get date_ofjoining() {
    return this.reactiveForm6.get('date_ofjoining')!;
  }
  get date_ofreleiving() {
    return this.reactiveForm6.get('date_ofreleiving')!;
  }
  get work_period() {
    return this.reactiveForm6.get('work_period')!;
  }
  get HR_name() {
    return this.reactiveForm6.get('HR_name')!;
  }
  get reason() {
    return this.reactiveForm6.get('reason')!;
  }
  get report() {
    return this.reactiveForm6.get('report')!;
  }
  get rmrks() {
    return this.reactiveForm6.get('rmrks')!;
  }

  public workexperiencesubmit(): void {
    if (this.reactiveForm6.value.empl_prevcomp != null && this.reactiveForm6.value.empl_prevcomp != '') {
      for (const control of Object.keys(this.reactiveForm6.controls)) {
        this.reactiveForm6.controls[control].markAsTouched();
      }
      this.reactiveForm6.value;
      var url15 = 'HrmTrnProfileManagement/WorkExperienceSubmit'
      this.service.post(url15, this.reactiveForm6.value).subscribe((result: any) => {
        if (result.status == false) {
        this.ToastrService.warning(result.message)
        }
        else {
          // this.reactiveForm6.get("empl_prevcomp")?.setValue(null);
          // this.reactiveForm6.get("empl_code")?.setValue(null);
          // this.reactiveForm6.get("prev_occp")?.setValue(null);
          // this.reactiveForm6.get("department")?.setValue(null);
          // this.reactiveForm6.get("date_ofjoining")?.setValue(null);
          // this.reactiveForm6.get("date_ofreleiving")?.setValue(null);
          // this.reactiveForm6.get("work_period")?.setValue(null);
          // this.reactiveForm6.get("HR_name")?.setValue(null);
          // this.reactiveForm6.get("reason")?.setValue(null);
          // this.reactiveForm6.get("report")?.setValue(null);
          // this.reactiveForm6.get("rmrks")?.setValue(null);
          this.ToastrService.success(result.message)
        }
        
      });
    }
   window.location.reload();
  }

  // Nomination
  get name() {
    return this.reactiveForm5.get('name')!;
  }
  get dateofbirth() {
    return this.reactiveForm5.get('dateofbirth')!;
  }
  get age() {
    return this.reactiveForm5.get('age')!;
  }
  get mobile_no() {
    return this.reactiveForm5.get('mobile_no')!;
  }
  get relt_employee() {
    return this.reactiveForm5.get('relt_employee')!;
  }
  get resign_employee() {
    return this.reactiveForm5.get('resign_employee')!;
  }
  get residing_addr() {
    return this.reactiveForm5.get('residing_addr')!;
  }
  get nominee_for() {
    return this.reactiveForm5.get('nominee_for')!;
  }

  public nominationsubmit(): void {
    if (this.reactiveForm5.value.name != null && this.reactiveForm5.value.name != '') {
      for (const control of Object.keys(this.reactiveForm5.controls)) {
        this.reactiveForm5.controls[control].markAsTouched();
      }
      this.reactiveForm5.value;
      var url14 = 'HrmTrnProfileManagement/NominationSubmit'
      this.service.post(url14, this.reactiveForm5.value).subscribe((result: any) => {
        if (result.status == false) {
          this.ToastrService.warning('Error Occured while Adding Nomination')
        }
        else {
          // this.reactiveForm5.get("name")?.setValue(null);
          // this.reactiveForm5.get("dateofbirth")?.setValue(null);
          // this.reactiveForm5.get("age")?.setValue(null);
          // this.reactiveForm5.get("mobile_no")?.setValue(null);
          // this.reactiveForm5.get("relt_employee")?.setValue(null);
          // this.reactiveForm5.get("resign_employee")?.setValue(null);
          // this.reactiveForm5.get("residing_addr")?.setValue(null);
          // this.reactiveForm5.get("nominee_for")?.setValue(null);
          this.ToastrService.success('Nomination Added Successfully')
        }
      });
    }
    window.location.reload();
  }

  // Statutory
  get provident_no() {
    return this.reactiveForm1.get('provident_no')!;
  }
  get date_ofjoinPF() {
    return this.reactiveForm1.get('date_ofjoinPF')!;
  }
  get employee_no() {
    return this.reactiveForm1.get('employee_no')!;
  }

  public statutorysubmit(): void {
    if (this.reactiveForm1.value.provident_no != null && this.reactiveForm1.value.provident_no != '') {
      for (const control of Object.keys(this.reactiveForm1.controls)) {
        this.reactiveForm1.controls[control].markAsTouched();
      }
      this.reactiveForm1.value;
      var url10 = 'HrmTrnProfileManagement/StatutorySubmit'
      this.service.post(url10, this.reactiveForm1.value).subscribe((result: any) => {
        if (result.status == false) {
          this.ToastrService.warning('Error Occured while Adding Statutory')

        }
        else {
          // this.reactiveForm1.get("provident_no")?.setValue(null);
          // this.reactiveForm1.get("date_ofjoinPF")?.setValue(null);
          // this.reactiveForm1.get("employee_no")?.setValue(null);
          this.ToastrService.success('Statutory Added Successfully')
         
        }
      });
      
    }  
    window.location.reload();
  }

 
  


  // Emergency Contact
  get contact_person() {
    return this.reactiveForm3.get('contact_person')!;
  }
  get cont_addr() {
    return this.reactiveForm3.get('cont_addr')!;
  }
  get cont_no() {
    return this.reactiveForm3.get('cont_no')!;
  }
  get cont_emailid() {
    return this.reactiveForm3.get('cont_emailid')!;
  }
  get remarks() {
    return this.reactiveForm3.get('remarks')!;
  }

  public emergencysubmit(): void {
   
    if (this.reactiveForm3.value.contact_person != null && this.reactiveForm3.value.contact_person != '') {
      for (const control of Object.keys(this.reactiveForm3.controls)) {
        this.reactiveForm3.controls[control].markAsTouched();
      }
      this.reactiveForm3.value;
      var url12 = 'HrmTrnProfileManagement/EmergencySubmit'
      this.service.post(url12, this.reactiveForm3.value).subscribe((result: any) => {
        
        if (result.status == false) {
        this.ToastrService.warning('Error Occurred While Adding Emergency Contact')
        }
        else {
          // this.reactiveForm3.get("contact_person")?.setValue(null);
          // this.reactiveForm3.get("cont_addr")?.setValue(null);
          // this.reactiveForm3.get("cont_no")?.setValue(null);
          // this.reactiveForm3.get("cont_emailid")?.setValue(null);
          // this.reactiveForm3.get("remarks")?.setValue(null);
          this.ToastrService.success('Emergency Contact Added Successfully')
        }
      });
    }
    window.location.reload();
  }

  // Dependent
  get name_user() {
    return this.reactiveForm2.get('name_user')!;
  }
  get relationship() {
    return this.reactiveForm2.get('relationship')!;
  }
  get date_ofbirth() {
    return this.reactiveForm2.get('date_ofbirth')!;
  }

  public dependentsubmit(): void {
    if (this.reactiveForm2.value.name_user != null && this.reactiveForm2.value.name_user != '') {
      for (const control of Object.keys(this.reactiveForm2.controls)) {
        this.reactiveForm2.controls[control].markAsTouched();
      }
      this.reactiveForm2.value;
      var url11 = 'HrmTrnProfileManagement/DependentSubmit'
      this.service.post(url11, this.reactiveForm2.value).subscribe((result: any) => {
        if (result.status == false) {
          this.ToastrService.warning('Error Occured While Adding Dependent')
        }
        else {
          // this.reactiveForm2.get("name_user")?.setValue(null);
          // this.reactiveForm2.get("relationship")?.setValue(null);
          // this.reactiveForm2.get("date_ofbirth")?.setValue(null);
          this.ToastrService.success('Dependent Added Successfully')
        }
      });
    }
    window.location.reload();
  }

  // Education
  get inst_name() {
    return this.reactiveForm4.get('inst_name')!;
  }
  get deg_dip() {
    return this.reactiveForm4.get('deg_dip')!;
  }
  get field_ofstudy() {
    return this.reactiveForm4.get('field_ofstudy')!;
  }
  get date_ofcompletion() {
    return this.reactiveForm4.get('date_ofcompletion')!;
  }
  get addn_notes() {
    return this.reactiveForm4.get('addn_notes')!;
  }

  public educationsubmit(): void {
    if (this.reactiveForm4.value.inst_name != null && this.reactiveForm4.value.inst_name != '') {
      for (const control of Object.keys(this.reactiveForm4.controls)) {
        this.reactiveForm4.controls[control].markAsTouched();
      }
      this.reactiveForm4.value;
      var url13 = 'HrmTrnProfileManagement/EducationSubmit'
      this.service.post(url13, this.reactiveForm4.value).subscribe((result: any) => {
        if (result.status == false) {
          this.ToastrService.warning('Error Occured While Adding Education')
        }
        else {
          // this.reactiveForm4.get("inst_name")?.setValue(null);
          // this.reactiveForm4.get("deg_dip")?.setValue(null);
          // this.reactiveForm4.get("field_ofstudy")?.setValue(null);
          // this.reactiveForm4.get("date_ofcompletion")?.setValue(null);
          // this.reactiveForm4.get("addn_notes")?.setValue(null);
          this.ToastrService.success('Education Added Successfully')
        }
      });
    }
    window.location.reload();
  }
}
