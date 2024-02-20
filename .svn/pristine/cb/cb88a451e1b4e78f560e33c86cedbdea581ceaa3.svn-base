import { Component, ElementRef, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router,ActivatedRoute } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';


interface IProduct {

  product_gid:string;
  producttypename: string;
  productgroupname: string;
  productuomclassname: string;
  productuomname: string;
  product_code:string;
  product_name: string;
  cost_price: string;
  product_desc: string;
  mrp_price:string;
}

@Component({
  selector: 'app-crm-smm-shopifyproductedit',
  templateUrl: './crm-smm-shopifyproductedit.component.html',
  styleUrls: ['./crm-smm-shopifyproductedit.component.scss']
})
export class CrmSmmShopifyproducteditComponent {
  isReadOnly = true;
  product!: IProduct;
  defaultAuth: any = { }; 
  product_gid:any;
  producttype_list: any;
  productgroup_list: any;
  productunitclass_list:any;
  productunit_list: any[] = [];
  productform!: FormGroup | any;
  hasError?: boolean;
  selectedProducttype: any;
  selectedProductgroup: any;
  selectedUnitclass: any;
  selectedUnits: any;

  returnUrl?: string;
  reactiveForm!: FormGroup;

  submitted = false;
  Productgid: any;
  shopifyproductid:any;
  Product_list:any;
  // private fields
  responsedata: any;
  editProductSummary_list: any;


 constructor(private renderer: Renderer2,private NgxSpinnerService: NgxSpinnerService, private el: ElementRef,public service :SocketService, private ToastrService: ToastrService,private route:Router,private router: ActivatedRoute ) {
    this.product = {} as IProduct;
  }


  ngOnInit(): void {

 const shopify_productid = this.router.snapshot.paramMap.get('id');
    this.shopifyproductid = shopify_productid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.shopifyproductid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)

    this.productform = new FormGroup({
      product_type: new FormControl('', Validators.required),
      product_name: new FormControl('', Validators.required),
      product_desc: new FormControl(''),
      vendor: new FormControl(''),
      shopify_productid: new FormControl(''),
      product_status: new FormControl(''),


    });
   

    this.GetEditProductSummary(deencryptedParam)
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
  GetEditProductSummary(shopifyproductid: any) {
    var url = 'Product/GetEditShopifyProductSummary'
    let param = {shopifyproductid : shopifyproductid}
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata=result;
      this.editProductSummary_list = result.GetEditProductSummary;

      // this.product = result;
      // console.log(this.product)
      console.log(this.editProductSummary_list)

      this.productform.get("shopify_productid")?.setValue(this.editProductSummary_list[0].shopify_productid);
      this.productform.get("product_type")?.setValue(this.editProductSummary_list[0].product_type);
      this.productform.get("product_name")?.setValue(this.editProductSummary_list[0].product_name);
      this.productform.get("vendor")?.setValue(this.editProductSummary_list[0].vendor_name);
       this.productform.get("product_status")?.setValue(this.editProductSummary_list[0].product_status);
       console.log(this.editProductSummary_list[0].product_status)
      this.productform.get("product_desc")?.setValue(this.editProductSummary_list[0].product_desc);
 
    });
  } 
  get producttypename() {
    return this.productform.get('producttypename');
  }
  get productgroupname() {
    return this.productform.get('productgroupname');
  }
  get productuomclassname() {
    return this.productform.get('productuomclassname');
  }
  get productuomname() {
    return this.productform.get('productuomname');
  }
  get productcodecontrol() {
    return this.productform.get('product_code');
  }
  get productnamecontrol() {
    return this.productform.get('product_name');
  }
  get costpricecontrol() {
    return this.productform.get('cost_price');
  }
  get mrpcontrol() {
    return this.productform.get('mrp_price');
  }
 

  public validate(): void {
    console.log(this.productform.status =="VALID");
   
    if (this.productform.status =="VALID") {
      
     //console.log(this.productform.value)
     this.NgxSpinnerService.show();
     const api = 'Product/ShopifyProductUpdate';

     this.service.post(api, this.productform.value).subscribe(
      (result: any) => {
  
        if(result.status ==false){
          this.NgxSpinnerService.hide();
          this.ToastrService.warning(result.message)

        }
        else{
          this.NgxSpinnerService.hide();
          this.ToastrService.success(result.message)
          this.route.navigate(['/crm/CrmSmmShopifycustomer']);
            

        }

      });
  }
}
}
