import { Component, ElementRef, Renderer2 } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgSelectModule } from '@ng-select/ng-select';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import {CountryISO,SearchCountryField,} from "ngx-intl-tel-input";
import { NgxSpinnerService } from 'ngx-spinner';

interface ICustomer {
  country_code: any;
  area_code: any;
  mobiles: any;
  user_gid :string;
  countryname: string;
  taxname: string;
  customer_state: string;
  fax: string;
  fax_area_code: string;
  fax_country_code: string;
  customer_city: string;
  mobile: string;
  customer_id: string;
  email: string;
  postal_code: string;
  address1: string;
  address2: string;
  customer_name: string;
  customercontact_name: string;
  gst_number: string;
  designation: string;
  region_name: string;
  company_website :string;
  currencyexchange_gid : String;
  customer_gid : string;
  customer_code : string;
  customer_type:string;
  
}


@Component({
  selector: 'app-smr-trn-customeradd',
  templateUrl: './smr-trn-customeradd.component.html',
  styleUrls: ['./smr-trn-customeradd.component.scss']
})
export class SmrTrnCustomeraddComponent {
  
  file!: File;
  customer!: ICustomer;
  reactiveForm!: FormGroup;
  country_list: any[] = [];
  currency_list: any[] = [];
  tax_list: any[] = [];
  region_list: any[] = [];
  Email_Address: any;
  responsedata: any;
  customer_id1:any;
  customertype_list:any[] = [];
  mdlType:any;
  SearchCountryField = SearchCountryField;
  CountryISO = CountryISO;
  preferredCountries: CountryISO[] = [
    CountryISO.India,
    CountryISO.India
  ];

  constructor(private renderer: Renderer2, private el: ElementRef, public service: SocketService, private ToastrService: ToastrService, private route: Router, public NgxSpinnerService:NgxSpinnerService ) {
    this.customer = {} as ICustomer;
  }
  ngOnInit(): void {
    this.reactiveForm = new FormGroup({

      customer_state: new FormControl(this.customer.customer_state, [
        Validators.required,

      ]),
      
      customer_gid: new FormControl(''),

      area_code: new FormControl(this.customer.area_code, [
        Validators.required,

      ]),

      country_code: new FormControl(this.customer.country_code, [
        Validators.required,

      ]),
      
      fax: new FormControl(this.customer.fax, [
        Validators.required,

      ]),
      customer_city: new FormControl(this.customer.customer_city, [
        Validators.required,

      ]),
      address1: new FormControl(this.customer.address1, [
        Validators.required,
      ]),
      address2: new FormControl(this.customer.address2, [
        Validators.required,
      ]),
      customer_id: new FormControl(''),
      customer_name: new FormControl(this.customer.customer_name, [
        Validators.required,

      ]),
      mobiles: new FormControl(this.customer.mobiles, [
        Validators.required,

      ]),
      gst_number: new FormControl(this.customer.gst_number, [
        Validators.required,

      ]),
      region_name: new FormControl(this.customer.region_name, [
        Validators.required,

      ]),

      company_website: new FormControl(this.customer.company_website, [
        Validators.required,

      ]),

      
      designation: new FormControl(this.customer.designation, [
        Validators.required,

      ]),


      email: new FormControl(''),
      postal_code: new FormControl(''),
      customercontact_name: new FormControl(''),

      customer_type: new FormControl(this.customer.customer_type, [
        Validators.required,

      ]),

      countryname: new FormControl(this.customer.countryname, [
        Validators.required,
      ]),

  

    }

    );
    //Get Customer Code
    var url = 'SmrTrnCustomerSummary/GetCustomerCode'
    this.service.get(url).subscribe((result: any) => {
     this.customer_id1  =result.customer_code;
     console.log(this.customer_id1)
    });

    //country drop down//
    var url = 'SmrTrnCustomerSummary/Getcountry'
    this.service.get(url).subscribe((result: any) => {
      this.country_list = result.Getcountry;
    });
    var url = 'SmrTrnCustomerSummary/GetCustomerTypeSummary'
    this.service.get(url).subscribe((result: any) => {
      this.customertype_list = result.customertype_list1;
    });
    //currency dropdown//
    // var url = 'SmrTrnCustomerSummary/Getcurency'
    // this.service.get(url).subscribe((result: any) => {
    //   this.currency_list = result.Getcurency;
    // });
    //tax dropdown//
    var url = 'SmrTrnCustomerSummary/Gettax'
    this.service.get(url).subscribe((result: any) => {
      this.tax_list = result.Gettax;
    });
    //regionname dropdown//

    var url = 'SmrTrnCustomerSummary/Getregion'
    this.service.get(url).subscribe((result: any) => {
      this.region_list = result.Getregion;
    });


  }

