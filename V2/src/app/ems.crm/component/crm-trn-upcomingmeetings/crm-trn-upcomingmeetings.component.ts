import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { ToastrService } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { AES } from 'crypto-js';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';

interface IMyleads {

  campaign_title: string;
  leadbank_name: string;
  contact_details: string;
  region_name: string;
  leadstage_name: string;
  remarks: string;
  lead_notes: string;
  details: string;
  customer_type:string;
  leadbank_gid: string;
  leadbank_gid1: string;
  lead2campaign_gid: string;
  customer_address: string;
  schedule_type: string;
  schedule: string;
  ScheduleRemarks1: string;
  schedule_status: string;
  meeting_time: string;
  date_of_demo: string;
  contact_person: string;
  alternate_person: string;
  location: string;
  prosperctive_percentage: string;
  // schedule_remarks: string;
  postponed_date: string;
  meeting_time_postponed: string;
  drop_reason: string;
  log_gid: string;
  date_of_demo_online: string;
  meeting_time_online: string;
  contact_person_online: string;
  technical_assist: string;
  prosperctive_percentage_online: string;
  productgroup_name: string;
  productgroup_name1: string;
  product_name: string;
  product_name1:string;
  demo_remarks: string;
  product_gid: string;
  product_gid1:string;
  meeting_remarks_offline: string;
  productgroup_name_offline: string;
  product_name_offline: string;
  prosperctive_percentage_offline: string;
  schedule_type_offline: string;
  location_offline: string;
  visited_by: string;
  contact_person_offline: string;
  meeting_time_offline: string;
  date_of_visit_offline: string;
  regionname: string;
  dialed_number: string;
  call_response: string;
  call_feedback: string;
  productgroup_gid: string;
  productgroup_gid1: string;
  schedule_date: string;
  Select_Response: string;
  schedule_time: string;
  
}

@Component({
  selector: 'app-crm-trn-upcomingmeetings',
  templateUrl: './crm-trn-upcomingmeetings.component.html',
  styleUrls: ['./crm-trn-upcomingmeetings.component.scss']
})

export class CrmTrnUpcomingmeetingsComponent {
  parameterValue: any;
  responsedata: any;
  parameterValue1: any;
  myleadslist: any[] = [];
  inprogresslist: any[] = [];
  potentiallist: any[] = [];
  customerlist: any[] = [];
  alllist: any[] = [];
  droplist: any[] = [];
  today_list: any[] = [];
  new_list: any[] = [];
  upcoming_list: any[] = [];
  product_group_list: any[] = [];
  product_list: any[] = [];
  MyLeadsCount_List: any[] = [];
  myleads!: IMyleads;

