import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { HttpClient } from '@angular/common/http';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { NgxSpinnerService } from 'ngx-spinner';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';
interface SalesOD {
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
  selector: 'app-smr-trn-salesorderamend',
  templateUrl: './smr-trn-salesorderamend.component.html',
  styleUrls: ['./smr-trn-salesorderamend.component.scss']
})
export class SmrTrnSalesorderamendComponent {
  branch_name1: any;
  customer_mobile1: any;
  customer_name1: any;
  salesorder_gid: any;
  customercontact_names1: any;
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
  combinedFormData: FormGroup | any;
  productform: FormGroup | any;
  customer_list: any;
  currency_code1: any;
  amendsalesorderdtl: any[] = [];
  contact_list: any[] = [];
  branch_list: any[] = [];
  currency_list: any[] = [];
  currency_list1: any[] = [];
  user_list: any[] = [];
  product_list: any[] = [];
  tax_list: any[] = [];
  tax2_list: any[] = [];
  tax3_list: any[] = [];
  tax4_list: any[] = [];
  calci_list: any[] = [];
  POproductlist: any[] = [];
  terms_list: any[] = [];
  mdlBranchName: any;
  GetCustomerDet: any;
  mdlCustomerName: any;
  mdlUserName: any;
  mdlProductName: any;
  mdlCurrencyName: any;
  salesorder_date: any;
  mdlTaxName: any;
  GetproductsCode: any;
  mdlContactName: any;
  packing_charges: any;
  unitprice: number = 0;
  quantity: number = 0;
  discountpercentage: number = 0;
  discountamount: number = 0;
  totalamount: number = 0;
  addon_charge: number = 0;
  POdiscountamount: number = 0;
  freight_charges: number = 0;
  forwardingCharges: number = 0;
  insurance_charges: number = 0;
  roundoff: number = 0;
  grandtotal: number = 0;
  tax_amount: number = 0;
  buyback_charges: number = 0;
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
  mdlTerms: any;
  allchargeslist :any[]=[];
  additional_discount: number = 0;
  mdlproductName: any;
  responsedata: any;
  ExchangeRate: any;
  salesOD!: SalesOD;
  Productsummarys_list: any;
  salesorders_list: any;
  cuscontact_gid: any;
  mdlProductCode: any;
  mdlProductUom: any;
  mdlCurrencyRate: any;
  constructor(private http: HttpClient, private fb: FormBuilder, private router: ActivatedRoute,
    public NgxSpinnerService: NgxSpinnerService, private route: Router, private service: SocketService, private ToastrService: ToastrService) {
    this.salesOD = {} as SalesOD

    this.productform = new FormGroup({
      unit: new FormControl(''),
      tmpsalesorderdtl_gid: new FormControl(''),
      tax_gid: new FormControl(''),
      product_gid: new FormControl(''),
      productuom_gid: new FormControl(''),
      productgroup_gid: new FormControl(''),
      product_code: new FormControl('', Validators.required),
      productcode: new FormControl('', Validators.required),
      productgroup: new FormControl('', Validators.required),
      productuom: new FormControl('', Validators.required),
      productname: new FormControl('', Validators.required),
      tax_name: new FormControl('', Validators.required),
      tax_name2: new FormControl('', Validators.required),
      tax_name3: new FormControl('', Validators.required),
      remarks: new FormControl('', Validators.required),
      product_name: new FormControl('', Validators.required),
      productuom_name: new FormControl('', Validators.required),
      productgroup_name: new FormControl('', Validators.required),
      unitprice: new FormControl('', Validators.required),
      quantity: new FormControl('', Validators.required),
      discountpercentage: new FormControl('', Validators.required),
      discountamount: new FormControl('', Validators.required),
      taxname1: new FormControl('', Validators.required),
      tax_amount: new FormControl('', Validators.required),
      taxname2: new FormControl('', Validators.required),
      tax_amount2: new FormControl('', Validators.required),
      taxname3: new FormControl('', Validators.required),
      tax_amount3: new FormControl('', Validators.required),
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

    this.combinedFormData = new FormGroup({
      tmpsalesorderdtl_gid: new FormControl(''),
      salesorder_gid: new FormControl(''),
      salesorder_date: new FormControl(''),
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
      unit: new FormControl(''),
      tmpsalesorderdtl_gid: new FormControl(''),
      tax_gid: new FormControl(''),
      product_gid: new FormControl(''),
      productuom_gid: new FormControl(''),
      productgroup_gid: new FormControl(''),
      product_code: new FormControl('', Validators.required),
      productcode: new FormControl('', Validators.required),
      productgroup: new FormControl('', Validators.required),
      productuom: new FormControl('', Validators.required),
      productname: new FormControl('', Validators.required),
      tax_name: new FormControl('', Validators.required),
      tax_name2: new FormControl('', Validators.required),
      tax_name3: new FormControl('', Validators.required),
      remarks: new FormControl('', Validators.required),
      product_name: new FormControl('', Validators.required),
      productuom_name: new FormControl('', Validators.required),
      productgroup_name: new FormControl('', Validators.required),
      unitprice: new FormControl('', Validators.required),
      quantity: new FormControl('', Validators.required),
      discountpercentage: new FormControl('', Validators.required),
      discountamount: new FormControl('', Validators.required),
      taxname1: new FormControl('', Validators.required),
      tax_amount: new FormControl('', Validators.required),
      taxname2: new FormControl('', Validators.required),
      tax_amount2: new FormControl('', Validators.required),
      taxname3: new FormControl('', Validators.required),
      tax_amount3: new FormControl('', Validators.required),
      totalamount: new FormControl('', Validators.required),
      customerproduct_code: new FormControl(''),
      selling_price: new FormControl(''),
      product_requireddate: new FormControl(''),
      product_requireddateremarks: new FormControl(''),
    });




    const salesorder_gid = this.router.snapshot.paramMap.get('salesorder_gid');

    this.salesorder_gid = salesorder_gid;

    const secretKey = 'storyboarderp';

    const deencryptedParam = AES.decrypt(this.salesorder_gid, secretKey).toString(enc.Utf8);
    this.salesorder_gid = deencryptedParam;
    console.log(deencryptedParam)




    //// Sales person Dropdown ////
    var url = 'SmrTrnSalesorder/GetPersonDtl'
    this.service.get(url).subscribe((result: any) => {
      this.contact_list = result.GetPersonDtl;
    });

    //// Currency Dropdown ////
    var url = 'SmrTrnSalesorder/GetCurrencyDtl'
    this.service.get(url).subscribe((result: any) => {
      this.currency_list = result.GetCurrencyDtl;
    });

    //// Tax 1 Dropdown ////
    var url = 'SmrTrnSalesorder/GetTax1Dtl'
    this.service.get(url).subscribe((result: any) => {
      this.tax_list = result.GetTax1Dtl;
    });

    //// Tax 2 Dropdown ////
    var url = 'SmrTrnSalesorder/GetTax2Dtl'
    this.service.get(url).subscribe((result: any) => {
      this.tax2_list = result.GetTax2Dtl;
    });

    //// Tax 3 Dropdown ////
    var url = 'SmrTrnSalesorder/GetTax3Dtl'
    this.service.get(url).subscribe((result: any) => {
      this.tax3_list = result.GetTax3Dtl;
    });

    //// Tax 3 Dropdown ////
    var url = 'SmrTrnSalesorder/GetTax4Dtl'
    this.service.get(url).subscribe((result: any) => {
      this.tax4_list = result.GetTax4Dtl;
    });

    //// Product Dropdown ////
    var url = 'SmrTrnSalesorder/GetProductNamDtl'
    this.service.get(url).subscribe((result: any) => {
      this.product_list = result.GetProductNamDtl;
    });

    // Termd and Conditions //
    //// T & C Dropdown ////
    var url = 'SmrTrnQuotation/GetTermsandConditions'
    this.service.get(url).subscribe((result: any) => {
      this.terms_list = result.GetTermsandConditions;
    });
    this.GetEditEmployeetosalarygrade(deencryptedParam);

    var api = 'SmrMstSalesConfig/GetAllChargesConfig';
     this.service.get(api).subscribe((result: any) => {
       this.responsedata = result;
       this.allchargeslist = this.responsedata.salesconfigalllist;
       this.addon_charge = this.allchargeslist[0].flag;
       this.additional_discount = this.allchargeslist[1].flag;
       this.freight_charges = this.allchargeslist[2].flag;
       this.buyback_charges = this.allchargeslist[3].flag;
       this.insurance_charges = this.allchargeslist[4].flag;
       this.roundoff = this.allchargeslist[5].flag;
 
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
  GetEditEmployeetosalarygrade1(params: any) {
    debugger;
    var url = 'SmrTrnSalesorder/Getamendsalesorderdetails'
    let param = {
      salesorder_gid: params
    }
    this.NgxSpinnerService.show();
    this.service.getparams(url, param).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      this.responsedata = result;
      this.salesorders_list = result.summarydtl_list;
      let n = this.salesorders_list.length;
      this.combinedFormData.get("producttotalamount")?.setValue(this.salesorders_list[n - 1].grand_total);
    });

  }
  GetEditEmployeetosalarygrade(params: any) {
    var url = 'SmrTrnSalesorder/Getamendsalesorderdtl'
    let param1 = {
      salesorder_gid: params
    }
    this.NgxSpinnerService.show();
    this.service.getparams(url, param1).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      this.responsedata = result;
      this.amendsalesorderdtl = result.Getamendsalesorderdtl;
      this.combinedFormData.get("salesorder_gid")?.setValue(this.amendsalesorderdtl[0].salesorder_gid);
      this.combinedFormData.get("salesorder_date")?.setValue(this.amendsalesorderdtl[0].salesorder_date);
      this.combinedFormData.get("branch_name")?.setValue(this.amendsalesorderdtl[0].branch_name);
      this.combinedFormData.get("so_referencenumber")?.setValue(this.amendsalesorderdtl[0].salesorder_refno);
      this.combinedFormData.get("customer_name")?.setValue(this.amendsalesorderdtl[0].customer_name);
      this.combinedFormData.get("customercontact_names")?.setValue(this.amendsalesorderdtl[0].customer_contact_person);
      this.combinedFormData.get("customer_address")?.setValue(this.amendsalesorderdtl[0].customer_address);
      this.combinedFormData.get("freight_terms")?.setValue(this.amendsalesorderdtl[0].freight_terms);
      this.combinedFormData.get("payment_terms")?.setValue(this.amendsalesorderdtl[0].payment_terms);

      this.combinedFormData.get("exchange_rate")?.setValue(this.amendsalesorderdtl[0].exchange_rate);
      this.combinedFormData.get("shipping_to")?.setValue(this.amendsalesorderdtl[0].shipping_to);
      this.combinedFormData.get("payment_days")?.setValue(this.amendsalesorderdtl[0].payment_days);
      this.combinedFormData.get("delivery_days")?.setValue(this.amendsalesorderdtl[0].delivery_days);

      this.combinedFormData.get("customer_mobile")?.setValue(this.amendsalesorderdtl[0].customer_mobile);
      this.combinedFormData.get("freight_charges")?.setValue(this.amendsalesorderdtl[0].freight_charges);
      this.combinedFormData.get("buyback_charges")?.setValue(this.amendsalesorderdtl[0].buyback_charges);
      this.combinedFormData.get("packing_charges")?.setValue(this.amendsalesorderdtl[0].packing_charges);

      this.combinedFormData.get("insurance_charges")?.setValue(this.amendsalesorderdtl[0].insurance_charges);

      this.combinedFormData.get("customer_email")?.setValue(this.amendsalesorderdtl[0].customer_email);
      this.combinedFormData.get("so_remarks")?.setValue(this.amendsalesorderdtl[0].so_remarks);
      this.combinedFormData.get("customer_gid")?.setValue(this.amendsalesorderdtl[0].customer_gid);


      this.combinedFormData.get("addon_charge")?.setValue(this.amendsalesorderdtl[0].addon_charge);
      this.combinedFormData.get("additional_discount")?.setValue(this.amendsalesorderdtl[0].additional_discount);
      this.mdlUserName = this.amendsalesorderdtl[0].salesperson_gid;
      this.mdlCurrencyName = this.amendsalesorderdtl[0].currency_gid;
      this.combinedFormData.get("currency_gid")?.setValue(this.amendsalesorderdtl[0].currency_gid);
      this.currency_code1 = this.amendsalesorderdtl[0].currency_code;
      //date format binding

      const salesOrderDate = new Date(this.amendsalesorderdtl[0].salesorder_date);
      const formattedDate = salesOrderDate.toISOString().substring(0, 10);
      this.combinedFormData.get("salesorder_date")?.setValue(formattedDate);

      const Startdate = new Date(this.amendsalesorderdtl[0].start_date);
      const formattedDate1 = Startdate.toISOString().substring(0, 10);
      this.combinedFormData.get("start_date")?.setValue(formattedDate1);

      const Enddate = new Date(this.amendsalesorderdtl[0].end_date);
      const formattedDate2 = Enddate.toISOString().substring(0, 10);
      this.combinedFormData.get("end_date")?.setValue(formattedDate2);

      this.finaltotal()
      this.GetEditEmployeetosalarygrade1(this.salesorder_gid)

    });
  }

  get branch_name() {
    return this.combinedFormData.get('branch_name')!;
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
  get tax_name() {
    return this.productform.get('tax_name')!;
  }
  get tax_name2() {
    return this.productform.get('tax_name2')!;
  }
  get tax_name3() {
    return this.productform.get('tax_name3')!;
  }


  OnChangeCurrency() {
    debugger
    let currencyexchange_gid = this.combinedFormData.get("currency_code")?.value;
    console.log(currencyexchange_gid)
    let param = {
      currencyexchange_gid: currencyexchange_gid
    }
    var url = 'SmrTrnSalesorder/GetOnChangeCurrency';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.currency_list1 = this.responsedata.GetOnchangeCurrency;
      this.combinedFormData.get("exchange_rate")?.setValue(this.currency_list1[0].exchange_rate);
      this.currency_code1 = this.currency_list1[0].currency_code
    });
  }

