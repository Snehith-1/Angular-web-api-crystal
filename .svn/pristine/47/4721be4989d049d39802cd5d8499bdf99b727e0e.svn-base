import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription, Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../../environments/environment.development';
import { SocketService } from '../../../ems.utilities/services/socket.service';

import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-pmr-mst-product-add',
  templateUrl: './pmr-mst-product-add.component.html',
  styleUrls: ['./pmr-mst-product-add.component.scss']
})
export class PmrMstProductAddComponent   implements OnInit, OnDestroy{
  producttype_list: any[] = [];
  productgroup_list: any[] = [];
  productunitclass_list: any[] = [];
  productunit_list: any[] = [];
  rbo_status: any[] = [];
  Form: FormGroup | any;
  hasError?: boolean;
  returnUrl?: string;
  mdlproducttype:any;
  mdlPrdUnitC:any;
  mdlPrdUnit:any;
  mdlPrdName:any;
  mdlproductgroup:any;
  mdlproductunit:any;
  submitted = false;


  // private fields
  private unsubscribe: Subscription[] = []; 
  responsedata: any;
  result: any;
  productform: FormGroup<{ product_code: FormControl<any>; product_name: FormControl<any>; product_desc: FormControl<any>; mrp: FormControl<any>; cost_price: FormControl<any>; }> | any;
  

  constructor(
    private fb: FormBuilder,
    public NgxSpinnerService:NgxSpinnerService,

    private route: ActivatedRoute,
    private router: Router,
    private service: SocketService,
    private ToastrService: ToastrService,
  ) 
  {
    this.productform = new FormGroup({
      producttype_name: new FormControl('', Validators.required),
      productgroup_name:new FormControl('', Validators.required),
      productuomclass_name:new FormControl('', Validators.required),
      productuom_name:new FormControl('',Validators.required),
      product_code:new FormControl('',Validators.required),
      product_name:new FormControl('',Validators.required),
      product_desc:new FormControl(''),
      mrp_price:new FormControl('',Validators.required),
      cost_price:new FormControl('',Validators.required),
      expirytracking_flag:new FormControl('N',Validators.required),
      batch_flag:new FormControl('N',Validators.required),
      serial_flag:new FormControl('N',Validators.required),
      purchasewarrenty_flag:new FormControl('N',Validators.required),

    });
  }
  ngOnDestroy(): void {

  }
  ngOnInit(): void {

    var api = 'PmrMstProduct/GetProductGroup';
    this.service.get(api).subscribe((result: any) => {
      $('#productgroup_list').DataTable().destroy();
      this.productgroup_list = result.GetProductGroup;
      // setTimeout(()=>{  

      //   $('#productgroup_list').DataTable();

      // }, 0.1);
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
  }

  get producttype_name() {

    return this.productform.get('producttype_name')!;

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
      producttype_name: [ this.productform.producttype_name,  Validators.compose([

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
      

      mrp_price: [
        this.productform.mrp_price,
        Validators.compose([
        Validators.required,

        ]),
      ],
       batch_flag: new FormControl(''),
       serial_flag: new FormControl(''),
       product_desc:new FormControl(''),
       expirytracking_flag: new FormControl(''),
       purchasewarrenty_flag: new FormControl(''),

      cost_price: [
        this.productform.cost_price,
        Validators.compose([
          Validators.required

        ]),
      ],
    });
  
  }
 
onadd() {
  debugger
  if(this.productform.value.producttype_name!=null &&
     this.productform.value.productgroup_name!=null &&
      this.productform.value.productuomclass_name!=null &&
       this.productform.value.productuom_name!=null &&
        this.productform.value.product_code !='' &&
         this.productform.value.product_name !='' &&
         this.productform.value.mrp_price !='' &&
          this.productform.value.cost_price != ''){
  console.log(this.productform)
  var api = 'PmrMstProduct/PostProduct';
  this.NgxSpinnerService.show();
  this.service.post(api, this.productform.value).subscribe(
    (result: any) => {

      if (result.status == true) {
        this.ToastrService.success(result.message)
       
        this.router.navigate(['pmr/PmrMstProductSummary']);
        this.NgxSpinnerService.hide();
        

      }
      else {
        this.ToastrService.warning(result.message)
  
      }
        
        

      }
    ,(error: any) => {
      if (error.status === 401)
        this.router.navigate(['pages/401'])
      else if (error.status === 404)
        this.router.navigate(['pages/404'])
    });
  }
  else{
    this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
  }
  
}
redirecttolist(){
  this.router.navigate(['/pmr/PmrMstProductSummary']);

}
productunitclass() {
  let productuomclass_gid = this.productform.get("productuomclass_name")?.value;
   
   let param = {
    productuomclass_gid : productuomclass_gid
  }
    var url = 'PmrMstProduct/GetOnChangeProductUnitClass';
  this.service.getparams(url,param).subscribe((result:any)=>{    
    this.responsedata=result;
    
     this.productunit_list = this.responsedata.GetProductUnit;
    
  });
}
}


