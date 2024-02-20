import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';

import { Subscription, Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
@Component({
  selector: 'app-crm-campaign-mailmanagementsummary',
  templateUrl: './crm-campaign-mailmanagementsummary.component.html',
  styleUrls: ['./crm-campaign-mailmanagementsummary.component.scss']
})
export class CrmCampaignMailmanagementsummaryComponent {
  response_data: any;
  mailmanagement: any[] = [];
  reactiveForm!: FormGroup;
  parameterValue1: any;
  mailView_list: any;
  mail: any;
  from_mail: any;
  to_mail: any;
  subject: any;
  body_content: any;


  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService,) { }
  ngOnInit(): void {
    this.GetMailSummary();
  }
  GetMailSummary() {

    var api = 'Mailmanagement/GetMailSummary';
    this.service.get(api).subscribe((result: any) => {
      $('#mail').DataTable().destroy();

      this.response_data = result;
      this.mailmanagement = this.response_data.mail_list;
      setTimeout(() => {
        $('#mail').DataTable();
      }, 1);
    });

  }
  
  onadd() {
    this.router.navigate(['/crm/CrmCampaignMailmanagement'])

  }
  popmodal(parameter: string) {
    this.parameterValue1 = parameter

    this.to_mail = this.parameterValue1.to;

    this.from_mail = this.parameterValue1.from;

    this.subject = this.parameterValue1.sub;

    this.body_content = this.parameterValue1.body;

  }
}
