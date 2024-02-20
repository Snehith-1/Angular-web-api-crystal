import { Component, ElementRef, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router,ActivatedRoute } from '@angular/router';
import { AES, enc } from 'crypto-js';



@Component({
  selector: 'app-crm-mst-customerview',
  templateUrl: './crm-mst-customerview.component.html',
  styleUrls: ['./crm-mst-customerview.component.scss']
})
export class CrmMstCustomerviewComponent implements OnInit {
  // KeenThemes mock, change it to:
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
  customerlist:any;
  result: any;

  constructor(private renderer: Renderer2, private el: ElementRef,public service :SocketService,private route:Router,private router: ActivatedRoute ) {
  }


  ngOnInit(): void {
    const customer_gid = this.router.snapshot.paramMap.get('customer_gid');
    this.customergid = customer_gid

    const secretKey = 'storyboarderp';

    const deencryptedParam = AES.decrypt(this.customergid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)


   
   
    // var url = 'EinvoiceCustomer/viewcustomer';
    // this.service.get(url).subscribe((result: any) => {
    //   this.currency_list = result.Getcurrencydropdown;
    // });
    // var url1 = 'EinvoiceCustomer/Getcountrydropdown';
    // this.service.get(url1).subscribe((result: any) => {
    //   this.country_list = result.Getcountrydropdown;
    // });
    // var url2 = 'EinvoiceCustomer/GetRegiondropdown';
    // this.service.get(url2).subscribe((result: any) => {
    //   this.region_list = result.GetRegiondropdown;
    // });

    this.viewcustomer(deencryptedParam);
    
  } 
  viewcustomer(customer_gid: any) {
    var url = 'EinvoiceCustomer/viewcustomer';
    this.customer_gid = customer_gid
    var params={
      customer_gid : customer_gid
    }
    this.service.getparams(url, params).subscribe((result: any) => {
      this.responsedata = result;
      this.customerlist = result.Customer_list;
 console.log(this.customerlist);
      
      // this.customerform.get("customercode")?.setValue(this.customer.customercode);
      
      // this.customerform.get("customername")?.setValue(this.customer.customername);
      // this.customerform.get("contactpersonname")?.setValue(this.customer.contactpersonname);
      // this.customerform.get("designation")?.setValue(this.customer.designation);
      // this.customerform.get("contacttelephonenumber")?.setValue(this.customer.contacttelephonenumber);
      // this.customerform.get("Email_ID")?.setValue(this.customer.Email_ID);
      // this.customerform.get("Contact_No")?.setValue(this.customer.Contact_No);
      // this.customerform.get("Fax")?.setValue(this.customer.Fax);
      // this.customerform.get("Address1")?.setValue(this.customer.Address1);
      // this.customerform.get("address2")?.setValue(this.customer.address2);
      // this.customerform.get("city")?.setValue(this.customer.city);
      // this.customerform.get("state")?.setValue(this.customer.state);
      // this.customerform.get("pincode")?.setValue(this.customer.pincode);      
      // this.customerform.get("country")?.setValue(this.customer.country);
      // this.customerform.get("Region")?.setValue(this.customer.Region);
      // this.customerform.get("CompanyWebsite")?.setValue(this.customer.CompanyWebsite);
      // this.customerform.get("gstnumber")?.setValue(this.customer.gstnumber);
      // this.customerform.get("customer_gid")?.setValue(this.customer_gid);
      // this.customerform.get("currency_code")?.setValue(this.currency);
      
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
 
  

 
  redirecttolist(){
    this.route.navigate(['/einvoice/CrmMstCustomer']);
  }
  // ngOnDestroy(): void {

  // }
}
