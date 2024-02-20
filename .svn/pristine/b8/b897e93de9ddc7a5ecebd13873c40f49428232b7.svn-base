import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
interface IMyvisits {
  leadbank_name: string;
  contact_details: string;
  customer_address: string;
  region_name: string;
  schedule_type: string;
  schedule: string;
  ScheduleRemarks: string;
  schedule_status: string;
  meeting_time: string;
  date_of_demo: string;
  contact_person: string;
  alternate_person: string;
  location: string;
  prosperctive_percentage: string;
  schedule_remarks: string;
  postponed_date: string;
  meeting_time_postponed: string;
  drop_reason: string;
  leadbank_gid: string;
  log_gid: string;
  date_of_demo_online: string;
  meeting_time_online: string;
  contact_person_online: string;
  technical_assist: string;
  prosperctive_percentage_online: string;
  productgroup_name: string;
  product_name: string;
  demo_remarks: string;
  product_gid: string;
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

}

@Component({
  selector: 'app-crm-trn-myvisit',
  templateUrl: './crm-trn-myvisit.component.html',
  styleUrls: ['./crm-trn-myvisit.component.scss']
})
export class CrmTrnMyvisitComponent {
  expired_list: any[] = [];
  today_list: any[] = [];
  upcoming_list: any[] = [];
  product_group_list: any[] = [];
  product_list: any[] = [];
  myvisits!: IMyvisits;

