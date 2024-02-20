import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';

import { Subscription, Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
interface Ileadbank {
  country_name: string;
  region_name: string;
  leadbank_gid: string;
  leadbankcontact_gid: string;
  area_code2: string;
  phone2: string;
  country_code1: string;
  area_code1: string;
  phone1: string;
  fax_area_code: string;
  fax_country_code: string;
  fax: string;
  designation: string;
  email: string;
  mobile: string;
  leadbankcontact_name: string;
  leadbankbranch_name: string;
  address1: string;
  address2: string;
  state: string;
  city: string;
  pincode: string;
}

@Component({
  selector: 'app-crm-trn-leadbankcontact',
  templateUrl: './crm-trn-leadbankcontact.component.html',
  styleUrls: ['./crm-trn-leadbankcontact.component.scss']
})
export class CrmTrnLeadbankcontactComponent implements OnInit{
  
  leadbank!: Ileadbank;
  leadbank_gid: any;
  leadbankcontact_gid: any;
  leadaddbranch_list: any[] = [];
  response_data: any;
  branch_list: any;
  branch_list1: any[] = [];
  contactform: FormGroup<{}> | any;
  country_list: any[] = [];
  selectedCountry: any;
  selectedRegion: any;

  constructor(private fb: FormBuilder, private router: ActivatedRoute, private route: Router,
    private service: SocketService, private ToastrService: ToastrService) {

    this.leadbank = {} as Ileadbank;
  }
  ngOnInit(): void {

    const leadbank_gid = this.router.snapshot.paramMap.get('leadbank_gid');

    this.leadbank_gid = leadbank_gid;

    const secretKey = 'storyboarderp';

    const deencryptedParam = AES.decrypt(this.leadbank_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.leadbank_gid = deencryptedParam;

    


   // this.GetleadbranchaddSummary(deencryptedParam);
    this.GetleadbankcontactaddSummary(deencryptedParam);
    this.Getbranchdropdown();
    this.contactform = new FormGroup({
      leadbankcontact_name: new FormControl(this.leadbank.leadbankcontact_name, [
        Validators.required,
      ]),

      leadbankbranch_name: new FormControl('', ),
      address2: new FormControl(''),
      state: new FormControl(''),
      pincode: new FormControl(''),
      city: new FormControl(this.leadbank.city, [
        Validators.required,
      ]),
      region_name: new FormControl(this.leadbank.region_name, [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(250),
      ]),

      mobile: new FormControl(this.leadbank.mobile, [
        Validators.required,
        Validators.maxLength(10),
      ]),
      fax: new FormControl(this.leadbank.fax, [
        Validators.required,
        Validators.maxLength(10),
      ]),
      phone1: new FormControl(this.leadbank.phone1, [
        Validators.required,
        Validators.maxLength(10),
      ]),
      phone2: new FormControl(this.leadbank.phone2, [
        Validators.required,
        Validators.maxLength(10),
      ]),
      designation: new FormControl(this.leadbank.designation, [
        Validators.required,
        Validators.minLength(1),
      ]),
      country_name: new FormControl(this.leadbank.country_name, [
        Validators.minLength(1),
      ]),
      address1: new FormControl(this.leadbank.address1, [
        Validators.maxLength(1000),
      ]),
      email: new FormControl(this.leadbank.email, [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(250),
        Validators.pattern('^([a-z0-9-]+|[a-z0-9-]+([.][a-z0-9-]+)*)@([a-z0-9-]+\.[a-z]{2,20}(\.[a-z]{2})?|\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\]|localhost)$')
      ]),
      
      leadbank_gid: new FormControl(deencryptedParam),
      leadbankcontact_gid: new FormControl(''),
    });
    
   
    var api2 = 'registerlead/Getcountrynamedropdown'
    this.service.get(api2).subscribe((result: any) => {
      $('#product').DataTable().destroy();
      this.response_data = result;
      console.log("countrydropdown value:"+this.response_data);
      
      this.country_list = this.response_data.Getcountrynamedropdown;
      setTimeout(() => {
        $('#product').DataTable();
      }, 1);
    });

  }
  Getbranchdropdown() {
    let param = {
      leadbank_gid: this.leadbank_gid
    }
    var api2 = 'Leadbank/Getbranchdropdown'
    this.service.getparams(api2, param).subscribe((result: any) => {

      this.response_data = result;
      this.branch_list1 = this.response_data.leadbankcontact_list;
      setTimeout(() => {
        $('#product').DataTable();
      }, 1);
    });
  }
  get leadbank_name() {
    return this.contactform.get('leadbank_name')!;
  }

  GetleadbankcontactaddSummary(leadbank_gid: any) {
    let param = {
      leadbank_gid: leadbank_gid
    }
    console.log(this.Getbranchdropdown)
    var api = 'Leadbank/GetleadbankcontactaddSummary';
    this.service.getparams(api, param).subscribe((result: any) => {
      $('#leadaddbranch_list').DataTable().destroy();
      this.response_data = result;
      this.leadaddbranch_list = this.response_data.leadbank_list;
      setTimeout(() => {
        $('#leadaddbranch_list').DataTable();
      }, 1);
    });
  }

  onedit(param1: any, param2: any) {
    const secretKey = 'storyboarderp';
    console.log(param1);
    console.log(param2);
    const leadbank_gid = AES.encrypt(param1, secretKey).toString();
    const leadbankcontact_gid = AES.encrypt(param2, secretKey).toString();
    this.route.navigate(['/crm/CrmTrnLeadbankcontactEdit', leadbank_gid, leadbankcontact_gid]);
  }


  onadd() {
    this.leadbank = this.contactform.value;

    if (this.leadbank.leadbankbranch_name != null && this.leadbank.leadbankcontact_name != null
      && this.leadbank.mobile != null && this.leadbank.email != null) 
    {
      console.log(this.contactform.value)
      //  this.leadbank.region_name != null,this.leadbank.country_name != null  &&
      var api = 'Leadbank/Postleadbankcontactadd';
      this.service.post(api, this.contactform.value).subscribe((result: any) => {
        console.log(result);
        if (result.status == false) {
          window.location.reload()
          this.ToastrService.warning(result.message)

        }
        else {
          this.GetleadbankcontactaddSummary(this.leadbank_gid);
          // this.route.navigate(['/crm/CrmTrnLeadBankbranch',this.leadbank_gid]);
          window.location.reload()
          this.ToastrService.success(result.message)
        }
      });
    }
  }

  
}