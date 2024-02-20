import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ActivatedRoute } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
export class ISendSms {
  smsleadcustomerdetails_list: string[] = [];
  mailmanagement_gid: string = "";
  id: string = "";

}
@Component({
  selector: 'app-crm-smm-smscampaignsend',
  templateUrl: './crm-smm-smscampaignsend.component.html',
  styleUrls: ['./crm-smm-smscampaignsend.component.scss']
})
export class CrmSmmSmscampaignsendComponent {
  CurObj: ISendSms = new ISendSms();
  responsedata: any;
  sms_id: any;
  selection = new SelectionModel<ISendSms>(true, []);
  pick: Array<any> = [];
  smsleadcustomerdetailslist: any;
  id:any;

  constructor(private formBuilder: FormBuilder,private ToastrService: ToastrService,private route:Router,private router:ActivatedRoute,public service :SocketService, private NgxSpinnerService: NgxSpinnerService) { }

  ngOnInit(): void {
   const camp_id =this.router.snapshot.paramMap.get('id');
    this.sms_id= camp_id;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.sms_id,secretKey).toString(enc.Utf8);
    this.id = deencryptedParam;
    console.log(deencryptedParam)
     this.GetSmsCampaignSummary();
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.smsleadcustomerdetailslist.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.smsleadcustomerdetailslist.forEach((row: ISendSms) => this.selection.select(row));
  }
  GetSmsCampaignSummary() {
    this.NgxSpinnerService.show();
    var api = 'SmsCampaign/SmsLeadCustomerDetails';
    this.service.get(api).subscribe((result: any) => {
      // $('#mailtemplatesendsummary_list').DataTable().destroy();
      this.responsedata = result;
      this.smsleadcustomerdetailslist = this.responsedata.smsleadcustomerdetails_list;
      this.NgxSpinnerService.hide();
      setTimeout(() => {
        $('#smsleadcustomerdetailslist').DataTable();
      }, 1);
    });

  }
  public onsend(): void {
    this.selection.selected.forEach(s => s.id);
    this.pick = this.selection.selected
    let list = this.pick
    this.CurObj.id = this.id;
    this.CurObj.smsleadcustomerdetails_list = list
    //console.log(this.CurObj)
    if (this.CurObj.smsleadcustomerdetails_list.length != 0) {
      var url = 'SmsCampaign/Smssendtolead'
      this.service.post(url,this.CurObj).subscribe((result: any) => {
 
        if (result.status == false) {
          window.scrollTo({
 
            top: 0, // Code is used for scroll top after event done
 
          });
          this.ToastrService.warning(result.message)
          this.GetSmsCampaignSummary();
         
        }
        else {
          window.scrollTo({
 
            top: 0, // Code is used for scroll top after event done
 
          });
          this.ToastrService.success(result.message)
          this.route.navigate(['/crm/CrmSmmSmscampaign'])
        
 
        }
        this.GetSmsCampaignSummary();
        
 
      });
 
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
 
  }
  onback() {
    this.route.navigate(['/crm/CrmSmmSmscampaign']);

  }
}
