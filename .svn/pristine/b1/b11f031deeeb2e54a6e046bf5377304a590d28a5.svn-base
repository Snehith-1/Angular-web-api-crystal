import { Component, ElementRef, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router,ActivatedRoute } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';

interface IProduct {

  product_gid:string;
  producttypename: string;
  productgroupname: string;
  productuomclassname: string;
  productuomname: string;
  product_code:string;
  product_name: string;
  cost_price: string;
  mrp_price: string;
  product_desc: string;
}
@Component({
  selector: 'app-pmr-mst-product-edit',
  templateUrl: './pmr-mst-product-edit.component.html',
  styleUrls: ['./pmr-mst-product-edit.component.scss']
})
export class PmrMstProductEditComponent  {
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
  Product_list:any;
  // private fields
  responsedata: any;
  editProductSummary_list: any;


 constructor(private renderer: Renderer2, private el: ElementRef,public service :SocketService, private ToastrService: ToastrService,private route:Router,private router: ActivatedRoute ) {
    this.product = {} as IProduct;
  }


  ngOnInit(): void {

 const product_gid = this.router.snapshot.paramMap.get('product_gid');
    this.product_gid = product_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.product_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)


    this.productform = new FormGroup({

      product_code: new FormControl(this.product.product_code, [
        Validators.required,
      ]),
      product_name: new FormControl(this.product.product_name, [
        Validators.required,
      ]),
      product_gid: new FormControl(''),
      purchasewarrenty_flag: new FormControl(''),
      serial_flag: new FormControl(''),
      batch_flag: new FormControl(''),
      expirytracking_flag: new FormControl(''),
      product_desc: new FormControl(''),
      
      
      producttypename: new FormControl(this.product.producttypename, [
        Validators.required,
       
      ]),
      productgroupname: new FormControl(this.product.productgroupname, [
        Validators.required,
      
      ]),

      productuomclassname: new FormControl(this.product.productuomclassname, [
        Validators.required,
      ]),
      productuomname: new FormControl(this.product.productuomname, [
        Validators.required,
      ]),
     
      cost_price: new FormControl(this.product.cost_price, [
        Validators.required,
      ]),
      mrp_price: new FormControl(this.product.mrp_price, [
        Validators.required,
      ]),
    
    }


    );

    var api = 'PmrMstProduct/GetProductGroup';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productgroup_list = this.responsedata.GetProductGroup;
      setTimeout(()=>{  
         $('#productgroup_list').DataTable();
      }, 0.1);
    });

    var api = 'PmrMstProduct/GetProducttype';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.producttype_list = this.responsedata.GetProducttype;

    });

    var api = 'PmrMstProduct/GetProductUnitclass';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productunitclass_list = this.responsedata.GetProductUnitclass;

    });

    var api = 'PmrMstProduct/GetProductUnit';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productunit_list = this.responsedata.GetProductUnit;

    });
    this.GetEditProductSummary(deencryptedParam)
  } 

  GetEditProductSummary(product_gid: any) {
    var url = 'PmrMstProduct/GetEditProductSummary'
    let param = {product_gid : product_gid}
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata=result;
      this.editProductSummary_list = result.GetEditProductSummary;

      // this.product = result;
      console.log(this.product)
      console.log(this.editProductSummary_list)

      this.productform.get("product_gid")?.setValue(this.editProductSummary_list[0].product_gid);
      this.productform.get("producttypename")?.setValue(this.editProductSummary_list[0].producttype_name);
      this.productform.get("productgroupname")?.setValue(this.editProductSummary_list[0].productgroup_name);
      this.productform.get("productuomclassname")?.setValue(this.editProductSummary_list[0].productuomclass_name);
      this.productform.get("productuomname")?.setValue(this.editProductSummary_list[0].productuom_name);
      this.selectedProducttype = this.editProductSummary_list[0].producttypename;
      this.selectedProductgroup = this.editProductSummary_list[0].productgroupname;
      this.selectedUnitclass = this.editProductSummary_list[0].productuomclassname;
      this.selectedUnits = this.editProductSummary_list[0].productuomname;
      this.productform.get("product_code")?.setValue(this.editProductSummary_list[0].product_code);
      this.productform.get("product_name")?.setValue(this.editProductSummary_list[0].product_name);
      this.productform.get("mrp_price")?.setValue(this.editProductSummary_list[0].mrp_price);
      this.productform.get("cost_price")?.setValue(this.editProductSummary_list[0].cost_price);
      this.productform.get("product_desc")?.setValue(this.editProductSummary_list[0].product_desc);
      this.productform.get("purchasewarrenty_flag")?.setValue(this.editProductSummary_list[0].purchasewarrenty_flag);
      this.productform.get("serial_flag")?.setValue(this.editProductSummary_list[0].serial_flag);
      this.productform.get("batch_flag")?.setValue(this.editProductSummary_list[0].batch_flag);
      this.productform.get("expirytracking_flag")?.setValue(this.editProductSummary_list[0].expirytracking_flag);
      
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
    this.product = this.productform.value;
   
    if (this.product.product_code != null && this.product.product_name != null && this.product.producttypename != null && this.product.productgroupname != null 
      && this.product.productuomclassname != null && this.product.productuomname != null &&
       this.product.cost_price != null && this.product.product_desc != null) {
      
     console.log(this.productform.value)
     const api = 'PmrMstProduct/PmrMstProductUpdate';

     this.service.post(api, this.productform.value).subscribe(
      (result: any) => {
  
        if(result.status ==false){

          this.ToastrService.warning(result.message)

        }
        else{
          this.ToastrService.success(result.message)
          this.route.navigate(['/pmr/PmrMstProductSummary']);
            

        }

      });
  }
}
   }