  onCurrencyCodeChange(event: Event) {

  }

  GetOnChangeProductsNameAmend() {
    debugger;
    let product_gid = this.productform.value.product_name.product_gid;
    let param = {
      product_gid: product_gid
    }
    var url = 'SmrTrnSalesorder/GetOnChangeProductsNameAmend';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.GetproductsCode = this.responsedata.ProductsCodes;
      this.productform.get("product_code")?.setValue(result.ProductsCodes[0].product_code);
      this.productform.get("unit")?.setValue(result.ProductsCodes[0].productuom_name);
      this.productform.get("productgroup_name")?.setValue(result.ProductsCodes[0].productgroup_name);
      this.productform.value.productgroup_gid = result.ProductsCodes[0].productgroup_gid
      // this.productform.value.productuom_gid = result.GetProductsCode[0].productuom_gid
    });


  }

  productAdd() {
    debugger;
    console.log(this.productform.value)
    var params = {
      salesorder_gid: this.salesorder_gid,
      productgroup_name: this.productform.value.productgroup_name,
      customerproduct_code: this.productform.value.customerproduct_code,
      product_code: this.productform.value.product_code,
      product_name: this.productform.value.product_name.product_name,
      productuom_name: this.productform.value.unit,
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
      totalamount: this.productform.value.totalamount,
      producttotalamount: this.productform.value.producttotalamount,
      shipping_to: this.productform.value.shipping_to,
    }
    console.log(params)
    var api = 'SmrTrnSalesorder/PostOnAdds';
    this.NgxSpinnerService.show();
    this.service.postparams(api, params).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      this.productform.reset();
      debugger;
      this.GetEditEmployeetosalarygrade1(this.salesorder_gid)
    },
    );
  }


  GetOnChangeTerms() {
    debugger

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

  overallsubmit() {
    debugger
    console.log(this.combinedFormData.value)
    var params = {
      salesorder_gid: this.combinedFormData.value.salesorder_gid,
      salesorder_date: this.combinedFormData.value.salesorder_date,
      customer_branch: this.combinedFormData.value.branch_name,
      branch_gid: this.combinedFormData.value.branch_gid,
      so_referencenumber: this.combinedFormData.value.so_referencenumber,
      customer_name: this.combinedFormData.value.customer_name,
      customer_contact_person: this.combinedFormData.value.customercontact_names,
      customer_email: this.combinedFormData.value.customer_email,
      customer_mobile: this.combinedFormData.value.customer_mobile,
      so_remarks: this.combinedFormData.value.so_remarks,
      customer_address: this.combinedFormData.value.customer_address,
      freight_terms: this.combinedFormData.value.freight_terms,
      payment_terms: this.combinedFormData.value.payment_terms,
      currency_code: this.combinedFormData.value.currency_code,
      user_name: this.combinedFormData.value.user_name,
      exchange_rate: this.combinedFormData.value.exchange_rate,
      payment_days: this.combinedFormData.value.payment_days,
      customer_gid: this.combinedFormData.value.customer_gid,
      termsandconditions: this.combinedFormData.value.termsandconditions,
      template_name: this.combinedFormData.value.template_name,
      template_gid: this.combinedFormData.value.template_gid,
      Grandtotal: this.combinedFormData.value.grandtotal,
      roundoff: this.combinedFormData.value.roundoff,
      start_date: this.combinedFormData.value.start_date,
      end_date: this.combinedFormData.value.end_date,
      insurance_charges: this.combinedFormData.value.insurance_charges,
      packing_charges: this.combinedFormData.value.packing_charges,
      buyback_charges: this.combinedFormData.value.buyback_charges,
      freight_charges: this.combinedFormData.value.freight_charges,
      additional_discount: this.combinedFormData.value.additional_discount,
      addon_charge: this.combinedFormData.value.addon_charge,
      total_amount: this.combinedFormData.value.producttotalamount,
      producttotalamount: this.combinedFormData.value.producttotalamount,
      customercontact_gid: this.cuscontact_gid,
      delivery_days: this.combinedFormData.value.delivery_days,
      total_price: this.combinedFormData.value.producttotalamount,
      currency_gid: this.combinedFormData.value.currency_gid
    }
    var url = 'SmrTrnSalesorder/AmendSalesOrder'
    this.NgxSpinnerService.show();
    this.service.postparams(url, params).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.ToastrService.success(result.message)
        this.route.navigate(['/smr/SmrTrnSalesorderSummary']);
      }
    });
  }
  getDimensionsByFilter(id: any) {
    return this.tax_list.filter((x: { tax_gid: any; }) => x.tax_gid === id);
  }

  prodtotalcal() {

    // Assuming this.unitprice is 13,000 as a number
    const unitpriceWithoutCommas = this.unitprice.toString().replace(/,/g, '');
    const subtotal = Number(unitpriceWithoutCommas) * Number(this.quantity);
    this.discountamount = (subtotal * Number(this.discountpercentage)) / 100;
    this.totalamount = subtotal - this.discountamount;



    this.productform.get("totalamount")?.setValue(this.totalamount);

  }

  OnChangeTaxAmount1() {
    debugger;
    let tax_gid = this.productform.get('tax_name')?.value;
    const unitpriceWithoutCommas = this.unitprice.toString().replace(/,/g, '');
    const quantityCommas = this.quantity.toString().replace(/,/g, '');
    const totalamountCommas = this.totalamount.toString().replace(/,/g, '');

    this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    console.log(this.taxpercentage);
    let tax_percentage = this.taxpercentage[0].percentage;
    console.group(tax_percentage)
    const subtotal = Number(unitpriceWithoutCommas) * Number(quantityCommas);
    this.discountamount = (Number(subtotal)) * Number((this.discountpercentage)) / 100;
    this.totalamount = subtotal - this.discountamount;
    this.tax_amount = (Number(tax_percentage)) * this.totalamount / 100;
    this.tax_amount = Number(this.tax_amount.toFixed(2));

    this.totalamount = Number(this.totalamount.toFixed(2)) + this.tax_amount;
  }
  finaltotal() {

    const addoncharges = isNaN(this.addon_charge) ? 0 : +this.addon_charge;
    const frieghtcharges = isNaN(this.freight_charges) ? 0 : +this.freight_charges;
    const forwardingCharges = isNaN(this.buyback_charges) ? 0 : +this.buyback_charges;
    const insurancecharges = isNaN(this.insurance_charges) ? 0 : +this.insurance_charges;
    const packing_charges = isNaN(this.packing_charges) ? 0 : +this.packing_charges;
    const roundoff = isNaN(this.roundoff) ? 0 : +this.roundoff;
    const discountamount = isNaN(this.additional_discount) ? 0 : +this.additional_discount;

    this.grandtotal = ((this.producttotalamount) + (addoncharges) + (frieghtcharges) + (forwardingCharges) + (insurancecharges) + (roundoff) - (discountamount)+(packing_charges));

    //this.combinedFormData.get("grandtotal")?.setValue(this.grandtotal);
  }
  onKeyPress(event: any) {
    // Get the pressed key
    const key = event.key;

    if (!/^[0-9.]$/.test(key)) {
      // If not a number or dot, prevent the default action (key input)
      event.preventDefault();
    }
  }

  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  }

  ondelete() {
    var url = 'SmrTrnSalesorder/GetDeleteDirectSOProductSummary'
    let param = {
      tmpsalesorderdtl_gid: this.parameterValue
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {

        this.ToastrService.success(result.message)
        this.GetEditEmployeetosalarygrade1(this.salesorder_gid)
      }
    });
  }

  close() {
    this.route.navigate(['/smr/SmrTrnSalesorderSummary']);
  }
  edit(data: any) {
    debugger
    this.mdlproductName = data.product_name;
    this.mdlTaxName = data.tax_gid;
    this.mdlTaxName = data.tax_name;
    this.productform.get("tax_name")?.setValue(data.tax_name);
    this.productform.get("tax_gid")?.setValue(data.tax_gid);
    this.productform.get("product_name")?.setValue(data.product_name);


    this.productform.get("tmpsalesorderdtl_gid")?.setValue(data.tmpsalesorderdtl_gid);
    this.productform.get("product_code")?.setValue(data.product_code);
    this.productform.get("unit")?.setValue(data.uom_name);
    this.productform.get("tax_amount")?.setValue(data.tax_amount);
    this.productform.get("unitprice")?.setValue(data.product_price);
    this.productform.get("quantity")?.setValue(data.qty_quoted);
    this.productform.get("discountpercentage")?.setValue(data.discount_percentage);
    this.productform.get("discountamount")?.setValue(data.discount_amount);
    this.productform.get("totalamount")?.setValue(data.totalamount);
    this.productform.get("productgroup_name")?.setValue(data.productgroup_name);
  }
  productUpdate() {

    debugger
    var params = {
      tmpsalesorderdtl_gid: this.productform.value.tmpsalesorderdtl_gid,
      tax_name: this.productform.value.tax_name,
      tax_gid: this.productform.value.tax_gid,
      tax_amount: this.productform.value.tax_amount,
      total_amount: this.productform.value.totalamount,
      productgroup_name: this.productform.value.productgroup_name,
      discountamount: this.productform.value.discountamount,
      discountpercentage: this.productform.value.discountpercentage,
      quantity: this.productform.value.quantity,
      unitprice: this.productform.value.unitprice,
      unit: this.productform.value.unit,
      product_name: this.productform.value.product_name.product_name,
      product_code: this.productform.value.product_code
    }

    var api = 'SmrTrnSalesorder/updateSalesOrderedit'
    this.NgxSpinnerService.show();
    console.log(params);
    this.service.postparams(api, params).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      this.GetEditEmployeetosalarygrade1(this.salesorder_gid)
      this.productform.reset();
    });
  }

  OnClearTax(){
    this.tax_amount = 0; 
      const subtotal = this.unitprice * this.quantity;
      this.discountamount = (subtotal * this.discountpercentage) / 100;
      this.totalamount = +(subtotal - this.discountamount + this.tax_amount).toFixed(2);
  }
  onClearProduct(){
    this.mdlProductCode='';
    this.mdlProductUom='';
  }
  onClearCurrency(){
    this.mdlCurrencyRate=''
  }
}

