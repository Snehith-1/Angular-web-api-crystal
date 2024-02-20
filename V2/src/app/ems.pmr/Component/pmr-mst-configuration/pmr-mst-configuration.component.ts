import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-pmr-mst-configuration',
  templateUrl: './pmr-mst-configuration.component.html',
  styleUrls: ['./pmr-mst-configuration.component.scss']
})
export class PmrMstConfigurationComponent {

  purchaseconfigform: FormGroup;

  responsedata: any;
  allchargeslist: any;

  ngOnInit(): void {
    var api = 'PmrMstPurchaseConfig/GetAllChargesConfig';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.allchargeslist = this.responsedata.salesconfigalllist;

      if(this.allchargeslist[0].flag == 'Y') {
        this.purchaseconfigform.get("addoncharges")?.setValue(true)
      }

      if(this.allchargeslist[1].flag == 'Y') {
        this.purchaseconfigform.get("additionaldiscount")?.setValue(true)
      }

      if(this.allchargeslist[2].flag == 'Y') {
        this.purchaseconfigform.get("freightcharges")?.setValue(true)
      }

      if(this.allchargeslist[3].flag == 'Y') {
        this.purchaseconfigform.get("packing_forwardingcharges")?.setValue(true)
      }

      if(this.allchargeslist[4].flag == 'Y') {
        this.purchaseconfigform.get("insurancecharges")?.setValue(true)
      }
    });
  }

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService, private router: ActivatedRoute, private NgxSpinnerService:NgxSpinnerService,private route: Router) {

    this.purchaseconfigform = new FormGroup({
      addoncharges: new FormControl(''),
      additionaldiscount: new FormControl(''),
      freightcharges: new FormControl(''),
      packing_forwardingcharges: new FormControl(''),
      insurancecharges: new FormControl('')
    })
  }

  addonchargestoggle() {
    this.NgxSpinnerService.show();
    var api = 'PmrMstPurchaseConfig/UpdateAddOnChargesConfig';
    this.service.post(api, this.purchaseconfigform.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }

  additionaldiscounttoggle() {
    this.NgxSpinnerService.show();
    var api = 'PmrMstPurchaseConfig/UpdateAdditionalDiscountConfig';
    this.service.post(api, this.purchaseconfigform.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }

  freightchargestoggle() {
    var api = 'PmrMstPurchaseConfig/UpdateFreightChargesConfig';
    this.NgxSpinnerService.show();
    this.service.post(api, this.purchaseconfigform.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }

  packing_forwardingchargestoggle() {
    this.NgxSpinnerService.show();
    var api = 'PmrMstPurchaseConfig/UpdatePacking_ForwardingChargesConfig';
    this.service.post(api, this.purchaseconfigform.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }

  insurancechargestoggle() {
    this.NgxSpinnerService.show();
    var api = 'PmrMstPurchaseConfig/UpdateInsuranceChargesConfig';
    this.service.post(api, this.purchaseconfigform.value).subscribe((result: any) => {
      if(result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
      }
    });
  }
}
