import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { AES, enc } from 'crypto-js';
import { param } from 'jquery';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { NgxSpinnerService } from 'ngx-spinner';
import { Component, OnInit } from '@angular/core';


interface SalesOD{
  salesorder_gid: string;
  salesorder_date: string;
  branch_name: string;
  branch_gid: string;
  so_referencenumber: string;
  leadbank_name: string;
  leadbank_gid: string;
  customercontact_names: string;
  customercontact_gid: string;
  customer_mobile: string;
  customer_email: string;
  so_remarks: string;
  customer_address: string;
  shipping_to: string;
  user_name: string;
  user_gid: string;
  start_date: string;
  end_date: string;
  freight_terms: string;
  payment_terms: string;
  currencyexchange_gid: string;
  currency_code: string;
  exchange_rate: string;
  product_name: string;
  product_gid: string;
  productgroup_name: string;
  customerproduct_code: string;
  product_code: string;
  productuom: string;
  product_price: string;
  qty_quoted: string;
  margin_percentage: string;
  margin_amount: string;
  selling_price: string;
  product_requireddate: string;
  product_requireddateremarks: string;
  tax_name: string;
  tax_gid: string;
  tax_amount: string;
  tax_name2: string;
  tax_amount2: string;
  tax_name3: string;
  tax_amount3: string;
  price: string;
}
@Component({
  selector: 'app-smr-trn-raisesalesorder',
  templateUrl: './smr-trn-raisesalesorder.component.html',
  styleUrls: ['./smr-trn-raisesalesorder.component.scss']
})

