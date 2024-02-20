import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { HttpClient } from '@angular/common/http';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import {CountryISO,SearchCountryField,} from "ngx-intl-tel-input";

@Component({
  selector: 'app-smr-trn-amend-quotation',
  templateUrl: './smr-trn-amend-quotation.component.html',
  styleUrls: ['./smr-trn-amend-quotation.component.scss']
})
export class SmrTrnAmendQuotationComponent {
  customer_mobile1:any;
  customer_name1:any;
  salesorder_gid:any;
  customercontact_names1:any;
  customer_mobile:any;
  SearchCountryField = SearchCountryField;
  CountryISO = CountryISO;
  preferredCountries: CountryISO[] = [
    CountryISO.India,
    CountryISO.India
  ];
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '25rem',
    minHeight: '5rem',
    width: '975px',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
  };
  QuotationForm: FormGroup | any;
  productform: FormGroup | any;
  customer_list: any ;
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
  mdlBranchName:any;
  GetCustomerDet:any;
  mdlCustomerName:any;
  mdlUserName:any;
  mdlProductName:any;
  mdlTaxName3:any;
  mdlCurrencyName:any;
  mdlTaxName2:any;
  mdlTaxName1:any;
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
  grandtotal: number = 0;
  tax_amount: number = 0;
  buyback_charges: number =0;
  taxpercentage: any;
  txtProductUnit:any;
  txtUnitPrice:any;
  txtProductcode:any;
  txtExchangeRate:any;
  productdetails_list: any;
  tax_amount2: number = 0;
  tax_amount3: number = 0;
  producttotalamount: number=0;
  parameterValue: string | undefined;
  productnamelist: any;
  selectedCurrencyCode: any;
  POadd_list: any;
  total_amount: any;
  mdlTerms:any;
  additional_discount: number=0;
  mdlproductName: any;
  responsedata: any;
  ExchangeRate: any;
  Productsummarys_list: any;
  salesorders_list: any;
  cuscontact_gid: any;
  quotation_gid: any;
  amend_list: any;
  quotationproductlist:any;
  currencylist:any;
  currencycode:any
  mdlTaxName:any;
  total_price: any;
  
  constructor(private http:HttpClient, private fb: FormBuilder,public NgxSpinnerService:NgxSpinnerService, private router: ActivatedRoute, private route: Router, private service: SocketService, private ToastrService: ToastrService,public ngxspinner: NgxSpinnerService) {
    this.QuotationForm = new FormGroup ({
      tmpquotationdtl_gid: new FormControl(''),
      quotation_gid : new FormControl (''),
      quotationrefno : new FormControl (''),
      quotation_date: new FormControl(''),
      branch_name: new FormControl(''),
      branch_gid: new FormControl(''),
      Quo_referencenumber: new FormControl(''),
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
          producttotalamount: new FormControl(''),
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
          
    
    });
    this.productform = new FormGroup({
      tmpquotationdtl_gid: new FormControl(''),
      tax_gid:new FormControl(''),
      product_gid: new FormControl(''),
      productuom_gid: new FormControl(''),
      productgroup_gid: new FormControl(''),
      product_code: new FormControl('', Validators.required),
      productcode: new FormControl('', Validators.required),
      productgroup: new FormControl('', Validators.required),
      productuom: new FormControl('', Validators.required),
      productname: new FormControl('', Validators.required),
      tax_name: new FormControl('', Validators.required), 
      remarks: new FormControl('', Validators.required),
      product_name: new FormControl('', Validators.required),
      productuom_name: new FormControl('', Validators.required),
      productgroup_name: new FormControl('', Validators.required),
      unitprice: new FormControl('', Validators.required),
      quantity: new FormControl('', Validators.required),
      discountpercentage: new FormControl('', Validators.required),
      discountamount: new FormControl('', Validators.required),
      tax_amount: new FormControl('', Validators.required),   
      totalamount: new FormControl('', Validators.required),
      customerproduct_code: new FormControl(''),
      selling_price: new FormControl(''),
      product_requireddate: new FormControl(''),
      product_requireddateremarks: new FormControl(''),
    });

  }
    ngOnInit(): void {
      
        const options: Options = {
          dateFormat: 'd-m-Y',    
        };
        flatpickr('.date-picker', options); 
     
      const quotation_gid = this.router.snapshot.paramMap.get('quotation_gid');
    this.quotation_gid = quotation_gid;

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.quotation_gid, secretKey).toString(enc.Utf8);
    

      /// Customer Name Dropdown ////
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
      var url = 'SmrTrnSalesorder/GetCurrencyDtl'
      this.service.get(url).subscribe((result:any)=>{
      this.currency_list = result.GetCurrencyDtl;
   });
 //// Tax 1 Dropdown ////
 var url = 'SmrTrnSalesorder/GetTax1Dtl'
 this.service.get(url).subscribe((result:any)=>{
   this.tax_list = result.GetTax1Dtl;
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
this.quotation_gid=deencryptedParam;
      this.GetQuotationamend(deencryptedParam);
      }   



      GetQuotationamend(quotation_gid :any) {
       
        let param = {
          quotation_gid: this.quotation_gid
        }
    
        var api = 'SmrTrnQuotation/GetQuotationamend'
        this.NgxSpinnerService.show()
      
    this.service.getparams(api, param).subscribe((result: any) => {
      this.amend_list = result.amend_list;
      this.QuotationForm.get("quotation_gid")?.setValue(this.quotation_gid);
      this.QuotationForm.get("quotationrefno")?.setValue(this.amend_list[0].quotation_gid);
      this.QuotationForm.get("quotation_date")?.setValue(this.amend_list[0].quotation_date);
      this.QuotationForm.get("branch_name")?.setValue(this.amend_list[0].branch_name);
      this.QuotationForm.get("Quo_referencenumber")?.setValue(this.amend_list[0].quotation_referenceno1);
      this.QuotationForm.get("customer_name")?.setValue(this.amend_list[0].customer_name);
      this.QuotationForm.get("customercontact_names")?.setValue(this.amend_list[0].contact_person);
      this.QuotationForm.get("customer_mobile")?.setValue(this.amend_list[0].contact_no);
      this.QuotationForm.get("customer_email")?.setValue(this.amend_list[0].contact_mail);
      this.QuotationForm.get("customer_address")?.setValue(this.amend_list[0].customer_address);
      this.QuotationForm.get("customer_address")?.setValue(this.amend_list[0].customer_address);
      this.QuotationForm.get("so_remarks")?.setValue(this.amend_list[0].quotation_remarks);
      this.QuotationForm.get("shipping_to")?.setValue(this.amend_list[0].customer_email);
      this.QuotationForm.get("user_name")?.setValue(this.amend_list[0].salesperson_gid);
      this.QuotationForm.get("freight_terms")?.setValue(this.amend_list[0].freight_terms);
      this.QuotationForm.get("payment_terms")?.setValue(this.amend_list[0].payment_terms);
      this.QuotationForm.get("currency_code")?.setValue(this.amend_list[0].currency_gid);
      this.QuotationForm.get("exchange_rate")?.setValue(this.amend_list[0].exchange_rate);
      this.QuotationForm.get("delivery_days")?.setValue(this.amend_list[0].delivery_days);
      this.QuotationForm.get("payment_days")?.setValue(this.amend_list[0].payment_days);
      this.QuotationForm.get("addon_charge")?.setValue(this.amend_list[0].addon_charge);
      this.QuotationForm.get("additional_discount")?.setValue(this.amend_list[0].additional_discount);
      this.QuotationForm.get("freight_charges")?.setValue(this.amend_list[0].freight_charges);
      this.QuotationForm.get("buyback_charges")?.setValue(this.amend_list[0].buyback_charges);
      this.QuotationForm.get("packing_charges")?.setValue(this.amend_list[0].packing_charges);
      this.QuotationForm.get("insurance_charges")?.setValue(this.amend_list[0].insurance_charges);
      this.QuotationForm.get("roundoff")?.setValue(this.amend_list[0].roundoff);
      // this.QuotationForm.get("grandtotal")?.setValue(this.amend_list[0].grandtotal);

        this.ProductSummary()
      this.NgxSpinnerService.hide()
    });
  }


  get payment_days(){
    return this.QuotationForm.get('payment_days')!;
  }
  get delivery_days(){
    return this.QuotationForm.get('delivery_days')!;
  }
  OnChangeCustomer(){
    let customer_gid = this.QuotationForm.value.customer_name.customer_gid;
    let param ={
      customer_gid :customer_gid
    }
    var url = 'SmrTrnSalesorder/GetOnChangeCustomer'
    this.NgxSpinnerService.show()
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.GetCustomerDet = this.responsedata.GetCustomer;
      
      this.QuotationForm.get("customer_mobile")?.setValue(result.GetCustomer[0].customer_mobile);      
      this.QuotationForm.get("customercontact_names")?.setValue(result.GetCustomer[0].customercontact_names);
      this.QuotationForm.get("customer_address")?.setValue(result.GetCustomer[0].customer_address);
      this.QuotationForm.value.leadbank_gid = result.GetCustomer[0].leadbank_gid;
      this.QuotationForm.get("customer_email")?.setValue(result.GetCustomer[0].customer_email);
      this.QuotationForm =this.QuotationForm.value.customer_gid;
      this.NgxSpinnerService.hide()
    });
  }
  GetOnChangeProductsName() {

    let product_gid = this.productform.value.product_name.product_gid;
    let param = {
      product_gid: product_gid
    }
    var url = 'SmrTrnQuotation/GetOnChangeProductsNames'
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.GetproductsCode = this.responsedata.ProductsCode;
      this.productform.get("product_code")?.setValue(result.ProductsCode[0].product_code);
      this.productform.get("productuom_name")?.setValue(result.ProductsCode[0].productuom_name);
      this.productform.get("productgroup_name")?.setValue(result.ProductsCode[0].productgroup_name);
      this.productform.get("selling_price")?.setValue(result.ProductsCode[0].selling_price);
 
      //this.productform.value.productgroup_gid = result.ProductsCode[0].productgroup_gid
      //this.productform.value.selling_price = result.GetProductsCode[0].mrp
    });
  }
  OnClearProduct()
{
  this.txtProductcode='';
  this.txtUnitPrice='';
  this.txtProductUnit='';
}

