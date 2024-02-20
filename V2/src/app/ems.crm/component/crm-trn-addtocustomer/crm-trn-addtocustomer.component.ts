import { Component, ElementRef, Renderer2 } from '@angular/core';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
interface IAddtoCustomer {
  leadbank_gid: string;
  leadbank_name: string;
  leadbankcontact_name: string;
  designation: string;
  email: string;
  mobile: string;
  leadbank_address1: string;
  leadbank_address2: string;
  phone1: string;
  fax: string;
  leadbank_pin: string;
  country_name: string;
  region_name: string;
  company_website: string;
  currency_code: string;
  gst_no:string;
}

@Component({
  selector: 'app-crm-trn-addtocustomer',
  templateUrl: './crm-trn-addtocustomer.component.html',
  styleUrls: ['./crm-trn-addtocustomer.component.scss']
})
export class CrmTrnAddtocustomerComponent {
  addtocusotmer!: IAddtoCustomer;
  reactiveForm!: FormGroup;
  regionname_list: any[] = [];
  designation_list: any[] = [];
  country_list: any[] = [];
  currencycodelist: any[] = [];
  Email_Address: any;
  responsedata: any;
  selectedregion_name: any;
  selectedcountry_name: any;

  leadbank_gid: any;
  leadbankcustomer_list: any;
  
  constructor(private renderer: Renderer2, private el: ElementRef, public service: SocketService, private ToastrService: ToastrService, private route: Router,private router: ActivatedRoute) {
    this.addtocusotmer = {} as IAddtoCustomer;
  }

  ngOnInit(): void {
    const leadbank_gid = this.router.snapshot.paramMap.get('leadbank_gid');
    // console.log(termsconditions_gid)
    this.leadbank_gid = leadbank_gid;

    const secretKey = 'storyboarderp';

    const deencryptedParam = AES.decrypt(this.leadbank_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam);

    

    this.GetleadbankeditSummary(deencryptedParam)

    this.reactiveForm = new FormGroup({
      leadbank_name: new FormControl(this.addtocusotmer.leadbank_name, [
        Validators.required,
      ]),
      leadbankcontact_name: new FormControl(this.addtocusotmer.leadbankcontact_name, [
        Validators.required,
      ]),
      designation: new FormControl(this.addtocusotmer.designation, [
        Validators.required,
        Validators.minLength(1),
      ]),
      email: new FormControl(this.addtocusotmer.email, [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(250), Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')
      ]),
      mobile: new FormControl(this.addtocusotmer.mobile, [
        Validators.required,
        Validators.maxLength(10),
      ]),
      leadbank_address1: new FormControl(this.addtocusotmer.leadbank_address1, [
        Validators.maxLength(1000),
      ]),
      leadbank_address2: new FormControl(''),
      phone1: new FormControl(this.addtocusotmer.phone1, [
        Validators.required,
        Validators.maxLength(10),
      ]),
      fax: new FormControl(this.addtocusotmer.fax, [
        Validators.required,
        Validators.maxLength(10),
      ]),
      fax_area_code : new FormControl(''),
      fax_country_code : new FormControl(''),
      leadbank_pin: new FormControl(this.addtocusotmer.leadbank_pin, [
        Validators.pattern('r^\d{6}$'),
      ]),
      country_name: new FormControl(this.addtocusotmer.country_name, [
        Validators.minLength(1),
      ]),
      region_name: new FormControl(this.addtocusotmer.region_name, [
        Validators.required,
        Validators.minLength(1),
      ]),
      company_website: new FormControl(this.addtocusotmer.company_website, [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(250),
        Validators.pattern('^(https?://)?([a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4})(/[a-zA-Z0-9%.-]*)*(\\?[a-zA-Z0-9.-_]+=[a-zA-Z0-9.-_]+)*/?$')
      ]),
      currency_code:  new FormControl(''),
      gst_no: new FormControl(this.addtocusotmer.gst_no, [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(15),
        Validators.pattern('^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$'),
      ]),
      leadbank_gid:  new FormControl(''),
    });
    var api1 = 'MyLead/Getcountrydropdown'
    this.service.get(api1).subscribe((result: any) => {
      this.responsedata = result;
      this.country_list = result.country_list;
    });

    var api2 = 'MyLead/Getregiondropdown'
    this.service.get(api2).subscribe((result: any) => {
      this.responsedata = result;
      this.regionname_list = result.regionname_list;
    });

    var api3 = 'MyLead/Getcurrencydropdown'
    this.service.get(api3).subscribe((result: any) => {
      this.responsedata = result;
      this.currencycodelist = result.currencycodelist;
    });

  }
 

