import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, Observable } from 'rxjs';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';
import { NgxSpinnerService } from 'ngx-spinner';

interface Customeren {
  enquiry_gid: string,
  customer_name : string,
  customercontact_gid : string,
  product_gid : string,
  vendor_gid:string,
  enquiry_date : string,
  branch_name : string,
  enquiry_referencenumber : string,
  contact_number : string,
  customercontact_name : string,
  contact_email : string,
  enquiry_remarks : string,
  contact_address : string,
  vendor_requirement : string,
  landmark : string,
  closure_date : string,
  product_name : string,
  product_code : string,
  productuom_name : string,
  productgroup_name : string,
  vendorproduct_code : string,
  qty_requested : string,
  potential_value : string,
  product_requireddate : string,
  customerbranch_name: string,
  user_firstname: string,
  product_requireddateremarks: string,
  display_field: string,
  customer_rating: string,
  branch_gid: string,
  employee_gid: string;
  Vendor_gid: string;
  vendor_companyname:String;
  contact_telephonenumber:string;
}

@Component({
  selector: 'app-pmr-trn-raise-enquiryadd',
  templateUrl: './pmr-trn-raise-enquiryadd.component.html',
  styleUrls: ['./pmr-trn-raise-enquiryadd.component.scss']
})
export class PmrTrnRaiseEnquiryaddComponent {



  private unsubscribe: Subscription[] = [];
  file!:File;

  PostAll: any;
  combinedFormData: FormGroup | any;
  productform: FormGroup;
  ProductEdit: FormGroup | any;
  responsedata: any;
  parameterValue: any;
  customer_list: any;
  products_list: any;
  products: any[] = [];
  branch_list: any[] = [];
  assign_list: any[]=[];
  enquiry_list: any[]=[];
  directeditenquiry_list:any[]=[];
  response_data :any;
  mdlcus:any;
  mdlEnq:any;
  txtproductgroup_name:any;
  txtproductcode:any;
  txtunit:any;
  mdlBranch:any;
  mdlEmployee:any;
  mdlproduct:any;
  uom_list:any;
  customeren!: Customeren
  POproductlist: any;
  productname_list : any[] = [];
  productgrp_list : any[] = [];
  products_list1: any;
  products_unit: any;
  parameterValue1: any;
  GetVendor:any;
  GetVendorlist:any;
  GetVendor_list:any;
  constructor(private fb: FormBuilder,private route: ActivatedRoute,private router: Router,private service: SocketService,private ToastrService: ToastrService,private NgxSpinnerService:NgxSpinnerService) {
    
    this.productform = new FormGroup({
      product_gid : new FormControl(''),
      product_name : new FormControl(''),
      product_code : new FormControl(''),
      productuom_name : new FormControl(''),
      productgroup_name : new FormControl(''),
      vendorproduct_code : new FormControl(''),
      qty_requested : new FormControl(''),
      potential_value : new FormControl(''),
      product_requireddate : new FormControl(''),
      product_requireddateremarks: new FormControl(''),
      display_field: new FormControl('')


    });

    this.combinedFormData = new FormGroup({
      enquiry_gid: new FormControl(''),
      customer_name : new FormControl(''),
      customercontact_gid : new FormControl(''),
      product_gid : new FormControl(''),
      vendor_gid:new FormControl(''),
      enquiry_date : new FormControl(this.getCurrentDate()),
      branch_name : new FormControl(''),
      branch_gid: new FormControl(''),
      enquiry_referencenumber : new FormControl(''),
      contact_number : new FormControl(''),
      customercontact_name : new FormControl(''),
      contact_email : new FormControl(''),
      enquiry_remarks : new FormControl(''),
      contact_address : new FormControl(''),
      vendor_requirement : new FormControl(''),
      landmark : new FormControl(''),
      closure_date : new FormControl(''),
      product_name : new FormControl(''),
      product_code : new FormControl(''),
      productuom_name : new FormControl(''),
      productgroup_name : new FormControl(''),
      vendorproduct_code : new FormControl(''),
      qty_requested : new FormControl(''),
      potential_value : new FormControl(''),
      product_requireddate : new FormControl(''),
      customerbranch_name: new FormControl(''),
      user_firstname: new FormControl(''),
      product_requireddateremarks: new FormControl(''),
      display_field: new FormControl(''),
      customer_rating: new FormControl(''),
      employee_gid: new FormControl(''),
      Vendor_gid: new FormControl(''),
      vendor_companyname: new FormControl(''),
      contact_telephonenumber: new FormControl(''),
      address1: new FormControl(''),
      contactperson_name: new FormControl(''),
      email_id: new FormControl(''),
      vendorbranch_name: new FormControl(''),

    });
     
  } 
  

