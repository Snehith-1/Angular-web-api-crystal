import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

interface IEmployee {
  branchname: string;
  entityname: string;
  departmentname: string;
  designationname: string;
  first_name: string;
  last_name: string;
  gender: string;
  mobile: string;
  email: string;
  permanent_address1: string;
  permanent_address2: string;
  permanent_city: string;
  permanent_postal: string;
  permanent_state: string;
  temporary_address1: string;
  temporary_address2: string;
  temporary_city: string;
  temporary_postal: string;
  temporary_state: string;
  country: string;
  countryname: string;
  user_code: string;
  user_password: string;
  password: string;
  confirmpassword: string;
  active_flag: string;
  showPassword: boolean;
}

@Component({
  selector: 'app-sys-mst-employee-add',
  templateUrl: './sys-mst-employee-add.component.html',
  styleUrls: ['./sys-mst-employee-add.component.scss']
})

export class SysMstEmployeeAddComponent implements OnInit {
  file!: File;
  employee!: IEmployee;
  reactiveForm!: FormGroup;
  entity_list: any[] = [];
  branch_list: any[] = [];
  department_list: any[] = [];
  designation_list: any[] = [];
  country_list: any[] = [];
  country_list2: any[] = [];
  selectedEmpPassword: any;
  Emp_Join_date: any;
  selectedEmpAccess: string = 'Yes';
  Emp_Code: any;
  Email_Address: any;
  Entity: any;
  responsedata: any;
  formdata = { selectedEmpPassword: "", Confrim_Emp_password: "", Emp_Code: "", Emp_Join_date: "", Emp_mobile_number: "", Email_Address: "" }

  entityList: any;
  branchList: any;
  departmentList: any[] = []

  constructor(private renderer: Renderer2, private el: ElementRef, public service: SocketService, private ToastrService: ToastrService, private route: Router) {
    this.employee = {} as IEmployee;
  }

  ngOnInit(): void {
    this.reactiveForm = new FormGroup({
      first_name: new FormControl(this.employee.first_name, [Validators.required,]),
      user_code: new FormControl(this.employee.user_code, [Validators.required,]),
      file: new FormControl(''),
      permanent_address2: new FormControl(''),
      permanent_state: new FormControl(''),
      active_flag: new FormControl('Y'),
      gender: new FormControl('male'),
      user_phtoto: new FormControl(''),
      last_name: new FormControl(''),
      permanent_postal: new FormControl(this.employee.permanent_postal, []),
      permanent_city: new FormControl(''),
      branchname: new FormControl(this.employee.branchname, [Validators.required, Validators.minLength(1), Validators.maxLength(250)]),
      entityname: new FormControl(this.employee.entityname, [Validators.required, Validators.minLength(1), Validators.maxLength(250)]),
      mobile: new FormControl(this.employee.mobile, [Validators.required, Validators.maxLength(10)]),
      departmentname: new FormControl(this.employee.departmentname, [Validators.required, Validators.minLength(1)]),
      designationname: new FormControl(this.employee.designationname, [Validators.required, Validators.minLength(1)]),
      countryname: new FormControl(this.employee.countryname, [Validators.minLength(1)]),
      country: new FormControl(this.employee.country, [Validators.minLength(1)]),
      temporary_address2: new FormControl(''),
      temporary_address1: new FormControl(''),
      temporary_postal: new FormControl(this.employee.temporary_postal, []),
      temporary_city: new FormControl(''),
      temporary_state: new FormControl(''),
      permanent_address1: new FormControl(this.employee.permanent_address1, [Validators.maxLength(1000)]),
      email: new FormControl(this.employee.email, [Validators.required, Validators.pattern('^([a-z0-9]+(?:[-.][a-z0-9]+)*)@([a-z0-9]+\\.[a-z]{2,20}(\\.[a-z]{2})?|\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\]|localhost)$')]),
      password: new FormControl(this.employee.password, [Validators.required, Validators.minLength(1)]),
      confirmpassword: new FormControl({ value: '', disabled: false }),
    }
    );

    var url = 'Employeelist/Getbranchdropdown';
    this.service.get(url).subscribe((result: any) => {
      this.branchList = result.Getbranchdropdown;
    });

    var api1 = 'Employeelist/Getentitydropdown'
    this.service.get(api1).subscribe((result: any) => {
      this.entity_list = result.Getentitydropdown;
    });

    var api3 = 'Employeelist/Getdepartmentdropdown'
    this.service.get(api3).subscribe((result: any) => {
      this.departmentList = result.Getdepartmentdropdown;      
    });

    var api4 = 'Employeelist/Getdesignationdropdown'
    this.service.get(api4).subscribe((result: any) => {
      this.designation_list = result.Getdesignationdropdown;      
    });

    var api5 = 'Employeelist/Getcountrydropdown'
    this.service.get(api5).subscribe((result: any) => {
      this.country_list = result.Getcountrydropdown;      
    });

    var api6 = 'Employeelist/Getcountry2dropdown'
    this.service.get(api6).subscribe((result: any) => {
      this.country_list2 = result.Getcountry2dropdown;    
    });
  }

  onChange2(event: any) {
    this.file = event.target.files[0];
  }

  get permanent_postal() {
    return this.reactiveForm.get('permanent_postal')!;
  }
  get temporary_postal() {
    return this.reactiveForm.get('temporary_postal')!;
  }
  get branchname() {
    return this.reactiveForm.get('branchname')!;
  }
  get departmentname() {
    return this.reactiveForm.get('departmentname')!;
  }
  get designationname() {
    return this.reactiveForm.get('designationname')!;
  }
  get countryname() {
    return this.reactiveForm.get('countryname')!;
  }
  get country() {
    return this.reactiveForm.get('country')!;
  }
  get first_name() {
    return this.reactiveForm.get('first_name')!;
  }
  get user_code() {
    return this.reactiveForm.get('user_code')!;
  }
  get mobile() {
    return this.reactiveForm.get('mobile')!;
  }
  get email() {
    return this.reactiveForm.get('email')!;
  }
  get password() {
    return this.reactiveForm.get('password')!;
  }
  get confirmpassword() {
    return this.reactiveForm.get('confirmpassword')!;
  }
  get user_password() {
    return this.reactiveForm.get('user_password')!;
  }
  get entityname() {
    return this.reactiveForm.get('entityname')!;
  }
  userpassword(password: any) {
    this.reactiveForm.get("confirmpassword")?.setValue(password.value);
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

        var api = 'Employeelist/EmployeeProfileUpload'
        this.service.postfile(api, formData).subscribe((result: any) => {
          this.responsedata = result;
          if (result.status == false) {
            this.ToastrService.warning(result.message)
          }
          else {
            this.route.navigate(['/system/SysMstEmployeeSummary']);
            this.ToastrService.success(result.message)
          }
        });
      }
      else {
        var api7 = 'Employeelist/PostEmployeedetails'
        this.service.post(api7, this.employee).subscribe((result: any) => {
          if (result.status == false) {
            this.ToastrService.warning(result.message)
          }
          else {
            this.route.navigate(['/system/SysMstEmployeeSummary']);
            this.ToastrService.success(result.message)
          }
          this.responsedata = result;
        });
      }
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !!')
    }
    return;
  }
}