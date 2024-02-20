import { Component, ElementRef, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router,ActivatedRoute,Route } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';


interface ICustomer {
  Customer_gid: string;
  customercode: string;
  customername: string;
  contactpersonname: string;
  designation: string;
  contacttelephonenumber:string;
  Email_ID: string;
  Address1: string;
  city:string;
  state:string;
  pincode: string;
  country: string;
  Region: string;
  CompanyWebsite: string;
  FaxCountryCode: string;
  gstnumber:string;
}

@Component({
  selector: 'app-crm-mst-customeredit',
  templateUrl: './crm-mst-customeredit.component.html',
  styleUrls: ['./crm-mst-customeredit.component.scss']
})
export class CrmMstCustomereditComponent implements OnInit {
  // KeenThemes mock, change it to:
  customer!: ICustomer;
  defaultAuth: any = { }; 
  Customer_gid:any[] = [];
  region_list: any[] = [];
  currency_list: any[] = [];
  country_list: any[] = [];
  customerform: FormGroup | any;
  hasError?: boolean;
  returnUrl?: string;
  submitted = false;
  customergid: any;
  Customer_list:any;
  customer_gid:any;
  // private fields
  responsedata: any;
  Region:any;
  currency:any;
  result: any;
  

  constructor(private renderer: Renderer2, private el: ElementRef,public service :SocketService,private route:Router,private router: ActivatedRoute , private ToastrService: ToastrService) {
    this.customer = {} as ICustomer;
  }

  ngOnInit(): void {
    const customer_gid = this.router.snapshot.paramMap.get('customer_gid');
    this.customergid = customer_gid

    const secretKey = 'storyboarderp';

    const deencryptedParam = AES.decrypt(this.customergid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)

    this.customerform = new FormGroup({
      customer_gid: new FormControl(''),      
      customercode: new FormControl('', [Validators.required,]),
      customername: new FormControl('', [Validators.required,]),
      contactpersonname: new FormControl('', [Validators.required,Validators.minLength(1),]),
      designation: new FormControl(''),
      contacttelephonenumber:new FormControl('',[Validators.required, Validators.pattern(/^[0-9]+$/)]),
      Email_ID: new FormControl('', [Validators.required, Validators.pattern(/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/)]),
      Address1: new FormControl('', [Validators.required,]),
      city: new FormControl(''),
      state: new FormControl(''),
      pincode: new FormControl('', [Validators.pattern(/^[0-9]+$/), Validators.maxLength(6)]),
      country: new FormControl(''),
      Region: new FormControl(''),
      currency: new FormControl('',Validators.required),
      CompanyWebsite: new FormControl('',Validators.required),
      gstnumber:new FormControl('',[Validators.required, Validators.pattern(/[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[0-9]{1}[A-Z]{1}[0-9A-Z]{1}$/)]),
      file: new FormControl(''),      
    }
    );

    var url = 'EinvoiceCustomer/Getcurrencydropdown';
    this.service.get(url).subscribe((result: any) => {
      this.currency_list = result.Getcurrencydropdown;
    });

    var url1 = 'EinvoiceCustomer/Getcountrydropdown';
    this.service.get(url1).subscribe((result: any) => {
      this.country_list = result.Getcountrydropdown;
    });

    var url2 = 'EinvoiceCustomer/GetRegiondropdown';
    this.service.get(url2).subscribe((result: any) => {
      this.region_list = result.GetRegiondropdown;
    });

    this.GetEditcustomer(deencryptedParam);    
  }

  GetEditcustomer(customer_gid: any) {
    var url = 'EinvoiceCustomer/GetEditcustomer';
    this.customer_gid = customer_gid
    
    var params={
      customer_gid : customer_gid
    }

    this.service.getparams(url, params).subscribe((result: any) => {
      this.customer = result;
      this.customerform.get("customercode")?.setValue(this.customer.customercode);      
      this.customerform.get("customername")?.setValue(this.customer.customername);
      this.customerform.get("contactpersonname")?.setValue(this.customer.contactpersonname);
      this.customerform.get("designation")?.setValue(this.customer.designation);
      this.customerform.get("contacttelephonenumber")?.setValue(this.customer.contacttelephonenumber);
      this.customerform.get("Email_ID")?.setValue(this.customer.Email_ID);
      this.customerform.get("Address1")?.setValue(this.customer.Address1);
      this.customerform.get("city")?.setValue(this.customer.city);
      this.customerform.get("state")?.setValue(this.customer.state);
      this.customerform.get("pincode")?.setValue(this.customer.pincode);      
      this.customerform.get("country")?.setValue(this.customer.country);
      this.customerform.get("Region")?.setValue(this.customer.Region);
      this.customerform.get("CompanyWebsite")?.setValue(this.customer.CompanyWebsite);
      this.customerform.get("gstnumber")?.setValue(this.customer.gstnumber);
      this.customerform.get("customer_gid")?.setValue(this.customer_gid);
      this.customerform.get("currency_code")?.setValue(this.currency);      
    });
  } 

  get gstControl(){
    return this.customerform.get('gstnumber');
  }
  get customercodeControl(){
    return this.customerform.get('customercode');
  }
  get customernameControl() {
    return this.customerform.get('customername')
  }
  get contactpersonnamecontrol() {
    return this.customerform.get('contactpersonname')
  }
  get contacttelephonenumberControl() {
    return this.customerform.get('contacttelephonenumber')
  }
  get CurrencyCodecontol() {
    return this.customerform.get('currency')
  }
  get authmailControl() {
    return this.customerform.get('Email_ID')
  }
  get CompanyWebsitecontrol() {
    return this.customerform.get('CompanyWebsite')
  }
  get regioncontrol() {
    return this.customerform.get('Region')
  }
  get addressControl() {
    return this.customerform.get('Address1')
  }
  get pincode() {
    return this.customerform.get('pincode');
  }
  
  updated() {
    console.log(this.customerform.value)
    console.log(this.customerform.invalid)
     const api = 'EinvoiceCustomer/UpdatedCustomer';
     this.service.post(api,this.customerform.value).subscribe((result: any) => {

            
          if (result.status == false) {
            this.ToastrService.warning(result.message)
            
          }
          else {
            this.ToastrService.success(result.message)
            this.route.navigate(['/einvoice/CrmMstCustomer']);            
            }    
            this.responsedata = result;

            }
            );
     
}  
redirecttolist() {
  this.route.navigate(['/einvoice/CrmMstCustomer']);
}
  ngOnDestroy(): void {

  }
}