OnClearCurrency()
{
  this.txtExchangeRate='';
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
  }
 
  taxAmount() {
    let tax_gid = this.productform.get('tax_name')?.value;
    this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    let tax_percentage = this.taxpercentage[0].percentage;
    
   // Calculate the tax amount with two decimal points
   this.tax_amount = +(tax_percentage * this.totalamount / 100).toFixed(2);
  
   // Calculate the new total amount
   const subtotal =  this.unitprice * this.quantity;
   this.discountamount = (subtotal * this.discountpercentage) / 100;
   this.totalamount = +(subtotal - this.discountamount + this.tax_amount).toFixed(2);
  }
  finaltotal(){
    // this.producttotalamount=this.QuotationForm.value.producttotalamount;
    //   this.grandtotal  =  ((this.producttotalamount ||0)+ (+this.addon_charge||0) + 
    //   (+this.freight_charges||0) + (+this.buyback_charges||0) + (+this.insurance_charges||0) + 
    //   (+this.roundoff||0)  - (+this.additional_discount||0));
    //   this.producttotalamount = +(this.producttotalamount).toFixed(2);
    //   this.QuotationForm.get("grandtotal")?.setValue(this.grandtotal);
    debugger
    const addoncharges = isNaN(this.addon_charge) ? 0 : +this.addon_charge;
    const frieghtcharges = isNaN(this.freight_charges) ? 0 : +this.freight_charges;
    const forwardingCharges = isNaN(this.buyback_charges) ? 0 : +this.buyback_charges;
    const insurancecharges = isNaN(this.insurance_charges) ? 0 : +this.insurance_charges;
    const roundoff = isNaN(this.roundoff) ? 0 : +this.roundoff;
    const discountamount = isNaN(this.additional_discount) ? 0 : +this.additional_discount;

    this.grandtotal = ((this.producttotalamount) + (addoncharges) + (frieghtcharges) + (forwardingCharges) + (insurancecharges) + (roundoff) - (discountamount));
    this.grandtotal = +(this.grandtotal).toFixed(2);
    }
  onadd()
{
  debugger
  var params = {
    quotation_gid : this.quotation_gid,
    productgroup_name: this.productform.value.productgroup_name,    
    product_code: this.productform.value.product_code,
    product_name: this.productform.value.product_name.product_name,
    productuom_name: this.productform.value.productuom_name,
    quantity: this.productform.value.quantity,    
    product_gid: this.productform.value.product_name.product_gid,
    productgroup_gid: this.productform.value.productgroup_gid,
    productuom_gid: this.productform.value.productuom_gid,
    unitprice:this.productform.value.unitprice,
    discountpercentage:this.productform.value.discountpercentage,
    discountamount:this.productform.value.discountamount,
    tax_name:this.productform.value.tax_name,
    tax_amount:this.productform.value.tax_amount,
    totalamount:this.productform.value.totalamount


}
debugger
  var api = 'SmrTrnQuotation/PostAddProduct';
  console.log(this.productform.value);
  this.service.post(api,params).subscribe((result: any) => {
    if(result.status ==false){
      this.ToastrService.warning(result.message)
     
    }
    else{
      this.ToastrService.success(result.message)
      // this.reload1()
      window.location.reload();      
    }
  },
  );
  }