  ngOnInit(): void {
    this.POproductsummary();
    var api = 'PmrTrnRaiseEnquiry/GetProductGroup';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.products_list = this.responsedata.GetProductGrp;

    });
   
    const options: Options = {
      dateFormat: 'd-m-Y',
    };
flatpickr('.date-picker', options);
    var api = 'PmrTrnRaiseEnquiry/GetProduct';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.products_list1 = this.responsedata.GetProducts;

    });

    
    var api = 'PmrTrnRaiseEnquiry/GetProductUnit';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.uom_list = this.responsedata.GetProductUnits;

    });
    var api = 'PmrTrnRaiseEnquiry/GetVendorName';
    debugger
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.GetVendor_list = result.GetVendor;
      this.combinedFormData.get("vendor_gid")?.setValue(this.GetVendor_list[0].vendor_gid);
      // this.combinedFormData.get("vendor_companyname")?.setValue(this.GetVendor_list[0].vendor_companyname);

    });

    var url = 'PmrTrnRaiseEnquiry/GetBranchDet';
this.service.get(url).subscribe((result:any)=>{
  this.branch_list = result.GetBranchDet;
   this.combinedFormData.get("branch_gid")?.setValue(this.branch_list[0].branch_gid);
 });

 var url = 'PmrTrnRaiseEnquiry/GetEmployeePerson'
 this.service.get(url).subscribe((result:any)=>{
   this.assign_list = result.GetEmployeePerson;
  });
  

}
getCurrentDate(): string {
  const today = new Date();
  const dd = String(today.getDate()).padStart(2, '0');
  const mm = String(today.getMonth() + 1).padStart(2, '0'); // January is 0!
  const yyyy = today.getFullYear();
 
  return dd + '-' + mm + '-' + yyyy;
}
  onadd(){}


  GetOnChangeCustomerName(){
    
    let vendor_gid = this.combinedFormData.value.vendor_companyname.vendor_gid;
    let param ={
      vendor_gid :vendor_gid
    }
    var url = 'PmrTrnRaiseEnquiry/GetVendorDtl';
    this.service.getparams(url,param).subscribe((result: any) => {
      this.GetVendorlist = result.GetVendorlist;
      //this.cusraiseform.get("customercontact_gid")?.setValue(result.GetCustomer[0].customercontact_gid  );
      this.combinedFormData.get("vendorbranch_name")?.setValue("H.Q");
      this.combinedFormData.get("email_id")?.setValue(result.GetVendorlist[0].email_id);
      this.combinedFormData.get("contactperson_name")?.setValue(result.GetVendorlist[0].contactperson_name);
      this.combinedFormData.get("address1")?.setValue(result.GetVendorlist[0].address1);
      this.combinedFormData.get("contact_telephonenumber")?.setValue(result.GetVendorlist[0].contact_telephonenumber);
      //this.cusraiseform.value.leadbank_gid = result.GetCustomer[0].leadbank_gid
      //this.cusraiseform.value.leadbank_gid = result.GetCustomer[0].leadbank_gid
    });

  }
  GetOnChangeProductsName() {
    debugger;
    let product_gid = this.productform.value.product_name.product_gid;
    let param = {
      product_gid: product_gid
    }
    var url = 'PmrTrnRaiseEnquiry/GetOnChangeProductsName';
    this.service.getparams(url, param).subscribe((result: any) => {
      debugger
      this.productform.get("product_code")?.setValue(result.GetProductsName[0].product_code);
      this.productform.get("productuom_name")?.setValue(result.GetProductsName[0].productuom_name);
      this.productform.get("productgroup_name")?.setValue(result.GetProductsName[0].productgroup_name);
      this.productform.value.productgroup_gid = result.GetProductsName[0].productgroup_gid
        //this.cusraiseform.value.productuom_gid = result.GetProductsName[0].productuom_gid
    });

  }
  OnClearProduct(){

    this.txtproductgroup_name='';
    this.txtproductcode='';
    this.txtunit='';
  }
  

  
  productSubmit(){
    debugger
    
    if (this.productform.value.qty_requested != null && this.productform.value.qty_requested != "") {

    console.log(this.productform.value)
    var params = {
      productgroup_name: this.productform.value.productgroup_name,
      vendorproduct_code: this.productform.value.vendorproduct_code,
      product_code: this.productform.value.product_code,
      product_name: this.productform.value.product_name.product_name,
      productuom_name: this.productform.value.productuom_name,
      qty_requested: this.productform.value.qty_requested,
      potential_value: this.productform.value.potential_value,
      product_requireddate: this.productform.value.product_requireddate,
      product_gid: this.productform.value.product_name.product_gid,
      productgroup_gid: this.productform.value.productgroup_gid,
      productuom_gid: this.productform.value.productuom_gid,
}
    console.log(params)
    var api = 'PmrTrnRaiseEnquiry/PostOnAdds';
    this.NgxSpinnerService.show();
    this.service.post(api, params).subscribe((result: any) => {
    this.POproductsummary();
    this.productform.reset();
    this.NgxSpinnerService.hide();
    },
    );}
    else {
      this.ToastrService.warning('Kindly Fill All Mandatory Fields !! ')
    }
  }
  POproductsummary() {
    this.NgxSpinnerService.show();
    var api = 'PmrTrnRaiseEnquiry/GetProductsSummary';
    this.service.get(api).subscribe((result: any) => {
    this.responsedata   = result;
    this.POproductlist = this.responsedata.productsummarys_list;
    this.NgxSpinnerService.hide();
      
    });
  }
  
  openModaldelete(parameter: string){
    this.parameterValue = parameter
}
ondelete(){
  var url = 'PmrTrnRaiseEnquiry/GetDeleteEnquiryProductSummary'
  this.NgxSpinnerService.show();
  let param = {
    tmpsalesenquiry_gid : this.parameterValue 
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
openModaledit(tmpsalesenquiry_gid: string){

  this.NgxSpinnerService.show();
  var url = 'PmrTrnRaiseEnquiry/GetDirectEnquiryEditProductSummary'
    let param = {
      tmpsalesenquiry_gid: tmpsalesenquiry_gid
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      this.directeditenquiry_list = result.directeditenquiry_list;
      this.productform.get("tmpsalesenquiry_gid")?.setValue(this.directeditenquiry_list[0].tmpsalesenquiry_gid);
      this.productform.get("product_name")?.setValue(this.directeditenquiry_list[0].product_name);
      this.productform.get("product_code")?.setValue(this.directeditenquiry_list[0].product_code);
      this.productform.get("productuom_name")?.setValue(this.directeditenquiry_list[0].productuom_name); 
      this.productform.get("productgroup_name")?.setValue(this.directeditenquiry_list[0].productgroup_name);      
      this.productform.get("qty_requested")?.setValue(this.directeditenquiry_list[0].qty_requested);
      this.productform.get("potential_value")?.setValue(this.directeditenquiry_list[0].potential_value);
      this.productform.get("product_requireddate")?.setValue(this.directeditenquiry_list[0].product_requireddate);    
      this.NgxSpinnerService.hide();
    });
  
}

onupdate(){

}

onSubmit() {
debugger
console.log(this.combinedFormData.value)
  var params = {
    branch_name:this.combinedFormData.value.branch_name,
    branch_gid: this.combinedFormData.value.branch_name.branch_gid,
    enquiry_date:this.combinedFormData.value.enquiry_date,
    enquiry_gid:this.combinedFormData.value.enquiry_gid,
    vendor_companyname:this.combinedFormData.value.vendor_companyname.vendor_companyname,
    vendorbranch_name: this.combinedFormData.value.vendor_companyname.vendorbranch_name,
    contactperson_name: this.combinedFormData.value.contactperson_name,
    email_id: this.combinedFormData.value.email_id,
    address1: this.combinedFormData.value.address1,
    contact_email: this.combinedFormData.value.contact_email,
    enquiry_referencenumber:this.combinedFormData.value.enquiry_referencenumber,
    contact_number: this.combinedFormData.value.contact_number,
    enquiry_remarks: this.combinedFormData.value.enquiry_remarks,
    contact_address: this.combinedFormData.value.contact_address,
    vendor_requirement: this.combinedFormData.value.vendor_requirement,
    // landmark : this.combinedFormData.value.landmark,
    closure_date: this.combinedFormData.value.closure_date,
    user_firstname: this.combinedFormData.value.user_firstname,
    // customer_rating: this.combinedFormData.value.customer_rating,
    customerbranch_name: this.combinedFormData.value.customerbranch_name,
    vendor_gid:this.combinedFormData.value.vendor_companyname.vendor_gid,
    contact_telephonenumber:this.combinedFormData.value.contact_telephonenumber,

  }
  
  var url='PmrTrnRaiseEnquiry/PostVendorEnquiry'
  this.NgxSpinnerService.show();
  this.service.postparams(url, params).subscribe((result: any) => {
    if(result.status ==false){
      this.ToastrService.warning(result.message)
      this.NgxSpinnerService.hide();
    }
    else{
      this.ToastrService.success(result.message)
      this.NgxSpinnerService.hide();
      this.router.navigate(['/pmr/PmrTrnRaiseEnquiry']);   
    }
   
  },
  );
}

onclose(){
  this.router.navigate(['/pmr/PmrTrnRaiseEnquiry']);
}
}
