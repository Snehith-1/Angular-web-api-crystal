import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { NgxSpinnerService } from 'ngx-spinner';

interface IMailform {
  leadbank_gid: string;
  mail_from: string;
  sub: string;
  to: string;
  body: string;
  bcc: any;
  cc: any;
  reply_to: any;
  displayName: any;
  lastname_edit: any;
  name: any;
  email_address: any;
  customer_type: any;
  customertype_edit: any;
  to_mail: any;

}
@Component({
  selector: 'app-crm-smm-emailmanagement',
  templateUrl: './crm-smm-emailmanagement.component.html',
  styleUrls: ['./crm-smm-emailmanagement.component.scss']
})
export class CrmSmmEmailmanagementComponent {

  response_data: any;
  mailmanagement: any[] = [];
  reactiveForm!: FormGroup;
  mailsummary_list: any;
  mail: any;
  from_mail: any;
  subject: any;
  body_content: any;
  mailcount_list: any[] = [];
  responsedata: any;
  filteredData: any;
  clicktotal_count: any;
  deliverytotal_count: any;
  opentotal_count: any;
  mailevent_list: any;
  file1!: FileList;
  file: any;
  AutoIDkey: any;
  formDataObject: FormData = new FormData();
  allattchement: any[] = [];
  file_name: any;
  mailform!: IMailform;
  mailview_list: any;
  mailopen: boolean = true;
  mailreply: boolean = true;
  attachment_type: any; 
  created_date: any;
  created_time: any;
  openDiv: boolean = false;
  direction: any;
  document_path: any;
  to_address: any;
  leadbank: any;
  mailmanagement_gid: any;
  sending_domain: any;
  receiving_domain: any;
  reactiveFormContactEdit!: FormGroup;
  displayName: any;
  lastname_edit: any;
  email_address: any;
  customer_type: any;
  customertype_edit: any;
  customertype_list: any[] = [];
  isReadOnly: boolean = true;
  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService, private NgxSpinnerService: NgxSpinnerService) { 
    this.mailform = {} as IMailform;
  }
  ngOnInit(): void {
    this.GetMailSummary();
    this.GetMailEventCount();
    // this.GetMailEventClick();
    // this.GetMailEventDelivery();
    // this.GetMailEventOpen();
    this.reactiveForm = new FormGroup({

      sub: new FormControl(this.mailform.sub, [
        Validators.required,
        Validators.pattern("^(?!\s*$).+"),
       
      ]),

      file: new FormControl(''),
      body: new FormControl(this.mailform.body, [
        Validators.required,
       
      ]),
      bcc: new FormControl(''),
      cc: new FormControl(''),
      to_mail: new FormControl(this.mailform.to_mail, [
        Validators.required,
        Validators.pattern("^(?!\s*$).+"),
       
      ]),
      mail_from: new FormControl(''),
      leadbank_gid: new FormControl(''),
    });
    this.reactiveFormContactEdit = new FormGroup({
      email_address: new FormControl(''),

      displayName: new FormControl(''),
      customer_type: new FormControl(''),
      customertype_edit: new FormControl(''),

    });

    // //customer type dropdown
    var api3 = 'MailCampaign/GetCustomerTypeSummary'
    this.service.get(api3).subscribe((result: any) => {
      this.responsedata = result;
      this.customertype_list = result.customertype_list3;
    });

  }

  GetMailView(params: any, params1: any) {
    const secretKey = 'storyboarderp';
    //console.log(params)
    const mailmanagement_gid = AES.encrypt(params, secretKey).toString();
    const leadbank_gid = AES.encrypt(params1, secretKey).toString();

    this.router.navigate(['/crm/CrmSmmSendmail', mailmanagement_gid, leadbank_gid])
  }
  GetMailSummary() {
    // this.NgxSpinnerService.show();
    var api = 'MailCampaign/GetMailSummary';
    this.service.get(api).subscribe((result: any) => {
      this.response_data = result;
      this.mailsummary_list = this.response_data.mailsummary_list;
      this.sending_domain = this.mailsummary_list[0].sending_domain;
      this.receiving_domain = this.mailsummary_list[0].receiving_domain;
      // this.NgxSpinnerService.hide();
      this.reactiveForm.patchValue({
        mail_from: this.mailsummary_list[0]?.sending_domain || '',

      });
      setTimeout(() => {
        $('#mail').DataTable();
      }, 1);
    });

  }

  get mail_from() {
    return this.reactiveForm.get('mail_from')!;
  }
  get to_mail() {
    return this.reactiveForm.get('to_mail')!;
  }
  get sub() {
    return this.reactiveForm.get('sub')!;
  }
  
  get body() {
    return this.reactiveForm.get('body')!;
  }
  get reply_to() {
    return this.reactiveForm.get('reply_to')!;
  }
  get cc() {
    return this.reactiveForm.get('cc')!;
  }
  get bcc() {
    return this.reactiveForm.get('bcc')!;
  }


  onChange2($event: any): void {
    this.file1 = $event.target.files;

    if (this.file1 != null && this.file1.length !== 0) {
      for (let i = 0; i < this.file1.length; i++) {
        this.AutoIDkey = this.generateKey();
        this.formDataObject.append(this.AutoIDkey, this.file1[i]);
        this.file_name = this.file1[i].name;
        this.allattchement.push({
          AutoID_Key: this.AutoIDkey,
          file_name: this.file1[i].name
        });
        console.log(this.file1[i]);
      }
    }

    //console.log(this.files[i]);
  }

  generateKey(): string {

    return `AutoIDKey${new Date().getTime()}`;
  }




  public onadd(): void {
    console.log(this.reactiveForm.value)
    this.mailform = this.reactiveForm.value;
    if (this.mailform.mail_from != null && this.mailform.sub != null && this.mailform.to_mail != null) {
      const allattchement = "" + JSON.stringify(this.allattchement) + "";
      if (this.file1 != null && this.file1 != undefined) {
        this.formDataObject.append("filename", allattchement);
        this.formDataObject.append("mail_from", this.mailform.mail_from);
        this.formDataObject.append("sub", this.mailform.sub);
        this.formDataObject.append("to", this.mailform.to_mail);
        this.formDataObject.append("body", this.mailform.body);
        this.formDataObject.append("bcc", this.mailform.bcc);
        this.formDataObject.append("cc", this.mailform.cc);
        this.formDataObject.append("leadbank_gid", this.mailform.leadbank_gid);

        this.NgxSpinnerService.show();
        var api7 = 'MailCampaign/MailUpload'
        this.service.post(api7, this.formDataObject).subscribe((result: any) => {
          this.responsedata = result;
          if (result.status == false) {
            this.ToastrService.warning(result.message)
            this.reactiveForm.reset();
            this.GetMailSummary();

          }
          else {
            window.scrollTo({

              top: 0, // Code is used for scroll top after event done

            });
            // this.router.navigate(['/crm/CrmSmmEmailmanagement']);
            this.ToastrService.success(result.message)
            this.reactiveForm.reset();
            this.GetMailSummary();
          }
        });
        this.NgxSpinnerService.hide();
      }
      else {
        this.NgxSpinnerService.show();
        var api7 = 'MailCampaign/MailSend'
        //console.log(this.file)
        this.service.post(api7, this.mailform).subscribe((result: any) => {

          if (result.status == false) {
            window.scrollTo({

              top: 0, // Code is used for scroll top after event done

            });
            this.ToastrService.warning(result.message)
            this.reactiveForm.reset();
            this.GetMailSummary();
          }
          else {
            window.scrollTo({

              top: 0, // Code is used for scroll top after event done

            });
            // this.router.navigate(['/crm/CrmSmmEmailmanagement']);
            this.ToastrService.success(result.message)
            this.reactiveForm.reset();
            this.GetMailSummary();
          }
          this.responsedata = result;
        });
        this.NgxSpinnerService.hide();
      }
    }

    else {
      window.scrollTo({

        top: 0, // Code is used for scroll top after event done

      });
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
      this.reactiveForm.reset();
      this.GetMailSummary();
    }

    return;

  }
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '120px',
    minHeight: '0rem',
    width: '630px',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
  };
  config_compose_mail: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '120px',
    minHeight: '0rem',
    width: '1013px',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',

  };

  GetMailEventCount() {
    var api2 = 'MailCampaign/GetMailEventCountSummary'
    this.service.get(api2).subscribe((result: any) => {
      this.mailcount_list = result.mailcount_list;
      this.clicktotal_count = this.mailcount_list[0].clicktotal_count;
      this.opentotal_count = this.mailcount_list[0].opentotal_count;
      this.deliverytotal_count = this.mailcount_list[0].deliverytotal_count;
    });
  }
  // GetMailEventOpen() {
  //   var api = 'MailCampaign/GetMailEventOpen'
  //   this.service.get(api).subscribe((result: any) => {
  //     this.response_data = result;
  //     this.mailevent_list = this.response_data.mailevent_list;
  //   });
  // }
  // GetMailEventClick() {
  //   var api = 'MailCampaign/GetMailEventClick'
  //   this.service.get(api).subscribe((result: any) => {
  //     this.response_data = result;
  //     this.mailevent_list = this.response_data.mailevent_list;
  //   });
  // }
  // GetMailEventDelivery() {
  //   var api = 'MailCampaign/GetMailEventDelivery'
  //   this.service.get(api).subscribe((result: any) => {
  //     this.response_data = result;
  //     this.mailevent_list = this.response_data.mailevent_list;
  //   });
  // }

  onback() {
    this.router.navigate(['/crm/CrmSmmMailcampaignsummary']);

  }


  openModaledit(params: any) {
    this.reactiveFormContactEdit.get("email_address")?.setValue(params);
    this.reactiveFormContactEdit.get("displayName")?.setValue(this.displayName);
    // this.reactiveFormContactEdit.get("lastname_edit")?.setValue(this.lastname_edit);
    this.reactiveFormContactEdit.get("customertype_edit")?.setValue(this.customertype_edit);
    this.reactiveFormContactEdit.get("customer_type")?.setValue(this.customer_type);
  }

  public onupdatecontact(): void {
    if (this.reactiveFormContactEdit.value.email_address != null && this.reactiveFormContactEdit.value.displayName != null && this.reactiveFormContactEdit.value.customer_type != null) {
      for (const control of Object.keys(this.reactiveFormContactEdit.controls)) {
        this.reactiveFormContactEdit.controls[control].markAsTouched();
      }
      this.reactiveFormContactEdit.value;
      var url = 'MailCampaign/Addaslead'
      this.service.post(url, this.reactiveFormContactEdit.value).pipe().subscribe((result: any) => {
        this.responsedata = result;
        if (result.status == false) {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.ToastrService.warning(result.message)
          this.reactiveFormContactEdit.reset();

        }
        else {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.ToastrService.success(result.message)
          this.reactiveFormContactEdit.reset();

        }
        this.reactiveFormContactEdit.reset();
        this.GetMailSummary();

      });

    }
    else {
      window.scrollTo({

        top: 0, // Code is used for scroll top after event done

      });
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }


}
