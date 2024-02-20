import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ActivatedRoute } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';

import { TableModule } from 'primeng/table';
import { Table } from 'primeng/table';
import { constrainPoint } from '@fullcalendar/core/internal';

export class IAssign {
  mailsendchecklist: any[] = [];
  mailmanagement_gid: string = "";
  temp_mail_gid: string = "";
  leadbank_gid: string = "";

}
export interface Customer {

  leadbank_gid?: any;
  temp_mail_gid?: any | null;
  email?: any | null;
  created_date?: any | null;
  default_phone?: any | null;
  customer_type?: any | null;
  names?: any | null;
  address1?: any | null;
  date?: any | null;
  to_mail?: any | null;
  status_delivery?: any | null;
  status_open?: any | null;
  status_click?: any | null;
  address2?: any | null;
  city?: any | null;
  state?: any | null;
  sub?: any | null;
  to?: any | null;
  body?: any | null;
  direction?: any | null;
  source_gid?: any | null;
  source_name?: any | null;
  lead_status?: any | null;
  template_name?: any | null;

  message?: any | null;
  status?: boolean | null;

}
/////////////


@Component({
  selector: 'app-crm-smm-mailcampaignsend',
  templateUrl: './crm-smm-mailcampaignsend.component.html',
  styleUrls: ['./crm-smm-mailcampaignsend.component.scss']
})
export class CrmSmmMailcampaignsendComponent {
  mailtemplate_list: any;
  responsedata: any;
  mailtemplatesendsummary_list: any;
  temp_mail_gid: any;
  CurObj: IAssign = new IAssign();
  selection = new SelectionModel<IAssign>(true, []);
  pick: Array<any> = [];
  mailsummarylist: any[] = [];
  reactiveForm: any;
  template_name: any;
  mailtemplateview_list: any;
  mail_from: any;
  sub: any;
  body: any;
  created_date: any;
  filteredData: IAssign[] = [];
  //////////////demo////////////////////
  customer!: Customer;
  selectedCustomer: Customer[] = [];

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private router: ActivatedRoute, private route: Router, private NgxSpinnerService: NgxSpinnerService) {

  }
  ngOnInit(): void {
    const temp_mail_gid = this.router.snapshot.paramMap.get('temp_mail_gid');
    this.temp_mail_gid = temp_mail_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.temp_mail_gid, secretKey).toString(enc.Utf8);
    this.temp_mail_gid = deencryptedParam;
    this.GetMailTemplateSendSummary();
    this.GetMailView(deencryptedParam);
  }
  onGlobalFilterChange(event: Event, dt: Table): void {
    const inputValue = (event.target as HTMLInputElement).value;
    dt.filterGlobal(inputValue, 'contains');
  }
  onGlobalFilterChange1(event: Event, dt: Table): void {
    const inputValue = (event.target as HTMLInputElement).value;
    dt.filterGlobal(inputValue, 'contains');
  }
 
 
  
  GetMailTemplateSendSummary(): void {
    this.NgxSpinnerService.show();
    let param = {
      temp_mail_gid: this.temp_mail_gid,
    }

    var api = 'MailCampaign/MailTemplateSendSummary';
    this.service.getparams(api, param).subscribe((result: any) => {
      this.responsedata = result;
      this.mailtemplatesendsummary_list = this.responsedata.mailtemplatesendsummary_list;
      this.template_name = this.mailtemplatesendsummary_list[0].template_name;
      this.NgxSpinnerService.hide();
    });
  }

  public onsend(): void {
     this.CurObj.temp_mail_gid = this.temp_mail_gid;
    this.CurObj.mailsendchecklist = this.selectedCustomer;
    // console.log(this.CurObj)
    if (this.CurObj.mailsendchecklist.length != 0 ) {
      var url = 'MailCampaign/SendTemplate'
      this.service.post(url,this.CurObj).subscribe((result: any) => {

        if (result.status == false) {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.ToastrService.warning(result.message)
          this.GetMailTemplateSendSummary();
          this.reactiveForm.reset();

        }
        else {
          window.scrollTo({

            top: 0, // Code is used for scroll top after event done

          });
          this.ToastrService.success(result.message)
          this.route.navigate(['/crm/CrmSmmMailcampaignsummary'])
          this.reactiveForm.reset();

        }
        this.GetMailTemplateSendSummary();
        this.reactiveForm.reset();

      });

    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }

  }

  onback() {
    this.route.navigate(['/crm/CrmSmmMailcampaignsummary']);

  }


  GetMailView(temp_mail_gid: any) {
    this.NgxSpinnerService.show();
    var url = 'MailCampaign/MailTemplateView';
    let param = {
      temp_mail_gid: temp_mail_gid
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      this.mailtemplateview_list = result.mailtemplate_list;
      this.mail_from = this.mailtemplateview_list[0].mail_from;
      this.template_name = this.mailtemplateview_list[0].template_name
      this.body = this.mailtemplateview_list[0].body;
      this.sub = this.mailtemplateview_list[0].sub;
      this.created_date = this.mailtemplateview_list[0].created_date;
      this.NgxSpinnerService.hide();
    });
  }

}

