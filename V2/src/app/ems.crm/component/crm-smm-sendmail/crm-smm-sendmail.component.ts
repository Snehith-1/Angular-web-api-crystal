import { Component } from '@angular/core';
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
  customertype_edit: any;

}
@Component({
  selector: 'app-crm-smm-sendmail',
  templateUrl: './crm-smm-sendmail.component.html',
  styleUrls: ['./crm-smm-sendmail.component.scss']
})
export class CrmSmmSendmailComponent {
  response_data: any;
  mailview_list: any;
  mail: any;
  from_mail: any;
  to_mail: any;
  subject: any;
  body_content: any;
  mail_froms: any;
  sub: any;
  body: any;
  created_date: any;
  mailmanagement_gid: any;
  direction: any;
  reactiveForm!: FormGroup;
  file1!: FileList;
  file: any;
  AutoIDkey: any;
  formDataObject: FormData = new FormData();
  allattchement: any[] = [];
  file_name: any;
  attachment_type: any; 
  created_time: any;
  document_path: any;
  to_address: any;
  leadbank: any;
  sending_domain: any;
  receiving_domain: any;
  responsedata: any;
  mailform!: IMailform;
  mailsummary_list: any;
  leadbank_gid: any;
  // deencryptedParam1:any;
  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService,private NgxSpinnerService: NgxSpinnerService) { }
  ngOnInit(): void {
  
    const mailmanagement_gid = this.route.snapshot.paramMap.get('mailmanagement_gid');
    const leadbank_gid = this.route.snapshot.paramMap.get('leadbank_gid');
    this.mailmanagement_gid = mailmanagement_gid;
    this.leadbank_gid=leadbank_gid;
    const secretKey = 'storyboarderp';
    const decryptedParam = AES.decrypt(this.mailmanagement_gid,secretKey).toString(enc.Utf8);
    const decryptedParam1 = AES.decrypt(this.leadbank_gid,secretKey).toString(enc.Utf8);
    
   
    this. GetMailView(decryptedParam);  
    this.reactiveForm = new FormGroup({

      sub: new FormControl(''),

      file: new FormControl(''),
      body: new FormControl(''),
      bcc: new FormControl(''),
      cc: new FormControl(''),
      // reply_to: new FormControl('receiving_domain'),
      to: new FormControl(''),
      mail_from: new FormControl(''),
      leadbank_gid: new FormControl(''),
    });
    this.GetMailSummary();
    this.reactiveForm.patchValue({
      leadbank_gid:decryptedParam1,
    
    });
  }
  GetMailView(mailmanagement_gid:any) {
    this.NgxSpinnerService.show();
    var url = 'MailCampaign/GetMailView';
    let param = {
      mailmanagement_gid : mailmanagement_gid 
    }
    this.service.getparams(url,param).subscribe((result: any) => {
      this.mailview_list = result.mailsummary_list;
      this.mail_froms = this.mailview_list[0].mail_from;
      this.to_mail = this.mailview_list[0].to;
      this.sub = this.mailview_list[0].sub;
      this.body = this.mailview_list[0].body;
      this.created_date = this.mailview_list[0].created_date;
      this.direction = this.mailview_list[0].direction;

      this.reactiveForm.patchValue({
        to: this.to_mail,
      });
    });
    this.NgxSpinnerService.hide();
}

GetMailSummary() {
  this.NgxSpinnerService.show();
  var api = 'MailCampaign/GetMailSummary';
  this.service.get(api).subscribe((result: any) => {
    this.response_data = result;
    this.mailsummary_list = this.response_data.mailsummary_list;
    this.sending_domain = this.mailsummary_list[0].sending_domain;
    this.receiving_domain = this.mailsummary_list[0].receiving_domain;
    this.NgxSpinnerService.hide();
    this.reactiveForm.patchValue({
      mail_from: this.mailsummary_list[0]?.sending_domain || '',
    
    });
    setTimeout(() => {
      $('#mail').DataTable();
    }, 1);
  });
}

onback()
{
  this.router.navigate(['/crm/CrmSmmEmailmanagement']);
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
  if (this.mailform.mail_from != null && this.mailform.sub != null && this.mailform.to != null) {
    const allattchement = "" + JSON.stringify(this.allattchement) + "";
    if (this.file1 != null && this.file1 != undefined) {
      this.formDataObject.append("filename", allattchement);
      this.formDataObject.append("mail_from", this.mailform.mail_from);
      this.formDataObject.append("sub", this.mailform.sub);
      this.formDataObject.append("to", this.mailform.to);
      this.formDataObject.append("body", this.mailform.body);
      this.formDataObject.append("bcc", this.mailform.bcc);
      this.formDataObject.append("cc", this.mailform.cc);
      this.formDataObject.append("leadbank_gid", this.mailform.leadbank_gid);

      var api7 = 'MailCampaign/MailUpload'
      this.service.post(api7, this.formDataObject).subscribe((result: any) => {
        this.responsedata = result;
        if (result.status == false) {
          this.ToastrService.warning(result.message)
        }
        else {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          // this.router.navigate(['/crm/CrmSmmEmailmanagement']);
          this.ToastrService.success(result.message)
        }
      });
    }
    else {
      var api7 = 'MailCampaign/MailSend'
      //console.log(this.file)
      this.service.post(api7, this.mailform).subscribe((result: any) => {

        if (result.status == false) {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.ToastrService.warning(result.message)
        }
        else {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          // this.router.navigate(['/crm/CrmSmmEmailmanagement']);
          this.ToastrService.success(result.message)
        }
        this.responsedata = result;
      });
    }
  }

  else {
    window.scrollTo({

      top: 0, // Code is used for scroll top after event done

    });
    this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
  }
  return;

}

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

}
