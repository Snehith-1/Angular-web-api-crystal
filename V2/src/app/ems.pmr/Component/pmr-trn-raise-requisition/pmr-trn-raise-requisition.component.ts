import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { NgxSpinnerService } from 'ngx-spinner';

interface RaisePR
{
  purchaserequisition_gid: string;
  purchaserequisition_date: string;
  branch_name: string;
  department_name: string;
  costcenter_name: string;
  available_amount: string;
  user_firstname: string;
  product_gid: string;
  product_name: string;
  productgroup_name: string;
  product_code: string;
  productuom_name: string;
  qty_requested: string;
}

@Component({
  selector: 'app-pmr-trn-raise-requisition',
  templateUrl: './pmr-trn-raise-requisition.component.html',
  styleUrls: ['./pmr-trn-raise-requisition.component.scss']
})
export class PmrTrnRaiseRequisitionComponent {

  RaiseRequestForm: FormGroup | any; 
  productform: FormGroup | any;
  raisepr!: RaisePR;
  costcenter_list: any [] = [];
  branch_list: any [] = [];
  department_list: any [] = [];
  responsedata: any;
  product_list: any [] = [];
  products_list: any [] = [];
  parameterValue: any;
  productcode_list: any;
  productgroup_list: any;
  productunit_list: any;
  productdetails_list: any;
  productsummary_list1: any;
  POproductlist: any;
  Getuserdtl: any [] = [];
  Getcostcenter: any [] = [];
  mdlBranchName: any;
  mdlCost: any;
  mdlproduct: any;
  mdlproductdes: any;
  mdlproductuomname: any;
  mdlproductcode: any;
  mdlproductgroup: any;
  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService, private route :ActivatedRoute,private router:Router,public service: SocketService ,public NgxSpinnerService:NgxSpinnerService) {
    this.raisepr = {} as RaisePR;
    
  }
 
  ngOnInit(): void 
  
  {
    this.POproductsummary();
    this.RaiseRequestForm = new FormGroup({
      purchaserequisition_gid: new FormControl(''),
      purchaserequisition_date: new FormControl(this.getCurrentDate()),
      branch_name: new FormControl(''),
      department_name: new FormControl(''),
      costcenter_name: new FormControl(''),
      available_amount: new FormControl(''),
      user_firstname: new FormControl(''),
 
    });
    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    flatpickr('.date-picker', options);
//branch
    var api = 'PmrTrnPurchaseRequisition/GetBranch1';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.branch_list = this.responsedata.GetBranch1;
      this.RaiseRequestForm.get("branch_gid")?.setValue(this.branch_list[0].branch_gid);

    });
    //deptandname
    var api = 'PmrTrnPurchaseRequisition/Getuserdtl';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.Getuserdtl = this.responsedata.Getuserdtl;
      this.RaiseRequestForm.get("department_name")?.setValue(this.Getuserdtl[0].department_name);
      this.RaiseRequestForm.get("user_firstname")?.setValue(this.Getuserdtl[0].employee_name);

    });