  onChange2(event: any) {
    this.file = event.target.files[0];

  }

  get countryname() {
    return this.reactiveForm.get('countryname')!;
  }


  get customer_type() {
    return this.reactiveForm.get('customer_type')!;
  }

  get country_code() {
    return this.reactiveForm.get('country_code')!;
  }
  
  get area_code() {
    return this.reactiveForm.get('area_code')!;
  }
  
  get fax() {
    return this.reactiveForm.get('fax')!;
  }
  
  get address1() {
    return this.reactiveForm.get('address1')!;
  }



  get customer_name() {
    return this.reactiveForm.get('customer_name')!;
  }
  get customercontact_name() {
    return this.reactiveForm.get('customercontact_name')!;
  }
 
  get email() {
    return this.reactiveForm.get('email')!;
  }
  get region_name() {
    return this.reactiveForm.get('region_name')!;
  }









  public validate(): void {


    this.customer = this.reactiveForm.value;

    if (this.customer.customer_name != null && this.customer.customercontact_name != null 
      && this.customer.customer_type != null 
      && this.customer.email != null && this.customer.countryname != null
       && this.customer.address1 != null && this.customer.region_name != null
        && this.reactiveForm.value.mobiles.e164Number != null ) {
      let formData = new FormData();
      if (this.file != null && this.file != undefined) {
        debugger
        formData.append("customer_state", this.customer.customer_state);
        formData.append("customer_city", this.customer.customer_city);
        formData.append("address1", this.customer.address1);
        formData.append("mobiles", this.customer.mobiles.e164Number);
        formData.append("customer_id", this.customer_id1 );
        formData.append("customer_code", this.customer.customer_code );
        formData.append("customer_name", this.customer.customer_name);
        formData.append("customercontact_name", this.customer.customercontact_name);
        formData.append("email", this.customer.email);
        formData.append("postal_code", this.customer.postal_code);
        formData.append("address2", this.customer.address2);
        formData.append("countryname", this.customer.countryname);
        formData.append("taxname", this.customer.taxname);
        formData.append("region_name", this.customer.region_name);
        formData.append("area_code", this.customer.area_code);
        formData.append("country_code", this.customer.country_code);
        formData.append("fax", this.customer.fax);
        formData.append("company_website", this.customer.company_website);
        formData.append("designation", this.customer.designation);

    
        var api = 'SmrTrnCustomerSummary/Postcustomer'
        this.NgxSpinnerService.show();
        this.service.postfile(api, formData).subscribe((result: any) => {
          debugger
          this.responsedata = result;
          if (result.status == false) {
            this.NgxSpinnerService.hide();
            this.ToastrService.warning(result.message)
          }
          else {
            this.NgxSpinnerService.hide();
            this.route.navigate(['/smr/SmrTrnCustomerSummary']);
            this.ToastrService.success(result.message)
          }
        });

      }
      else {
        var api7 = 'SmrTrnCustomerSummary/Postcustomer'
        this.NgxSpinnerService.show();
        this.service.post(api7, this.customer).subscribe((result: any) => {

          if (result.status == false) {
            this.NgxSpinnerService.hide();
            this.ToastrService.warning(result.message)
          }
          else {
            this.NgxSpinnerService.hide();
            this.route.navigate(['/smr/SmrTrnCustomerSummary']);
            this.ToastrService.success(result.message)
          }
          this.responsedata = result;
        });
      }
    }
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
    return;


  }

  onclose(){
    this.route.navigate(['/smr/SmrTrnCustomerSummary/'])
  }
}