export class SmrTrnRaisesalesorderComponent {
  showUpdateButton: boolean = false;
  showAddButton: boolean = true;
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '50rem',
    minHeight: '5rem',
    width: '640px',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',

  };
  combinedFormData: FormGroup | any;
  productform: FormGroup | any;
  customer_list: any ;
  allchargeslist:any [] = [];
  customercontact_name:any;
  branch_list: any [] = [];
  contact_list: any [] = [];
  currency_list: any [] = [];
  user_list:any [] = [];
  product_list: any [] = [];
  tax_list: any [] = [];
  tax2_list: any [] = [];
  tax3_list: any [] = [];
  tax4_list: any [] = [];
  calci_list: any [] = [];
  POproductlist: any [] = [];
  terms_list: any[] = [];
  directeditsalesorder_list: any [] = [];
  mdlBranchName:any;
  GetCustomerDet:any;
  mdlCustomerName:any;
  customercontact_names1:any;
  customer_mobile1:any;
  customer_email1:any;
  customer_address1:any;
  product_code1:any;
  productuom_name1:any;
  unitprice1:any;
  mdlUserName:any;
  mdlProductName:any;
  mdlTaxName3:any;
  mdlCurrencyName:any;
  mdlTaxName2:any;
  mdlTaxName1:any;
  grandtotal:any;
  GetproductsCode:any;
  mdlContactName:any;
  unitprice: number = 0;
  quantity: number = 0;
  discountpercentage: number = 0;
  discountamount: any;
  totalamount: number = 0;
  addon_charge: number = 0;
  POdiscountamount: number = 0;
  freight_charges: number = 0;
  forwardingCharges: number = 0;
  insurance_charges: number = 0;
  roundoff: number = 0;
  packing_charges: number = 0;
  // grandtotal: number = 0;
  tax_amount: number = 0;
  buyback_charges: number =0;
  tax_amount4: number=0;
  taxpercentage: any;
  productdetails_list: any;
  tax_amount2: number = 0;
  tax_amount3: number = 0;
  producttotalamount: any;
  parameterValue: string | undefined; 
  productnamelist: any;
  selectedCurrencyCode: any;
  POadd_list: any;
  total_amount: any;
  mdlTerms:any;
  additional_discount: number=0;
  total_price:number=0;
  mdlproductName: any;
  responsedata: any;
  ExchangeRate: any;
  salesOD!: SalesOD;
  Productsummarys_list: any;
  salesorders_list: any;
  cuscontact_gid: any;
  salesorder: any;
  leadbank_gid: any;
  exchange: number = 0;
  taxtotal: number = 0;
  currency_code1:any;
  Salesorderdetail_list:any;
  constructor(private http:HttpClient, private fb: FormBuilder, private router: ActivatedRoute, private route: Router, private service: SocketService, private ToastrService: ToastrService,public NgxSpinnerService:NgxSpinnerService,) {
  this.salesOD = {} as SalesOD
}

    ngOnInit(): void {
      this.salesorederSummary();
      // this.SmrTrnSalesorderSummary();
      const options: Options = {
        dateFormat: 'd-m-Y',    
      };
      flatpickr('.date-picker', options); 

      // this.salesorederSummary();
 this.combinedFormData = new FormGroup ({
  tmpsalesorderdtl_gid: new FormControl(''),
  salesorder_gid : new FormControl (''),
  salesorder_date: new FormControl(this.getCurrentDate()),
  branch_name: new FormControl(''),
  branch_gid: new FormControl(''),
  so_referencenumber: new FormControl(''),
  customer_name: new FormControl(''),
  customer_gid: new FormControl(''),
  customercontact_names: new FormControl(''),
  customercontact_gid: new FormControl(''),
  customer_mobile: new FormControl(''),
  customer_email: new FormControl(''),
  so_remarks: new FormControl(''),
  customer_address: new FormControl(''),
  shipping_to: new FormControl(''),
  user_name: new FormControl(''),
  user_gid: new FormControl(''),
  start_date: new FormControl(''),
  end_date: new FormControl(''),
  freight_terms: new FormControl(''),
  payment_terms: new FormControl(''),
  currencyexchange_gid: new FormControl(''),
  currency_code: new FormControl(''),
  exchange_rate: new FormControl(''),
  payment_days: new FormControl(''),
      delivery_days: new FormControl(''),
      total_price: new FormControl(''),
      tax_name4: new FormControl(''),
      tax4_gid: new FormControl(''),
      totalamount: new FormControl(''),
      txttaxamount_1: new FormControl(''),
      addon_charge: new FormControl(''),
      additional_discount: new FormControl(''),
      freight_charges: new FormControl(''),
      buyback_charges: new FormControl(''),
      insurance_charges: new FormControl(''),
      roundoff: new FormControl(''),
      grandtotal: new FormControl(''),
      packing_charges: new FormControl(''),
      termsandconditions: new FormControl(''),
      template_gid: new FormControl(''),
      template_name: new FormControl(''),
      tax_amount4:new FormControl(''),
     
      

});
this.productform = new FormGroup({
  tmpsalesorderdtl_gid: new FormControl(''),
  tax_gid:new FormControl(''),
  product_gid: new FormControl(''),
  productuom_gid: new FormControl(''),
  productgroup_gid: new FormControl(''),
  product_code: new FormControl(''),
  tax_name: new FormControl(''),
  remarks: new FormControl(''),
  product_name: new FormControl(''),
  productuom_name: new FormControl(''),
  productgroup_name: new FormControl('', Validators.required),
  unitprice: new FormControl('', Validators.required),
  quantity: new FormControl('', Validators.required),
  discount_percentage: new FormControl('', Validators.required),
  discountamount: new FormControl('', Validators.required),
  taxname1: new FormControl('', Validators.required),
  tax_amount: new FormControl('', Validators.required),
  taxname2: new FormControl('', Validators.required),
  tax_amount2: new FormControl('', Validators.required),
  taxname3: new FormControl('', Validators.required),
  tax_amount3: new FormControl('', Validators.required),
  totalamount: new FormControl('', Validators.required),
  selling_price: new FormControl(''),
  product_requireddate: new FormControl(''),
  product_requireddateremarks: new FormControl(''),
  exchange_rate:new FormControl('')
});

//// Branch Dropdown /////
var url = 'SmrTrnSalesorder/GetBranchDtl'
this.service.get(url).subscribe((result:any)=>{
  this.branch_list = result.GetBranchDtl;
 });


var url = 'SmrTrnSalesorder/GetCustomerDtl'
this.service.get(url).subscribe((result:any)=>{
  this.customer_list = result.GetCustomerDtl;
 });

 //// Sales person Dropdown ////
 var url = 'SmrTrnSalesorder/GetPersonDtl'
this.service.get(url).subscribe((result:any)=>{
  this.contact_list = result.GetPersonDtl;
 });
 

  //// Currency Dropdown ////

  var url = 'SmrTrnQuotation/GetCurrencyDtl'
  this.service.get(url).subscribe((result:any)=>{
    this.currency_list = result.GetCurrencyDt;  
    this.mdlCurrencyName = this.currency_list[0].currencyexchange_gid;    
    const defaultCurrency = this.currency_list.find(currency => currency.default_currency === 'Y');
    const defaultCurrencyExchangeRate = defaultCurrency.exchange_rate;   
      if (defaultCurrency) {
        this.mdlCurrencyName = defaultCurrency.currencyexchange_gid;
        this.combinedFormData.get("exchange_rate")?.setValue(defaultCurrencyExchangeRate);
      } 
   });

  // var url = 'SmrTrnSalesorder/GetCurrencyDtl'
  // this.service.get(url).subscribe((result:any)=>{
  //   this.currency_list = result.GetCurrencyDtl;
  //  });

   //// Tax 1 Dropdown ////
   var url = 'SmrTrnSalesorder/GetTax1Dtl'
   this.service.get(url).subscribe((result:any)=>{
     this.tax_list = result.GetTax1Dtl;
    });

     //// Tax 2 Dropdown ////
   var url = 'SmrTrnSalesorder/GetTax2Dtl'
   this.service.get(url).subscribe((result:any)=>{
     this.tax2_list = result.GetTax2Dtl;
    });

     //// Tax 3 Dropdown ////
   var url = 'SmrTrnSalesorder/GetTax3Dtl'
   this.service.get(url).subscribe((result:any)=>{
     this.tax3_list = result.GetTax3Dtl;
    });

      //// Tax 3 Dropdown ////
   var url = 'SmrTrnSalesorder/GetTax4Dtl'
   this.service.get(url).subscribe((result:any)=>{
     this.tax4_list = result.GetTax4Dtl;
    });

      //// Product Dropdown ////
   var url = 'SmrTrnSalesorder/GetProductNamDtl'
   this.service.get(url).subscribe((result:any)=>{
     this.product_list = result.GetProductNamDtl;
    });

    // Termd and Conditions //
    //// T & C Dropdown ////
  var url = 'SmrTrnQuotation/GetTermsandConditions'
  this.service.get(url).subscribe((result:any)=>{
    this.terms_list = result.GetTermsandConditions;
   });

   var api = 'SmrMstSalesConfig/GetAllChargesConfig';
     this.service.get(api).subscribe((result: any) => {
       this.responsedata = result;
       this.allchargeslist = this.responsedata.salesconfigalllist;
       this.addon_charge = this.allchargeslist[0].flag;
       this.additional_discount = this.allchargeslist[1].flag;
       this.freight_charges = this.allchargeslist[2].flag;
       this.buyback_charges = this.allchargeslist[3].flag;
       this.insurance_charges = this.allchargeslist[4].flag;
       
 
       if (this.allchargeslist[0].flag == 'Y') {
         this.addon_charge = 0;
       } else {
         this.addon_charge = this.allchargeslist[0].flag;
       }
 
       if (this.allchargeslist[1].flag == 'Y') {
         this.additional_discount = 0;
       } else {
         this.additional_discount = this.allchargeslist[1].flag;
       }
 
       if (this.allchargeslist[2].flag == 'Y') {
         this.freight_charges = 0;
       } else {
         this.freight_charges = this.allchargeslist[2].flag;
       }
 
       if (this.allchargeslist[3].flag == 'Y') {
         this.buyback_charges = 0;
       } else {
         this.buyback_charges = this.allchargeslist[3].flag;
       }
 
       if (this.allchargeslist[4].flag == 'Y') {
         this.insurance_charges = 0;
       } else {
         this.insurance_charges = this.allchargeslist[4].flag;
       }
     
     });

}

  get branch_name() {
    return this.combinedFormData.get('branch_name')!;
  }
  get payment_days(){
    return this.combinedFormData.get('payment_days')!;
  }
  get delivery_days(){
    return this.combinedFormData.get('delivery_days')!;
  }
  get customer_name() {
    return this.combinedFormData.get('customer_name')!;
  }
  get customercontact_names() {
    return this.combinedFormData.get('customercontact_names')!;
  }
  get user_name() {
    return this.combinedFormData.get('user_name')!;
  }
  get currency_code() {
    return this.combinedFormData.get('currency_code')!;
  }
  get product_name() {
    return this.productform.get('product_name')!;
  }
  get tax_name(){
    return this.productform.get('tax_name')!;
  }
  get tax_name2(){
    return this.productform.get('tax_name2')!;
  }
  get tax_name3(){
    return this.productform.get('tax_name3')!;
  }
  getCurrentDate(): string {
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0'); // January is 0!
    const yyyy = today.getFullYear();
  
    return dd + '-' + mm + '-' + yyyy;
  }


  OnChangeCustomer(){
   
    let customer_gid = this.combinedFormData.value.customer_name.customer_gid;
    let param ={
      customer_gid :customer_gid
    }
    var url = 'SmrTrnSalesorder/GetOnChangeCustomer';
    
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.GetCustomerDet = this.responsedata.GetCustomer;
      
      this.combinedFormData.get("customer_mobile")?.setValue(result.GetCustomer[0].customer_mobile);
      
      this.combinedFormData.get("customercontact_names")?.setValue(result.GetCustomer[0].customercontact_names);
      this.combinedFormData.get("customer_address")?.setValue(result.GetCustomer[0].customer_address);
      this.combinedFormData.value.leadbank_gid = result.GetCustomer[0].leadbank_gid;
      this.combinedFormData.get("customer_email")?.setValue(result.GetCustomer[0].customer_email);
      this.cuscontact_gid =this.combinedFormData.value.customercontact_gid;
      //this.combinedFormData.value.leadbank_gid = result.GetCustomer[0].leadbank_gid
    });

  }


  
  OnChangeCurrency(){
    
    let currencyexchange_gid = this.combinedFormData.get("currency_code")?.value;
    console.log(currencyexchange_gid)
    let param = {
      currencyexchange_gid: currencyexchange_gid
    }
    var url = 'SmrTrnQuotation/GetOnChangeCurrency';
    
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.currency_list = this.responsedata.GetOnchangecurrency;
      this.combinedFormData.get("exchange_rate")?.setValue(this.currency_list[0].exchange_rate);
      
  })}
 
  OnChangeCustomer1(){
   this.customercontact_names1='';
   this.customer_mobile1='';
   this.customer_email1='';
   this.customer_address1='';
     }
   GetOnChangeProductsName1(){
    this.product_code1='';
    this.productuom_name1='';
    this.unitprice1='';
     }
     OnClearTax() {
      this.tax_amount = 0; 
      const subtotal = this.exchange * this.unitprice * this.quantity;
      this.discountamount = (subtotal * this.discountpercentage) / 100;
      this.totalamount = +(subtotal - this.discountamount + this.tax_amount).toFixed(2);
        
    }
    OnClearOverallTax() {

      this.tax_amount4=0;
     
      this.total_amount = +((+this.producttotalamount) + (+this.tax_amount4));  
      this.total_amount = +this.total_amount.toFixed(2);
      this.total_price = (this.tax_amount4) + (this.producttotalamount);
      this.total_price = +this.total_price.toFixed(2);
      this.grandtotal = ((this.total_amount) + (+this.addon_charge) + (+this.freight_charges) + (+this.buyback_charges) + (+this.packing_charges) + (+this.insurance_charges) + (+this.roundoff) + (+this.totalamount)  - (+this.additional_discount));
      this.grandtotal =+this.grandtotal.toFixed(2);
    }
  onCurrencyCodeChange(event: Event){

    
    const target = event.target as HTMLSelectElement;
    const selectedCurrencyCode = target.value;

    this.selectedCurrencyCode = selectedCurrencyCode;
    this.combinedFormData.controls.currency_code.setValue(selectedCurrencyCode);
    this.combinedFormData.get("currency_code")?.setValue(this.currency_list[0].currency_code);

  }

  GetOnChangeProductsName(){
     
    let product_gid = this.productform.value.product_name.product_gid;
    //let customercontact_gid = this.combinedFormData.value.customer_name.customer_gid;
    if( this.combinedFormData.value.customer_name != undefined){
      let customercontact_gid = this.combinedFormData.value.customer_name.customer_gid;
    let param = {
      product_gid: product_gid,
      customercontact_gid:customercontact_gid
    }

    var url = 'SmrTrnSalesorder/GetOnChangeProductsName';
    this.NgxSpinnerService.show();
    this.service.getparams(url, param).subscribe((result: any) => {
      this.productform.get("product_code")?.setValue(result.ProductsCode[0].product_code);
      this.productform.get("productuom_name")?.setValue(result.ProductsCode[0].productuom_name);
      this.productform.get("productgroup_name")?.setValue(result.ProductsCode[0].productgroup_name);
      this.productform.get("unitprice")?.setValue(result.ProductsCode[0].unitprice);
      this.productform.value.productgroup_gid = result.ProductsCode[0].productgroup_gid
       // this.productform.value.productuom_gid = result.GetProductsCode[0].productuom_gid
       this.NgxSpinnerService.hide();
          this.ToastrService.warning(result.message)
     
    });
  }
  else {
    this.ToastrService.warning('Kindly Select Customer Before Adding Product !! ')
  }
  }