ProductSummary(){
   
    let param = {
      quotation_gid : this.quotation_gid
    }
    var api = 'SmrTrnQuotation/GetQuotattionproductSummary';
    this.service.getparams(api, param).subscribe((result: any) => {
      this.responsedata = result;
      this.quotationproductlist = this.responsedata.amend_list;
      debugger
      let n =this.quotationproductlist.length;
      debugger;
      this.QuotationForm.get("producttotalamount")?.setValue(result.grandtotal);
      this.QuotationForm.get("grandtotal")?.setValue(result.grandtotal);

      //this.producttotalamount=this.quotationproductlist[n-1].grand_total;

    });

}
  OnChangeCurrency() {
    debugger
    let currencyexchange_gid = this.QuotationForm.get("currency_code")?.value;
    console.log(currencyexchange_gid)
    let param = {
      currencyexchange_gid: currencyexchange_gid
    }
    var url = 'SmrTrnSalesorder/GetOnChangeCurrency';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.currencylist = this.responsedata.GetOnchangeCurrency;
      this.QuotationForm.get("exchange_rate")?.setValue(this.currencylist[0].exchange_rate);
      this.currencycode=this.currencylist[0].currency_code
    });
  }
overallsub() {
  //debugger

  console.log(this.QuotationForm)

  var api = 'SmrTrnQuotation/PostQuotationAmend';
  this.service.post(api, this.QuotationForm.value).subscribe(
    (result: any) => {

      if (result.status == true) {
        this.ToastrService.success(result.message)
       
        this.route.navigate(['/smr/SmrMstProductSummary']);       

      }
      else {
        this.ToastrService.warning(result.message)  
      } 
      }
    );  
}


