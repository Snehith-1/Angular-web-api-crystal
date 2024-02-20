import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { AES, enc } from 'crypto-js';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-smr-trn-raisequote',
  templateUrl: './smr-trn-raisequote.component.html',
  styleUrls: ['./smr-trn-raisequote.component.scss']
})
export class SmrTrnRaisequoteComponent {

    showInput: boolean = false;
    inputValue: string = ''
    config: AngularEditorConfig = {
      editable: true,
      spellcheck: true,
      height: '45rem',
      minHeight: '5rem',
      width: '845px',
      placeholder: 'Enter text here...',
      translate: 'no',
      defaultParagraphSeparator: 'p',
      defaultFontName: 'Arial',
  };

  CombineForm!: FormGroup;
  quotationform!: FormGroup;
  template_content!: FormGroup;
  productform!: FormGroup;
  user_list: any[] = [];
  currency_list: any[] = [];
  product_list: any[] = [];
  productgroup_list: any[] = [];
  productname_list: any[] = [];
  tax_list: any[] = [];
  tax2_list: any[] = [];
  tax3_list: any[] = [];
  tax4_list: any[] = [];
  terms_list: any[] = [];
  EnqtoQuote_list: any[] = [];
  editenquirytoquote_list: any[] = [];
  raisequotedetail_list:any[] = [];
  taxamount3: any;
  txtUnitPrice:any;
  txtProductCode:any;
  txtProductUnit:any;
  mdlTaxName3: any;
  mdlUserName: any;
  taxamount2: any;
  taxamount1: any;
  mdlProductName: any;
  mdlTaxName1: any;
  mdlTaxName2: any;
  mdlTaxName4: any;
  mdlTerms: any;
  mdlCurrencyName :any;
  quote: any;
  Quote_list: any[] = [];
  responsedata: any;
  unitprice: number = 0;
  quantity: number = 0;
  discountpercentage: number = 0;
  discountamount: any;
  totalamount: number = 0;
  total_price: number = 0;
  addon_charges: number = 0;
  POdiscountamount: number = 0;
  frieght_charges: number = 0;
  forwardingCharges: number = 0;
  insurance_charges: number = 0;
  roundoff: number = 0;
  buyback_charges: number = 0;
  additional_discount: number = 0;
  addon_charge: number = 0;
  freight_charges: number = 0;
  grandtotal: number = 0;
  tax_amount: number = 0;
  taxpercentage: any;
  productdetails_list: any;
  tax_amount2: number = 0;
  tax_amount3: number = 0;
  tax_amount4: any;
  packing_charges:number = 0;
  producttotalamount: number=0;
  parameterValue: string | undefined;
  productnamelist: any;
  selectedCurrencyCode: any;
  POadd_list: any;
  total_amount: number=0;
  addoncharge: any;
  freightcharges: any;
  // packing_charges: any;
  buybackorscrap: any;
  data:any;
  EnqtoQuote_list1:any;
  txtExchangeRate: any;
  enq_gid:any;
  showUpdateButton: boolean = false;
  showAddButton: boolean = true;
  allchargeslist: any [] = [];



