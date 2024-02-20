import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-crm-smm-mailcampaignsendstatus',
  templateUrl: './crm-smm-mailcampaignsendstatus.component.html',
  styleUrls: ['./crm-smm-mailcampaignsendstatus.component.scss']
})
export class CrmSmmMailcampaignsendstatusComponent {
  responsedata: any;
  mailtemplatesendsummary_list: any;
  filteredData: any;
  mailevent_list: any;
  mailcount_list: any[] = [];
  clicktotal_count:any;
  deliverytotal_count:any;
  opentotal_count:any;
  temp_mail_gid:any;
  to_mail: any;
  sub: any;
  status_delivery: any;
  body: any;
  date: any;
  status_open: any;
  status_click: any;
  template_name: any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private router: ActivatedRoute, private route: Router, private NgxSpinnerService: NgxSpinnerService) {
 
  }

  ngOnInit(): void {
    const temp_mail_gid = this.router.snapshot.paramMap.get('temp_mail_gid');
    this.temp_mail_gid = temp_mail_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.temp_mail_gid, secretKey).toString(enc.Utf8);
    this.temp_mail_gid = deencryptedParam;
    this.GetMailTemplateSendSummary();

  }
  GetMailTemplateSendSummary() {
    //this.NgxSpinnerService.show();
    let param = {
      temp_mail_gid: this.temp_mail_gid,
    }
    var api = 'MailCampaign/MailSendStatusSummary';
    this.service.getparams(api,param).subscribe((result: any) => {
      this.responsedata = result;
      this.mailtemplatesendsummary_list = this.responsedata.mailtemplatesendsummary_list;
      this.template_name=this.mailtemplatesendsummary_list[0].template_name;
      // this.sub=this.mailtemplatesendsummary_list[0].sub;
      // this.body=this.mailtemplatesendsummary_list[0].body;
      // this.date=this.mailtemplatesendsummary_list[0].date;
      // this.status_open=this.mailtemplatesendsummary_list[0].status_open;
      // this.status_delivery=this.mailtemplatesendsummary_list[0].status_delivery;
      // this.status_click=this.mailtemplatesendsummary_list[0].status_click;

      //this.NgxSpinnerService.hide();
      setTimeout(() => {
        $('#mailtemplatesendsummary_list').DataTable();
      }, 1);
    });

  }

  onback() {
    this.route.navigate(['/crm/CrmSmmMailcampaignsummary']);
  }

  popmodal(product_gid: any) {
    var params = {
      product_gid: product_gid
    }
    debugger
    var url = 'RskMstProductManagement/Getrm';
    
 
    
  }
 

}