GetOnChangeTerms() {
  debugger

  let template_gid = this.QuotationForm.value.template_name;
  let param = {
    template_gid: template_gid
  }
  var url = 'SmrTrnQuotation/GetOnChangeTerms';
  this.service.getparams(url, param).subscribe((result: any) => {
    this.QuotationForm.get("termsandconditions")?.setValue(result.terms_list[0].termsandconditions);
    this.QuotationForm.value.template_gid = result.terms_list[0].template_gid
    //this.cusraiseform.value.productuom_gid = result.GetProductsName[0].productuom_gid
  });
}

openModaldelete(parameter: string){
  
  this.parameterValue = parameter
    }

ondelete() {    
  debugger
  var url = 'SmrTrnQuotation/GetdeleteamendProductSummary'
  let param = {
    tmpquotationdtl_gid : this.parameterValue 
  }
  this.service.getparams(url,param).subscribe((result: any) => {
    if(result.status ==false){
      this.ToastrService.warning(result.message)
    }
    else{
      
      this.ToastrService.success(result.message)
      window.location.reload();

      
    }     
    });
}

// edit(data :any){
//   debugger
//   this.mdlproductName = data.product_name;      
//   this.mdlTaxName = data.tax_gid;
//   this.productform.get("tax_name")?.setValue(data.tax_name);
//   this.productform.get("tax_gid")?.setValue(data.tax_gid);
//   this.productform.get("product_name")?.setValue(data.product_name);
//   this.productform.get("tmpquotationdtl_gid")?.setValue(data.tmpquotationdtl_gid);
//   this.productform.get("product_code")?.setValue(data.product_code);
//   this.productform.get("productuom_name")?.setValue(data.uom_name);
//   this.productform.get("tax_amount")?.setValue(data.tax_amount);
//   this.productform.get("unitprice")?.setValue(data.product_price);
//   this.productform.get("quantity")?.setValue(data.qty_quoted);
//   this.productform.get("discountpercentage")?.setValue(data.discount_percentage); 
//   this.productform.get("discountamount")?.setValue(data.discount_amount);
//   this.productform.get("totalamount")?.setValue(data.totalamount);
//   this.productform.get("productgroup_name")?.setValue(data.productgroup_name);      
// }
productUpdate(){

  debugger
  var params = {
    tmpsalesorderdtl_gid:this.productform.value.tmpsalesorderdtl_gid,
    tax_name:this.productform.value.tax_name,
    tax_gid:this.productform.value.tax_gid,
    tax_amount:this.productform.value.tax_amount,
    total_amount:this.productform.value.totalamount,
    productgroup_name:this.productform.value.productgroup_name, 
    discountamount:this.productform.value.discountamount,
    discountpercentage:this.discountpercentage,
    quantity: this.productform.value.quantity,
    unitprice: this.productform.value.unitprice,
    unit: this.productform.value.unit,
    product_name: this.productform.value.product_name,
    product_code: this.productform.value.product_code
  }
  
  var api='SmrTrnSalesorder/updateSalesOrderedit'
  this.NgxSpinnerService.show();
  console.log(params);
  this.service.post(api, params).subscribe((result: any) => {
    this.NgxSpinnerService.hide();     
  });
}

