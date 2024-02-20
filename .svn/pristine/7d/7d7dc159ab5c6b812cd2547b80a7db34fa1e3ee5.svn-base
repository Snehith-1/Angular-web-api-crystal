import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription, Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../../environments/environment.development';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-crm-smm-shopifyproductadd',
  templateUrl: './crm-smm-shopifyproductadd.component.html',
  styleUrls: ['./crm-smm-shopifyproductadd.component.scss']
})
export class CrmSmmShopifyproductaddComponent {
  shopifyproduct:any;
  producttype_list: any[] = [];
  productgroup_list: any[] = [];
  productunitclass_list: any[] = [];
  productunit_list: any[] = [];
  rbo_status: any[] = [];
  Form: FormGroup | any;
  hasError?: boolean;
  returnUrl?: string;

  submitted = false;


  // private fields
  private unsubscribe: Subscription[] = [];
  responsedata: any;
  result: any;
  productform: FormGroup<{ vendor: FormControl<any>; product_name: FormControl<any>; product_desc: FormControl<any>; product_status: FormControl<any>; product_type: FormControl<any>; }> | any;


  constructor(
    private fb: FormBuilder,

    private route: ActivatedRoute,
    private router: Router,
    private service: SocketService,
    private ToastrService: ToastrService, private NgxSpinnerService: NgxSpinnerService
  ) {
    this.productform = new FormGroup({
      product_type: new FormControl('', Validators.required),
      product_name: new FormControl('', Validators.required),
      product_desc: new FormControl(''),
      vendor: new FormControl(''),
      product_status: new FormControl('active', Validators.required),


    });
  }
  ngOnDestroy(): void {

  }
  ngOnInit(): void {

    var api = 'Product/Getproductgroupdropdown';
    this.service.get(api).subscribe((result: any) => {
      $('#productgroup_list').DataTable().destroy();
      this.responsedata = result;
      this.productgroup_list = this.responsedata.Getproductgroupdropdown;
      setTimeout(() => {

        $('#productgroup_list').DataTable();

      }, 0.1);
    });
    var api = 'Product/Getproducttypedropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.producttype_list = this.responsedata.Getproducttypedropdown;

    });

    var api = 'Product/Getproductunitclassdropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productunitclass_list = this.responsedata.Getproductunitclassdropdown;

    });

    var api = 'Product/Getproductunitdropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productunit_list = this.responsedata.Getproductunitdropdown;

    });
  }

  get product_type() {

    return this.productform.get('product_type')!;

  };
  get productgroup_name() {

    return this.productform.get('product_name')!;

  };
  get productuomclass_name() {

    return this.productform.get('productuomclass_name')!;

  };
  get productuom_name() {

    return this.productform.get('productuom_name')!;

  };
  get product_code() {

    return this.productform.get('product_code')!;

  };
  get mrp_price() {

    return this.productform.get('mrp_price')!;

  };
  get product_name() {

    return this.productform.get('product_name')!;

  }




  initForm() {
    this.productform = this.fb.group({
      producttype_name: [this.productform.producttype_name, Validators.compose([

        Validators.required,
      ]),
      ],
      productgroup_name: [
        this.productform.productgroup_name,
        Validators.compose([
          Validators.required

        ]),
      ],
      productuomclass_name: [
        this.productform.productuomclass_name,
        Validators.compose([
          Validators.required

        ]),
      ],
      productuom_name: [
        this.productform.productuom_name,
        Validators.compose([
          Validators.required,

        ]),
      ],
      product_code: [
        this.productform.product_code,
        Validators.compose([
          Validators.required,

        ]),
      ],
      product_name: [
        this.productform.product_name,
        Validators.compose([
          Validators.required,

        ]),
      ],
      product_desc: new FormControl(''),

      mrp_price: new FormControl(''),
      batch_flag: new FormControl(''),
      serial_flag: new FormControl(''),
      expirytracking_flag: new FormControl(''),
      purchasewarrenty_flag: new FormControl(''),

      cost_price: new FormControl(''),
    });

  }

  onadd() {
    // debugger

    if (this.productform.value.product_name != null && this.productform.value.product_type != null && this.productform.value.product_name != '' && this.productform.value.product_type != '') {
      //console.log(this.productform.value)
      this.NgxSpinnerService.show();

      var api = 'Product/PostShopifyProduct';
      this.service.post(api, this.productform.value).subscribe(
        (result: any) => {
          this.responsedata = result;

          if (result.status == true) {
            this.NgxSpinnerService.hide();
            var url = 'Product/GetShopifyProductdetails'
            this.service.get(url).subscribe((result: any) => {

              this.shopifyproduct = result;
              // this.shopifyproduct_list = this.shopifyproduct.products;


            });
            this.ToastrService.success(result.message)
            this.router.navigate(['crm/CrmSmmShopifycustomer']);
          }
          else {
            this.NgxSpinnerService.hide();
            this.ToastrService.warning(result.message)

          }

        });
    }

    else {

      window.scrollTo({

        top: 0, // Code is used for scroll top after event done

      });
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }


  redirecttolist() {
    this.router.navigate(['/crm/CrmSmmShopifycustomer']);

  }
  productunitclass() {
    let productuomclass_gid = this.productform.get("productuomclass_name")?.value;

    let param = {
      productuomclass_gid: productuomclass_gid
    }
    var url = 'Product/GetOnChangeProductUnitClass';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;

      this.productunit_list = this.responsedata.GetProductUnit;

    });
  }
}