  get leadbank_name() {
    return this.reactiveForm.get('leadbank_name')!;
  }

  get leadbankcontact_name() {
    return this.reactiveForm.get('leadbankcontact_name')!;
  }
  get designation() {
    return this.reactiveForm.get('designation')!;
  }

  get email() {
    return this.reactiveForm.get('email')!;
  }
  get mobile() {
    return this.reactiveForm.get('mobile')!;
  }
  get leadbank_address1(){
    return this.reactiveForm.get('leadbank_address1')!;
  }
  get leadbank_address2(){
    return this.reactiveForm.get('leadbank_address2')!;
  }
  get phone1() {
    return this.reactiveForm.get('phone1')!;
  }
  get fax() {
    return this.reactiveForm.get('fax')!;
  }

  get leadbank_pin(){
    return this.reactiveForm.get('leadbank_pin')!;
  }
  get country_name() {
    return this.reactiveForm.get('country_name')!;
  }
  get region_name() {
    return this.reactiveForm.get('region_name')!;
  }
  get company_website() {
    return this.reactiveForm.get('company_website')!;
  }
  get currency_code() {
    return this.reactiveForm.get('currency_code')!;
  }
  get gst_no() {
    return this.reactiveForm.get('gst_no')!;
  }

  GetleadbankeditSummary(leadbank_gid: any) {
    var url = 'MyLead/GetleadbankeditSummary'
    let param = {
      leadbank_gid : leadbank_gid 
    }
    console.log(leadbank_gid);
    
    this.service.getparams(url, param).subscribe((result: any) => {
      // this.responsedata=result;
      this.leadbankcustomer_list = result.leadbankedit_list;
      console.log(this.leadbankcustomer_list)

      this.reactiveForm.get("leadbank_gid")?.setValue(this.leadbankcustomer_list[0].leadbank_gid);
      this.reactiveForm.get("mobile")?.setValue(this.leadbankcustomer_list[0].mobile);
      this.reactiveForm.get("region_name")?.setValue(this.leadbankcustomer_list[0].region_name);
      this.reactiveForm.get("country_name")?.setValue(this.leadbankcustomer_list[0].country_name);
      this.selectedregion_name = this.leadbankcustomer_list[0].region_gid;
      this.selectedcountry_name=this.leadbankcustomer_list[0].country_gid;
      this.reactiveForm.get("leadbank_name")?.setValue(this.leadbankcustomer_list[0].leadbank_name);
      this.reactiveForm.get("leadbankcontact_name")?.setValue(this.leadbankcustomer_list[0].leadbankcontact_name);
      this.reactiveForm.get("leadbank_address1")?.setValue(this.leadbankcustomer_list[0].leadbank_address1);
      this.reactiveForm.get("leadbank_address2")?.setValue(this.leadbankcustomer_list[0].leadbank_address2);
      this.reactiveForm.get("email")?.setValue(this.leadbankcustomer_list[0].email);
      this.reactiveForm.get("leadbank_pin")?.setValue(this.leadbankcustomer_list[0].leadbank_pin);
      this.reactiveForm.get("company_website")?.setValue(this.leadbankcustomer_list[0].company_website);
     });
  }
  public validate(): void {
    console.log(this.reactiveForm.value)

    this.addtocusotmer = this.reactiveForm.value;
    if (this.addtocusotmer.leadbank_name != null && this.addtocusotmer.leadbankcontact_name != null
      && this.addtocusotmer.leadbank_address1 != null && this.addtocusotmer.country_name != null
      && this.addtocusotmer.currency_code != null && this.addtocusotmer.gst_no != null)
    {
      var api7 = 'MyLead/Postleadbank'
      this.service.post(api7, this.addtocusotmer).subscribe((result: any) => {
        if (result.status == false) {
          this.ToastrService.warning(result.message)
        }
        else {
          this.route.navigate(['/crm/CrmTrnMycampaign']);
          this.ToastrService.success(result.message)
        }
        this.responsedata = result;
      });
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
    return;
  }

}