  constructor(private formBuilder: FormBuilder,public NgxSpinnerService: NgxSpinnerService, private ToastrService: ToastrService, private route: Router, public service: SocketService, private router: ActivatedRoute) {

    this.quotationform = new FormGroup({
      quotation_referenceno1: new FormControl(''),
      quotation_date: new FormControl(''),
      branch_name: new FormControl(''),
      customer_name: new FormControl(''),
      customer_contact: new FormControl(''),
      customer_mobile: new FormControl(''),
      enquiry_gid: new FormControl(''),
      customer_email: new FormControl(''),
      customer_address: new FormControl(''),
      enq: new FormControl(''),
      so_remarks: new FormControl(''),
      currencyexchange_gid: new FormControl(''),
      currency_code: new FormControl(''),
      exchange_rate: new FormControl(''),
      freight_terms: new FormControl(''),
      payment_terms: new FormControl(''),
      productgroup_gid: new FormControl(''),
      productgroup_name: new FormControl(''),
      customerproduct_code: new FormControl(''),
      product_code: new FormControl(''),
      product_gid: new FormControl(''),
      product_name: new FormControl(''),
      display_field: new FormControl(''),
      quantity: new FormControl(''),
      selling_price: new FormControl(''),
      discountpercentage: new FormControl(''),
      discountamount: new FormControl(''),
      unitprice: new FormControl(''),
      addon_charge: new FormControl(''),
      additional_discount: new FormControl(''),
      freight_charges: new FormControl(''),
      buyback_charges: new FormControl(''),
      packing_charges: new FormControl(''),
      insurance_charges: new FormControl(''),
      roundoff: new FormControl(''),
      grandtotal: new FormControl(''),
      tax_gid: new FormControl(''),
      tax_gid2: new FormControl(''),
      tax_gid3: new FormControl(''),
      tax_gid4: new FormControl(''),
      tax_name: new FormControl(''),
      tax_amount: new FormControl(''),
      tax_name2: new FormControl(''),
      tax_amount2: new FormControl(''),
      tax_name3: new FormControl(''),
      tax_amount3: new FormControl(''),
      tax_name4: new FormControl(''),
      tax_amount4: new FormControl(''),
      totalamount: new FormControl(''),
      productuom_name: new FormControl(''),
      delivery_days: new FormControl(''),
      payment_days: new FormControl(''),
      total_price: new FormControl(''),
      total_amount: new FormControl(''),
      product_requireddate: new FormControl(''),
      product_requireddateremarks: new FormControl(''),
      slno: new FormControl(''),
      template_gid: new FormControl(''),
      template_name: new FormControl(''),
      template_content: new FormControl(''),
      producttotalamount: new FormControl(''),
      tmpquotationdtl_gid: new FormControl('')
      
    });


    this.productform = new FormGroup({
      productgroup_gid: new FormControl(''),
      productgroup_name: new FormControl(''),
      customerproduct_code: new FormControl(''),
      product_code: new FormControl(''),
      product_gid: new FormControl(''),
      product_name: new FormControl(''),
      display_field: new FormControl(''),
      quantity: new FormControl(''),
      selling_price: new FormControl(''),
      discountpercentage: new FormControl(''),
      discountamount: new FormControl(''),
      unitprice: new FormControl(''),
      tax_gid: new FormControl(''),
      tax_gid2: new FormControl(''),
      tax_gid3: new FormControl(''),
      tax_gid4: new FormControl(''),
      tax_name: new FormControl(''),
      tax_amount: new FormControl(''),
      tax_name2: new FormControl(''),
      tax_amount2: new FormControl(''),
      tax_name3: new FormControl(''),
      tax_amount3: new FormControl(''),
      tax_name4: new FormControl(''),
      tax_amount4: new FormControl(''),
      totalamount: new FormControl(''),
      productuom_name: new FormControl(''),
      product_requireddate: new FormControl(''),
      product_requireddateremarks: new FormControl(''),
      slno: new FormControl(''),
      total_price: new FormControl(''),
      tmpquotationdtl_gid: new FormControl('')
     
    });
  }


