import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-pmr-trn-directpo-add',
  templateUrl: './pmr-trn-directpo-add.component.html',
  styleUrls: ['./pmr-trn-directpo-add.component.scss']
})

export class PmrTrnDirectpoAddComponent {
  currency_code1: any
  showInput: boolean = false;
  inputValue: string = ''
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '28rem',
    minHeight: '0rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
  };

  selectedValue: string = '';
  POAddForm: FormGroup | any;
  product_list: any;
  branch_list: any;
  vendor_list: any;
  dispatch_list: any;
  currency_list: any;
  currency_list1: any;
  netamount:any;
  overall_tax:any;
  tax_list: any;
  tax4_list: any;
  payment_days: number = 0;
  delivery_days : number = 0;
  productcode_list: any;
  productgroup_list: any;
  terms_list: any[] = [];
  productform: FormGroup | any;
  responsedata: any;
  productunit_list: any;
  mdlProductName: any;
  mdlTerms :any;
  mdlProductGroup: any;
  mdlProductUnit: any;
  mdlProductCode: any;
  mdlBranchName: any;
  mdlVendorName: any;
  mdlDispatchName: any;
  mdlCurrencyName: any;
  mdlTaxName1: any;
  mdlTaxName2: any;
  mdlTaxName3: any;
  unitprice: number = 0;
  quantity: number = 0;
  discountpercentage: number = 0;
  discountamount:any;
  totalamount: number = 0;
  addoncharge: number = 0;
  POdiscountamount: number = 0;
  frieghtcharges: number = 0;
  forwardingCharges: number = 0;
  insurancecharges: number = 0;
  roundoff: number = 0;
  grandtotal: number = 0;
  taxamount1: number = 0;
  taxamount : number =0;
  taxpercentage: any;
  productdetails_list: any;
  taxamount2: number = 0;
  taxamount3: number = 0;
  producttotalamount: any;
  parameterValue: string | undefined;
  POproductlist: any;
  productnamelist: any;
  selectedCurrencyCode: any;
  POadd_list: any;
  total_amount: any;
  insurance_charges: number = 0;
  additional_discount: any;
  freightcharges: any;
  packing_charges: number =0;
  buybackorscrap: any;
  tax_amount4:number=0;
  mdlTaxName4:any;
  exchange: any;
  mdlProductcode:any;
  mdlProductunit:any;
  mdlvendoraddress:any;
  mdlemailaddress:any;
  mdlcontactnumber:any;
  mdlcontactperson:any;
  mdlvendorfax:any;
  GetVendord:any;
  // totalamount: number = 0;
  // addoncharges: number = 0;
  invoicediscountamount: number = 0;
  allchargeslist: any[] = [];
  // frieghtcharges: number = 0;
  // forwardingCharges: number = 0;
  // insurancecharges: number = 0;
  // roundoff: number = 0;
  // grandtotal: number = 0;



  ngOnInit(): void {
    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    flatpickr('.date-picker', options);

    this.POproductsummary();
    var api = 'PmrTrnPurchaseOrder/GetBranch';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.branch_list = this.responsedata.GetBranch;

    });
    var api = 'PmrTrnPurchaseOrder/GetVendor';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.vendor_list = this.responsedata.GetVendor;

    });
    var api = 'PmrTrnPurchaseOrder/GetDispatchToBranch';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.dispatch_list = this.responsedata.GetDispatchToBranch;

    });

    // var api = 'PmrTrnPurchaseOrder/GetCurrency';
    // this.service.get(api).subscribe((result: any) => {
    //   this.responsedata = result;
    //   this.currency_list = this.responsedata.GetCurrency;

    // });
    
    var url = 'PmrTrnPurchaseOrder/GetCurrency';
    this.service.get(url).subscribe((result:any)=>{
      this.currency_list = result.GetCurrency;  
    this.mdlCurrencyName = this.currency_list[0].currencyexchange_gid;    
    const defaultCurrency = this.currency_list.find((currency: { default_currency: string; }) => currency.default_currency === 'Y');
    const defaultCurrencyExchangeRate = defaultCurrency.exchange_rate;   
      if (defaultCurrency) {
        this.mdlCurrencyName = defaultCurrency.currencyexchange_gid;
        this.POAddForm.get("exchange_rate")?.setValue(defaultCurrencyExchangeRate);
        
      } 
     
   });
    var api = 'PmrTrnPurchaseOrder/GetTax';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.tax_list = result.GetTax;

    });
    var url = 'PmrTrnPurchaseOrder/GetTax4Dtl'
    this.service.get(url).subscribe((result:any)=>{
      this.tax4_list = result.GetTax4Dtl;
     });
    var api = 'PmrTrnPurchaseOrder/GetProductCode';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productcode_list = this.responsedata.GetProductCode;

    });
    var api = 'PmrMstProduct/GetProductUnit';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productunit_list = this.responsedata.GetProductUnit;

    });
    var api = 'PmrTrnPurchaseQuotation/GetTermsandConditions';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.terms_list = this.responsedata.GetTermsandConditions
    });
    var api = 'PmrMstProduct/GetProductGroup';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productgroup_list = this.responsedata.GetProductGroup;
      setTimeout(() => {

        $('#productgroup_list').DataTable();

      }, 0.1);
    });
    var api = 'PmrTrnPurchaseOrder/GetProduct';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productdetails_list = this.responsedata.GetProduct;

      setTimeout(() => {

        $('#product_list').DataTable();

      }, 0.1);
    });
    this.POproductsummary();
    debugger
    var api = 'PmrMstPurchaseConfig/GetAllChargesConfig';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.allchargeslist = this.responsedata.salesconfigalllist;
      this.addoncharge = this.allchargeslist[0].flag;
      this.invoicediscountamount = this.allchargeslist[1].flag;
      this.frieghtcharges = this.allchargeslist[2].flag;
      this.forwardingCharges = this.allchargeslist[3].flag;
      this.insurancecharges = this.allchargeslist[4].flag;

      if (this.allchargeslist[0].flag == 'Y') {
        this.addoncharge = 0;
      } else {
        this.addoncharge = this.allchargeslist[0].flag;
      }

      if (this.allchargeslist[1].flag == 'Y') {
        this.invoicediscountamount = 0;
      } else {
        this.invoicediscountamount = this.allchargeslist[1].flag;
      }

      if (this.allchargeslist[2].flag == 'Y') {
        this.frieghtcharges = 0;
      } else {
        this.frieghtcharges = this.allchargeslist[2].flag;
      }

      if (this.allchargeslist[3].flag == 'Y') {
        this.forwardingCharges = 0;
      } else {
        this.forwardingCharges = this.allchargeslist[3].flag;
      }

      if (this.allchargeslist[4].flag == 'Y') {
        this.insurancecharges = 0;
      } else {
        this.insurancecharges = this.allchargeslist[4].flag;
      }
    });
  }
  

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService,public NgxSpinnerService:NgxSpinnerService) {

    this.POAddForm = new FormGroup({
      branch: new FormControl('', Validators.required),
      branch_name: new FormControl('', Validators.required),
      dispatch_name: new FormControl('', Validators.required),
      po_date: new FormControl(this.getCurrentDate(), Validators.required),
      vendor_companyname: new FormControl(''),
      tax_amount4: new FormControl(''),
      currency_code: new FormControl(''),
      payment_term: new FormControl(''),
      contact_person: new FormControl(''),
      email_address: new FormControl(''),
      contact_number: new FormControl(''),
      currency: new FormControl('', Validators.required),
      exchange_rate: new FormControl(''),
      remarks: new FormControl(''),
      Shipping_address: new FormControl(''),
      vendor_address: new FormControl(''),
      vendor_fax: new FormControl(''),
      priority_n: new FormControl('N'),
      taxamount1: new FormControl(''),
      buybackorscrap: new FormControl(''),
      payment_terms: new FormControl(''),
      freight_terms: new FormControl(''),
      delivery_location: new FormControl(''),
      template_content: new FormControl(''),
      delivery_period: new FormControl(''),
      payment_days: new FormControl(''),
      product_total: new FormControl(''),
      tax_name: new FormControl(''),
      discount_percentage: new FormControl(''),
      qty: new FormControl(''),
      mrp: new FormControl(''),
      unitprice: new FormControl(''),
      productuom_name: new FormControl(''),
      product_code: new FormControl(''),
      productgroup_name: new FormControl(''),
      product_name: new FormControl(''),
      totalamount: new FormControl(''),
      totalamount3: new FormControl(''),
      tax_name3: new FormControl(''),
      taxamount2: new FormControl(''),
      tax_name2: new FormControl(''),
      tax_name1: new FormControl(''),
      discountamount: new FormControl(''),
      discountpercentage: new FormControl(''),
      quantity: new FormControl(''),
      productcode: new FormControl(''),
      productgroup: new FormControl(''),
      priority_remarks: new FormControl(''),
      pocovernote_address: new FormControl(''),
      roundoff: new FormControl(''),
      ship_via: new FormControl(''),
      po_no: new FormControl(''),
      grandtotal: new FormControl('',[Validators.required]),
      additional_discount: new FormControl(''),
      insurance_charges: new FormControl(''),
      freightcharges: new FormControl(''),
      addoncharge: new FormControl(''),
      delivery_days: new FormControl(''),
      template_name :new FormControl(''),
      total_amount: new FormControl(''),
      packing_charges: new FormControl(''),
      tax_name4: new FormControl(''),


    })

    this.productform = new FormGroup({
      tmppurchaseorderdtl_gid: new FormControl(''),
      product_gid: new FormControl(''),
      productuom_gid: new FormControl(''),
      productgroup_gid: new FormControl(''),
      product_code: new FormControl(''),
      productcode: new FormControl(''),
      productgroup: new FormControl(''),
      productuom_name: new FormControl(''),
      productname: new FormControl(''),
      tax_name1: new FormControl(''),
      tax_name2: new FormControl(''),
      tax_name3: new FormControl(''),
      remarks: new FormControl(''),
      product_name: new FormControl(''),
      productgroup_name: new FormControl(''),
      unitprice: new FormControl(''),
      quantity: new FormControl(''),
      discountpercentage: new FormControl(''),
      discountamount: new FormControl(''),
      taxname1: new FormControl(''),
      taxamount1: new FormControl(''),
      taxname2: new FormControl(''),
      taxamount2: new FormControl(''),
      taxname3: new FormControl(''),
      taxamount3: new FormControl(''),
      totalamount: new FormControl(''),
      tax_name: new FormControl(''),
      total_amount: new FormControl(''),
      packing_charges: new FormControl(''),
      netamount: new FormControl(''),
      overall_tax: new FormControl(''),
      template_content :new FormControl(''),





    })
  }
  getCurrentDate(): string {
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0'); // January is 0!
    const yyyy = today.getFullYear();
   
    return dd + '-' + mm + '-' + yyyy;
  }

  redirecttolist() {
    this.router.navigate(['/pmr/PmrTrnPurchaseorderSummary']);

  }
  get Shipping_address() {
    return this.POAddForm.get('Shipping_address')!;
  }
  get vendor_address() {
    return this.POAddForm.get('vendor_address')!;
  }
  get contact_person() {
    return this.POAddForm.get('contact_person')!;
  }
  get contact_number() {
    return this.POAddForm.get('contact_number')!;
  }
  get vendor_fax() {
    return this.POAddForm.get('vendor_fax')!;
  }
  get email_address() {
    return this.POAddForm.get('email_address')!;
  }
  get product_name() {
    return this.productform.get('product_name')!;
  }
  get product_code() {
    return this.productform.get('product_code')!;
  }
  get branch_name() {
    return this.productform.get('branch_name')!;
  }
  get tax_name1() {
    return this.productform.get('tax_name1')!;
  }
  get productuom_name() {
    return this.productform.get('productuom_name')!;
  }
  get productgroup_name() {
    return this.productform.get('productgroup_name')!;
  }
  get prodnameControl() {
    return this.productform.get('productgid');
  }
  get priority_remarks() {
    return this.productform.get('priority_remarks')
  }
  get tax() {
    return this.productform.get('tax')!;
  }
  OnChangeBranch() {
    let branch_gid = this.POAddForm.get("branch_name")?.value;

    let param = {
      branch_gid: branch_gid
    }
    var url = 'PmrTrnPurchaseOrder/GetOnChangeBranch';
    this.service.getparams(url, param).subscribe((result: any) => {

      this.POAddForm.get("Shipping_address")?.setValue(result.GetBranch[0].address1);
      console.log(result.GetBranch[0].address1)
    });

  }
  OnChangeVendor() {
    debugger
    let vendorregister_gid = this.POAddForm.get("vendor_companyname")?.value;

    // let param = {
    //   vendorregister_gid: vendorregister_gid
      
    // }
    // var url = 'PmrTrnPurchaseOrder/GetOnChangeVendor';
    // this.service.getparams(url, param).subscribe((result: any) => {
    //   this.POAddForm.get("vendor_address")?.setValue(result.GetVendor[0].address1);
    //   this.POAddForm.get("contact_number")?.setValue(result.GetVendor[0].contact_telephonenumber);
    //   this.POAddForm.get("contact_person")?.setValue(result.GetVendor[0].contactperson_name);
    //   this.POAddForm.get("vendor_fax")?.setValue(result.GetVendor[0].fax);
    //   this.POAddForm.get("email_address")?.setValue(result.GetVendor[0].email_id);
    // });

    let param ={
      vendorregister_gid :vendorregister_gid
    }
    
    var url = 'PmrTrnPurchaseQuotation/GetOnChangeVendor';
    
      this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.GetVendord = result.GetVendordtl;
      this.POAddForm.get("contact_number")?.setValue(result.GetVendordtl[0].contact_telephonenumber);
      this.POAddForm.get("contact_person")?.setValue(result.GetVendordtl[0].contactperson_name);
      this.POAddForm.get("vendor_address")?.setValue(result.GetVendordtl[0].address1);
      this.POAddForm.get("email_address")?.setValue(result.GetVendordtl[0].email_id)
      this.POAddForm.value.vendorregister_gid = result.GetVendordtl[0].vendorregister_gid
      
      
      
    });
  

  }

  GetOnChangeProductName() {
    debugger;
    let product_gid = this.productform.value.productname.product_gid;
    let param = {
      product_gid: product_gid
    }
    var url = 'PmrTrnPurchaseOrder/GetOnChangeProductName';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.productform.get("productcode")?.setValue(result.GetProductCode[0].product_code);
      this.productform.get("productuom_name")?.setValue(result.GetProductCode[0].productuom_name);
      this.productform.get("productgroup")?.setValue(result.GetProductCode[0].productgroup_name);
      this.productform.get("unitprice")?.setValue(result.GetproductsCode[0].unitprice);
      this.productform.value.productgroup_gid = result.GetProductCode[0].productgroup_gid,
        this.productform.value.productuom_gid = result.GetProductCode[0].productuom_gid
    });

  }
  OnChangeProductGroup() {
    let product_gid = this.productform.get("product_gid")?.value;

    let param = {
      product_gid: product_gid
    }
  

  }
  GetOnChangeTerms() {
    debugger

    let termsconditions_gid = this.POAddForm.value.template_name;
    let param = {
      termsconditions_gid: termsconditions_gid
    }
    var url = 'PmrTrnPurchaseQuotation/GetOnChangeTerms';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.POAddForm.get("template_content")?.setValue(result.terms_list[0].termsandconditions);
      this.POAddForm.value.termsconditions_gid = result.terms_list[0].termsconditions_gid
      //this.cusraiseform.value.productuom_gid = result.GetProductsName[0].productuom_gid
    });

    }




  OnChangeProductName() {
    let product_gid = this.productform.value.productname.product_gid;

    let param = {
      product_gid: product_gid
    }
    var url = 'PmrTrnPurchaseOrder/GetOnChangeProductName';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      debugger
      console.log(this.productdetails_list[0].productgroup_name)
      this.productdetails_list = this.responsedata.Getproductonchangedetails;
      this.productform.get("productcode")?.setValue(this.productdetails_list[0].product_code);
      this.productform.get("mrp")?.setValue(this.productdetails_list[0].product_price);
    })
  }


  onInput(event: any) {
    const value = event.target.value;
    const parts = value.split('.');
    
    // Keep only the first two parts (before and after the decimal point)
    const integerPart = parts[0];
    let decimalPart = parts[1] || '';
  
    // Limit the decimal part to 2 digits
    decimalPart = decimalPart.slice(0, 2);
    
    // Update the input value
    event.target.value = `${integerPart}.${decimalPart}`;
    this.unitprice = event.target.value; // Update the model value if necessary
  }

  productSubmit() {
    console.log(this.productform.value)
    var params = {
      tmppurchaseorderdtl_gid: this.productform.value.tmppurchaseorderdtl_gid,
      productname: this.productform.value.productname.product_name,
      product_gid: this.productform.value.productname.product_gid,
      quantity: this.productform.value.quantity,
      mrp: this.productform.value.totalamount,
      tax_name1: this.productform.value.tax_name1,
      tax_name2: this.productform.value.tax_name2,
      tax_name3: this.productform.value.tax_name3,
      taxamount1: this.productform.value.taxamount1,
      taxamount2: this.productform.value.taxamount2,
      taxamount3: this.productform.value.taxamount3,
      discountpercentage: this.productform.value.discountpercentage,
      discountamount: this.productform.value.discountamount,
      unitprice: this.productform.value.unitprice,
      productgroup_gid: this.productform.value.productgroup_gid,
      productgroup: this.productform.value.productgroup,
      productcode: this.productform.value.productcode,
      productuom_gid: this.productform.value.productuom_gid,
      productuom_name: this.productform.value.productuom_name,
      totalamount: this.productform.value.totalamount,
    }
    console.log(params)
    var api = 'PmrTrnPurchaseOrder/GetOnAdd';
    this.NgxSpinnerService.show()
    this.service.post(api, params).subscribe((result: any) => {
    this.productform.reset();
    this.POproductsummary()
    this.NgxSpinnerService.hide()
    },
    );
  }

  prodtotalcal() {
    // const subtotal = this.unitprice * this.quantity;
    // this.discountamount = (subtotal * this.discountpercentage) / 100;
    // this.totalamount = subtotal - this.discountamount;
    const subtotal = this.exchange * this.unitprice * this.quantity;
      this.discountamount = (subtotal * this.discountpercentage) / 100;
      this.discountamount = +(this.discountamount).toFixed(2);
      this.totalamount = subtotal - this.discountamount;
      this.totalamount = +(subtotal - this.discountamount).toFixed(2);
     
      const value = this.producttotalamount.value;
      const formattedValue = parseFloat(value).toFixed(2);
      this.producttotalamount.setValue(formattedValue, { emitEvent: false });

  }
  OnClearproduct(){
    this.mdlProductName = null;
    this.mdlProductunit = null;
    this.mdlProductcode = null;

  }
  onclearvendor(){
    this.mdlcontactperson = null;
    this.mdlcontactnumber = null;
    this.mdlemailaddress = null;
    this.mdlvendoraddress = null;
    this.mdlvendorfax = null;

 
  }
  onclearcurrency(){
    this.exchange=null;
  }
  OnClearTax() {
     
    this.taxamount1 = 0; 
    const subtotal = this.exchange * this.unitprice * this.quantity;
    this.discountamount = (subtotal * this.discountpercentage) / 100;
    this.discountamount = +(this.discountamount).toFixed(2);
    this.totalamount = +(subtotal - this.discountamount + this.taxamount1).toFixed(2);
    this.totalamount = +(this.totalamount).toFixed(2);

  }

  OnChangeTaxAmount1() {
    debugger;
    let tax_gid = this.productform.get('tax_name1')?.value;

    this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    console.log(this.taxpercentage);
    let tax_percentage = this.taxpercentage[0].percentage;
    console.group(tax_percentage)

    this.taxamount1 = ((tax_percentage) * (this.totalamount) / 100);
    this.taxamount1 = +(this.taxamount1).toFixed(2);

    if (this.taxamount1 == undefined) {
      const subtotal = this.unitprice * this.quantity;
      this.discountamount = (subtotal * this.discountpercentage) / 100;
      this.totalamount = subtotal - this.discountamount;
      this.totalamount = +(this.totalamount).toFixed(2);
    }
    else {
      this.totalamount = ((this.totalamount) + (+this.taxamount1));
      this.totalamount = +(this.totalamount).toFixed(2);
    }
  }
  // OnChangeTaxAmount4() {
  //   debugger
  //   let tax_gid = this.POAddForm.get('tax_name4')?.value;
 
  //   this.taxpercentage = this.getDimensionsByFilter(tax_gid);
  //   console.log(this.taxpercentage);
  //   let tax_percentage = this.taxpercentage[0].percentage;
  //   console.group(tax_percentage);
 

  //   this.tax_amount4 = ((tax_percentage) * (this.netamount) / 100);
 
    
  // }
  OnChangeTaxAmount4() {
    debugger     
    
    let tax_gid = this.POAddForm.get('tax_name4')?.value;   
    this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    console.log(this.taxpercentage);
    let tax_percentage = this.taxpercentage[0].percentage;
    console.group(tax_percentage);
  
    this.tax_amount4 = +(tax_percentage * this.producttotalamount / 100).toFixed(2);
    this.total_amount = +((+this.producttotalamount) + (+this.tax_amount4));  
    this.total_amount = +this.total_amount.toFixed(2);
    this.grandtotal = ((this.total_amount) + (+this.tax_amount4) + (+this.addoncharge) + (+this.freightcharges) +  (+this.packing_charges) + (+this.insurance_charges) + (+this.roundoff) + (+this.totalamount)  - (+this.additional_discount));

    }
  OnClearOverallTax() {
    this.tax_amount4=0;
   
    this.total_amount = +((+this.producttotalamount) + (+this.tax_amount4));  
    this.total_amount = +this.total_amount.toFixed(2);
    
     this.grandtotal = ((this.total_amount) + (+this.addoncharge) + (+this.freightcharges) +  (+this.packing_charges) + (+this.insurance_charges) + (+this.roundoff) + (+this.totalamount)  - (+this.additional_discount));
    this.grandtotal =+this.grandtotal.toFixed(2);
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

    this.taxamount2 = ((tax_percentage) * (this.totalamount) / 100);

    if (this.taxamount1 == undefined && this.taxamount2 == undefined) {
      const subtotal = this.unitprice * this.quantity;
      this.discountamount = (subtotal * this.discountpercentage) / 100;
      this.totalamount = subtotal - this.discountamount;
    }
    else {
      this.totalamount = ((this.totalamount) + (+this.taxamount1) + (+this.taxamount2));
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

    this.taxamount3 = ((tax_percentage) * (this.totalamount) / 100);

    if (this.taxamount1 == undefined && this.taxamount2 == undefined && this.taxamount3 == undefined) {
      const subtotal = this.unitprice * this.quantity;
      this.discountamount = (subtotal * this.discountpercentage) / 100;
      this.totalamount = subtotal - this.discountamount;
    }
    else {
      this.totalamount = ((this.totalamount) + (+this.taxamount1) + (+this.taxamount2) + (+this.taxamount3));
    }
  }
  getDimensionsByFilter(id: any) {
    return this.tax_list.filter((x: { tax_gid: any; }) => x.tax_gid === id);
  }

  // finaltotal() {
  //   this.grandtotal = ((this.netamount) + (+this.addoncharge) + (+this.freightcharges) + (+this.buybackorscrap) + (+this.insurance_charges) + (+this.roundoff) - (+this.additional_discount)+(+this.tax_amount4));
  // }
  finaltotal() {
    const addoncharge = isNaN(this.addoncharge) ? 0 : +this.addoncharge;
    const frieghtcharges = isNaN(this.frieghtcharges) ? 0 : +this.frieghtcharges;
    const packing_charges = isNaN(this.packing_charges) ? 0 : +this.packing_charges;
    const insurance_charges = isNaN(this.insurance_charges) ? 0 : +this.insurance_charges;
    const roundoff = isNaN(this.roundoff) ? 0 : +this.roundoff;
    const additional_discount = isNaN(this.additional_discount) ? 0 : +this.additional_discount;
    const tax_amount4 = isNaN(this.tax_amount4) ? 0 : +this.tax_amount4;
    this.grandtotal = ((this.producttotalamount) + (tax_amount4) +(addoncharge) + (frieghtcharges) + (packing_charges) + (insurance_charges) + (roundoff) - (additional_discount));
    this.POAddForm.get("grandtotal")?.setValue(this.grandtotal);
    this.grandtotal = +(this.grandtotal).toFixed(2);
  }

  // openModaldelete() {

  // }
  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  }
  ondelete() {

    var url = 'PmrTrnPurchaseOrder/DeleteProductSummary'
    this.NgxSpinnerService.show()
    let param = {
      tmppurchaseorderdtl_gid: this.parameterValue
    }
    this.service.getparams(url, param).subscribe((result: any) => {

      if (result.status == false) {
        this.ToastrService.warning(result.message)
        window.location.reload()
        this.NgxSpinnerService.hide()
      }
      else {

        this.ToastrService.success(result.message)
        this.POproductsummary();
        this.NgxSpinnerService.hide()
      }
    });
  }
  POproductsummary() {
    var api = 'PmrTrnPurchaseOrder/GetProductSummary';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;

      this.POproductlist = this.responsedata.productsummary_list;
      this.POAddForm.get("totalamount")?.setValue(this.responsedata.grand_total);
      this.POAddForm.get("grandtotal")?.setValue(this.responsedata.grandtotal);

      this.currency_code1 = ""
    });
  }




  showTextBox(event: Event) {
    const target = event.target as HTMLInputElement;
    this.showInput = target.value === 'Y';
  }

  OnChangeCurrency() {
    debugger
    let currencyexchange_gid = this.POAddForm.get("currency_code")?.value;
    console.log(currencyexchange_gid)
    let param = {
      currencyexchange_gid: currencyexchange_gid
    }
    var url = 'PmrTrnPurchaseOrder/GetOnChangeCurrency';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.currency_list1 = this.responsedata.GetOnchangeCurrency;
      this.POAddForm.get("exchange_rate")?.setValue(this.currency_list1[0].exchange_rate);
      this.currency_code1 = this.currency_list1[0].currency_code


    });

  }
  onCurrencyCodeChange(event: Event) {
    debugger
    const target = event.target as HTMLSelectElement;
    const selectedCurrencyCode = target.value;

    this.selectedCurrencyCode = selectedCurrencyCode;
    this.POAddForm.controls.currency_code.setValue(selectedCurrencyCode);
    this.POAddForm.get("currency_code")?.setValue(this.currency_list[0].currency_code);

  }

  onSubmit() {
    var params = {
      po_no: this.POAddForm.value.po_no,
      template_name :this.POAddForm.value.template_name,
      po_date: this.POAddForm.value.po_date,
      branch_name: this.POAddForm.value.branch_name,
      vendor_companyname: this.POAddForm.value.vendor_companyname,
      dispatch_name: this.POAddForm.value.dispatch_name,
      contact_number: this.POAddForm.value.contact_number,
      contact_person: this.POAddForm.value.contact_person,
      vendor_fax: this.POAddForm.value.vendor_fax,
      email_address: this.POAddForm.value.email_address,
      vendor_address: this.POAddForm.value.vendor_address,
      exchange_rate: this.POAddForm.value.exchange_rate,
      ship_via: this.POAddForm.value.ship_via,
      payment_terms: this.POAddForm.value.payment_terms,
      freight_terms: this.POAddForm.value.freight_terms,
      delivery_location: this.POAddForm.value.delivery_location,
      currency_code: this.POAddForm.value.currency_code,
      currency_gid: this.POAddForm.value.currency_gid,
      Shipping_address: this.POAddForm.value.Shipping_address,
      pocovernote_address: this.POAddForm.value.pocovernote_address,
      priority_flag: this.POAddForm.value.priority_flag,
      priority_remarks: this.POAddForm.value.priority_remarks,
      remarks: this.POAddForm.value.remarks,
      additional_discount: this.POAddForm.value.additional_discount,
      totalamount: this.POAddForm.value.totalamount,
      addoncharge: this.POAddForm.value.addoncharge,
      freightcharges: this.POAddForm.value.freightcharges,
      buybackorscrap: this.POAddForm.value.buybackorscrap,
      packing_charges: this.POAddForm.value.packing_charges,
      insurance_charges: this.POAddForm.value.insurance_charges,
      roundoff: this.POAddForm.value.roundoff,
      grandtotal: this.POAddForm.value.grandtotal,
      payment_days: this.POAddForm.value.payment_days,
      delivery_days: this.POAddForm.value.delivery_days,
      template_content: this.POAddForm.value.template_content,
      template_gid: this.POAddForm.value.template_gid,
      priority_n: this.POAddForm.value.priority_n,      
      quantity: this.productform.value.quantity,
      mrp: this.productform.value.totalamount,
      tax_name1: this.productform.value.tax_name1,
      tax_name2: this.productform.value.tax_name2,
      tax_name3: this.productform.value.tax_name3,
      taxamount1: this.productform.value.taxamount1,
      taxamount2: this.productform.value.taxamount2,
      taxamount3: this.productform.value.taxamount3,
      discountpercentage: this.productform.value.discountpercentage,
      discountamount: this.productform.value.discountamount,
      unitprice: this.productform.value.unitprice,
      productgroup_gid: this.productform.value.productgroup_gid,
      productgroup: this.productform.value.productgroup,
      productcode: this.productform.value.productcode,
      productuom_gid: this.productform.value.productuom_gid,
      productuom_name: this.productform.value.productuom_name,

    }
    this.NgxSpinnerService.show();
    var api = 'PmrTrnPurchaseOrder/ProductSubmit';
    this.service.postparams(api, params).subscribe((result: any) => {
      if (result.status == false) {
        this.NgxSpinnerService.hide();
        this.ToastrService.warning(result.message)
      }
      else {
        this.NgxSpinnerService.hide();
        this.ToastrService.success(result.message)
        this.router.navigate(['/pmr/PmrTrnPurchaseorderSummary']);
      }
      this.NgxSpinnerService.hide();
    });

  }
  OnTax() {


  }

}