  reactiveForm!: FormGroup;
  reactiveForm1!: FormGroup;
  reactiveForm2!: FormGroup;
  reactiveFormfollow!: FormGroup;
  reactiveFormPostponed!: FormGroup;
  reactiveFormDrop!: FormGroup;
  reactiveFormInprogress!: FormGroup;
  reactiveFormCustomer!: FormGroup;
  movetodrop: any;
  leadbank_name:any;
  leadbank_gid: any;
  lead2campaign_gid: any;
  internal_notes: any;
  ScheduleRemarks1:any;
  schedule_remarks:any;

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService,
    private route: Router, public service: SocketService) {
    this.myleads = {} as IMyleads;
  }
  ngOnInit(): void {

    this.GetTodaySummary();

    this.reactiveForm2 = new FormGroup({

      dialed_number: new FormControl(this.myleads.dialed_number, [
        Validators.required,
        Validators.maxLength(10),
      ]),
      leadbank_gid: new FormControl(''),
      lead2campaign_gid: new FormControl(''),

      call_response: new FormControl(this.myleads.call_response, [
        Validators.required,
        // Validators.pattern( '^[a-zA-Z]+$')
      ]),
      prosperctive_percentage: new FormControl(null),
      product_name: new FormControl(null),
      productgroup_name: new FormControl(null),
      call_feedback: new FormControl(''),
      productgroup_gid: new FormControl(''),
      schedule_status: new FormControl(''),
    });
    this.reactiveFormfollow = new FormGroup({
      schedule_date: new FormControl(this.myleads.schedule_date, [
        Validators.required,
      ]),
      schedule_time: new FormControl(this.myleads.schedule_time, [
        Validators.required,
      ]),
      schedule_type: new FormControl(this.myleads.schedule_type, [
        Validators.required,
      ]),
      schedule_remarks: new FormControl(''),
      ScheduleRemarks1: new FormControl(''),
      leadbank_gid: new FormControl(''),
      lead2campaign_gid: new FormControl(''),
    });
    this.reactiveForm1 = new FormGroup({
      leadbank_gid: new FormControl(''),
      lead2campaign_gid: new FormControl(''),
    });
    this.reactiveFormInprogress = new FormGroup({
      leadbank_name: new FormControl(this.myleads.leadbank_name, [
        Validators.required,
      ]),
      leadbank_gid: new FormControl(''),
      contact_details: new FormGroup(''),
      region_name: new FormGroup(''),

    });
    this.reactiveForm = new FormGroup({
      date_of_demo: new FormControl(this.myleads.date_of_demo, [
        Validators.required,
      ]),
      meeting_time: new FormControl(this.myleads.meeting_time, [
        Validators.required,
      ]),
      contact_person: new FormControl(this.myleads.contact_person, [
        Validators.required,
      ]),
      alternate_person: new FormControl(this.myleads.alternate_person, [
        Validators.required,
      ]),
      location: new FormControl(this.myleads.location, [
        Validators.required,
      ]),
      prosperctive_percentage: new FormControl(""),
      
      leadbank_gid: new FormControl(''),
      log_gid: new FormControl(''),
      schedulelog_gid: new FormControl(''),
    });
    this.reactiveFormPostponed = new FormGroup({

      postponed_date: new FormControl(this.myleads.postponed_date, [
        Validators.required,
      ]),
      meeting_time_postponed: new FormControl(this.myleads.meeting_time_postponed, [
        Validators.required,
      ]),
      leadbank_gid: new FormControl(''),
      log_gid: new FormControl(''),
      schedulelog_gid: new FormControl(''),
    });
    this.reactiveFormDrop = new FormGroup({

      drop_reason: new FormControl(''),
      leadbank_gid: new FormControl(''),
      log_gid: new FormControl(''),
      schedulelog_gid: new FormControl(''),
      productgroup_gid: new FormControl(''),

    });

    // var api = 'MyLead/GetProductdropdown'
    // this.service.get(api).subscribe((result: any) => {
    //   this.responsedata = result;
    //   this.product_list = this.responsedata.product_list32;
    // });

    var api6 = 'MyLead/GetProductGroupdropdown'
    this.service.get(api6).subscribe((result: any) => {
      this.responsedata = result;
      this.product_group_list = this.responsedata.product_group_list12;
    });

    var api7 = 'MyLead/GetMyLeadsCount';
    this.service.get(api7).subscribe((result:any) => {
    this.responsedata = result;
    this.MyLeadsCount_List = this.responsedata.getMyLeadsCount_List; 
    console.log(this.MyLeadsCount_List,'testdata');
    });

    const options: Options = {
      // enableTime: true,
      dateFormat: 'Y-m-d',

    };
    flatpickr('.date-picker', options);
  }

  onedit(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    console.log(param);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    console.log(encryptedParam);
    this.route.navigate(['/crm/CrmTrnAddtocustomer', encryptedParam])
  }
  //*******close textfields******//
  get date_of_demo() {
    return this.reactiveForm.get('date_of_demo')!;
  }
  get meeting_time() {
    return this.reactiveForm.get('meeting_time')!;
  }
  get contact_person() {
    return this.reactiveForm.get('contact_person')!;
  }
  get alternate_person() {
    return this.reactiveForm.get('alternate_person')!;
  }
  get location() {
    return this.reactiveForm.get('location')!;
  }
  get prosperctive_percentage() {
    return this.reactiveForm.get('prosperctive_percentage')!;
  }
  // get schedule_remarks() {
  //   return this.reactiveForm.get('schedule_remarks')!;
  // }
  //*****postponed textfields******//
  get postponed_date() {
    return this.reactiveFormPostponed.get('postponed_date')!;
  }
  get meeting_time_postponed() {
    return this.reactiveFormPostponed.get('meeting_time_postponed')!;
  }
  //******drop textfields********//
  get drop_reason() {
    return this.reactiveFormDrop.get('drop_reason')!;
  }
  //*******Calllog & schedulelog*********//
  get dialed_number() {
    return this.reactiveForm2.get('dialed_number')!;
  }
  get call_response() {
    return this.reactiveForm2.get('call_response')!;
  }
  // get prosperctive_percentage() {
  //   return this.reactiveForm2.get('prosperctive_percentage')!;
  // }
  get product_name() {
    return this.reactiveForm2.get('product_name')!;
  }
  get productgroup_name() {
    return this.reactiveForm2.get('productgroup_name')!;
  }
  get call_feedback() {
    return this.reactiveForm2.get('call_feedback')!;
  }
  get schedule_date() {
    return this.reactiveFormfollow.get('schedule_date')!;
  }
  get schedule_time() {
    return this.reactiveFormfollow.get('schedule_time')!;
  }
  get schedule_type() {
    return this.reactiveFormfollow.get('schedule_type')!;
  }
  // get  schedule_remarks() {
  //   return this.reactiveFormfollow.get('schedule_remarks')!;
  // }
  //***Close or Postpone popup***//
  openModal1(parameter: string) {
    this.parameterValue1 = parameter
    this.reactiveForm.get("leadbank_gid")?.setValue(this.parameterValue1.leadbank_gid);
    this.reactiveForm.get("log_gid")?.setValue(this.parameterValue1.log_gid);
    this.reactiveForm.get("schedulelog_gid")?.setValue(this.parameterValue1.schedulelog_gid);
    this.reactiveFormPostponed.get("schedulelog_gid")?.setValue(this.parameterValue1.schedulelog_gid);
    this.reactiveFormDrop.get("leadbank_gid")?.setValue(this.parameterValue1.leadbank_gid);
    this.reactiveForm.get("leadbank_name")?.setValue(this.parameterValue1.leadbank_name1);
    this.leadbank_name = this.parameterValue1.leadbank_name1;
  }

  ////Add Lead page Routing////
  onadd() {
    const secretKey = 'storyboarderp'
    const lspage1 = 'MyLead';
    const lspage = AES.encrypt(lspage1, secretKey).toString();
    this.route.navigate(['/crm/CrmTrnLeadmasteradd', lspage]);

  }
  productname() {
    let productgroup_gid = this.reactiveForm2.get("productgroup_name")?.value;
    let params = {
      productgroup_gid: productgroup_gid
    }
    var url = 'MyLead/GetProductdropdown'
    this.service.getparams(url, params).subscribe((result: any) => {
      this.responsedata = result;
      this.product_list = this.responsedata.product_list32;
    });
  }
 
  /////Upcoming Meeting or My visit/////
  GetTodaySummary() {
    var url = 'MyLead/GetTodaySummary'
    this.service.get(url).subscribe((result: any) => {
      $('#today_list').DataTable().destroy();
      this.responsedata = result;
      this.today_list = this.responsedata.Todayvisit_list1;
      //console.log(this.entity_list)
      setTimeout(() => {
        $('#today_list').DataTable();
      }, 1);
    });
  }
  onclose() {
    this.reactiveForm.reset();
    this.reactiveFormPostponed.reset();
    this.reactiveFormDrop.reset();
    this.reactiveForm2.reset();
    this.reactiveFormfollow.reset();
  }

  ////////close submit///////
  public onsubmitClose(): void {
    if (this.reactiveForm.value.date_of_demo != null && this.reactiveForm.value.meeting_time != null && this.reactiveForm.value.contact_person != null && this.reactiveForm.value.alternate_person != null && this.reactiveForm.value.location != null && this.reactiveForm.value.prosperctive_percentage != '') {

      for (const control of Object.keys(this.reactiveForm.controls)) {
        this.reactiveForm.controls[control].markAsTouched();
      }
      console.log(this.reactiveForm.value);
      var url = 'MyLead/Postcloselog'
      this.service.post(url, this.reactiveForm.value).pipe().subscribe((result: any) => {
        console.log(this.reactiveForm);

        if (result.status == false) {
          window.location.reload()
          this.ToastrService.warning(result.message)
          // this.GetExpiredSummary();
          this.reactiveForm.reset();
        }
        else {
          this.reactiveForm.get("date_of_demo")?.setValue(null);
          this.reactiveForm.get("meeting_time")?.setValue(null);
          this.reactiveForm.get("contact_person")?.setValue(null);
          this.reactiveForm.get("altrnate_person")?.setValue(null);
          this.reactiveForm.get("location")?.setValue(null);
          this.reactiveForm.get("prosperctive_percentage")?.setValue('');
          window.location.reload()
          this.ToastrService.success(result.message)
          //this.GetExpiredSummary();
          this.reactiveForm.reset();
        }
        // this.GetExpiredSummary();
        this.reactiveForm.reset();
      });

    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }

  ////////close Postponed///////
  public onsubmitPostponed(): void {
    if (this.reactiveFormPostponed.value.postponed_date != null && this.reactiveFormPostponed.value.meeting_time_postponed != null) {

      for (const control of Object.keys(this.reactiveFormPostponed.controls)) {
        this.reactiveFormPostponed.controls[control].markAsTouched();
      }
      console.log(this.reactiveFormPostponed.value);
      var url = 'MyLead/Postpostonedlog'
      this.service.post(url, this.reactiveFormPostponed.value).pipe().subscribe((result: any) => {

        if (result.status == false) {
          window.location.reload()
          this.ToastrService.warning(result.message)
          // this.GetExpiredSummary();
          this.reactiveFormPostponed.reset();
        }
        else {
          this.reactiveFormPostponed.get("postponed_date")?.setValue(null);
          this.reactiveFormPostponed.get("meeting_time_postponed")?.setValue(null);
          window.location.reload()
          this.ToastrService.success(result.message)
          // this.GetExpiredSummary();
          this.reactiveFormPostponed.reset();
        }
        //this.GetExpiredSummary();
        this.reactiveFormPostponed.reset();
      });
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }

  ////////Drop submit///////
  public onsubmitDrop(): void {
    if (this.reactiveFormDrop.value.drop_reason != '') {

      for (const control of Object.keys(this.reactiveFormDrop.controls)) {
        this.reactiveFormDrop.controls[control].markAsTouched();
      }
      console.log(this.reactiveFormDrop.value);
      var url = 'MyLead/Postdroplog'
      this.service.post(url, this.reactiveFormDrop.value).pipe().subscribe((result: any) => {

        if (result.status == false) {
          window.location.reload()
          this.ToastrService.warning(result.message)
          // this.GetExpiredSummary();
          this.reactiveFormDrop.reset();
        }

        else {
          this.reactiveFormDrop.get("drop_reason")?.setValue('');
          window.location.reload()
          this.ToastrService.success(result.message)
          // this.GetExpiredSummary();
          this.reactiveFormDrop.reset();
        }
        //this.GetExpiredSummary();
        this.reactiveFormDrop.reset();
      });

    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }

  popmodal(parameter: string, parameter1: string) {
    this.schedule_remarks = parameter
    this.leadbank_name = parameter1;
  }
   //360//
 Onopen(param1: any, param2: any) {
  const secretKey = 'storyboarderp';
  const lspage1 = "My-Upcoming";
  const lspage = AES.encrypt(lspage1, secretKey).toString();
  console.log(param1);
  console.log(param2);
  const leadbank_gid = AES.encrypt(param1, secretKey).toString();
  const lead2campaign_gid = AES.encrypt(param2, secretKey).toString();
  this.route.navigate(['/crm/CrmTrn360view', leadbank_gid, lead2campaign_gid, lspage]);
}

  } 


