import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-smr-mst-configuration',
  templateUrl: './smr-mst-configuration.component.html',
  styleUrls: ['./smr-mst-configuration.component.scss']
})
export class SmrMstConfigurationComponent {

  salesconfigform: FormGroup;

  responsedata: any;
  allchargeslist: any;
  ratestatus:any;
  shopifystatus:any;
  GetExchangeRateAPI_List: any;
  GetExchangeRateAPICredential_List: any;

  

  ngOnInit(): void {

this.GetExchangeRateAPISummary();
this.salesconfigform = new FormGroup({
  addoncharges: new FormControl(''),
  additionaldiscount: new FormControl(''),
  freightcharges: new FormControl(''),
  packing_forwardingcharges: new FormControl(''),
  insurancecharges: new FormControl(''),
  exchangerate_baseurl: new FormControl(''),
  exchangerate_apikey: new FormControl(''),
  exchangerate_apihost: new FormControl(''),
})
   this.GetExchangeRateAPICredential();
  }
 //// Summary Grid//////
 GetExchangeRateAPISummary() {
  var url = 'PmrDashboard/GetExchangeRateAPISummary'
  this.service.get(url).subscribe((result: any) => {
    $('#GetExchangeRateAPI_List').DataTable().destroy();
    this.responsedata = result;
    this.GetExchangeRateAPI_List = this.responsedata.GetExchangeRateAPI_List;
    setTimeout(() => {
      $('#GetExchangeRateAPI_List').DataTable();
    }, 1);


  })
}
GetExchangeRateAPICredential() {
  var url = 'PmrDashboard/GetExchangeRateAPICredential'

  this.service.get(url).subscribe((result: any) => {

    this.GetExchangeRateAPICredential_List = result.GetExchangeRateAPICredential_List;
    this.salesconfigform.get("exchangerate_baseurl")?.setValue(this.GetExchangeRateAPICredential_List[0].api_url);
    this.salesconfigform.get("exchangerate_apikey")?.setValue(this.GetExchangeRateAPICredential_List[0].api_key);
    this.salesconfigform.get("exchangerate_apihost")?.setValue(this.GetExchangeRateAPICredential_List[0].api_host);
    
    
  })
}
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private router: ActivatedRoute, private route: Router) {

    this.salesconfigform = new FormGroup({
      addoncharges: new FormControl(''),
      additionaldiscount: new FormControl(''),
      freightcharges: new FormControl(''),
      packing_forwardingcharges: new FormControl(''),
      insurancecharges: new FormControl(''),
      exchangerate_baseurl: new FormControl(''),
      exchangerate_apikey: new FormControl(''),
      exchangerate_apihost: new FormControl(''),
    })
  }

  addonchargestoggle() {
    var api = 'SmrMstSalesConfig/UpdateAddOnChargesConfig';
    this.service.post(api, this.salesconfigform.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
      }
      else {
        this.ToastrService.warning(result.message)
      }
    });
  }

  additionaldiscounttoggle() {
    var api = 'SmrMstSalesConfig/UpdateAdditionalDiscountConfig';
    this.service.post(api, this.salesconfigform.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
      }
      else {
        this.ToastrService.warning(result.message)
      }
    });
  }

  freightchargestoggle() {
    var api = 'SmrMstSalesConfig/UpdateFreightChargesConfig';
    this.service.post(api, this.salesconfigform.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
      }
      else {
        this.ToastrService.warning(result.message)
      }
    });
  }

  packing_forwardingchargestoggle() {
    var api = 'SmrMstSalesConfig/UpdatePacking_ForwardingChargesConfig';
    this.service.post(api, this.salesconfigform.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
      }
      else {
        this.ToastrService.warning(result.message)
      }
    });
  }

  insurancechargestoggle() {
    console.log(this.salesconfigform.value)
    var api = 'SmrMstSalesConfig/UpdateInsuranceChargesConfig';
    this.service.post(api, this.salesconfigform.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
      }
      else {
        this.ToastrService.warning(result.message)
      }
    });
  }
  onrateupdate(){
   debugger
   console.log(this.salesconfigform.value)
    var params = {
      api_url: this.salesconfigform.value.exchangerate_baseurl,
      api_key: this.salesconfigform.value.exchangerate_apikey,
      api_host: this.salesconfigform.value.exchangerate_apihost
    }
      
    var api = 'PmrDashboard/SmrSalesExchangeRateUpdate';
   
    this.service.postparams(api,params).subscribe((result: any) => {
 
       if(result.status ==false){

         this.ToastrService.warning(result.message)

       }
       else{
         this.ToastrService.success(result.message)
        // this.route.navigate(['/smr/SmrMstProductSummary']);
           

       }

     });

  }


}
