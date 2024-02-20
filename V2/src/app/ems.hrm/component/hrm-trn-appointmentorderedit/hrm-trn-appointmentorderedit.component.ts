import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { AngularEditorConfig } from '@kolkov/angular-editor';


interface IAppointmentorder {
  template_name: string;
  appointmentorder_gid: string;
  temp_address1: string;
  temp_address2: string;
  email_address: string;
  experience_detail: string;
  // appointment_date: string;
  perm_address1: string;
  gender: string;
  temp_pincode: string;
  branch_name: string;
  qualification: string;
  // joiningdate: string;
  perm_country: string;
  temp_country: string;
  perm_city: string;
  department_name: string;
  designation_name: string;
  perm_state: string;
  first_name: string;
  employee_salary: string;
  perm_address2: string;
  perm_pincode: string;
  mobile_number: string;
  last_name: string;
  temp_state: string;
  temp_city: string;
  dob: string;
  template_gid:string;
  appointmentordertemplate_content:String;
}

@Component({
  selector: 'app-hrm-trn-appointmentorderedit',
  templateUrl: './hrm-trn-appointmentorderedit.component.html',
  styleUrls: ['./hrm-trn-appointmentorderedit.component.scss']
})


export class HrmTrnAppointmentordereditComponent {
  appointmentorder!: IAppointmentorder;
  reactiveForm!: FormGroup;
  appointmentorderform: any;
  responsedata: any;
  selectedbranch: any;
  branch_list: any;
  department_list: any;
  designation_list: any;
  selecteddepartment: any;
  selecteddesignation: any;
  selectedCountry1: any;
  selectedCountry2: any;
  country_list1: any;
  country_list: any;
  selectedcountry: any;
  Selectedcountry: any;
  appointmentordergid: any;
  editappoinmentorder: any;
  email_address: any;
  mdlTerms: any;
  terms_list: any[] = [];
  templatecontent_list: any;

  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '12rem',
    minHeight: '0rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
  };

  constructor(private renderer: Renderer2, private el: ElementRef, public service: SocketService, private ToastrService: ToastrService, private route: Router, private router: ActivatedRoute) {
    this.appointmentorder = {} as IAppointmentorder;
  }

  ngOnInit(): void {
    // const appointmentorder_gid = this.router.snapshot.paramMap.get('appointmentorder_gid');
    // this.appointmentordergid = appointmentorder_gid

    // const secretKey = 'storyboarderp';

    // const deencryptedParam = AES.decrypt(this.appointmentordergid, secretKey).toString(enc.Utf8);
    // console.log(deencryptedParam)

    this.appointmentorderform = new FormGroup({

      branch_name: new FormControl(''),
      appointmentorder_gid: new FormControl(''),
      appointment_date: new FormControl(''),
      first_name: new FormControl(''),
      last_name: new FormControl(''),
      gender: new FormControl(''),
      experience_detail: new FormControl(''),
      dob: new FormControl(''),
      mobile_number: new FormControl(''),
      email_address: new FormControl(''),
      joiningdate: new FormControl(''),
      qualification: new FormControl(''),
      department_name: new FormControl(''),
      designation_name: new FormControl(''),
      employee_salary: new FormControl(''),
      perm_address1: new FormControl(''),
      perm_address2: new FormControl(''),
      perm_country: new FormControl(''),
      perm_state: new FormControl(''),
      perm_city: new FormControl(''),
      perm_pincode: new FormControl(''),
      temp_address1: new FormControl(''),
      temp_address2: new FormControl(''),
      temp_country: new FormControl(''),
      temp_state: new FormControl(''),
      temp_city: new FormControl(''),
      template_name: new FormControl(''),
      appointmentordertemplate_content: new FormControl(''),

      temp_pincode: new FormControl(''),

      template_gid: new FormControl(''),
    });

    var api1 = 'AppointmentOrder/Getbranchdropdown';
    this.service.get(api1).subscribe((result: any) => {
      this.branch_list = result.Getbranchdropdown;

    });

    var api2 = 'AppointmentOrder/Getdepartmentdropdown';
    this.service.get(api2).subscribe((result: any) => {
      this.department_list = result.Getdepartmentdropdown;
    });
    
    var api3 = 'AppointmentOrder/Getdesignationdropdown';
    this.service.get(api3).subscribe((result: any) => {
      this.designation_list = result.Getdesignationdropdown;
    });

    var url = 'AppointmentOrder/TermsandConditions'
    this.service.get(url).subscribe((result: any) => {
      this.terms_list = result.GetAppointmentdropdown;
    });

    var api4 = 'AppointmentOrder/Getcountrydropdown';
    this.service.get(api4).subscribe((result: any) => {
      this.country_list = result.getcountrydropdown;
    });

    this.Geteditappoinmentorder()
  }

  get appointment_date() {
    return this.appointmentorderform.get('appointment_date')!;
  }
  get first_name() {
    return this.appointmentorderform.get('first_name')!;
  }


  Geteditappoinmentorder() {
    const appointmentorder_gid = this.router.snapshot.paramMap.get('appointmentorder_gid');
    this.appointmentordergid = appointmentorder_gid;

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.appointmentordergid, secretKey).toString(enc.Utf8);
    
    let param = {
      appointmentorder_gid: deencryptedParam
    }

    var api = 'AppointmentOrder/Geteditappoinmentorder';

    console.log(param);
    this.service.getparams(api, param).subscribe((result: any) => {
      this.responsedata=result;
      this.editappoinmentorder = result;

      this.appointmentorderform.get("appointmentorder_gid")?.setValue(this.editappoinmentorder.appointmentorder_gid);
      this.appointmentorderform.get("branch_name")?.setValue(this.editappoinmentorder.branch_name);
      this.appointmentorderform.get("appointment_date")?.setValue(this.editappoinmentorder.appointment_date);
      this.appointmentorderform.get("first_name")?.setValue(this.editappoinmentorder.first_name);
      this.appointmentorderform.get("last_name")?.setValue(this.editappoinmentorder.last_name);
      this.appointmentorderform.get("branch_gid")?.setValue(this.editappoinmentorder.branch_gid);
      this.appointmentorderform.get("department_gid")?.setValue(this.editappoinmentorder.department_gid);
      this.appointmentorderform.get("designation_gid")?.setValue(this.editappoinmentorder.designation_gid);
      this.appointmentorderform.get("gender")?.setValue(this.editappoinmentorder.gender);
      this.appointmentorderform.get("experience_detail")?.setValue(this.editappoinmentorder.experience_detail);
      this.appointmentorderform.get("dob")?.setValue(this.editappoinmentorder.dob);
      this.appointmentorderform.get("mobile_number")?.setValue(this.editappoinmentorder.mobile_number);
      this.appointmentorderform.get("email_address")?.setValue(this.editappoinmentorder.email_address);
      this.appointmentorderform.get("joiningdate")?.setValue(this.editappoinmentorder.joiningdate);
      this.appointmentorderform.get("qualification")?.setValue(this.editappoinmentorder.qualification);
      this.appointmentorderform.get("department_name")?.setValue(this.editappoinmentorder.department_name);
      this.appointmentorderform.get("designation_name")?.setValue(this.editappoinmentorder.designation_name);
      this.appointmentorderform.get("employee_salary")?.setValue(this.editappoinmentorder.employee_salary);
      this.appointmentorderform.get("perm_address1")?.setValue(this.editappoinmentorder.perm_address1);
      this.appointmentorderform.get("perm_address2")?.setValue(this.editappoinmentorder.perm_address2);
      this.appointmentorderform.get("perm_country")?.setValue(this.editappoinmentorder.perm_country);
      this.appointmentorderform.get("perm_state")?.setValue(this.editappoinmentorder.perm_state);
      this.appointmentorderform.get("perm_city")?.setValue(this.editappoinmentorder.perm_city);
      this.appointmentorderform.get("perm_pincode")?.setValue(this.editappoinmentorder.perm_pincode);
      this.appointmentorderform.get("temp_address1")?.setValue(this.editappoinmentorder.temp_address1);
      this.appointmentorderform.get("temp_address2")?.setValue(this.editappoinmentorder.temp_address2);
      this.appointmentorderform.get("temp_country")?.setValue(this.editappoinmentorder.temp_country);
      this.appointmentorderform.get("temp_state")?.setValue(this.editappoinmentorder.temp_state);
      this.appointmentorderform.get("temp_city")?.setValue(this.editappoinmentorder.temp_city);
      this.appointmentorderform.get("temp_pincode")?.setValue(this.editappoinmentorder.temp_pincode);
      this.appointmentorderform.get("template_gid")?.setValue(this.editappoinmentorder.template_gid);
      this.appointmentorderform.get("template_name")?.setValue(this.editappoinmentorder.temp_pincode);
      
    });
  }

  updated() {
    console.log(this.appointmentorderform)

    var api = 'AppointmentOrder/Updatedappointmentorder';
    this.service.post(api, this.appointmentorderform.value).subscribe((result: any) => {
      this.route.navigate(['/hrm/HrmTrnAppointmentorder']);
      if (result.status == true) {
        this.ToastrService.success(result.message)
      }
      else {
        this.ToastrService.warning(result.message)
      }
    },
  );
  }

  GetOnChangeTerms() {
    debugger

    let template_gid = this.appointmentorderform.value.template_name;
    let param = {
      template_gid: template_gid
    }

    var url = 'AppointmentOrder/GetOnChangeTerms';

    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.templatecontent_list = this.responsedata.GetAppointmentdropdown;
      this.appointmentorderform.get("appointmentordertemplate_content")?.setValue(this.templatecontent_list[0].template_content);
      this.appointmentorderform.value.template_gid = result.terms_list[0].template_gid
    });
  }

}





































































































