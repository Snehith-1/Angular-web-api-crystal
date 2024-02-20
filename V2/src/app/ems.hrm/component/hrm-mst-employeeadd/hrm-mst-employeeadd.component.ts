import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';

interface IEmployee {
  entityname: string;
  branchname: string;
  departmentname: string;
  workertype: string;
  role: string;
  jobtype: string;
  designationname: string;
  empjoiningdate: Date;
  active_flag: string;
  user_code: string;
  biometricid: string;
  password: string;
  confirmpassword: string;
  reportingto: string;
  shift: string;
  leavegrade: string;
  tagid: string;
  hide_flag: string;
  holidaygrade: string;

  first_name: string;
  last_name: string;
  gender: string;
  dob: Date;
  age: string;
  aadhar_no: string;
  father_spouse: string;
  mobile: string;
  mobileno: string;
  email: string;
  comp_email: string;
  bloodgroup: string;
  qualification: string;
  differentlyabled: string;
  remarks: string;

  permanent_address1: string;
  permanent_address2: string;
  permanent_city: string;
  permanent_postal: string;
  permanent_state: string;
  country: string;

  temporary_address1: string;
  temporary_address2: string;
  temporary_city: string;
  temporary_postal: string;
  temporary_state: string;
  countryname: string;
  user_password: string;
  showPassword: boolean;
}

@Component({
  selector: 'app-hrm-mst-employeeadd',
  templateUrl: './hrm-mst-employeeadd.component.html',
  styleUrls: ['./hrm-mst-employeeadd.component.scss']
})

export class HrmMstEmployeeaddComponent implements OnInit {
  file!: File;
  employee!: IEmployee;
  reactiveForm!: FormGroup;
  entity_list: any;
  branch_list: any[] = [];
  department_list: any[] = [];
  designation_list: any[] = [];
  country_list: any[] = [];
  country_list2: any[] = [];
  bloodgroup_list:any[]=[];

  selectedEmpPassword: any;

  entityList: any;
  branchList: any;
  departmentList: any[] = []
  workertype_list: any[] = []
  role_list: any[] = []
  jobtype_list: any[] = []
  shift_list: any[] = []
  leavegrade_list: any[] = []
  holidaygrade_list: any[] = []
  employeereportingto_list: any[] = []

  Entity: any;
  Emp_Code: any;
  Emp_Join_date: any;
  selectedEmpAccess: string = 'Yes';
  Email_Address: any;

  responsedata: any;

  formdata = { selectedEmpPassword: "", Confrim_Emp_password: "", Emp_Code: "", Emp_Join_date: "", Emp_mobile_number: "", Email_Address: "" }

  constructor(private renderer: Renderer2, private el: ElementRef, public service: SocketService, private ToastrService: ToastrService, private route: Router) {
    this.employee = {} as IEmployee;
  }

