import { Component, ElementRef, Renderer2 } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import {CountryISO,SearchCountryField,} from "ngx-intl-tel-input";
import { NgxSpinnerService } from 'ngx-spinner';

interface ICustomer {
  state: string;
  customerbranch_name: any;
  country_name: string;
  city: string;
  mobiles: string;
  email: string;
  zip_code: string;
  address1: string;
  address2: string;
  designation: string;
  region_name: string;
  customer_gid : string;
  customercontact_gid :string;
  customercontact_name : string; 
}
@Component({
  selector: 'app-smr-trn-customer-call',
  templateUrl: './smr-trn-customer-call.component.html',
  styleUrls: ['./smr-trn-customer-call.component.scss']
})
export class SmrTrnCustomerCallComponent {
  ile!: File;
  customer!: ICustomer;
  smrcustomerbranch_list: any[] = [];
  reactiveForm!: FormGroup;
  country_list: any[] = [];
  region_list: any[] = [];
  branch_list : any[] = [];
  responsedata: any;
  customer_gid1 :any;
  customer_gid :any;
  customercontact_list1:any;
  smrcustomer_list:any;
  mdlBranchName:any;
  SearchCountryField = SearchCountryField;
  CountryISO = CountryISO;
  preferredCountries: CountryISO[] = [
    CountryISO.India,
    CountryISO.India
  ];
  customergroup_gid: any;
  customercontact_gid: any;
  Form: any;
  constructor(private renderer: Renderer2, private el: ElementRef, public service: SocketService, private ToastrService: ToastrService, private route: Router,
    private router:ActivatedRoute,public NgxSpinnerService:NgxSpinnerService) {
    this.customer = {} as ICustomer;
    
  }
  ngOnInit(): void {
    debugger
    const customer_gid =this.router.snapshot.paramMap.get('customer_gid');
    this.customer_gid= customer_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.customer_gid,secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.GetSmrTrnCustomerContact(deencryptedParam);
    this.reactiveForm = new FormGroup({

      state: new FormControl(this.customer.state, [
        Validators.required,

      ]),
      
       customer_gid:new FormControl(this.customer.customer_gid, [
        Validators.required,
        
      ]),

      city: new FormControl(this.customer.city, [
        Validators.required,

      ]),

       address1: new FormControl(this.customer.address1, [
        Validators.required,
      ]),

       address2: new FormControl(this.customer.address2, [
        Validators.required,
      ]),

      customerbranch_name: new FormControl(this.customer.customerbranch_name, [
        Validators.required,


      ]),

      mobiles: new FormControl(this.customer.mobiles, [
        Validators.required,

      ]),
     
       region_name: new FormControl(this.customer.region_name, [
        Validators.required,

      ]),
      customercontact_name: new FormControl(this.customer.customercontact_name, [
        Validators.required,

      ]),

       designation: new FormControl(this.customer.designation, [
        Validators.required,

      ]),

       email: new FormControl(''),

       zip_code: new FormControl(''),


       country_name: new FormControl(this.customer.country_name, [
        Validators.required,
      ]),
      

    }

    );


      //// Branch Dropdown /////
      
      var url = 'SmrTrnCustomerSummary/Getbranch'
      let param = {
        customer_gid : deencryptedParam
          }
      this.service.getparams(url,param).subscribe((result:any)=>{
        this.branch_list = result.branch_list;
       });
    //country drop down//
    var url = 'SmrTrnCustomerSummary/Getcountry'
    this.service.get(url).subscribe((result: any) => {
      this.country_list = result.Getcountry;
    });
    // var url = 'SmrTrnCustomerSummary/GetSmrTrnCustomerSummary'
    // this.service.get(url).subscribe((result: any) => {
    //   $('#smrcustomer_list').DataTable().destroy();
    //    this.responsedata = result;
    //    this.smrcustomer_list = this.responsedata.smrcustomer_list;
    //    debugger
    //    this.reactiveForm.get("customer_gid")?.setValue(this.smrcustomer_list[0].customer_gid);
    // });

  }
  
  GetSmrTrnCustomerContact(customer_gid: any) {
    debugger
    var url = 'SmrTrnCustomerSummary/GetSmrTrnCustomerContact'
    
    let param = {
      customer_gid : customer_gid
        }
    this.service.getparams(url,param).subscribe((result: any) => {
       this.responsedata = result;
       this.customercontact_list1 = this.responsedata.customercontact_list1;
    });
  }
  
  get country_name() {
    return this.reactiveForm.get('country_name')!;
  }
  get state() {
    return this.reactiveForm.get('state')!;
  }


  get address1() {
    return this.reactiveForm.get('address1')!;
  }

  get customerbranch_name() {
    return this.reactiveForm.get('customerbranch_name')!;
  }

  get customercontact_name() {
    return this.reactiveForm.get('customercontact_name')!;
  }
  get zip_code() {
    return this.reactiveForm.get('zip_code')!;
  }
 
  get city() {
    return this.reactiveForm.get('city')!;
  }
 
  get email() {
    return this.reactiveForm.get('email')!;
  }


  get mobiles() {
    return this.reactiveForm.get('mobiles')!;
  }

  validated() {
    debugger
    console.log(this.reactiveForm)
    const customer_gid =this.router.snapshot.paramMap.get('customer_gid');
    this.customer_gid= customer_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.customer_gid,secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.reactiveForm.get("customer_gid")?.setValue(deencryptedParam);
    this.customer_gid1 = deencryptedParam;
    this.Form= this.reactiveForm.value.mobiles.e164Number;

    var api ='SmrTrnCustomerSummary/PostCustomercontact';
    this.NgxSpinnerService.show();
    this.service.post(api, this.reactiveForm.value).subscribe((result: any) => {
      if (result.status == false) {
        this.NgxSpinnerService.hide();
        this.ToastrService.warning('Error While Adding Customer Contact')
        }
         else {
          this.NgxSpinnerService.hide();
        this.ToastrService.success('Customer Contact Added Successfully')
        this.reactiveForm.reset();
        this.GetSmrTrnCustomerContact(this.customer_gid1);
        }
        this.responsedata = result;
        
    });
  }
  onclose(){
    this.route.navigate(['/smr/SmrTrnCustomerSummary/'])
  }
  onedit(){}

}