productAdd(){
    
console.log(this.productform.value)
var params = {
  productgroup_name: this.productform.value.productgroup_name,
  customerproduct_code: this.productform.value.customerproduct_code,
  product_code: this.productform.value.product_code,
  product_name: this.productform.value.product_name.product_name,
  productuom_name: this.productform.value.productuom_name,
  qty_requested: this.productform.value.qty_requested,
  potential_value: this.productform.value.potential_value,
  product_requireddate: this.productform.value.product_requireddate,
  product_gid: this.productform.value.product_name.product_gid,
  productgroup_gid: this.productform.value.productgroup_gid,
  productuom_gid: this.productform.value.productuom_gid,
  //selling_price: this.productform.value.selling_price,
  unitprice: this.productform.value.unitprice,
  quantity: this.productform.value.quantity,
  discount_percentage: this.productform.value.discount_percentage,
  discountamount: this.productform.value.discountamount,
  product_requireddateremarks: this.productform.value.product_requireddateremarks,
  tax_name: this.productform.value.tax_name,
  tax_amount: this.productform.value.tax_amount,
  // tax_name2: this.productform.value.tax_name2,
  // tax_amount2: this.productform.value.tax_amount2,
  // tax_name3: this.productform.value.tax_name3,
  // tax_amount3: this.productform.value.tax_amount3,
  totalamount: this.productform.value.totalamount,
  producttotalamount:this.productform.value.producttotalamount,
}
    console.log(params)
    var api = 'SmrTrnSalesorder/PostOnAdds';
    this.NgxSpinnerService.show();

    this.service.post(api, params).subscribe((result: any) => {
      if(result.status == false){
        this.NgxSpinnerService.hide();

        this.ToastrService.warning(result.message)
        this.salesorederSummary();
      }
      else{
        this.NgxSpinnerService.hide();
        this.ToastrService.success(result.message)
        this.salesorederSummary();
        this.productform.reset();
      }
    },
    );
  }
  salesorederSummary() {
    
    var api = 'SmrTrnSalesorder/GetSalesOrdersummary';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.salesorders_list = this.responsedata.salesorders_list;
      this.combinedFormData.get("totalamount")?.setValue(this.responsedata.grand_total,);
      this.combinedFormData.get("grandtotal")?.setValue(this.responsedata.grand_total);
      this.currency_code1 = ""
      
      
    });
  }

  GetOnChangeTerms() {
    
    let template_gid = this.combinedFormData.value.template_name;
    let param = {
      template_gid: template_gid
    }
    var url = 'SmrTrnQuotation/GetOnChangeTerms';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.combinedFormData.get("termsandconditions")?.setValue(result.terms_list[0].termsandconditions);
      this.combinedFormData.value.template_gid = result.terms_list[0].template_gid
      //this.cusraiseform.value.productuom_gid = result.GetProductsName[0].productuom_gid
    });
  }

  onSubmit()  
    {
      
      console.log(this.combinedFormData.value)
      var params = {
        customer_gid:this.combinedFormData.value.customer_gid.customer_name,
        branch_name:this.combinedFormData.value.branch_name,
        branch_gid: this.combinedFormData.value.branch_name.branch_gid,
        salesorder_date:this.combinedFormData.value.salesorder_date,
        salesorder_gid:this.combinedFormData.value.salesorder_gid,
        customer_name:this.combinedFormData.value.customer_name.customer_name,
        customercontact_names: this.combinedFormData.value.customercontact_names,
        customer_email: this.combinedFormData.value.customer_email,
        customer_mobile: this.combinedFormData.value.customer_mobile,
        so_remarks: this.combinedFormData.value.so_remarks,
        so_referencenumber: this.combinedFormData.value.so_referencenumber,
        customer_address: this.combinedFormData.value.customer_address,
        freight_terms: this.combinedFormData.value.freight_terms,
        payment_terms : this.combinedFormData.value.payment_terms,
        currency_code: this.combinedFormData.value.currency_code,
        user_name: this.combinedFormData.value.user_name,
        exchange_rate: this.combinedFormData.value.exchange_rate,
        payment_days: this.combinedFormData.value.payment_days,        
        termsandconditions:this.combinedFormData.value.termsandconditions,
        template_name:this.combinedFormData.value.template_name,
        template_gid:this.combinedFormData.value.template_gid,
        grandtotal:this.combinedFormData.value.grandtotal,
        roundoff:this.combinedFormData.value.roundoff,
        start_date:this.combinedFormData.value.start_date,
        end_date:this.combinedFormData.value.end_date,
        insurance_charges:this.combinedFormData.value.insurance_charges,
        packing_charges:this.combinedFormData.value.packing_charges,
        buyback_charges:this.combinedFormData.value.buyback_charges,
        freight_charges:this.combinedFormData.value.freight_charges,
        additional_discount:this.combinedFormData.value.additional_discount,
        addon_charge:this.combinedFormData.value.addon_charge,
        total_amount:this.combinedFormData.value.total_amount,
        taxamount_1:this.combinedFormData.value.taxamount_1,
        tax_name4:this.combinedFormData.value.tax_name4,
        totalamount:this.combinedFormData.value.totalamount,
        customercontact_gid:this.cuscontact_gid,
        delivery_days: this.combinedFormData.value.delivery_days,
        total_price: this.combinedFormData.value.total_price

      }
      
      //console.log(this.cuscontact_gid)
      var url='SmrTrnSalesorder/PostSalesOrder'
      this.NgxSpinnerService.show();
      this.service.postparams(url, params).subscribe((result: any) => {
        if(result.status == false){
          this.NgxSpinnerService.hide();
          this.ToastrService.warning(result.message)        
        }
        else{
          this.NgxSpinnerService.hide();
          this.ToastrService.success(result.message)
          this.route.navigate(['/smr/SmrTrnSalesorderSummary']);   
        }       
      });
    }
  getDimensionsByFilter(id: any) {
    return this.tax_list.filter((x: { tax_gid: any; }) => x.tax_gid === id);
  }
  prodtotalcal() {

    const subtotal = this.unitprice * this.quantity;
    this.discountamount = (subtotal * this.discountpercentage) / 100;
    this.discountamount = this.discountamount.toFixed(2);
    this.totalamount = subtotal - this.discountamount;
    this.totalamount = +(subtotal - this.discountamount).toFixed(2);
    
    // const subtotal = this.exchange * this.unitprice * this.quantity;
    // this.discountamount = (subtotal * this.discountpercentage) / 100;
    // this.discountamount = this.discountamount.toFixed(2);
    // this.totalamount = subtotal - this.discountamount;
    // this.totalamount = +(subtotal - this.discountamount).toFixed(2);
   
    // const value = this.producttotalamount.value;
    // const formattedValue = parseFloat(value).toFixed(2);
    // this.producttotalamount.setValue(formattedValue, { emitEvent: false });


    
  }

  OnChangeTaxAmount1() {
    let tax_gid = this.productform.get('tax_name')?.value;
    this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    let tax_percentage = this.taxpercentage[0].percentage;
   
   // Calculate the tax amount with two decimal points
   this.tax_amount = +(tax_percentage * this.totalamount / 100).toFixed(2);
   const subtotal =  this.unitprice * this.quantity;
     this.discountamount = (subtotal * this.discountpercentage) / 100;
     this.totalamount = +(subtotal - this.discountamount + this.tax_amount).toFixed(2);
  }
  onKeyPress(event: any) {
    // Get the pressed key
    const key = event.key;

    if (!/^[0-9.]$/.test(key)) {
      // If not a number or dot, prevent the default action (key input)
      event.preventDefault();
    }
  }
  
 
  OnChangeTaxAmount2() {
    let tax_gid2 = this.productform.get('tax_name2')?.value;
 
    this.taxpercentage = this.getDimensionsByFilter(tax_gid2);
    console.log(this.taxpercentage);
    let tax_percentage = this.taxpercentage[0].percentage;
    console.group(tax_percentage);
 
    const subtotal = this.unitprice * this.quantity;
    this.discountamount = (subtotal * this.discountpercentage) / 100;
    this.totalamount = subtotal - this.discountamount;
 
    this.tax_amount2 = ((tax_percentage) * (this.totalamount) / 100);
 
    if (this.tax_amount == undefined && this.tax_amount2 == undefined) {
      const subtotal = this.unitprice * this.quantity;
      this.discountamount = (subtotal * this.discountpercentage) / 100;
      this.totalamount = subtotal - this.discountamount;
    }
    else {
      this.totalamount = ((this.totalamount) + (+this.tax_amount) + (+this.tax_amount2));
    }
  }
