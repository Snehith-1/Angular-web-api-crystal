import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';
import { NgxSpinnerService } from 'ngx-spinner';
interface Iofferletter {  
  Offerlettertemplate_content:String;
}




@Component({
  selector: 'app-hrm-trn-add-offer-letter',
  templateUrl: './hrm-trn-add-offer-letter.component.html',
  styleUrls: ['./hrm-trn-add-offer-letter.component.scss']
})
export class HrmTrnAddOfferLetterComponent {
  appointmentorder!: Iofferletter;

  Offerletterform!: FormGroup;
  responsedata: any;
  selectedbranch: any;
  branchList: any;
  cbobranch : any;
  department_list: any;
  designationList: any;
  selecteddepartment: any;
  selecteddesignation: any;
  selectedCountry1: any;  
  cbodesignation: any;
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
  txtemployee_joining_date: any;
  dob: any;
  Offer_date: any;
  Qualification: any;
  permanent_country: any;
  temp_country:any;
  department_name:any;


  
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
  constructor(private renderer: Renderer2, private el: ElementRef,public NgxSpinnerService:NgxSpinnerService, public service: SocketService, private ToastrService: ToastrService, private route: Router, private router: ActivatedRoute) {
  }
  ngOnInit(): void {
    const options: Options = {
      dateFormat: 'd-m-Y', 
      
    };

    flatpickr('.date-picker', options);  
   

    this.Offerletterform = new FormGroup({
      branch_name: new FormControl(''),
      appointmentorder_gid: new FormControl(''),
      appointment_date: new FormControl(''),
      first_name: new FormControl(''),
      last_name: new FormControl(''),
      gender: new FormControl(''),
      Experience: new FormControl(''),
      dob: new FormControl(''),
      mobile_number: new FormControl(''),
      email_address: new FormControl(''),
      txtemployee_joining_date: new FormControl(null,Validators.required),
      Qualification: new FormControl(''),
      cbodesignation: new FormControl(''),
      designation_name: new FormControl(''),
      Salary: new FormControl(''),
      permanent_address1: new FormControl(''),
      permanent_address2: new FormControl(''),
      permanent_country: new FormControl(''),
      permanent_state: new FormControl(''),
      permanent_city: new FormControl(''),
      permanent_postal: new FormControl(''),
      temporary_address1: new FormControl(''),
      temporary_address2: new FormControl(''),
      temp_country: new FormControl(''),
      temporary_state: new FormControl(''),
      temporary_city: new FormControl(''),
      template_name: new FormControl(''),
      Offerlettertemplate_content: new FormControl(''), 
      cbobranch : new FormControl(''), 
      offer_no : new FormControl(''), 
      department_name : new FormControl(''), 
      temporary_postal: new FormControl(''),
      template_gid: new FormControl(''),
      Offer_date: new FormControl(''),

    });

    var url = 'EmployeeOnboard/PopBranch';
    this.service.get(url).subscribe((result: any) => {
      this.branchList  = result.employee;
    });

    var api2 = 'AppointmentOrder/Getdepartmentdropdown';
    this.service.get(api2).subscribe((result: any) => {
      this.department_list = result.Getdepartmentdropdown;
    });
    
    var url = 'EmployeeOnboard/PopDesignation';
    this.service.get(url).subscribe((result: any) => {
      this.designationList  = result.employee;
    });

    var url = 'AppointmentOrder/TermsandConditions'
    this.service.get(url).subscribe((result: any) => {
      this.terms_list = result.GetAppointmentdropdown;
    });

    var api4 = 'AppointmentOrder/Getcountrydropdown';
    this.service.get(api4).subscribe((result: any) => {
      this.country_list = result.getcountrydropdown;
    });
  }
  onsubmit() {
    debugger;
    var params={ 
      branch_gid : this.Offerletterform.value.cbobranch,
      offer_refno : this.Offerletterform.value.offer_no,
      offer_date : this.Offerletterform.value.Offer_date,
      first_name : this.Offerletterform.value.first_name,
      last_name : this.Offerletterform.value.last_name,
      gender : this.Offerletterform.value.gender,
      experience_detail : this.Offerletterform.value.Experience,
      dob : this.Offerletterform.value.dob,      
      mobile_number : this.Offerletterform.value.mobile_number,
      email_address : this.Offerletterform.value.email_address,
      joiningdate  : this.Offerletterform.value.txtemployee_joining_date,
      qualification : this.Offerletterform.value.Qualification,  
      department_gid : this.Offerletterform.value.department_name,
      designation_gid : this.Offerletterform.value.cbodesignation,
      employee_salary : this.Offerletterform.value.Salary,
      perm_address1 : this.Offerletterform.value.permanent_address1,      
      perm_address2 : this.Offerletterform.value.permanent_address2,
      perm_city : this.Offerletterform.value.permanent_city,
      perm_pincode : this.Offerletterform.value.permanent_postal,
      perm_state : this.Offerletterform.value.permanent_state,  
      perm_country : this.Offerletterform.value.permanent_country,      
      temp_address1 : this.Offerletterform.value.temporary_address1,
      temp_address2 : this.Offerletterform.value.temporary_address2,
      temp_city : this.Offerletterform.value.temporary_city,
      temp_pincode : this.Offerletterform.value.temporary_postal,  
      temp_state : this.Offerletterform.value.temporary_state,
      temp_country : this.Offerletterform.value.temp_country,  
      template_gid : this.Offerletterform.value.template_name,   
      offertemplate_content : this.Offerletterform.value.Offerlettertemplate_content, 
   
    }
    console.log(params)
  
    var url = 'OfferLetter/Addofferletter'
    this.NgxSpinnerService.show();
      this.service.postparams(url,params).subscribe((result: any) => {
        this.NgxSpinnerService.hide();
        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.route.navigate(['/hrm/HrmTrnofferletter']);
  
       }
       else{
        this.ToastrService.success(result.message)
        this.route.navigate(['/hrm/HrmTrnofferletter']);
       }
  
      });
  
  }
  onback(){
    this.route.navigate(['/hrm/HrmTrnOfferLetter'])

  }


  GetOnChangeTerms() {
    debugger

    let template_gid = this.Offerletterform.value.template_name;
    let param = {
      template_gid: template_gid
    }

    var url = 'AppointmentOrder/GetOnChangeTerms';

    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.templatecontent_list = this.responsedata.GetAppointmentdropdown;
      this.Offerletterform.get("Offerlettertemplate_content")?.setValue(this.templatecontent_list[0].template_content);
      this.Offerletterform.value.template_gid = result.terms_list[0].template_gid
    });
  }

}
