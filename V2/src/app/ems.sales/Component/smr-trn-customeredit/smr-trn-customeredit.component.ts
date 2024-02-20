import { Component, ElementRef, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Route } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';


interface ICustomer {
  Customer_gid: string;
  customercode: string;
  customername: string;
  contactpersonname: string;
  designation: string;
  contacttelephonenumber: string;
  Email_ID: string;
  Address1: string;
  city: string;
  state: string;
  pincode: string;
  country: string;
  Region: string;
  CompanyWebsite: string;
  FaxCountryCode: string;
  gstnumber: string;
  customer_type: string;
  Fax: string;
  Address2: string;

}
@Component({
  selector: 'app-smr-trn-customeredit',
  templateUrl: './smr-trn-customeredit.component.html',
  styleUrls: ['./smr-trn-customeredit.component.scss']
})
export class SmrTrnCustomereditComponent {

  customer!: ICustomer;
  defaultAuth: any = {};
  Customer_gid: any[] = [];
  Getregion: any[] = [];
  Getcurency: any[] = [];
  Getcountry: any[] = [];
  customerform: FormGroup | any;
  hasError?: boolean;
  returnUrl?: string;
  submitted = false;
  customergid: any;
  Customer_list: any;
  customer_gid: any;
  // private fields
  responsedata: any;
  Region: any;
  currency: any;
  result: any;
  selectedCustomerType: any;

  constructor(private renderer: Renderer2, private el: ElementRef, public service: SocketService,
     private route: Router, private router: ActivatedRoute, private ToastrService: ToastrService,
     private NgxSpinnerService: NgxSpinnerService) {
    this.customer = {} as ICustomer;
  }

  ngOnInit(): void {
    const customer_gid = this.router.snapshot.paramMap.get('customer_gid');
    this.customergid = customer_gid;

    const secretKey = 'storyboarderp';
  
    const deencryptedParam = AES.decrypt(this.customergid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam+"customer_gid");

    this.GetEditcustomer(deencryptedParam);

    this.customerform = new FormGroup({
      customer_gid: new FormControl(''),
      customercode: new FormControl('', [Validators.required,]),
      customername: new FormControl('', [Validators.required,]),
      contactpersonname: new FormControl('', [Validators.required, Validators.minLength(1),]),
      designation: new FormControl(''),
      contacttelephonenumber: new FormControl('', [Validators.required, Validators.pattern(/^[0-9]+$/)]),
      Email_ID: new FormControl('', [Validators.required, Validators.pattern(/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/)]),
      Address1: new FormControl('', [Validators.required,]),
      city: new FormControl(''),
      state: new FormControl(''),
      pincode: new FormControl('', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(6)]),
      country: new FormControl(''),
      Region: new FormControl(''),
      currency: new FormControl('', Validators.required),
      CompanyWebsite: new FormControl('', Validators.required),
      gstnumber: new FormControl('', [Validators.required, Validators.pattern(/[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[0-9]{1}[A-Z]{1}[0-9A-Z]{1}$/)]),
      file: new FormControl(''),
    }
    );

    var url = 'SmrTrnCustomerSummary/Getcurency';
    this.service.get(url).subscribe((result: any) => {
      this.Getcurency = result.Getcurency;
    });

    var url1 = 'SmrTrnCustomerSummary/Getcountry';
    this.service.get(url1).subscribe((result: any) => {
      this.Getcountry = result.Getcountry;
    });

    var url2 = 'SmrTrnCustomerSummary/Getregion';
    this.service.get(url2).subscribe((result: any) => {
      this.Getregion = result.Getregion;
    });

    this.GetEditcustomer(deencryptedParam);
  }
  GetEditcustomer(customer_gid:any) {
   
    var url = 'SmrTrnCustomerSummary/GetEditcustomer';
    this.NgxSpinnerService.show();
    this.customer_gid = customer_gid;
    var params = {
      customer_gid: customer_gid
    }
    this.service.getparams(url, params).subscribe((result: any) => {
      this.customer = result.customer;
      console.log(this.customer+'list of binded data');
      
      this.customerform.get("customercode")?.setValue(this.customer.customercode);
      this.customerform.get("customername")?.setValue(this.customer.customername);
      this.customerform.get("contactpersonname")?.setValue(this.customer.contactpersonname);
      this.customerform.get("designation")?.setValue(this.customer.designation);
      this.customerform.get("contacttelephonenumber")?.setValue(this.customer.contacttelephonenumber);
      this.customerform.get("Email_ID")?.setValue(this.customer.Email_ID);
      this.customerform.get("Address1")?.setValue(this.customer.Address1);
      this.customerform.get("Address2")?.setValue(this.customer.Address2);
      this.customerform.get("city")?.setValue(this.customer.city);
      this.customerform.get("state")?.setValue(this.customer.state);
      this.customerform.get("pincode")?.setValue(this.customer.pincode);
      this.customerform.get("country")?.setValue(this.customer.country);
      this.customerform.get("Region")?.setValue(this.customer.Region);
      this.customerform.get("CompanyWebsite")?.setValue(this.customer.CompanyWebsite);
      this.customerform.get("Fax")?.setValue(this.customer.Fax);
      this.customerform.get("customer_gid")?.setValue(this.customer_gid);
      this.customerform.get("currency_code")?.setValue(this.currency);
      this.customerform.get("customer_type")?.setValue(this.customer_type);
      this.customerform.get("gstnumber")?.setValue(this.customer.gstnumber);
      this.NgxSpinnerService.hide();
    });
  }

  get customer_type() {
    return this.customerform.get('customer_type');
  }
  get Fax() {
    return this.customerform.get('Fax');
  }
  get customercodeControl() {
    return this.customerform.get('customercode');
  }
  get customernameControl() {
    return this.customerform.get('customername')
  }
  get contactpersonnamecontrol() {
    return this.customerform.get('contactpersonname');
  }
  get contacttelephonenumberControl() {
    return this.customerform.get('contacttelephonenumber');
  }
  get CurrencyCodecontol() {
    return this.customerform.get('currency');
  }
  get authmailControl() {
    return this.customerform.get('Email_ID');
  }
  get CompanyWebsitecontrol() {
    return this.customerform.get('CompanyWebsite');
  }
  get regioncontrol() {
    return this.customerform.get('Region');
  }
  get addressControl() {
    return this.customerform.get('Address1');
  }
  get addressControl2() {
    return this.customerform.get('Address2');
  }
  get pincode() {
    return this.customerform.get('pincode');
  }
  get gstControl() {
    return this.customerform.get('gstnumber');
  }
  updated() {
    console.log(this.customerform.value);
    console.log(this.customerform.invalid);
    const api = 'SmrTrnCustomerSummary/UpdatedCustomer';
    this.service.post(api, this.customerform.value).subscribe((result: any) => {

      if (result.status == false) {
        this.ToastrService.warning(result.message);
      }
      else {
        this.ToastrService.success(result.message);
        this.route.navigate(['/smr/SmrTrnCustomerSummary']);
      }
      this.responsedata = result;

    }
    );

  }
  redirecttolist() {
    this.route.navigate(['/smr/SmrTrnCustomerSummary']);
  }
  ngOnDestroy(): void {

  }


  // GetEditcustomer(deencryptedParam: string) {
  //   throw new Error('Method not implemented.');
  // }
  // function GetEditcustomer(customer_gid: string | null, any: any) {
  //   throw new Error('Function not implemented.');
  // }

}