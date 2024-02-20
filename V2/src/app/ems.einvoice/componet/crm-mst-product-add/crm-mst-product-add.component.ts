import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription, Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';

@Component({
  selector: 'app-crm-mst-product-add',
  templateUrl: './crm-mst-product-add.component.html',
  styleUrls: ['./crm-mst-product-add.component.scss']
})

export class CrmMstProductAddComponent implements OnInit, OnDestroy {
  // KeenThemes mock, change it to:
  producttype_list: any[] = [];
  productgroup_list: any[] = [];
  productunitclass_list: any[] = [];
  productunit_list: any[] = [];
  rbo_status: any[] = [];
  productform: FormGroup | any;
  hsngroup_list: any;
  hsngroupcodeanddesc_list: any;
  hasError?: boolean;
  returnUrl?: string;
  submitted = false;
  GetProductSummary : any;

  // private fields
  private unsubscribe: Subscription[] = []; // Read more: => https://brianflove.com/2016/12/11/anguar-2-unsubscribe-observables/
  responsedata: any;
  result: any;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: SocketService,
    private ToastrService: ToastrService,
  ) {
    this.productform = new FormGroup({
      producttype_name: new FormControl('', Validators.required),
      productgroup_name: new FormControl('', Validators.required),
      productuomclass_name: new FormControl('', Validators.required),
      productuom_name: new FormControl('', Validators.required),
      product_code: new FormControl('', Validators.required),
      product_name: new FormControl('', Validators.required),
      product_desc: new FormControl(''),
      mrp: new FormControl('', Validators.required),
      cost_price: new FormControl('', Validators.required),
      hsngroup_code: new FormControl('', Validators.required),
      hsn: new FormControl(''),
    });
  }

  ngOnDestroy(): void { }

  ngOnInit(): void {
    var api = 'EinvoiceProduct/Getproductgroupdropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productgroup_list = this.responsedata.Getproductgroupdropdown;
    });

    var api = 'EinvoiceProduct/Gethsngroupdropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.hsngroup_list = this.responsedata.Gethsngroupdropdown;
    });

    var api = 'EinvoiceProduct/Getproducttypedropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.producttype_list = this.responsedata.Getproducttypedropdown;
    });

    var api = 'EinvoiceProduct/Getproductunitclassdropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productunitclass_list = this.responsedata.Getproductunitclassdropdown;
    });

    var api = 'EinvoiceProduct/Getproductunitdropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productunit_list = this.responsedata.Getproductunitdropdown;
    });
  }

  get producttypecontrol() {
    return this.productform.get('producttype_name');
  }
  get productgroupcontrol() {
    return this.productform.get('productgroup_name');
  }
  get mrpControl() {
    return this.productform.get('mrp');
  }
  get productcodecontrol() {
    return this.productform.get('product_code');
  }
  get productunitclasscontrol() {
    return this.productform.get('productuomclass_name');
  }
  get productunitscontrol() {
    return this.productform.get('productuom_name');
  }
  get productnamecontrol() {
    return this.productform.get('product_name');
  }
  get costpricecontrol() {
    return this.productform.get('cost_price');
  }
  get hsncodeControl() {
    return this.productform.get('hsngroup_code');
  }

  hsngroupfetchdetails() {
    let hsngroup_code = this.productform.get('hsngroup_code')?.value;
    let param = {
      hsngroup_code: hsngroup_code
    }

    var url = 'EinvoiceProduct/GetOnChangehsngroup';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.hsngroupcodeanddesc_list = this.responsedata.Gethsngroupcodedropdown;
    })
  }

  onadd() {
    const api = 'EinvoiceProduct/PostProduct'
    this.service.post(api, this.productform.value).subscribe((result: any) => {
      this.responsedata = result;
      if (result.status == false) {
        this.ToastrService.warning(result.message)
        // this.GetProductSummary()
      }
      else {
        this.ToastrService.success(result.message)
        this.router.navigate(['/einvoice/CrmMstProduct']);

      }
         
    });

   
  }
  
  redirecttolist() {
    this.router.navigate(['/einvoice/CrmMstProduct']);
  }
}
