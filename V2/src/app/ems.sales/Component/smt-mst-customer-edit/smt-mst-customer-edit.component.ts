import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import {CountryISO,SearchCountryField,} from "ngx-intl-tel-input";

@Component({
  selector: 'app-smt-mst-customer-edit',
  templateUrl: './smt-mst-customer-edit.component.html',
  styleUrls: ['./smt-mst-customer-edit.component.scss']
})
export class SmtMstCustomerEditComponent {
EditForm! : FormGroup;
region_list : any[]=[];
country_list: any[]=[];
currency_list : any[]=[];
GetCustomerList: any;
customer_list: any;
customeredit: any;
responsedata: any;
SearchCountryField = SearchCountryField;
CountryISO = CountryISO;
preferredCountries: CountryISO[] = [
  CountryISO.India,
  CountryISO.India
];
constructor(private http:HttpClient, private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) {
}
ngOnInit(): void
{
  this.EditForm = new FormGroup
  ({
     customer_gid: new FormControl(''),
     customer_id: new FormControl(''),
     customer_name: new FormControl(''),
     customercontact_name: new FormControl(''),
     mobiles: new FormControl(''),
     email: new FormControl(''),
     customer_type: new FormControl(''),
     address1: new FormControl(''),
     address2: new FormControl(''),
     city: new FormControl(''),
     postal_code: new FormControl(''),
     country_name: new FormControl(''),
     customer_state: new FormControl(''),
     gst_number: new FormControl(''),
     country_code: new FormControl(''),
     area_code: new FormControl(''),
     fax_number: new FormControl(''),
     designation: new FormControl(''),
     company_website: new FormControl(''),
     region_name: new FormControl('')
     

  })
  this.customeredit = this.route.snapshot.paramMap.get('customer_gid');
  const secretKey = 'storyboarderp';
  const deencryptedParam = AES.decrypt(this.customeredit, secretKey).toString(enc.Utf8);
 
  this.GetCustomerEditSummary(deencryptedParam);

//country drop down//
var url = 'SmrTrnCustomerSummary/Getcountry'
this.service.get(url).subscribe((result: any) => {
  this.country_list = result.Getcountry;
  //  this.EditForm.get("countryname")?.setValue(this.GetCustomerList[0].customer_gid);

});

//currency dropdown//
var url = 'SmrTrnCustomerSummary/Getcurency'
this.service.get(url).subscribe((result: any) => {
  this.currency_list = result.Getcurency;
});
//region dropdown//
  var url = 'SmrTrnCustomerSummary/Getregion'
  this.service.get(url).subscribe((result: any) => {
    this.region_list = result.Getregion;
  });

}
get countryname() {
  return this.EditForm.get('countryname')!;
}
get currencyname() {
  return this.EditForm.get('currencyname')!;
}

get customer_type() {
  return this.EditForm.get('customer_type')!;
}

get mobiles() {
  return this.EditForm.get('mobiles')!;
}
get address1() {
  return this.EditForm.get('address1')!;
}


get customer_name() {
  return this.EditForm.get('customer_name')!;
}
get customercontact_name() {
  return this.EditForm.get('customercontact_name')!;
}

get email() {
  return this.EditForm.get('email')!;
}
get region_name() {
  return this.EditForm.get('region_name')!;
}

  
GetCustomerEditSummary(customer_gid: any) {
      debugger;      
          var url = 'SmrTrnCustomerSummary/GetEditCustomer'      
          let param = {      
            customer_gid: customer_gid      
          }      
          this.service.getparams(url, param).subscribe((result: any) => {
      
            this.GetCustomerList = result.GetCustomerList;
            this.EditForm.get("customer_id")?.setValue(this.GetCustomerList[0].customer_id);
            this.EditForm.get("customer_gid")?.setValue(this.GetCustomerList[0].customer_gid);
            this.EditForm.get("customer_name")?.setValue(this.GetCustomerList[0].customer_name);
            this.EditForm.get("customercontact_name")?.setValue(this.GetCustomerList[0].customercontact_name);
            this.EditForm.get("customer_type")?.setValue(this.GetCustomerList[0].customer_type);
            this.EditForm.get("mobiles")?.setValue(this.GetCustomerList[0].mobile_number);
            this.EditForm.get("address1")?.setValue(this.GetCustomerList[0].address1);
            this.EditForm.get("address2")?.setValue(this.GetCustomerList[0].address2);
            this.EditForm.get("city")?.setValue(this.GetCustomerList[0].city);
            this.EditForm.get("email")?.setValue(this.GetCustomerList[0].email);
            this.EditForm.get("postal_code")?.setValue(this.GetCustomerList[0].postal_code);
            this.EditForm.get("country_name")?.setValue(this.GetCustomerList[0].country_name);
            this.EditForm.get("customer_state")?.setValue(this.GetCustomerList[0].customer_state);
            this.EditForm.get("gst_number")?.setValue(this.GetCustomerList[0].gst_number);
            this.EditForm.get("region_name")?.setValue(this.GetCustomerList[0].region_name);
            this.EditForm.get("company_website")?.setValue(this.GetCustomerList[0].company_website);
            this.EditForm.get("country_code")?.setValue(this.GetCustomerList[0].country_code);
            this.EditForm.get("area_code")?.setValue(this.GetCustomerList[0].area_code);
            this.EditForm.get("fax_number")?.setValue(this.GetCustomerList[0].fax_number);
            this.EditForm.get("designation")?.setValue(this.GetCustomerList[0].designation);
          })
}
  validate() {
    debugger
    var params={
      customer_id:this.EditForm.value.customer_id,
      customer_gid:this.EditForm.value.customer_gid,
      customer_name:this.EditForm.value.customer_name,
      customercontact_name:this.EditForm.value.customercontact_name,
      customer_type:this.EditForm.value.customer_type,
      mobiles:this.EditForm.value.mobiles.e164Number,
      address1:this.EditForm.value.address1,
      address2:this.EditForm.value.address2,
      city:this.EditForm.value.city,
      email:this.EditForm.value.email,
      postal_code:this.EditForm.value.postal_code,
      country_name:this.EditForm.value.country_name,
      customer_state:this.EditForm.value.customer_state,
      gst_number:this.EditForm.value.gst_number,
      region_name:this.EditForm.value.region_name,
      company_website:this.EditForm.value.company_website,
      country_code:this.EditForm.value.country_code,
      area_code:this.EditForm.value.area_code,
      fax_number:this.EditForm.value.fax_number,
      designation:this.EditForm.value.designation
    }

    var url = 'SmrTrnCustomerSummary/DaUpdateCostomer'
    
    debugger;
    this.service.postparams(url,params).subscribe((result: any) => {
      
      if (result.status == false) {
        this.ToastrService.warning(result.message)
        this.router.navigate(['/smr/SmrTrnCustomerSummary']);
      }
      else {
        this.ToastrService.success(result.message)
        this.EditForm.reset();
        this.router.navigate(['/smr/SmrTrnCustomerSummary']);
        
      }
    });
  }
  onclose(){
    this.router.navigate (['/smr/SmrTrnCustomerSummary'])
  }
}