  ngOnInit(): void {


    this.quotationform = new FormGroup({
      quotation_referenceno1: new FormControl(''),
      quotation_date: new FormControl(''),
      branch_name: new FormControl(''),
      customer_name: new FormControl(''),
      customer_contact: new FormControl(''),
      customer_mobile: new FormControl(''),
      enquiry_gid: new FormControl(''),
      customer_email: new FormControl(''),
      customer_address: new FormControl(''),
      enq: new FormControl(''),
      so_remarks: new FormControl(''),
      currencyexchange_gid: new FormControl(''),
      currency_code: new FormControl(''),
      exchange_rate: new FormControl(''),
      freight_terms: new FormControl(''),
      payment_terms: new FormControl(''),
      productgroup_gid: new FormControl(''),
      productgroup_name: new FormControl(''),
      customerproduct_code: new FormControl(''),
      product_code: new FormControl(''),
      product_gid: new FormControl(''),
      product_name: new FormControl(''),
      display_field: new FormControl(''),
      quantity: new FormControl(''),
      selling_price: new FormControl(''),
      discountpercentage: new FormControl(''),
      discountamount: new FormControl(''),
      unitprice: new FormControl(''),
      addon_charge: new FormControl(''),
      additional_discount: new FormControl(''),
      freight_charges: new FormControl(''),
      buyback_charges: new FormControl(''),
      packing_charges: new FormControl(''),
      insurance_charges: new FormControl(''),
      roundoff: new FormControl(''),
      grandtotal: new FormControl(''),
      tax_gid: new FormControl(''),
      tax_gid2: new FormControl(''),
      tax_gid3: new FormControl(''),
      tax_gid4: new FormControl(''),
      tax_name: new FormControl(''),
      tax_amount: new FormControl(''),
      tax_name2: new FormControl(''),
      tax_amount2: new FormControl(''),
      tax_name3: new FormControl(''),
      tax_amount3: new FormControl(''),
      tax_name4: new FormControl(''),
      tax_amount4: new FormControl(''),
      totalamount: new FormControl(''),
      productuom_name: new FormControl(''),
      delivery_days: new FormControl(''),
      payment_days: new FormControl(''),
      total_price: new FormControl(''),
      total_amount: new FormControl(''),
      product_requireddate: new FormControl(''),
      product_requireddateremarks: new FormControl(''),
      slno: new FormControl(''),
      template_gid: new FormControl(''),
      template_name: new FormControl(''),
      template_content: new FormControl(''),
      producttotalamount: new FormControl(''),
      tmpquotationdtl_gid: new FormControl('')
    });


    this.productform = new FormGroup({
      productgroup_gid: new FormControl(''),
      productgroup_name: new FormControl(''),
      customerproduct_code: new FormControl(''),
      product_code: new FormControl(''),
      product_gid: new FormControl(''),
      product_name: new FormControl(''),
      display_field: new FormControl(''),
      quantity: new FormControl(''),
      selling_price: new FormControl(''),
      discountpercentage: new FormControl(''),
      discountamount: new FormControl(''),
      unitprice: new FormControl(''),
      tax_gid: new FormControl(''),
      tax_gid2: new FormControl(''),
      tax_gid3: new FormControl(''),
      tax_gid4: new FormControl(''),
      tax_name: new FormControl(''),
      tax_amount: new FormControl(''),
      tax_name2: new FormControl(''),
      tax_amount2: new FormControl(''),
      tax_name3: new FormControl(''),
      tax_amount3: new FormControl(''),
      tax_name4: new FormControl(''),
      tax_amount4: new FormControl(''),
      totalamount: new FormControl(''),
      productuom_name: new FormControl(''),
      product_requireddate: new FormControl(''),
      product_requireddateremarks: new FormControl(''),
      slno: new FormControl(''),
      total_price: new FormControl(''),
      tmpquotationdtl_gid: new FormControl('')
    });

    const options: Options = {
      dateFormat: 'd-m-Y' ,    
    };
    flatpickr('.date-picker', options); 

    this.quote = this.router.snapshot.paramMap.get('enquiry_gid');
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.quote, secretKey).toString(enc.Utf8);
   this.enq_gid=deencryptedParam
    this.GetQOSummary(deencryptedParam);
   //console.log(deencryptedParam);

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

    //// Currency Dropdown ////

    var url = 'SmrTrnQuotation/GetCurrencyDtl'
    this.service.get(url).subscribe((result:any)=>{
    this.currency_list = result.GetCurrencyDt;
    console.log(this.currency_list) 
    this.mdlCurrencyName = this.currency_list[0].currencyexchange_gid;    
    const defaultCurrency = this.currency_list.find(currency => currency.default_currency === 'Y');
    const defaultCurrencyExchangeRate = defaultCurrency.exchange_rate;
    console.log(defaultCurrencyExchangeRate)
      if (defaultCurrency) {
        this.mdlCurrencyName = defaultCurrency.currencyexchange_gid;
        this.quotationform.get("exchange_rate")?.setValue(defaultCurrencyExchangeRate);
      } 
   });

    // var url = 'SmrTrnCustomerEnquiry/GetCurrencyDets'
    // this.service.get(url).subscribe((result: any) => {
    //   this.currency_list = result.GetCurrencyDets;
    // });

    //// Product Dropdown ////

    var url1 = 'SmrTrnCustomerEnquiry/GetProductDets'
    this.service.get(url1).subscribe((result: any) => {
      this.product_list = result.GetProductDets;
    });


    //// Tax 1 Dropdown ////

    var url2 = 'SmrTrnCustomerEnquiry/GetFirstTax'
    this.service.get(url2).subscribe((result: any) => {
      this.tax_list = result.GetFirstTax;
    });

    //// Tax 2 Dropdown ////