//cost center
debugger
    var api = 'PmrTrnPurchaseRequisition/Getcostcenter';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.costcenter_list = this.responsedata.Getcostcenter;
    
    });

    var api = 'PmrTrnPurchaseRequisition/GetProductCode1';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productcode_list = this.responsedata.GetProductCode1;

    });
    var api = 'PmrTrnPurchaseRequisition/GetProduct1';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productdetails_list = this.responsedata.GetProduct1;

      setTimeout(() => {

        $('#product_list').DataTable();

      }, 0.1);
    });

    var api = 'PmrMstProduct/GetProductUnit';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productunit_list = this.responsedata.GetProductUnit;

    });
    var api = 'PmrMstProduct/GetProductGroup';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productgroup_list = this.responsedata.GetProductGroup;
      setTimeout(() => {

        $('#productgroup_list').DataTable();

      }, 0.1);
    });
    
    
    
   
        
    this.productform = new FormGroup({
      product_gid: new FormControl(''),
      productgroup_name: new FormControl(''),
      product_name: new FormControl(''),
      productuom_gid: new FormControl(''),
      productgroup_gid: new FormControl(''),
      product_code: new FormControl(''),
      productgroup: new FormControl(''),
      productuom_name: new FormControl(''),
      productname: new FormControl(''),
      qty_requested: new FormControl(''),
      display_field: new FormControl(''),
    })
    this.RaiseRequestForm = new FormGroup({
      purchaserequisition_gid: new FormControl(''),
      purchaserequisition_date: new FormControl(this.getCurrentDate()),
      branch_name: new FormControl(''),
      branch_gid: new FormControl(''),
      department_name: new FormControl(''),
      costcenter_name: new FormControl(''),
      costcenter_gid: new FormControl(''),
      available_amount: new FormControl(''),
      user_firstname: new FormControl(''),
      purchaserequisition_remarks: new FormControl(''),
      purchaserequisition_referencenumber: new FormControl(''),
      priority_remarks: new FormControl('Y'),
    })
    
  }

  getCurrentDate(): string {
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0'); // January is 0!
    const yyyy = today.getFullYear();
   
    return dd + '-' + mm + '-' + yyyy;
  }
  get purchaserequisition_date() {
    return this.RaiseRequestForm.get('purchaserequisition_date')!;
  }
  get branch_name() {
    return this.RaiseRequestForm.get('branch_name')!;
  }
  get costcenter_name() {
    return this.RaiseRequestForm.get('costcenter_name')!;
  }
  get available_amount() {
    return this.RaiseRequestForm.get('available_amount')!;
  }
  get product_name() {
    return this.productform.get('product_name')!;
  }
  get product_code() {
    return this.productform.get('product_code')!;
  }
  get productuom_name() {
    return this.productform.get('productuom_name')!;
  }
  get productgroup_name() {
    return this.productform.get('productgroup_name')!;
  }

  GetOnChangecostcenter() {
    debugger
    let costcenter_gid = this.RaiseRequestForm.value.costcenter_name
    let param = {
      costcenter_gid: costcenter_gid
    }
    var url = 'PmrTrnPurchaseRequisition/GetOnChangecostcenter';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.RaiseRequestForm.get("available_amount")?.setValue(result.Getcostcenter[0].available_amount);
 
    });
  }

  GetOnChangeProductName(){
    debugger;
    let product_gid = this.productform.value.product_name.product_gid;
    let param = {
      product_gid: product_gid
    }
    var url = 'PmrTrnPurchaseRequisition/GetOnChangeProductName';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.productform.get("product_code")?.setValue(result.GetProductCode1[0].product_code);
      this.productform.get("productuom_name")?.setValue(result.GetProductCode1[0].productuom_name);
      this.productform.get("productgroup_name")?.setValue(result.GetProductCode1[0].productgroup_name);
      this.productform.get("display_field")?.setValue(result.GetProductCode1[0].display_field);
      this.productform.value.productgroup_gid = result.GetProductCode1[0].productgroup_gid,
        this.productform.value.productuom_gid = result.GetProductCode1[0].productuom_gid
    });

  }


  OnChangecostcenter() {
    debugger
    let costcenter_gid = this.RaiseRequestForm.value.costcenter_name
    let param = {
      costcenter_gid: costcenter_gid
    }
    var url = 'PmrTrnPurchaseRequisition/GetOnChangecostcenter';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.RaiseRequestForm.get("available_amount")?.setValue(result.Getcostcenter[0].available_amount);
 
    });
  }