OnChangeTaxAmount3() {
    let tax_gid3 = this.productform.get('tax_name3')?.value;
 
    this.taxpercentage = this.getDimensionsByFilter(tax_gid3);
    console.log(this.taxpercentage);
    let tax_percentage = this.taxpercentage[0].percentage;
    console.group(tax_percentage);
 
    const subtotal = this.unitprice * this.quantity;
    this.discountamount = (subtotal * this.discountpercentage) / 100;
    this.totalamount = subtotal - this.discountamount;
 
    this.tax_amount3 = ((tax_percentage) * (this.totalamount) / 100);
 
    if (this.tax_amount == undefined && this.tax_amount2 == undefined && this.tax_amount3 == undefined) {
      const subtotal = this.unitprice * this.quantity;
      this.discountamount = (subtotal * this.discountpercentage) / 100;
      this.totalamount = subtotal - this.discountamount;
    }
    else {
      this.totalamount = ((this.totalamount) + (+this.tax_amount) + (+this.tax_amount2)+ (+this.tax_amount3));
    }
  }

  OnChangeTaxAmount4() {
    let tax_gid = this.combinedFormData.get('tax_name4')?.value
    this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    console.log(this.taxpercentage);
    let tax_percentage = this.taxpercentage[0].percentage;
    console.group(tax_percentage);
 
    this.tax_amount4 = +((tax_percentage * this.producttotalamount / 100).toFixed(2));
    this.total_price = +((this.tax_amount4 + (+this.producttotalamount)).toFixed(2));
    this.grandtotal = ((this.total_price) + (+this.addon_charge) + (+this.freight_charges) + (+this.buyback_charges) + (+this.insurance_charges)+(+this.packing_charges) + (+this.roundoff) - (+this.additional_discount));

    
    // let tax_gid = this.combinedFormData.get('tax_name4')?.value;
 
    // this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    // console.log(this.taxpercentage);
    // let tax_percentage = this.taxpercentage[0].percentage;
    // console.group(tax_percentage);

    // this.tax_amount4 = +(tax_percentage * this.producttotalamount / 100).toFixed(2);
    // this.total_amount += (this.tax_amount4).toFixed(2);
    // this.total_price = (this.tax_amount4) + (this.producttotalamount);
    // this.total_price = +this.total_price.toFixed(2);
    //  this.grandtotal =  ((this.total_price)+ (+this.addon_charge) + (+this.freight_charges) + (+this.buyback_charges) + (+this.insurance_charges) + (+this.roundoff)  - (+this.additional_discount));


  }

  finaltotal(){
    const addoncharges = isNaN(this.addon_charge) ? 0 : +this.addon_charge;
    const frieghtcharges = isNaN(this.freight_charges) ? 0 : +this.freight_charges;
    const forwardingCharges = isNaN(this.buyback_charges) ? 0 : +this.buyback_charges;
    const insurancecharges = isNaN(this.insurance_charges) ? 0 : +this.insurance_charges;
    const packing_charges = isNaN(this.packing_charges) ? 0 : +this.packing_charges;
    const roundoff = isNaN(this.roundoff) ? 0 : +this.roundoff;
    const discountamount = isNaN(this.additional_discount) ? 0 : +this.additional_discount;
 
    this.grandtotal = ((this.total_price) + (addoncharges) + (frieghtcharges) + (forwardingCharges) + (insurancecharges) + (roundoff) - (discountamount)+(packing_charges));
    this.grandtotal = +(this.grandtotal).toFixed(2);

    
    // this.total_price = this.producttotalamount + this.tax_amount4;
    // this.grandtotal =  ((this.total_price) + (+this.addon_charge) + (+this.freight_charges) + (+this.buyback_charges) + (+this.insurance_charges) + (+this.packing_charges) + (+this.roundoff)  - (+this.additional_discount)).toFixed(2);
  }

  openModaldelete(parameter: string){
    this.parameterValue = parameter
    }
  
  ondelete() {    
    var url = 'SmrTrnSalesorder/GetDeleteDirectSOProductSummary'
    this.NgxSpinnerService.show();
    let param = {
      tmpsalesorderdtl_gid : this.parameterValue 
    }
    this.service.getparams(url,param).subscribe((result: any) => {
      if(result.status ==false){
        this.NgxSpinnerService.hide();
        this.ToastrService.warning(result.message)
      }
      else{
        
        this.ToastrService.success(result.message)
        this.salesorederSummary();       
        this.NgxSpinnerService.hide();   
      }     
      });
  }

   // PRODUCT EDIT SUMMARY
   editproductSO(tmpsalesorderdtl_gid: any) {
    var url = 'SmrTrnSalesorder/GetDirectSalesOrderEditProductSummary'
    this.NgxSpinnerService.show();
    let param = {
      tmpsalesorderdtl_gid: tmpsalesorderdtl_gid
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      this.directeditsalesorder_list = result.directeditsalesorder_list;
      this.productform.get("tmpsalesorderdtl_gid")?.setValue(this.directeditsalesorder_list[0].tmpsalesorderdtl_gid);
      this.productform.get("product_name")?.setValue(this.directeditsalesorder_list[0].product_name);
      this.productform.get("product_gid")?.setValue(this.directeditsalesorder_list[0].product_gid);
      this.productform.get("product_code")?.setValue(this.directeditsalesorder_list[0].product_code);
      this.productform.get("productuom_name")?.setValue(this.directeditsalesorder_list[0].productuom_name);      
      this.productform.get("quantity")?.setValue(this.directeditsalesorder_list[0].quantity);
      this.productform.get("totalamount")?.setValue(this.directeditsalesorder_list[0].totalamount);
      this.productform.get("tax_name")?.setValue(this.directeditsalesorder_list[0].tax_name);   
      this.productform.get("tax_gid")?.setValue(this.directeditsalesorder_list[0].tax_gid);   
      this.productform.get("unitprice")?.setValue(this.directeditsalesorder_list[0].unitprice);
      this.productform.get("tax_amount")?.setValue(this.directeditsalesorder_list[0].tax_amount);  
      this.productform.get("discount_percentage")?.setValue(this.directeditsalesorder_list[0].discount_percentage);
      this.productform.get("discountamount")?.setValue(this.directeditsalesorder_list[0].discountamount);  
      this.NgxSpinnerService.hide();
    });
    this.showUpdateButton = true;
    this.showAddButton = false;
  }

  onupdate() {
    var params = {
      tmpsalesorderdtl_gid: this.productform.value.tmpsalesorderdtl_gid,
      product_code: this.productform.value.product_code,
      product_name: this.productform.value.product_name,
      productuom_name: this.productform.value.productuom_name,
      quantity: this.productform.value.quantity,
      unitprice: this.productform.value.unitprice,
      discountamount: this.productform.value.discountamount,
      discount_percentage: this.productform.value.discount_percentage,
      product_gid: this.productform.value.product_name.product_gid,
      productuom_gid: this.productform.value.productuom_gid,
      tax_name: this.productform.value.tax_name,
      tax_gid: this.productform.value.tax_name.tax_gid,
      tax_amount: this.productform.value.tax_amount,
      totalamount: this.productform.value.totalamount
    }
    var url = 'SmrTrnSalesorder/PostUpdateDirectSOProduct'
  this.NgxSpinnerService.show();
    this.service.post(url,params).pipe().subscribe((result:any)=>{
      this.responsedata=result;
      if(result.status ==false){
        this.NgxSpinnerService.hide();
        this.ToastrService.warning(result.message)       
        this.salesorederSummary();
      }
      else{
        this.NgxSpinnerService.hide();
        this.ToastrService.success(result.message)
        this.productform.reset();
        
      }
      this.salesorederSummary();
    });
  }
  
 
close(){
this.route.navigate(['/smr/SmrTrnSalesorderSummary']);
  } 
  openModelDetail(product_gid:any){
    
    var url='SmrTrnSalesorder/GetSalesorderdetail'
    let params={
      product_gid: product_gid
    }
    this.service.getparams(url,params).subscribe((result: any) => {
      this.responsedata = result;
       this.Salesorderdetail_list = this.responsedata.Directeddetailslist2;
    });
  
  }


}