  ngOnInit(): void {

    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    
    flatpickr('.date-picker', options);

    this.reactiveForm = new FormGroup({
      entityname: new FormControl(this.employee.entityname, [Validators.required, Validators.minLength(1), Validators.maxLength(250)]),
      branchname: new FormControl(this.employee.branchname, [Validators.required, Validators.minLength(1), Validators.maxLength(250)]),
      departmentname: new FormControl(this.employee.departmentname, [Validators.required, Validators.minLength(1)]),
      workertype: new FormControl(this.employee.workertype, [Validators.required]),
      role: new FormControl(this.employee.role, [Validators.required]),
      jobtype: new FormControl(this.employee.jobtype, [Validators.required]),
      designationname: new FormControl(this.employee.designationname, [Validators.required, Validators.minLength(1)]),
      empjoiningdate: new FormControl(this.employee.role, [Validators.required]),
      active_flag: new FormControl('Y'),
      user_code: new FormControl(this.employee.user_code, [Validators.required]),
      biometricid: new FormControl(''),
      password: new FormControl(this.employee.password, [Validators.required, Validators.minLength(1)]),
      confirmpassword: new FormControl({ value: '', disabled: false }),
      reportingto: new FormControl(this.employee.reportingto),
      shift: new FormControl(this.employee.shift, [Validators.required]),
      leavegrade: new FormControl(this.employee.leavegrade),
      tagid: new FormControl(this.employee.tagid),
      hide_flag: new FormControl('Y'),
      file: new FormControl(''),
      holidaygrade: new FormControl(this.employee.holidaygrade),

      first_name: new FormControl(this.employee.first_name, [Validators.required]),
      last_name: new FormControl(''),
      gender: new FormControl('male'),
      dob: new FormControl(this.employee.dob, [Validators.required]),
      age: new FormControl(''),
      aadhar_no: new FormControl(''),
      father_spouse: new FormControl(''),
      mobile: new FormControl(this.employee.mobile, [Validators.required, Validators.maxLength(10)]),
      mobileno: new FormControl(this.employee.mobileno, [Validators.required, Validators.maxLength(10)]),
      email: new FormControl(this.employee.email, [Validators.required, Validators.minLength(1), Validators.maxLength(250), Validators.pattern('^([a-z0-9]+(?:[-.][a-z0-9]+)*)@([a-z0-9]+\\.[a-z]{2,20}(\\.[a-z]{2})?|\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\]|localhost)$')]),
      comp_email: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(250), Validators.pattern('^([a-z0-9]+(?:[-.][a-z0-9]+)*)@([a-z0-9]+\\.[a-z]{2,20}(\\.[a-z]{2})?|\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\]|localhost)$')]),
      user_phtoto: new FormControl(''),
      bloodgroup: new FormControl(''),
      qualification: new FormControl(''),
      differentlyabled: new FormControl('Y'),
      remarks: new FormControl(''),

      permanent_address1: new FormControl(this.employee.permanent_address1, [Validators.maxLength(1000)]),
      permanent_address2: new FormControl(''),
      permanent_city: new FormControl(''),
      permanent_postal: new FormControl(''),
      permanent_state: new FormControl(''),
      country: new FormControl(this.employee.country, [Validators.minLength(1)]),

      temporary_address1: new FormControl(''),
      temporary_address2: new FormControl(''),
      temporary_city: new FormControl(''),
      temporary_postal: new FormControl(''),
      temporary_state: new FormControl(''),
      countryname: new FormControl(this.employee.countryname, [Validators.minLength(1)]),
    });
    var url = 'HrmTrnAdmincontrol/Getbranchdropdown';
    this.service.get(url).subscribe((result: any) => {
      this.branchList = result.Getbranchdropdown;
    });
    var url = 'HrmTrnAdmincontrol/Getreportingtodropdown';
    this.service.get(url).subscribe((result: any) => {
      this.employeereportingto_list = result.Getreportingtodropdown;
    });

    var url = 'HrmTrnAdmincontrol/Getworkertypedropdown';
    this.service.get(url).subscribe((result: any) => {
      this.workertype_list = result.Getworkertypedropdown;
    });
    var url = 'HrmTrnAdmincontrol/Getroledropdown';
    this.service.get(url).subscribe((result: any) => {
      this.role_list = result.Getroledropdown;
    });

    var api1 = 'HrmTrnAdmincontrol/Getbloodgroupdropdown'
    this.service.get(api1).subscribe((result: any) => {
      this.bloodgroup_list = result.Getbloodgroupdropdown;
    });
    var api1 = 'HrmTrnAdmincontrol/Getentitydropdown'
    this.service.get(api1).subscribe((result: any) => {
      this.entity_list = result.Getentitydropdown;
    });
    var api1 = 'HrmTrnAdmincontrol/Getleavegradedropdown'
    this.service.get(api1).subscribe((result: any) => {
      this.leavegrade_list = result.Getleavegradenamedropdown;
    });

    var api1 = 'HrmTrnAdmincontrol/Getjobtypedropdown'
    this.service.get(api1).subscribe((result: any) => {
      this.jobtype_list = result.Getjobtypenamedropdown;
    });
    var api1 = 'HrmTrnAdmincontrol/Getshifttypedropdown'
    this.service.get(api1).subscribe((result: any) => {
      this.shift_list = result.Getshifttypenamedropdown;
    });
    var api1 = 'HrmTrnAdmincontrol/Getholidaygradedropdown'
    this.service.get(api1).subscribe((result: any) => {
      this.holidaygrade_list = result.Getholidaygradedropdown;
    });
    var api3 = 'HrmTrnAdmincontrol/Getdepartmentdropdown'
    this.service.get(api3).subscribe((result: any) => {
      this.departmentList = result.Getdepartmentdropdown;
    });

    var api4 = 'HrmTrnAdmincontrol/Getdesignationdropdown'
    this.service.get(api4).subscribe((result: any) => {
      this.designation_list = result.Getdesignationdropdown;
    });

    var api5 = 'HrmTrnAdmincontrol/Getcountrydropdown'
    this.service.get(api5).subscribe((result: any) => {
      this.country_list = result.Getcountrydropdown;
    });
    var api6 = 'HrmTrnAdmincontrol/Getcountry2dropdown'
    this.service.get(api6).subscribe((result: any) => {
      this.country_list2 = result.Getcountry2dropdown;
    });
  }


  onChange2(event: any) {
    this.file = event.target.files[0];
  }

  get entityname() {
    return this.reactiveForm.get('entityname')!;
  }
  get branchname() {
    return this.reactiveForm.get('branchname')!;
  }
  get departmentname() {
    return this.reactiveForm.get('departmentname')!;
  }
  get workertype() {
    return this.reactiveForm.get('workertype')!;
  }
  get role() {
    return this.reactiveForm.get('role')!;
  }
  get jobtype() {
    return this.reactiveForm.get('jobtype')!;
  }
  get designationname() {
    return this.reactiveForm.get('designationname')!;
  }
  get empjoiningdate() {
    return this.reactiveForm.get('empjoiningdate')!;
  }
  get user_code() {
    return this.reactiveForm.get('user_code')!;
  }
  get user_password() {
    return this.reactiveForm.get('user_password')!;
  }
  get password() {
    return this.reactiveForm.get('password')!;
  }
  get confirmpassword() {
    return this.reactiveForm.get('confirmpassword')!;
  }
  get shift() {
    return this.reactiveForm.get('shift')!;
  }

  get first_name() {
    return this.reactiveForm.get('first_name')!;
  }
  get dob() {
    return this.reactiveForm.get('dob')!;
  }
  get mobile() {
    return this.reactiveForm.get('mobile')!;
  }
  get mobileno() {
    return this.reactiveForm.get('mobileno')!;
  }
  get email() {
    return this.reactiveForm.get('email')!;
  }
  get comp_email() {
    return this.reactiveForm.get('comp_email')!;
  }
  get country() {
    return this.reactiveForm.get('country')!;
  }
  get countryname() {
    return this.reactiveForm.get('countryname')!;
  }

  userpassword(password: any) {
    this.reactiveForm.get("confirmpassword")?.setValue(password.value);
  }
  agecalculation(): number {
    debugger;
    const today: Date = new Date();

    const birthDate: Date = new Date(this.reactiveForm.value.dob);
    let age: number = today.getFullYear() - birthDate.getFullYear();
    const monthDiff: number = today.getMonth() - birthDate.getMonth();

    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }
    const age_calc = age;
    this.reactiveForm.get("age")?.setValue(age_calc);
    return age;



  }




  public validate(): void {
    debugger;
    console.log(this.reactiveForm.value)

    this.employee = this.reactiveForm.value;
    if (this.employee.entityname != null && this.employee.branchname != null && this.employee.departmentname != null && this.employee.designationname != null && this.employee.first_name != null && this.employee.active_flag != null && this.employee.password != null && this.employee.email != null && this.employee.mobile != null) {
      let formData = new FormData();
      if (this.file != null && this.file != undefined) {
        formData.append("file", this.file, this.file.name);
        formData.append("entityname", this.employee.entityname);
        formData.append("branchname", this.employee.branchname);
        formData.append("departmentname", this.employee.departmentname);
        formData.append("designationname", this.employee.designationname);
        formData.append("active_flag", this.employee.active_flag);
        formData.append("user_code", this.employee.user_code);
        formData.append("password", this.employee.password);
        formData.append("confirmpassword", this.employee.confirmpassword);
        formData.append("first_name", this.employee.first_name);
        formData.append("last_name", this.employee.last_name);
        formData.append("gender", this.employee.gender);
        formData.append("email", this.employee.email);
        formData.append("mobile", this.employee.mobile);
        formData.append("permanent_address1", this.employee.permanent_address1);
        formData.append("permanent_address2", this.employee.permanent_address2);
        formData.append("country", this.employee.country);
        formData.append("permanent_city", this.employee.permanent_city);
        formData.append("permanent_state", this.employee.permanent_state);
        formData.append("permanent_postal", this.employee.permanent_postal);
        formData.append("temporary_address1", this.employee.temporary_address1);
        formData.append("temporary_address2", this.employee.temporary_address2);
        formData.append("countryname", this.employee.countryname);
        formData.append("temporary_city", this.employee.temporary_city);
        formData.append("temporary_state", this.employee.temporary_state);
        formData.append("temporary_postal", this.employee.temporary_postal);

        var api = 'HrmTrnAdmincontrol/EmployeeProfileUpload'

        this.service.postfile(api, formData).subscribe((result: any) => {
          this.responsedata = result;
          if (result.status == false) {
            this.ToastrService.warning(result.message)
          }
          else {
            this.route.navigate(['/hrm/HrmTrnAdmincontrol']);
            this.ToastrService.success(result.message)
          }
        });
      }
      else {
        var api7 = 'HrmTrnAdmincontrol/PostEmployeedetails'

        this.service.post(api7, this.employee).subscribe((result: any) => {
          if (result.status == false) {
            this.ToastrService.warning(result.message)
          }
          else {
            this.route.navigate(['/hrm/HrmTrnAdmincontrol']);
            this.ToastrService.success(result.message)
          }
          this.responsedata = result;
        });
      }
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
    return;
  }
}