onsubmit() 
{
  debugger
  console.log(this.QuotationForm.value)
  var params = {
    branch_name:this.QuotationForm.value.branch_name,

    quotation_gid:this.QuotationForm.value.quotation_gid,
    quotation_referencenumber:this.QuotationForm.value.quotationrefno,
    // branch_name: this.QuotationForm.value.branch_name,
    quotation_date:this.QuotationForm.value.quotation_date,
    customer_name:this.QuotationForm.value.customer_name,
    quotation_referenceno1:this.QuotationForm.value.Quo_referencenumber,
    customercontact_names: this.QuotationForm.value.customercontact_names,
    customer_email: this.QuotationForm.value.customer_email,
    customer_mobile: this.QuotationForm.value.customer_mobile,
    quotation_remarks: this.QuotationForm.value.so_remarks,
    customer_address: this.QuotationForm.value.customer_address,
    freight_terms: this.QuotationForm.value.freight_terms,
    payment_terms : this.QuotationForm.value.payment_terms,
    currency_code: this.QuotationForm.value.currency_code,
    user_name: this.QuotationForm.value.user_name,
    exchange_rate: this.QuotationForm.value.exchange_rate,
    payment_days: this.QuotationForm.value.payment_days,
    customer_gid:this.QuotationForm.value.customer_name.customer_gid,
    termsandconditions:this.QuotationForm.value.termsandconditions,
    template_name:this.QuotationForm.value.template_name,
    roundoff:this.QuotationForm.value.roundoff,
    insurance_charges:this.QuotationForm.value.insurance_charges,
    packing_charges:this.QuotationForm.value.packing_charges,
    buyback_charges:this.QuotationForm.value.buyback_charges,
    freight_charges:this.QuotationForm.value.freight_charges,
    additional_discount:this.QuotationForm.value.additional_discount,
    addon_charge:this.QuotationForm.value.addon_charge,
    producttotalamount:this.QuotationForm.value.producttotalamount,
    delivery_days: this.QuotationForm.value.delivery_days,
    grandtotal:this.QuotationForm.value.grandtotal,

  }
  var url='SmrTrnQuotation/postQuotationAmend'

  this.service.post(url, params).subscribe((result: any) => {
    if(result.status == false){
      this.ToastrService.warning(result.message)        
    }
    else{
      this.ToastrService.success(result.message)
      this.route.navigate(['/smr/SmrTrnQuotationSummary']);   
    }       
  });
}
onback(){
  this.route.navigate(['/smr/SmrTrnQuotationSummary']); 
}   

  
Close(){
  this.route.navigate(['/smr/SmrTrnQuotationSummary']);
    }
    }
  
    