onclearproduct(){
  this.mdlproduct=""
  this.mdlproductuomname=""
  this.mdlproductcode=""
  this.mdlproductdes=""
  this.mdlproductgroup=""

}
  // OnChangeProductName(){
  //   debugger;
  //   let product_gid = this.productform.value.product_name
  //   let param = {
  //     product_gid: product_gid
  //   }
  //   var url = 'PmrTrnPurchaseRequisition/GetOnChangeProductName';
  //   this.service.getparams(url, param).subscribe((result: any) => {
  //     this.productform.get("product_code")?.setValue(result.GetProductCode1[0].product_code);

  //     this.productform.get("productuom_name")?.setValue(result.GetProductCode1[0].productuom_name);
  //     this.productform.get("productgroup_name")?.setValue(result.GetProductCode1[0].productgroup_name);
  //     this.productform.value.productgroup_gid = result.GetProductCode1[0].productgroup_gid,
  //       this.productform.value.productuom_gid = result.GetProductCode1[0].productuom_gid
  //   });

  // }
  productSubmit(){
    debugger
    console.log(this.productform.value)
    var params = {    
    
      product_name: this.productform.value.product_name.product_name,
      product_gid: this.productform.value.product_name.product_gid,
      qty_requested: this.productform.value.qty_requested,
      productgroup_gid: this.productform.value.productgroup_gid,
      productgroup_name: this.productform.value.productgroup_name,
      product_code: this.productform.value.product_code,
      productuom_gid: this.productform.value.productuom_gid,
      productuom_name: this.productform.value.productuom_name, 
      display_field: this.productform.value.display_field,   
    }
    console.log(params)
    var api = 'PmrTrnPurchaseRequisition/PostOnAddpr';
    this.NgxSpinnerService.show();
    this.service.post(api, params).subscribe((result: any) => {
    this.productform.reset();
    this.NgxSpinnerService.hide();
    this.POproductsummary()
    },
    );

  }
  
  
 

  onclose()
  {
    this.router.navigate(['/pmr/PmrTrnPurchaseRequisition']);  

  }
  POproductsummary() {
    debugger
    var api = 'PmrTrnPurchaseRequisition/GetProductSummary';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productsummary_list1 = this.responsedata.productsummary_list1;
    });
}
openModaldelete(parameter: string){
  this.parameterValue = parameter
}
ondelete(){
  var url = 'PmrTrnPurchaseRequisition/GetDeletePrProductSummary'
  this.NgxSpinnerService.show();
  let param = {
    tmppurchaserequisition_gid : this.parameterValue 
  }
  this.service.getparams(url,param).subscribe((result: any) => {
    if(result.status ==false){
      this.ToastrService.warning(result.message)
      this.NgxSpinnerService.hide();
    }
    else{
      
      this.ToastrService.success(result.message)
      this.NgxSpinnerService.hide();
      
    }
    
    this.POproductsummary();


  });
}
onSubmit() {
  debugger
  console.log(this.RaiseRequestForm.value)
    var params = {
      purchaserequisition_gid:this.RaiseRequestForm.value.purchaserequisition_gid,
      branch_gid: this.RaiseRequestForm.value.branch_name.branch_gid,
      purchaserequisition_date:this.RaiseRequestForm.value.purchaserequisition_date,
      department_name:this.RaiseRequestForm.value.department_name,
      costcenter_name:this.RaiseRequestForm.value.costcenter_name.costcenter_gid,
      available_amount: this.RaiseRequestForm.value.available_amount,
      user_firstname: this.RaiseRequestForm.value.user_firstname,
      purchaserequisition_remarks: this.RaiseRequestForm.value.purchaserequisition_remarks,
      priority_remarks:this.RaiseRequestForm.value.priority_remarks,
      purchaserequisition_referencenumber:this.RaiseRequestForm.value.purchaserequisition_referencenumber,
      
  
    }
    this.NgxSpinnerService.show();
    var url='PmrTrnPurchaseRequisition/PostPurchaseRequisition'
  
    this.service.postparams(url, params).subscribe((result: any) => {
      
      if(result.status ==false){
        this.NgxSpinnerService.hide();
        this.ToastrService.warning(result.message)
      
      }
      else{
        this.NgxSpinnerService.hide();
        this.ToastrService.success(result.message)
        this.router.navigate(['/pmr/PmrTrnPurchaseRequisition']); 
        
      }
    
     
    },
    );
  }
  

}