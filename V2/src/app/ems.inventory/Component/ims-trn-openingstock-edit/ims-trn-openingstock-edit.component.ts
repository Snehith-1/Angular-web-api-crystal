import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { enc } from 'crypto-js';
import { NgxSpinnerService } from 'ngx-spinner';
interface addopeningstock{
  
  branch_name: string;
  branch_gid: string;
  productgroup_gid: string;
  productgroup_name: string;
  customerproduct_code: string;
  product_code: string;
  product_gid: any;
  product_name: string;
  productuom_name: string;
  productuom_gid: string;
  cost_price: string;
  stock_qty: string;
  location_name: string;
  product_desc: string;
  stock_gid: string;
  product_status: string;

}


@Component({
  selector: 'app-ims-trn-openingstock-edit',
  templateUrl: './ims-trn-openingstock-edit.component.html',
  styleUrls: ['./ims-trn-openingstock-edit.component.scss']
})
export class ImsTrnOpeningstockEditComponent {
  producttype_list: any[] = [];
  productgroup_list: any[] = [];
  branch_list : any [] =[];
  GetLocation :any[]= [];
  productunitclass_list: any[] = [];
  productunit_list: any[] = [];
  product_list: any[] = [];
  productcode_list: any[] = [];
  rbo_status: any[] = [];
  Form: FormGroup | any;
  hasError?: boolean;
  returnUrl?: string;
  mdlPrdType:any;
  mdlPrdUnitC:any;
  mdlPrdUnit:any;
  mdlPrdName:any;
   GetproductsCode:any;
  responsedata: any;
  submitted = false;
  
  result: any;
  productform: FormGroup | any;
  addopeningstock !:addopeningstock;
  fb: any;
  mdlProductName:any;
  mdlBranchName :any;
 uom_gid:any;
 stock_gid:any;
 editProductSummary_list:any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, public service: SocketService,public NgxSpinnerService:NgxSpinnerService, private router: ActivatedRoute,private route:Router) {
    this.addopeningstock = {} as addopeningstock;
  
    this.productform = new FormGroup({
      branch_name: new FormControl(''),
      product_gid :new FormControl(''),
      productgroup_name:new FormControl(''),
      productuom_name:new FormControl(''),
      productuom_gid:new FormControl(''),
      product_code:new FormControl(''),
      product_name:new FormControl(''),
      product_desc:new FormControl(''),
      location_name :new FormControl(''),
      stock_qty:new FormControl(''),
      cost_price:new FormControl(''),
      stock_gid:new FormControl(''),
      product_status:new FormControl(''),
      branch_gid:new FormControl(''),

    });
  
  }
  ngOnInit(): void {
    const stock_gid = this.router.snapshot.paramMap.get('stock_gid');
    this.stock_gid = stock_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.stock_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
   
    debugger
    this.GetEditOpeningStockSummary(deencryptedParam)}

    GetEditOpeningStockSummary(stock_gid: any) {
      var url = 'ImsTrnOpeningStock/GetEditOpeningStockSummary'
      debugger
      let param = {stock_gid : stock_gid}
      this.service.getparams(url, param).subscribe((result: any) => {
        this.responsedata=result;
        this.editProductSummary_list = result.GetEditOpeningStock;
  
        // this.product = result;
       console.log(this.addopeningstock)
        console.log(this.editProductSummary_list)
        this.productform.get("stock_gid")?.setValue(result.GetEditOpeningStock[0].stock_gid);
        this.productform.get("product_name")?.setValue(result.GetEditOpeningStock[0].product_name);
        this.productform.get("product_code")?.setValue(result.GetEditOpeningStock[0].product_code);
        this.productform.get("productuom_name")?.setValue(result.GetEditOpeningStock[0].productuom_name);
        this.productform.get("productgroup_name")?.setValue(result.GetEditOpeningStock[0].productgroup_name);
        this.productform.get("branch_name")?.setValue(result.GetEditOpeningStock[0].branch_name);
        this.productform.get("location_name")?.setValue(result.GetEditOpeningStock[0].location_name);
        this.productform.get("product_desc")?.setValue(result.GetEditOpeningStock[0].product_desc);
        this.productform.get("stock_qty")?.setValue(result.GetEditOpeningStock[0].opening_stock);
        this.productform.get("cost_price")?.setValue(result.GetEditOpeningStock[0].cost_price);
        this.productform.get("product_gid")?.setValue(result.GetEditOpeningStock[0].product_gid);
        this.productform.get("productuom_gid")?.setValue(result.GetEditOpeningStock[0].productuom_gid);
        this.productform.get("branch_gid")?.setValue(result.GetEditOpeningStock[0].branch_gid);
        this.productform.get("product_status")?.setValue(result.GetEditOpeningStock[0].product_status);
 
      });
  }

  get branch_name() {

    return this.productform.get('branch_name')!;

  };
  get productgroup_name() {

    return this.productform.get('productgroup_name')!;

  };
  get productuomclass_name() {

    return this.productform.get('productuomclass_name')!;

  };
  get productuom_name() {

    return this.productform.get('productuom_name')!;

  };
  get productuom_gid() {

    return this.productform.get('productuom_gid')!;

  };
  get product_code() {

    return this.productform.get('product_code')!;

  };
  get stock_qty() {

    return this.productform.get('stock_qty')!;

  };
  get product_name() {

    return this.productform.get('product_name')!;

  }
  get location_name() {

    return this.productform.get('location_name')!;

  }




  initForm() {
    this.productform = this.fb.group({
    
      product_desc: [
        this.productform.product_desc,
        Validators.compose([
        Validators.required,

        ]),
      ],
      stock_gid: [
        this.productform.stock_gid,
        Validators.compose([
        Validators.required,

        ]),
      ],
      product_gid: [
        this.productform.product_gid,
        Validators.compose([
        Validators.required,

        ]),
      ],

      stock_qty: [
        this.productform.stock_qty,
        Validators.compose([
        Validators.required,

        ]),
      ],
     

      cost_price: [
        this.productform.cost_price,
        Validators.compose([
          Validators.required

        ]),
      ],
      product_status: [
        this.productform.product_status,
        Validators.compose([
          Validators.required

        ]),
      ],
    });
  
  }
 

redirecttolist(){
  this.route.navigate(['/ims/ImsTrnOpeningstockSummary']);

}


public validate(): void {
  this.addopeningstock = this.productform.value;
 
  if (this.addopeningstock.stock_qty != null && this.addopeningstock.product_desc != null && this.addopeningstock.cost_price != null) {
    debugger
   console.log(this.productform.value)
   const api = 'ImsTrnOpeningStock/PostOpeningStockUpdate';
   this.NgxSpinnerService.show();

   this.service.post(api, this.productform.value).subscribe(
    (result: any) => {

      if(result.status ==false){

        this.ToastrService.warning(result.message)

      }
      else{
        this.ToastrService.success(result.message)
        this.route.navigate(['/ims/ImsTrnOpeningstockSummary']);
          

      }
      this.NgxSpinnerService.hide();

    });
}
}


}