  reactiveForm!: FormGroup;
  reactiveFormPostponed!: FormGroup;
  reactiveFormDrop!: FormGroup;
  reactiveFormonline!: FormGroup;
  reactiveFormoffline!: FormGroup;
  responsedata: any;


  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, private route: Router, public service: SocketService) {
    this.myvisits = {} as IMyvisits;
  }
  ngOnInit(): void {

    this.reactiveForm = new FormGroup({
      date_of_demo: new FormControl(this.myvisits.date_of_demo, [
        Validators.required,
      ]),
      meeting_time: new FormControl(this.myvisits.meeting_time, [
        Validators.required,
      ]),
      contact_person: new FormControl(this.myvisits.contact_person, [
        Validators.required,
      ]),
      alternate_person: new FormControl(this.myvisits.alternate_person, [
        Validators.required,
      ]),
      location: new FormControl(this.myvisits.location, [
        Validators.required,
      ]),
      prosperctive_percentage: new FormControl(null),
      schedule_remarks: new FormControl(""),

      leadbank_gid: new FormControl(''),
      log_gid: new FormControl(''),
      schedulelog_gid: new FormControl(''),
    });

    this.reactiveFormPostponed = new FormGroup({

      postponed_date: new FormControl(this.myvisits.postponed_date, [
        Validators.required,
      ]),
      meeting_time_postponed: new FormControl(this.myvisits.meeting_time_postponed, [
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

    this.reactiveFormonline = new FormGroup({
      date_of_demo_online: new FormControl(this.myvisits.date_of_demo_online, [
        Validators.required,
      ]),
      meeting_time_online: new FormControl(this.myvisits.meeting_time_online, [
        Validators.required,
      ]),
      contact_person_online: new FormControl(this.myvisits.contact_person_online, [
        Validators.required,
      ]),
      technical_assist: new FormControl(this.myvisits.technical_assist, [
        Validators.required,
      ]),
      schedule_type: new FormControl(this.myvisits.schedule_type, [
        Validators.required,

      ]),
      product_name: new FormControl(this.myvisits.product_name, [
        Validators.required,

      ]),
      productgroup_name: new FormControl(this.myvisits.productgroup_name, [
        Validators.required,

      ]),
      productgroup_gid: new FormControl(''),
      prosperctive_percentage_online: new FormControl(null),
      demo_remarks: new FormControl(''),
      leadbank_gid: new FormControl(''),
      log_gid: new FormControl(''),
      schedulelog_gid: new FormControl(''),

    });
    this.reactiveFormoffline = new FormGroup({
      date_of_visit_offline: new FormControl(this.myvisits.date_of_demo_online, [
        Validators.required,
      ]),
      meeting_time_offline: new FormControl(this.myvisits.meeting_time_offline, [
        Validators.required,
      ]),
      contact_person_offline: new FormControl(this.myvisits.contact_person_offline, [
        Validators.required,
      ]),
      visited_by: new FormControl(this.myvisits.visited_by, [
        Validators.required,
      ]),
      location_offline: new FormControl(this.myvisits.location_offline, [
        Validators.required,
      ]),
      schedule_type_offline: new FormControl(this.myvisits.schedule_type_offline, [
        Validators.required,

      ]),
      prosperctive_percentage_offline: new FormControl(this.myvisits.prosperctive_percentage_offline, [
        Validators.required,

      ]),
      product_name_offline: new FormControl(this.myvisits.product_name_offline, [
        Validators.required,

      ]),
      productgroup_name_offline: new FormControl(this.myvisits.productgroup_name_offline, [
        Validators.required,

      ]),
      meeting_remarks_offline: new FormControl(''),
      productgroup_gid: new FormControl(''),

    });

    var api = 'Myvisit/GetProductdropdown'
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.product_list = this.responsedata.product_list1;
    });

    var api6 = 'Myvisit/GetProductGroupdropdown'
    this.service.get(api6).subscribe((result: any) => {
      this.responsedata = result;
      this.product_group_list = this.responsedata.product_group_list;
    });

    this.GetExpiredSummary();
    this.GetTodaySummary();
    this.GetUpcomingSummary();
    console.log(this.reactiveForm);


  }

  GetExpiredSummary() {
    var url = 'Myvisit/GetExpiredSummary'
    this.service.get(url).subscribe((result: any) => {
      $('#expired_list').DataTable().destroy();
      this.responsedata = result;
      this.expired_list = this.responsedata.ExpiredVisit_list;
      //console.log(this.entity_list)
      setTimeout(() => {
        $('#expired_list').DataTable();
      }, 1);
    });
  }
  GetTodaySummary() {
    var url = 'Myvisit/GetTodaySummary'
    this.service.get(url).subscribe((result: any) => {
      $('#today_list').DataTable().destroy();
      this.responsedata = result;
      this.today_list = this.responsedata.Todayvisit_list;
      //console.log(this.entity_list)
      setTimeout(() => {
        $('#today_list').DataTable();
      }, 1);
    });
  }
  GetUpcomingSummary() {
    var url = 'Myvisit/GetUpcomingSummary'
    this.service.get(url).subscribe((result: any) => {
      $('#upcoming_list').DataTable().destroy();
      this.responsedata = result;
      this.upcoming_list = this.responsedata.Upcomingvisit_list;
      //console.log(this.entity_list)
      setTimeout(() => {
        $('#upcoming_list').DataTable();
      }, 1);
    });
  }
  //////close textfields////////
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
  get schedule_remarks() {
    return this.reactiveForm.get('schedule_remarks')!;
  }
  //////postponed textfields////////
  get postponed_date() {
    return this.reactiveFormPostponed.get('postponed_date')!;
  }
  get meeting_time_postponed() {
    return this.reactiveFormPostponed.get('meeting_time_postponed')!;
  }
  //////drop textfields////////
  get drop_reason() {
    return this.reactiveFormDrop.get('drop_reason')!;
  }

  //////online textfields////////
  get date_of_demo_online() {
    return this.reactiveFormonline.get('date_of_demo_online')!;
  }
  get meeting_time_online() {
    return this.reactiveFormonline.get('meeting_time_online')!;
  }
  get contact_person_online() {
    return this.reactiveFormonline.get('contact_person_online')!;
  }
  get technical_assist() {
    return this.reactiveFormonline.get('technical_assist')!;
  }
  get schedule_type() {
    return this.reactiveFormonline.get('schedule_type')!;
  }
  get prosperctive_percentage_online() {
    return this.reactiveFormonline.get('prosperctive_percentage_online')!;
  }
  get product_name() {
    return this.reactiveFormonline.get('product_name')!;
  }
  get productgroup_name() {
    return this.reactiveFormonline.get('productgroup_name')!;
  }
  get demo_remarks() {
    return this.reactiveFormonline.get('demo_remarks')!;
  }

  //////offline textfields////////
  get date_of_visit_offline() {
    return this.reactiveFormoffline.get('date_of_visit_offline')!;
  }
  get meeting_time_offline() {
    return this.reactiveFormoffline.get('meeting_time_offline')!;
  }
  get contact_person_offline() {
    return this.reactiveFormoffline.get('contact_person_offline')!;
  }
  get visited_by() {
    return this.reactiveFormoffline.get('visited_by')!;
  }
  get location_offline() {
    return this.reactiveFormoffline.get('location_offline')!;
  }
  get schedule_type_offline() {
    return this.reactiveFormoffline.get('schedule_type_offline')!;
  }
  get prosperctive_percentage_offline() {
    return this.reactiveFormoffline.get('prosperctive_percentage_offline')!;
  }
  get product_name_offline() {
    return this.reactiveFormoffline.get('product_name_offline')!;
  }
  get productgroup_name_offline() {
    return this.reactiveFormoffline.get('productgroup_name_offline')!;
  }
  get meeting_remarks_offline() {
    return this.reactiveFormoffline.get('meeting_remarks_offline')!;
  }

  ////////close submit///////
  public onsubmitClose(): void {
    if (this.reactiveForm.value.date_of_demo != null && this.reactiveForm.value.meeting_time != null && this.reactiveForm.value.contact_person != null && this.reactiveForm.value.alternate_person != null && this.reactiveForm.value.location != null && this.reactiveForm.value.prosperctive_percentage != '') {

      for (const control of Object.keys(this.reactiveForm.controls)) {
        this.reactiveForm.controls[control].markAsTouched();
      }
      console.log(this.reactiveForm.value);
      var url = 'Myvisit/Postcloselog'
      this.service.post(url, this.reactiveForm.value).pipe().subscribe((result: any) => {
        console.log(this.reactiveForm);

        if (result.status == false) {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.ToastrService.warning(result.message)
          this.GetExpiredSummary();
          this.reactiveForm.reset();
        }
        else {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.reactiveForm.get("date_of_demo")?.setValue(null);
          this.reactiveForm.get("meeting_time")?.setValue(null);
          this.reactiveForm.get("contact_person")?.setValue(null);
          this.reactiveForm.get("altrnate_person")?.setValue(null);
          this.reactiveForm.get("location")?.setValue(null);
          this.reactiveForm.get("prosperctive_percentage")?.setValue('');

          this.ToastrService.success(result.message)
          this.GetExpiredSummary();
          this.reactiveForm.reset();
        }
        this.GetExpiredSummary();
        this.reactiveForm.reset();
      });

    }
    else {
      window.scrollTo({

        top: 0, // Code is used for scroll top after event done

      });
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }


  public onsubmitPostponed(): void {
    if (this.reactiveFormPostponed.value.postponed_date != null && this.reactiveFormPostponed.value.meeting_time_postponed != null) {

      for (const control of Object.keys(this.reactiveFormPostponed.controls)) {
        this.reactiveFormPostponed.controls[control].markAsTouched();
      }
      console.log(this.reactiveFormPostponed.value);
      var url = 'Myvisit/Postpostonedlog'
      this.service.post(url, this.reactiveFormPostponed.value).pipe().subscribe((result: any) => {

        if (result.status == false) {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.ToastrService.warning(result.message)
          this.GetExpiredSummary();
          this.reactiveFormPostponed.reset();
        }
        else {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.reactiveFormPostponed.get("postponed_date")?.setValue(null);
          this.reactiveFormPostponed.get("meeting_time_postponed")?.setValue(null);
          this.ToastrService.success(result.message)
          this.GetExpiredSummary();
          this.reactiveFormPostponed.reset();
        }
        this.GetExpiredSummary();
        this.reactiveFormPostponed.reset();
      });

    }
    else {
      window.scrollTo({

        top: 0, // Code is used for scroll top after event done

      });
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }

  public onsubmitDrop(): void {
    if (this.reactiveFormDrop.value.drop_reason != '') {

      for (const control of Object.keys(this.reactiveFormDrop.controls)) {
        this.reactiveFormDrop.controls[control].markAsTouched();
      }
      console.log(this.reactiveFormDrop.value);
      var url = 'Myvisit/Postdroplog'
      this.service.post(url, this.reactiveFormDrop.value).pipe().subscribe((result: any) => {

        if (result.status == false) {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.ToastrService.warning(result.message)
          this.GetExpiredSummary();
          this.reactiveFormDrop.reset();
        }

        else {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.reactiveFormDrop.get("drop_reason")?.setValue('');
          this.ToastrService.success(result.message)
          this.GetExpiredSummary();
          this.reactiveFormDrop.reset();
        }
        this.GetExpiredSummary();
        this.reactiveFormDrop.reset();
      });

    }
    else {
      window.scrollTo({

        top: 0, // Code is used for scroll top after event done

      });
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }

  openModal1(parameter: string, parameter1: string, parameter2: string) {
    this.reactiveForm.get("leadbank_gid")?.setValue(parameter);
    this.reactiveForm.get("log_gid")?.setValue(parameter1);
    this.reactiveFormPostponed.get("schedulelog_gid")?.setValue(parameter2);
    this.reactiveFormDrop.get("leadbank_gid")?.setValue(parameter);
    this.reactiveFormonline.get("leadbank_gid")?.setValue(parameter);
    this.reactiveFormonline.get("log_gid")?.setValue(parameter1);
    this.reactiveFormoffline.get("leadbank_gid")?.setValue(parameter);


  }
  //////////submit online //////
  public onsubmitonline(): void {
    if (this.reactiveFormonline.value.date_of_demo_online != null &&
      this.reactiveFormonline.value.meeting_time_online != null
      && this.reactiveFormonline.value.contact_person_online != null
      && this.reactiveFormonline.value.technical_assist != null
      && this.reactiveFormonline.value.schedule_type != null
      && this.reactiveFormonline.value.prosperctive_percentage_online != null
      && this.reactiveFormonline.value.product_name != null
      && this.reactiveFormonline.value.productgroup_name != null
    ) {

      for (const control of Object.keys(this.reactiveFormonline.controls)) {
        this.reactiveFormonline.controls[control].markAsTouched();
      }
      console.log(this.reactiveFormonline.value);
      var url = 'Myvisit/Postonline'
      this.service.post(url, this.reactiveFormonline.value).pipe().subscribe((result: any) => {

        if (result.status == false) {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.ToastrService.warning(result.message)
          this.GetExpiredSummary();
          this.reactiveFormonline.reset();
        }
        else {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.reactiveFormonline.get("date_of_demo_online")?.setValue(null);
          this.reactiveFormonline.get("meeting_time_online")?.setValue(null);
          this.reactiveFormonline.get("contact_person_online")?.setValue(null);
          this.reactiveFormonline.get("technical_assist")?.setValue(null);
          this.reactiveFormonline.get("schedule_type")?.setValue(null);
          this.reactiveFormonline.get("prosperctive_percentage_online")?.setValue(null);
          this.reactiveFormonline.get("product_name")?.setValue(null);
          this.reactiveFormonline.get("productgroup_name")?.setValue(null);
         
          this.ToastrService.success(result.message)
          this.GetExpiredSummary();
          this.reactiveFormonline.reset();
        }
        this.GetExpiredSummary();
        this.reactiveFormonline.reset();
      });

    }
    else {
      window.scrollTo({

        top: 0, // Code is used for scroll top after event done

      });
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }

  //////submit offline///////
  public onsubmitoffline(): void {
    if (this.reactiveFormoffline.value.date_of_visit_offline != null &&
      this.reactiveFormoffline.value.meeting_time_offline != null
      && this.reactiveFormoffline.value.contact_person_offline != null
      && this.reactiveFormoffline.value.visited_by != null
      && this.reactiveFormoffline.value.location_offline != null
      && this.reactiveFormoffline.value.schedule_type_offline != null
      && this.reactiveFormoffline.value.prosperctive_percentage_offline != null
      && this.reactiveFormoffline.value.product_name_offline != null
      && this.reactiveFormoffline.value.productgroup_name_offline != null
    ) {

      for (const control of Object.keys(this.reactiveFormoffline.controls)) {
        this.reactiveFormoffline.controls[control].markAsTouched();
      }
      console.log(this.reactiveFormoffline.value);
      var url = 'Myvisit/Postoffline'
      this.service.post(url, this.reactiveFormoffline.value).pipe().subscribe((result: any) => {

        if (result.status == false) {
          this.ToastrService.warning(result.message)
          this.GetExpiredSummary();
          this.reactiveFormoffline.reset();
        }
        else {
          this.reactiveFormoffline.get("date_of_visit_offline")?.setValue(null);
          this.reactiveFormoffline.get("meeting_time_offline")?.setValue(null);
          this.reactiveFormoffline.get("contact_person_offline")?.setValue(null);
          this.reactiveFormoffline.get("visited_by")?.setValue(null);
          this.reactiveFormoffline.get("location_offline")?.setValue(null);
          this.reactiveFormoffline.get("schedule_type_offline")?.setValue(null);
          this.reactiveFormoffline.get("prosperctive_percentage_offline")?.setValue(null);
          this.reactiveFormoffline.get("product_name_offline")?.setValue(null);
          this.reactiveFormoffline.get("productgroup_name_offline")?.setValue(null);

          this.ToastrService.success(result.message)
          this.GetExpiredSummary();
          this.reactiveFormoffline.reset();
        }
        this.GetExpiredSummary();
        this.reactiveFormoffline.reset();
      });

    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }
  onclose() {
    this.reactiveForm.reset();
    this.reactiveFormPostponed.reset();
    this.reactiveFormDrop.reset();
    this.reactiveFormonline.reset();
    this.reactiveFormoffline.reset();
  }
}