import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { formatPercent } from '@angular/common';
import { AES, enc } from 'crypto-js';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';
import { AngularEditorConfig } from '@kolkov/angular-editor';

@Component({
  selector: 'app-smrtrnquotetoorder',
  templateUrl: './smrtrnquotetoorder.component.html',
  styleUrls: ['./smrtrnquotetoorder.component.scss']
})
export class SmrtrnquotetoorderComponent implements OnInit{


  showUpdateButton: boolean = false;
  showAddButton: boolean = true;

  OrderForm: FormGroup | any;
  productform: FormGroup | any;
  SO_list: any [] = [];
  user_list: any [] = [];
  currency_list: any [] = [];
  product_list: any [] = [];
  tax_list: any [] = [];
  tax2_list: any [] = [];
  tax3_list: any [] = [];
  tax4_list: any [] = [];
  QSOproductlist: any [] = [];
  mdlTaxName1:any;
  mdlTaxName2:any;
  mdlTaxName3:any;
  mdlTaxName4 :any;
  mdlProductName:any;
  salesorder: any;
  quatation_gid:any;
  unitprice: number=0;
  quantity: number=0;
  discountpercentage: number=0;
  discountamount: any;
  totalamount: number=0;
  total_price:any;
  addon_charges: any;
  POdiscountamount: any;
  frieght_charges: any;
  parameterValue: string | undefined;
  forwardingCharges: any;
  insurance_charges: number=0;
  roundoff: number=0;
  buyback_charges: number=0;
  additional_discount: number=0;
  addon_charge: number=0;
  freight_charges: number=0;
  Grandtotal: any;
  tax_amount: any;
  tax_amount2: any;
  tax_amount3: any;
  tax_amount4: any;
  packing_charges: number = 0;
  total_amount: any;
  taxpercentage: any;
  responsedata: any;
  directeditquotation_list: any [] = [];
  producttotalamount: any;
  allchargeslist : any[]=[];
  mdlProductUomPrice:any;
  mdlProductUom:any;
  mdlProductCode:any;
  mdlTerms:any
  terms_list:any[]=[];

  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '25rem',
    minHeight: '5rem',
    width: '680px',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',

  };
  mdlCurrencyName: any;
  

  constructor(private http:HttpClient, private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) {
  
    this.OrderForm = new FormGroup({
      quotation_gid: new FormControl(''),
      salesorder_date: new FormControl(''),
      branch_name: new FormControl(''),
      quotation_referenceno1: new FormControl(''),
      customer_name: new FormControl(''),
      so_referencenumber: new FormControl(''),
      customercontact_names: new FormControl(''),
      customer_mobile: new FormControl(''),
      customer_email: new FormControl(''),
      customer_address: new FormControl(''),
      so_remarks: new FormControl(''),
      shipping_to: new FormControl(''),
      user_gid: new FormControl(''),
      user_name: new FormControl(''),
      start_date: new FormControl(''),
      end_date: new FormControl(''),
      freight_terms: new FormControl(''),
      payment_terms: new FormControl(''),
      currencyexchange_gid: new FormControl(''),
      currency_code: new FormControl(''),
      exchange_rate: new FormControl(''),
      product_gid : new FormControl(''),
      product_name: new FormControl(''),
      productgroup_name: new FormControl(''),
      productuom_name: new FormControl(''),
      product_code: new FormControl(''),
      product_price: new FormControl(''),
      qty_quoted: new FormControl(''),
      margin_percentage: new FormControl(''),
      margin_amount: new FormControl(''),
      selling_price: new FormControl(''),
      product_requireddate: new FormControl(''),
      product_requireddateremarks: new FormControl(''),
      tax_gid: new FormControl(''),
      tax_name: new FormControl(''),
      tax_amount: new FormControl(''),
      tax_name2: new FormControl(''),
      tax_amount2: new FormControl(''),
      tax_name3: new FormControl(''),
      tax_amount3: new FormControl(''),
      totalamount: new FormControl(''),
      additional_discount: new FormControl(''),
      freight_charges: new FormControl(''),
      buyback_charges: new FormControl(''),
      packing_charges: new FormControl(''),
      insurance_charges: new FormControl(''),
      roundoff: new FormControl(''),
      Grandtotal: new FormControl(''),
      addon_charge: new FormControl(''),
      payment_days: new FormControl(''),
      delivery_days: new FormControl(''),
      total_price: new FormControl(''),
      customerproduct_code: new FormControl(''),
      total_amount: new FormControl(''),
      tax_name4: new FormControl(''),
      tax_amount4:new FormControl(''),
      tmpsalesorderdtl_gid:new FormControl(''),
      grandtotal:new FormControl(''),
      termsandconditions:new FormControl(''),
      template_gid:new FormControl(''),
      template_name: new FormControl(''),
    });
    

    this.productform = new FormGroup({

      product_gid : new FormControl(''),
      product_name: new FormControl(''),
      productgroup_name: new FormControl(''),
      productuom_name: new FormControl(''),
      product_code: new FormControl(''),
      unitprice: new FormControl(''),
      quantity: new FormControl(''),
      discountpercentage: new FormControl(''),
      discountamount: new FormControl(''),
      selling_price: new FormControl(''),
      product_requireddate: new FormControl(''),
      product_requireddateremarks: new FormControl(''),
      tax_gid: new FormControl(''),
      tax_name: new FormControl(''),
      tax_amount: new FormControl(''),
      tax_name2: new FormControl(''),
      tax_amount2: new FormControl(''),
      tax_name3: new FormControl(''),
      tax_amount3: new FormControl(''),
      totalamount: new FormControl(''),
      additional_discount: new FormControl(''),
      freight_charges: new FormControl(''),
      buyback_charges: new FormControl(''),
      packing_charges: new FormControl(''),
      insurance_charges: new FormControl(''),
      roundoff: new FormControl(''),
      Grandtotal: new FormControl(''),
      addon_charge: new FormControl(''),
      payment_days: new FormControl(''),
      delivery_days: new FormControl(''),
      total_price: new FormControl(''),
      customerproduct_code: new FormControl(''),
      total_amount: new FormControl(''),
      tax_name4: new FormControl(''),
      tmpsalesorderdtl_gid:new FormControl(''),
      termsandconditions:new FormControl(''),
      template_gid:new FormControl(''),
      template_name: new FormControl(''),
    });
  
  }

    
  ngOnInit(): void {

    this.salesorder = this.route.snapshot.paramMap.get('quotation_gid');

     const secretKey = 'storyboarderp';
 
     const deencryptedParam = AES.decrypt(this.salesorder, secretKey).toString(enc.Utf8);

     this.GetRaiseSOSummary(deencryptedParam);

     this.quatation_gid=deencryptedParam;
     this.GetTemporarySummary();
 
      const options: Options = {
        dateFormat: 'd-m-Y',    
      };
      flatpickr('.date-picker', options); 
    
      
 
    this.productform = new FormGroup ({
      product_gid : new FormControl(''),
      quotation_gid: new FormControl(''),
      product_name: new FormControl(''),
      productgroup_name: new FormControl(''),
      productuom_name: new FormControl(''),
      product_code: new FormControl(''),
      unitprice: new FormControl(''),
      quantity: new FormControl(''),
      discountpercentage: new FormControl(''),
      discountamount: new FormControl(''),
      selling_price: new FormControl(''),
      product_requireddate: new FormControl(''),
      product_requireddateremarks: new FormControl(''),
      tax_gid: new FormControl(''),
      tax_name: new FormControl(''),
      tax_amount: new FormControl(''),
      tax_name2: new FormControl(''),
      tax_amount2: new FormControl(''),
      tax_name3: new FormControl(''),
      tax_amount3: new FormControl(''),
      totalamount: new FormControl(''),
      additional_discount: new FormControl(''),
      frieght_charges: new FormControl(''),
      buyback_charges: new FormControl(''),
      packing_charges: new FormControl(''),
      insurance_charges: new FormControl(''),
      roundoff: new FormControl(''),
      Grandtotal: new FormControl(''),
      addon_charge: new FormControl(''),
      payment_days: new FormControl(''),
      delivery_days: new FormControl(''),
      total_price: new FormControl(''),
      customerproduct_code: new FormControl(''),
      total_amount: new FormControl(''),
      tax_name4: new FormControl(''),
      tax_amount4:new FormControl(''),
      tmpsalesorderdtl_gid:new FormControl(''),
      grandtotal:new FormControl(''),
      termsandconditions : new FormControl(''),
      template_gid:new FormControl(''),template_name: new FormControl(''),
    })

    //// Sales Dropdown ////

    var url = 'SmrTrnQuotation/GetSalesDtl'
    this.service.get(url).subscribe((result: any) =>{
      this.user_list= result.GetSalesDtl;
    });

    //// Currency Dropdown ////
    
    var url = 'SmrTrnQuotation/GetCurrencyCodeDtl'
    this.service.get(url).subscribe((result: any) =>{
      this.currency_list= result.GetCurrencyCodeDtl;
    });


     //// Product Dropdown ////
    
     var url = 'SmrTrnQuotation/GetProductNamesDtl'
     this.service.get(url).subscribe((result: any) =>{
       this.product_list= result.GetProductNamesDtl;
     });

      //// Tax 1 Dropdown ////
    
      var url = 'SmrTrnQuotation/GetTaxOnceDtl'
      this.service.get(url).subscribe((result: any) =>{
        this.tax_list= result.GetTaxOnceDtl;
      });

       //// Tax 2 Dropdown ////
    
     var url = 'SmrTrnQuotation/GetTaxTwiceDtl'
     this.service.get(url).subscribe((result: any) =>{
       this.tax2_list= result.GetTaxTwiceDtl;
     });

      //// Tax 3 Dropdown ////
    
      var url = 'SmrTrnQuotation/GetTaxThriceDtl'
      this.service.get(url).subscribe((result: any) =>{
        this.tax3_list= result.GetTaxThriceDtl;
      });


       //// Tax 4 Dropdown ////
    
     var url = 'SmrTrnQuotation/GetTaxFourSDtl'
     this.service.get(url).subscribe((result: any) =>{
       this.tax4_list= result.GetTaxFourSDtl;
     });


     //// T & C Dropdown ////
    var url = 'SmrTrnQuotation/GetTermsandConditions'
    this.service.get(url).subscribe((result: any) => {
      this.terms_list = result.GetTermsandConditions;
    });

    //// Currency Dropdown ////
 debugger
  var url = 'SmrTrnQuotation/GetCurrencyDtl'
  this.service.get(url).subscribe((result:any)=>{
    this.currency_list = result.GetCurrencyDt;
    this.mdlCurrencyName = this.currency_list[0].currencyexchange_gid;    
    const defaultCurrency = this.currency_list.find(currency => currency.default_currency === 'Y');
    const defaultCurrencyExchangeRate = defaultCurrency.exchange_rate;  
      if (defaultCurrency) {
        this.mdlCurrencyName = defaultCurrency.currencyexchange_gid;
        this.OrderForm.get("exchange_rate")?.setValue(defaultCurrencyExchangeRate);
      }
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
  

  GetRaiseSOSummary(quotation_gid: any) {
    
    
        var url = 'SmrTrnQuotation/GetRaiseSOSummary'
    
        let param = {
    
          quotation_gid: quotation_gid
    
        }
    
        this.service.getparams(url, param).subscribe((result: any) => {
    
          this.SO_list = result.SO_list;
          this.OrderForm.get("quotation_gid")?.setValue(this.SO_list[0].quotation_gid);
          this.OrderForm.get("customer_gid")?.setValue(this.SO_list[0].customer_gid);
          this.OrderForm.get("branch_gid")?.setValue(this.SO_list[0].branch_gid)
          this.OrderForm.get("salesorder_date")?.setValue(this.SO_list[0].salesorder_date);
          this.OrderForm.get("branch_name")?.setValue(this.SO_list[0].branch_name);
          this.OrderForm.get("quotation_referenceno1")?.setValue(this.SO_list[0].quotation_referenceno1);
          this.OrderForm.get("customer_name")?.setValue(this.SO_list[0].customer_name);
          this.OrderForm.get("so_referencenumber")?.setValue(this.SO_list[0].so_referencenumber);
          this.OrderForm.get("customercontact_names")?.setValue(this.SO_list[0].customercontact_names);
          this.OrderForm.get("customer_mobile")?.setValue(this.SO_list[0].customer_mobile);
          this.OrderForm.get("customer_email")?.setValue(this.SO_list[0].customer_email);
          this.OrderForm.get("customer_address")?.setValue(this.SO_list[0].customer_address);
          this.OrderForm.get("so_remarks")?.setValue(this.SO_list[0].so_remarks);
          this.OrderForm.get("shipping_to")?.setValue(this.SO_list[0].shipping_to);
          this.OrderForm.get("user_name")?.setValue(this.SO_list[0].user_name);
          this.OrderForm.get("start_date")?.setValue(this.SO_list[0].start_date);
          this.OrderForm.get("end_date")?.setValue(this.SO_list[0].end_date);
          this.OrderForm.get("freight_terms")?.setValue(this.SO_list[0].freight_terms);
          this.OrderForm.get("payment_terms")?.setValue(this.SO_list[0].payment_terms);
          this.OrderForm.get("currency_code")?.setValue(this.SO_list[0].currency_code);
          this.OrderForm.get("exchange_rate")?.setValue(this.SO_list[0].exchange_rate);
          this.OrderForm.get("product_name")?.setValue(this.SO_list[0].product_name);
          this.OrderForm.get("productgroup_name")?.setValue(this.SO_list[0].productgroup_name);
          this.OrderForm.get("customerproduct_code")?.setValue(this.SO_list[0].customerproduct_code);
          this.OrderForm.get("product_code")?.setValue(this.SO_list[0].product_code);
          this.OrderForm.get("productuom_name")?.setValue(this.SO_list[0].productuom_name);
          this.OrderForm.get("product_price")?.setValue(this.SO_list[0].product_price);
          this.OrderForm.get("qty_quoted")?.setValue(this.SO_list[0].qty_quoted);
          this.OrderForm.get("margin_percentage")?.setValue(this.SO_list[0].margin_percentage);
          this.OrderForm.get("margin_amount")?.setValue(this.SO_list[0].margin_amount);
          this.OrderForm.get("selling_price")?.setValue(this.SO_list[0].selling_price);
          this.OrderForm.get("product_requireddate")?.setValue(this.SO_list[0].product_requireddate);
          this.OrderForm.get("product_requireddateremarks")?.setValue(this.SO_list[0].product_requireddateremarks);
          this.OrderForm.get("tax_name")?.setValue(this.SO_list[0].tax_name);
          this.OrderForm.get("tax_amount")?.setValue(this.SO_list[0].tax_amount);
          this.OrderForm.get("tax_name2")?.setValue(this.SO_list[0].tax_name2);
          this.OrderForm.get("tax_amount2")?.setValue(this.SO_list[0].tax_amount2);
          this.OrderForm.get("tax_name3")?.setValue(this.SO_list[0].tax_name3);
          this.OrderForm.get("tax_amount3")?.setValue(this.SO_list[0].tax_amount3);
          this.OrderForm.get("price")?.setValue(this.SO_list[0].price);
          this.OrderForm.get("payment_days")?.setValue(this.SO_list[0].payment_days);
          this.OrderForm.get("delivery_days")?.setValue(this.SO_list[0].delivery_days);
          this.OrderForm.get("total_price")?.setValue(this.SO_list[0].total_price);
          this.OrderForm.get("tax_name4")?.setValue(this.SO_list[0].tax_name4);
          this.OrderForm.get("tmpsalesorderdtl_gid")?.setValue(this.SO_list[0].tmpsalesorderdtl_gid);
          this.OrderForm.get("total_amount")?.setValue(this.SO_list[0].total_amount);
          this.OrderForm.get("Grandtotal")?.setValue(this.SO_list[0].Grandtotal);
          this.OrderForm.get("addon_charge")?.setValue(this.SO_list[0].addon_charge);
          this.OrderForm.get("additional_discount")?.setValue(this.SO_list[0].additional_discount);
          this.OrderForm.get("freight_charges")?.setValue(this.SO_list[0].freight_charges);
          this.OrderForm.get("buyback_charges")?.setValue(this.SO_list[0].buyback_charges);
          this.OrderForm.get("packing_charges")?.setValue(this.SO_list[0].packing_charges);
          this.OrderForm.get("insurance_charges")?.setValue(this.SO_list[0].insurance_charges);
          this.OrderForm.get("roundoff")?.setValue(this.SO_list[0].roundoff);
          this.OrderForm.get("termsandconditions")?.setValue(this.SO_list[0].termsandconditions);
          //this.OrderForm.get("template_name")?.setValue(this.SO_list[0].template_name);

          this.responsedata=result;
     
        });
      }


      
  get currency_code() {
    return this.OrderForm.get('currency_code')!;
  }
  get payment_days(){
    return this.OrderForm.get('payment_days')!;
  }
  get delivery_days(){
    return this.OrderForm.get('delivery_days')!;
  }

  OnChangeCurrency(){
    let currencyexchange_gid = this.OrderForm.get("currency_code")?.value;
    console.log(currencyexchange_gid)
    let param = {
      currencyexchange_gid: currencyexchange_gid
    }
    var url = 'SmrTrnQuotation/GetOnChangeCurrency';
    
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.currency_list = this.responsedata.GetOnchangecurrency;
      this.OrderForm.get("exchange_rate")?.setValue(this.currency_list[0].exchange_rate);
      
      
    });

  }

  onCurrencyCodeChange(event: Event){

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


    // const value = this.producttotalamount.value;
    //   const formattedValue = parseFloat(value).toFixed(2);
    //   this.producttotalamount.setValue(formattedValue, { emitEvent: false });

  }
 
  OnChangeTaxAmount1() {
    
    
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
    
    let tax_gid = this.OrderForm.get('tax_name4')?.value
    this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    console.log(this.taxpercentage);
    let tax_percentage = this.taxpercentage[0].percentage;
    console.group(tax_percentage);

    this.tax_amount4 = +((tax_percentage * this.total_price / 100).toFixed(2));
    this.total_amount = +((this.tax_amount4 + (+this.total_price)).toFixed(2));
    this.Grandtotal = ((this.total_price) + (+this.addon_charge) + (+this.freight_charges) + (+this.buyback_charges) + (+this.insurance_charges)+(+this.packing_charges) + (+this.roundoff) - (+this.additional_discount));
    
  }

  GetOnChangeProductName(){

    
    let product_gid = this.productform.value.product_name.product_gid;
    let param = {
      product_gid: product_gid
    }
    var url = 'SmrTrnQuotation/GetOnChangeProductsNameQTO';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.productform.get("product_code")?.setValue(result.ProductsCode[0].product_code);
      this.productform.get("productuom_name")?.setValue(result.ProductsCode[0].productuom_name);
      this.productform.get("selling_price")?.setValue(result.ProductsCode[0].selling_price);
      this.productform.value.productgroup_gid = result.ProductsCode[0].productgroup_gid,
        this.productform.value.productuom_gid = result.ProductsCode[0].productuom_gid
    });

  }

  

  

  // onedit(parameter: string){

  // }

  
  openModaldelete(data: any){
    
    this.parameterValue = data.tmpsalesorderdtl_gid;
}
  ondelete(){
    
    var url = 'SmrTrnQuotation/GetDeleteQuotetoOrderProductSummary'
    let param = {
      tmpsalesorderdtl_gid : this.parameterValue 
    }
    this.service.getparams(url,param).subscribe((result: any) => {
      if(result.status ==false){
        this.ToastrService.warning(result.message)
      }
      else{
        this.ToastrService.success(result.message) 
        this.GetTemporarySummary()
      }
      
    });

  }
  onSubmit(){

  
  console.log(this.OrderForm.value)
  var params={
    
    quotation_gid:this.OrderForm.value.quotation_gid,
    salesorder_date:this.OrderForm.value.salesorder_date,
    branch_gid:this.OrderForm.value.branch_gid,
    branch_name:this.OrderForm.value.branch_name,
    customer_gid : this.OrderForm.value.customer_gid,
    quotation_referenceno1:this.OrderForm.value.quotation_referenceno1,
    customer_name:this.OrderForm.value.customer_name,
    so_referencenumber:this.OrderForm.value.so_referencenumber,
    customercontact_names:this.OrderForm.value.customercontact_names,
    customer_mobile:this.OrderForm.value.customer_mobile,
    customer_email:this.OrderForm.value.customer_email,
    customer_address:this.OrderForm.value.customer_address,
    so_remarks:this.OrderForm.value.so_remarks,
    shipping_to:this.OrderForm.value.shipping_to,
    user_name:this.OrderForm.value.user_name,
    exchange_rate:this.OrderForm.value.exchange_rate,
    currency_code:this.OrderForm.value.currency_code,
    freight_terms:this.OrderForm.value.freight_terms,
    payment_terms:this.OrderForm.value.payment_terms,
    end_date:this.OrderForm.value.end_date,
    start_date:this.OrderForm.value.start_date,
    addon_charge:this.OrderForm.value.addon_charge,
    additional_discount:this.OrderForm.value.additional_discount,
    roundoff:this.OrderForm.value.roundoff,
    frieght_charges:this.OrderForm.value.frieght_charges,
    buyback_charges:this.OrderForm.value.buyback_charges,
    packing_charges:this.OrderForm.value.packing_charges,
    insurance_charges:this.OrderForm.value.insurance_charges,
    tax_amount:this.OrderForm.value.tax_amount,
    Grandtotal: this.OrderForm.value.Grandtotal,
    payment_days: this.OrderForm.value.payment_days,
    delivery_days: this.OrderForm.value.delivery_days,
    termsandconditions: this.OrderForm.value.termsandconditions,
    template_name: this.OrderForm.value.template_name,
    template_gid: this.OrderForm.value.template_gid,
   


  }
  var url = 'SmrTrnQuotation/PostQuotationToOrder'
    this.service.post(url,params).subscribe((result: any) => {
      if(result.status ==false){
        this.ToastrService.warning(result.message)
       
      }
      else
      {
        this.ToastrService.success(result.message)
        this.router.navigate(['/smr/SmrTrnSalesorderSummary'])
      }

     
      
    });

  }

  finaltotal(){
    const addoncharges = isNaN(this.addon_charge) ? 0 : +this.addon_charge;
    const frieghtcharges = isNaN(this.freight_charges) ? 0 : +this.freight_charges;
    const forwardingCharges = isNaN(this.buyback_charges) ? 0 : +this.buyback_charges;
    const insurancecharges = isNaN(this.insurance_charges) ? 0 : +this.insurance_charges;
    const packing_charges = isNaN(this.packing_charges) ? 0 : +this.packing_charges;
    const roundoff = isNaN(this.roundoff) ? 0 : +this.roundoff;
    const discountamount = isNaN(this.additional_discount) ? 0 : +this.additional_discount;

    this.Grandtotal = ((this.total_amount) + (addoncharges) + (frieghtcharges) + (forwardingCharges) + (insurancecharges) + (roundoff) - (discountamount)+(packing_charges));
    this.Grandtotal = +(this.Grandtotal).toFixed(2);
  }

   
  OnSubmit(){

    console.log(this.productform.value)
    var params = {
      quotation_gid: this.quatation_gid,
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
      selling_price: this.productform.value.selling_price,
      unitprice: this.productform.value.unitprice,
      quantity: this.productform.value.quantity,
      discountpercentage: this.productform.value.discountpercentage,
      discountamount: this.productform.value.discountamount,
      product_requireddateremarks: this.productform.value.product_requireddateremarks,
      tax_name: this.productform.value.tax_name,
      tax_amount: this.productform.value.tax_amount,
      tax_name2: this.productform.value.tax_name2,
      tax_amount2: this.productform.value.tax_amount2,
      tax_name3: this.productform.value.tax_name3,
      tax_amount3: this.productform.value.tax_amount3,
      tax_gid: this.productform.value.tax_gid,
      tax_gid2: this.productform.value.tax_gid2,
      tax_gid3: this.productform.value.tax_gid3,
      totalamount: this.productform.value.totalamount,



    }
    console.log(params)
    var api = 'SmrTrnQuotation/GetProductAdd';
    this.service.post(api, params).subscribe((result: any) => {
      this.productform.reset()
      this.GetTemporarySummary();
    },
    );

  }


  GetTemporarySummary() {
    var url = 'SmrTrnQuotation/GetTemporarySummary'
    let param = {
      quotation_gid: this.quatation_gid
    }
    
    this.service.getparams(url,param).subscribe((result: any) => {
      this.responsedata = result;
      this.QSOproductlist = result.temp_list;
      this.OrderForm.get("Grandtotal")?.setValue(result.grandtotal);
      this.OrderForm.get("total_price")?.setValue(result.totalprice);


    });
  }

  onedit(tmpsalesorderdtl_gid: any) {
    var url = 'SmrTrnQuotation/GetQuotetoOrderProductEditSummary'
    let param = {
      tmpsalesorderdtl_gid: tmpsalesorderdtl_gid
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      this.directeditquotation_list = result.directeditquotation_list;
      this.productform.get("tmpsalesorderdtl_gid")?.setValue(this.directeditquotation_list[0].tmpsalesorderdtl_gid);
      this.productform.get("product_name")?.setValue(this.directeditquotation_list[0].product_name);
      this.productform.get("product_gid")?.setValue(this.directeditquotation_list[0].product_gid);
      this.productform.get("product_code")?.setValue(this.directeditquotation_list[0].product_code);
      this.productform.get("productuom_name")?.setValue(this.directeditquotation_list[0].productuom_name);      
      this.productform.get("quantity")?.setValue(this.directeditquotation_list[0].quantity);
      this.productform.get("totalamount")?.setValue(this.directeditquotation_list[0].totalamount);
      this.productform.get("tax_name")?.setValue(this.directeditquotation_list[0].tax_name);   
      this.productform.get("tax_gid")?.setValue(this.directeditquotation_list[0].tax_gid);   
      this.productform.get("selling_price")?.setValue(this.directeditquotation_list[0].selling_price);
      this.productform.get("tax_amount")?.setValue(this.directeditquotation_list[0].tax_amount);  
      this.productform.get("discount_percentage")?.setValue(this.directeditquotation_list[0].discount_percentage);
      this.productform.get("discountamount")?.setValue(this.directeditquotation_list[0].discountamount);  
  
    });
    this.showUpdateButton = true;
    this.showAddButton = false;
  }

  onupdate() {
    var params = {
      tmpsalesorderdtl_gid: this.productform.value.tmpsalesorderdtl_gid,
      product_code: this.productform.value.product_code,
      product_name: this.productform.value.product_name.product_name,
      productuom_name: this.productform.value.productuom_name,
      quantity: this.productform.value.quantity,
      product_price: this.productform.value.product_price,
      discountamount: this.productform.value.discountamount,
      discountpercentage: this.productform.value.discountpercentage,
      product_gid: this.productform.value.product_name.product_gid,      
      tax_name: this.productform.value.tax_name,
      tax_gid: this.productform.value.tax_name.tax_gid,
      tax_amount: this.productform.value.tax_amount,
      price: this.productform.value.price
    }
    var url = 'SmrTrnSalesorder/PostUpdateDirectSOProduct'

    this.service.post(url,params).pipe().subscribe((result:any)=>{
      this.responsedata=result;
      if(result.status ==false){
        this.ToastrService.warning(result.message)
      }
      else{
        this.ToastrService.success(result.message)
        this.productform.reset();
        
      }
      this.GetTemporarySummary();
    });
  }
  
  

  close(){
     this.router.navigate(['/smr/SmrTrnQuotationSummary'])
  }
  OnClearTax() {
     
    this.tax_amount = 0; 
    const subtotal =  this.unitprice * this.quantity;
    this.discountamount = (subtotal * this.discountpercentage) / 100;
    this.totalamount = +(subtotal - this.discountamount + this.tax_amount).toFixed(2);
  }
  OnClearOverallTax() {
    //this.tax_amount4=0;
   
    this.total_amount = +((+this.total_price) + (+this.tax_amount4));  
    this.total_amount = +this.total_amount.toFixed(2);
    
    this.Grandtotal = ((this.total_amount) + (+this.addon_charge) + (+this.freight_charges) + (+this.buyback_charges) + (+this.packing_charges) + (+this.insurance_charges) + (+this.roundoff) + (+this.totalamount)  - (+this.additional_discount));
    this.Grandtotal =+this.Grandtotal.toFixed(2);
  }
  onClearProduct(){
    this.mdlProductCode=''
    this.mdlProductUom=''
    }
  onKeyPress(event: any) {
    // Get the pressed key
    const key = event.key;

    if (!/^[0-9.]$/.test(key)) {
      // If not a number or dot, prevent the default action (key input)
      event.preventDefault();
    }
  }
  GetOnChangeTerms(){
    

    let template_gid = this.OrderForm.value.template_name;
    let param = {
      template_gid: template_gid
    }
    var url = 'SmrTrnQuotation/GetOnChangeTerms';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.OrderForm.get("termsandconditions")?.setValue(result.terms_list[0].termsandconditions);
      this.OrderForm.value.template_gid = result.terms_list[0].template_gid
      //this.cusraiseform.value.productuom_gid = result.GetProductsName[0].productuom_gid
    });
  }
}