    var url3 = 'SmrTrnCustomerEnquiry/GetSecondTax'
    this.service.get(url3).subscribe((result: any) => {
      this.tax2_list = result.GetSecondTax;
    });

    //// Tax 3 Dropdown ////

    var url4 = 'SmrTrnCustomerEnquiry/GetThirdTax'
    this.service.get(url4).subscribe((result: any) => {
      this.tax3_list = result.GetThirdTax;
    });

    //// Tax 4 Dropdown ////

    var url5 = 'SmrTrnCustomerEnquiry/GetFourthTax'
    this.service.get(url5).subscribe((result: any) => {
      this.tax4_list = result.GetFourthTax;
    });

    //// Terms And Condition ////

    var url6 = 'SmrTrnCustomerEnquiry/GetTerms'
    this.service.get(url6).subscribe((result: any) => {
      this.terms_list = result.terms_lists;
    });
  }



  GetQOSummary(enquiry_gid: any) {

    var url = 'SmrTrnCustomerEnquiry/GetQOSummary'
    this.NgxSpinnerService.show()
    let param = {
      enquiry_gid: this.enq_gid
    }

    this.service.getparams(url, param).subscribe((result: any) => {

      this.Quote_list = result.Quote_list;
      this.quotationform.get("enquiry_gid")?.setValue(this.enq_gid);   
      this.quotationform.get("quotation_referenceno1")?.setValue(this.Quote_list[0].quotation_referenceno1);
      this.quotationform.get("quotation_date")?.setValue(this.Quote_list[0].quotation_date);
      this.quotationform.get("branch_name")?.setValue(this.Quote_list[0].branch_name);
      this.quotationform.get("customer_name")?.setValue(this.Quote_list[0].customer_name);
      this.quotationform.get("customer_contact")?.setValue(this.Quote_list[0].customer_contact);
      this.quotationform.get("enq")?.setValue(this.Quote_list[0].enquiry_gid);
      this.quotationform.get("customer_mobile")?.setValue(this.Quote_list[0].customer_mobile);
      this.quotationform.get("customer_email")?.setValue(this.Quote_list[0].customer_email);
      this.quotationform.get("customer_address")?.setValue(this.Quote_list[0].customer_address);
      this.quotationform.get("so_remarks")?.setValue(this.Quote_list[0].so_remarks);
      this.quotationform.get("freight_terms")?.setValue(this.Quote_list[0].freight_terms);
      this.quotationform.get("payment_terms")?.setValue(this.Quote_list[0].payment_terms);
      this.quotationform.get("currency_code")?.setValue(this.Quote_list[0].currency_code);
      this.quotationform.get("exchange_rate")?.setValue(this.Quote_list[0].exchange_rate);
      this.quotationform.get("grandtotal")?.setValue(this.Quote_list[0].grandtotal);
      this.quotationform.get("producttotalamount")?.setValue(this.Quote_list[0].producttotalamount);
      
      this.GetTempSummary();
      this.NgxSpinnerService.hide()
    });
  }

  onKeyPress(event: any) {
    // Get the pressed key
    const key = event.key;
 
    if (!/^[0-9.]$/.test(key)) {
      // If not a number or dot, prevent the default action (key input)
      event.preventDefault();
    }
  }
  GetTempSummary() {
    
   
    var params ={
     enquiry_gid:this.enq_gid
    }
    var url = 'SmrTrnCustomerEnquiry/GetTempSummary'
    this.service.getparams(url,params).subscribe((result: any) => {
      
      this.responsedata = result;
      this.EnqtoQuote_list = this.responsedata.EnqtoQuote_list;
      //console.log(this.entity_list)
     
      this.quotationform.get("producttotalamount")?.setValue(result.grand_total);
      this.quotationform.get("grandtotal")?.setValue(result.grand_total);
      this.quotationform.get("product_gid")?.setValue(result.product_gid);

    });

  }
  get product_name() {
    return this.productform.get('product_name')!;
  }
  get product_code() {
    return this.productform.get('product_code')!;
  }

  get payment_days() {
    return this.quotationform.get('payment_days')!;
  }
  get delivery_days() {
    return this.quotationform.get('delivery_days')!;
  }

  OnSubmit() {
    debugger

    console.log(this.productform.value)
    var params = {
      enquiry_gid:this.enq_gid,
      tmpquotationdtl_gid: this.productform.value.tmpquotationdtl_gid,
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
    var api = 'SmrTrnCustomerEnquiry/GetOnProductAdd'
    this.NgxSpinnerService.show()
    this.service.postparams(api, params).subscribe((result: any) => {
      this.responsedata = result;
      if(result.status == false){
        this.ToastrService.warning(result.message)
        
      }
      else{
        this.ToastrService.success(result.message) 
        this.GetTempSummary();
        this.productform.reset();
        this.NgxSpinnerService.hide()
      }
      this.GetTempSummary();
    },
    );
  }
  OnChangeCurrency() {
    debugger
    let currencyexchange_gid = this.quotationform.get("currency_code")?.value;
    console.log(currencyexchange_gid)
    let param = {
      currencyexchange_gid: currencyexchange_gid
    }
    var url = 'SmrTrnSalesorder/GetOnChangeCurrency';
    this.NgxSpinnerService.show()
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      
      this.currency_list = this.responsedata.GetOnchangeCurrency;
      this.quotationform.get("exchange_rate")?.setValue(this.currency_list[0].exchange_rate);
      this.NgxSpinnerService.hide()
    });
  }
  GetOnChangeProductName() {

    let product_gid = this.productform.value.product_name.product_gid;
    let param = {
      product_gid: product_gid
    }
    var url = 'SmrTrnCustomerEnquiry/GetOnChangeProductNameDets'
    this.NgxSpinnerService.show()
    this.service.getparams(url, param).subscribe((result: any) => {
      this.productform.get("product_code")?.setValue(result.GetProductNameDets[0].product_code);
      this.productform.get("productuom_name")?.setValue(result.GetProductNameDets[0].productuom_name);
      this.productform.get("productgroup_name")?.setValue(result.GetProductNameDets[0].productgroup_name);
      this.productform.get("unitprice")?.setValue(result.GetProductNameDets[0].unitprice);
      this.productform.value.productgroup_gid = result.GetProductNameDets[0].productgroup_gid
      this.NgxSpinnerService.hide()
      //this.cusraiseform.value.productuom_gid = result.GetProductsName[0].productuom_gid
    });
  }

  GetOnChangeTerms() {
    debugger
    let template_gid = this.quotationform.value.template_name;
    let param = {
      template_gid: template_gid
    }
    var url = 'SmrTrnCustomerEnquiry/GetOnChangeTerms'
    this.NgxSpinnerService.show()
    this.service.getparams(url, param).subscribe((result: any) => {
      this.quotationform.get("template_content")?.setValue(result.terms_lists[0].template_content);
      this.quotationform.value.template_gid = result.terms_lists[0].template_gid
      this.NgxSpinnerService.hide()
      //this.cusraiseform.value.productuom_gid = result.GetProductsName[0].productuom_gid
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
   debugger
    let tax_gid4 = this.quotationform.get('tax_name4')?.value
    this.taxpercentage = this.getDimensionsByFilter(tax_gid4);
    console.log(this.taxpercentage);
    let tax_percentage = this.taxpercentage[0].percentage;
    console.group(tax_percentage);

    this.tax_amount4 = +((tax_percentage * this.producttotalamount / 100).toFixed(2));
    this.total_amount = +((this.tax_amount4 + (+this.producttotalamount)).toFixed(2));
    this.grandtotal = ((this.total_amount) + (+this.addon_charge) + (+this.freight_charges) + (+this.buyback_charges) + (+this.insurance_charges)+(+this.packing_charges) + (+this.roundoff) - (+this.additional_discount));
    
  }
  // addedpackage() {
  //   this.grandtotal = ((this.total_amount) + (+this.packing_charges));
  // }

  finaltotal() {
    debugger
    const addoncharges = isNaN(this.addon_charge) ? 0 : +this.addon_charge;
    const frieghtcharges = isNaN(this.freight_charges) ? 0 : +this.freight_charges;
    const forwardingCharges = isNaN(this.buyback_charges) ? 0 : +this.buyback_charges;
    const insurancecharges = isNaN(this.insurance_charges) ? 0 : +this.insurance_charges;
    const packing_charges = isNaN(this.packing_charges) ? 0 : +this.packing_charges;
    const roundoff = isNaN(this.roundoff) ? 0 : +this.roundoff;
    const discountamount = isNaN(this.additional_discount) ? 0 : +this.additional_discount;
 
    this.grandtotal = ((this.total_amount) + (addoncharges) + (frieghtcharges) + (forwardingCharges) + (insurancecharges) + (roundoff) - (discountamount)+(packing_charges));
    this.grandtotal = +(this.grandtotal).toFixed(2);
  }
  onChange2() { }
 

  onSubmit() {


   debugger

   var params = {    
    enquiry_gid:this.enq_gid,
    quotation_gid:this.quotationform.value.quotation_gid,
    quotation_referenceno1:this.quotationform.value.quotation_referenceno1,
    quotation_date:this.quotationform.value.quotation_date,
    branch_name:this.quotationform.value.branch_name,
    customer_name:this.quotationform.value.customer_name,
    customer_contact:this.quotationform.value.customer_contact,
    customer_mobile:this.quotationform.value.customer_mobile,
    customer_email:this.quotationform.value.customer_email,
    customer_address:this.quotationform.value.customer_address,
    enq:this.quotationform.value.enq,
    so_remarks:this.quotationform.value.so_remarks,
    currencyexchange_gid:this.quotationform.value.currencyexchange_gid,
    exchange_rate :this.quotationform.value.exchange_rate,
    freight_terms:this.quotationform.value.freight_terms,
    payment_terms:this.quotationform.value.payment_terms,
    payment_days:this.quotationform.value.payment_days,
    delivery_days:this.quotationform.value.delivery_days,
    producttotalamount:this.quotationform.value.producttotalamount,
    tax_amount4:this.quotationform.value.tax_amount4,
    tax_name4:this.quotationform.value.tax_name4,
    total_amount:this.quotationform.value.total_amount,
    addon_charge:this.quotationform.value.addon_charge,
    additional_discount:this.quotationform.value.additional_discount,
    freight_charges:this.quotationform.value.freight_charges,
    buyback_charges:this.quotationform.value.buyback_charges,
    packing_charges:this.quotationform.value.packing_charges,
    insurance_charges:this.quotationform.value.insurance_charges,
    roundoff:this.quotationform.value.roundoff,
    grandtotal:this.quotationform.value.grandtotal,
    template_name:this.quotationform.value.template_name,
    template_content:this.quotationform.value.template_content,
    customerproduct_code: this.quotationform.value.customerproduct_code,
    customer_gid: this.quotationform.value.customer_name.customer_gid,
    }
   

  var url='SmrTrnCustomerEnquiry/PostCustomerEnquirytoQuotation'
  this.NgxSpinnerService.show()
  if (this.quotationform.value.payment_days != null && this.quotationform.value.quotation_referenceno1 != '',
  this.quotationform.value.customer_mobile != null && this.quotationform.value.customer_email != null) {
    for (const control of Object.keys(this.quotationform.controls)) {
      this.quotationform.controls[control].markAsTouched();
    }
  this.service.postparams(url, params).subscribe((result: any) => {
    if(result.status ==false){
      this.ToastrService.warning(result.message);
      this.NgxSpinnerService.hide()
    
    }
    else{
      this.ToastrService.success(result.message)
      this.route.navigate(['/smr/SmrTrnCustomerenquirySummary']);   
      this.NgxSpinnerService.hide()
    }
   
  });
}
}
  close() {

    this.route.navigate(['/smr/SmrTrnCustomerenquirySummary'])
  }
  openModaldelete(parameter: string){
    this.parameterValue = parameter
}

ondelete(){
  var url = 'SmrTrnCustomerEnquiry/GetDeleteQuoteProductSummary'
  this.NgxSpinnerService.show()
  let param = {
    tmpquotationdtl_gid : this.parameterValue 
  }
  this.service.getparams(url,param).subscribe((result: any) => {
    if(result.status ==false){
      this.ToastrService.warning(result.message)
      this.NgxSpinnerService.hide()
    }
    else{
      this.ToastrService.success(result.message) 
      this.NgxSpinnerService.hide()
    }
    this.GetTempSummary();
  });
}


// ENQUIRY TO QUOTATION EDIT PRODUCT
editsummary(tmpquotationdtl_gid: any) {
  var url = 'SmrTrnCustomerEnquiry/GetEnqtoQuoteEditProductSummary'
  this.NgxSpinnerService.show()
  let param = {
    tmpquotationdtl_gid: tmpquotationdtl_gid
  }
  this.service.getparams(url, param).subscribe((result: any) => {
    this.editenquirytoquote_list = result.editenquirytoquote_list;
    this.productform.get("tmpquotationdtl_gid")?.setValue(this.editenquirytoquote_list[0].tmpquotationdtl_gid);
    this.productform.get("product_name")?.setValue(this.editenquirytoquote_list[0].product_name);
    this.productform.get("product_gid")?.setValue(this.editenquirytoquote_list[0].product_gid);
    this.productform.get("product_code")?.setValue(this.editenquirytoquote_list[0].product_code);
    this.productform.get("productuom_name")?.setValue(this.editenquirytoquote_list[0].productuom_name);
    this.productform.get("unitprice")?.setValue(this.editenquirytoquote_list[0].unitprice);
    this.productform.get("quantity")?.setValue(this.editenquirytoquote_list[0].quantity);
    this.productform.get("discountpercentage")?.setValue(this.editenquirytoquote_list[0].discountpercentage);
    this.productform.get("discountamount")?.setValue(this.editenquirytoquote_list[0].discountamount);
    this.productform.get("tax_gid")?.setValue(this.editenquirytoquote_list[0].tax_gid);
    this.productform.get("tax_name")?.setValue(this.editenquirytoquote_list[0].tax_name);
    this.productform.get("tax_amount")?.setValue(this.editenquirytoquote_list[0].tax_amount);
    this.productform.get("totalamount")?.setValue(this.editenquirytoquote_list[0].totalamount); 
this.NgxSpinnerService.hide()
this.showUpdateButton = true;
      this.showAddButton = false;
  });
}

openModelDetail(product_gid:any){
  debugger
  var url='SmrTrnCustomerEnquiry/GetRaiseQuotedetail'
  let params={
    product_gid: product_gid
  }
  this.service.getparams(url,params).subscribe((result: any) => {
    this.responsedata = result;
    this.raisequotedetail_list = this.responsedata.Directeddetailslist;
  });

}

OnClearCurrency()
{
  this.txtExchangeRate='';
}
OnClearProduct()
{
  this.txtUnitPrice='';
  this.txtProductCode='';
  this.txtProductUnit='';
}

OnClearTax() {   
  this.tax_amount = 0; 
  const subtotal =  this.unitprice * this.quantity;
  this.discountamount = (subtotal * this.discountpercentage) / 100;
  this.totalamount = +(subtotal - this.discountamount + this.tax_amount).toFixed(2);
}
OnClearOverallTax() {
  this.tax_amount4=0;
 
  this.total_amount = +((+this.producttotalamount) + (+this.tax_amount4));  
  this.total_amount = +this.total_amount.toFixed(2);
  
  this.grandtotal = ((this.total_amount) + (+this.addoncharge) + (+this.freightcharges) + (+this.buyback_charges) + (+this.packing_charges) + (+this.insurance_charges) + (+this.roundoff) + (+this.totalamount)  - (+this.additional_discount));
  this.grandtotal =+this.grandtotal.toFixed(2);
}
// UPDATE API FOR PRODUCT SUMMARY

onupdate() {
  var params = {
    tmpquotationdtl_gid: this.productform.value.tmpquotationdtl_gid,
    product_code: this.productform.value.product_code,
    product_name: this.productform.value.product_name.product_name,
    productuom_name: this.productform.value.productuom_name,
    quantity: this.productform.value.quantity,
    unitprice: this.productform.value.unitprice,
    discountamount: this.productform.value.discountamount,
    discountpercentage: this.productform.value.discountpercentage,
    product_gid: this.productform.value.product_name.product_gid,
    tax_name: this.productform.value.tax_name,
    tax_gid: this.productform.value.tax_name.tax_gid,
    tax_amount: this.productform.value.tax_amount,
    totalamount: this.productform.value.totalamount
  }
  var url = 'SmrTrnCustomerEnquiry/PostUpdateEnquirytoQuotationProduct'
this.NgxSpinnerService.show()
  this.service.post(url,params).pipe().subscribe((result:any)=>{
    this.responsedata=result;
    if(result.status ==false){
      this.ToastrService.warning(result.message)       
      this.GetTempSummary();
      this.NgxSpinnerService.hide()
    }
    else{
      this.ToastrService.success(result.message)
      this.productform.reset();
      this.NgxSpinnerService.hide()
      
    }
    this.GetTempSummary();
  });
}
}