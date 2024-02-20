import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';
import { AES, enc } from 'crypto-js';

@Component({
  selector: 'app-rbl-trn-bobateainvoiceadd',
  templateUrl: './rbl-trn-bobateainvoiceadd.component.html',
  styleUrls: ['./rbl-trn-bobateainvoiceadd.component.scss']
})

export class RblTrnBobateainvoiceaddComponent {
  invoiceform: FormGroup | any;
  invoiceproductlist: any;
  customer_gid: any;
  customernamelist: any;
  currency_list: any;
  productform: FormGroup | any;
  product_list: any;
  productgroup_list: any;
  productnamelist: any;
  productcodelist: any;
  productunitlist: any;

  unitprice: number = 0;
  quantity: number = 0;
  discountpercentage: number = 0;
  discountamount: number = 0;
  tax_list: any;
  currencylist: any;
  branchgstlist: any;
  totalamount: number = 0;
  addoncharges: number = 0;
  invoicediscountamount: number = 0;
  frieghtcharges: number = 0;
  forwardingCharges: number = 0;
  insurancecharges: number = 0;
  roundoff: number = 0;
  grandtotal: number = 0;
  responsedata: any;
  customerdetails_list: any;
  productdetails_list: any;
  product_price: any;
  taxpercentage: any;
  taxamount1: number = 0;
  taxpercentage1: any;
  branch_list: any;
  taxpercentage2: any;
  taxamount2: number = 0;
  producttotalamount: any;
  parameterValue: string | undefined;
  currency_name: any;
  mdlTerms: any;
  terms_list: any[] = [];
  templatecontent_list: any;
  allchargeslist: any[] = [];
  
  mdlCustomer: any;
  mdlBranch: any;
  mdlCurrency: any;
  mdlProduct: any;
  mdlTax: any;
  leadbank_gid:any;
  leadbankcontact_gid:any;
  lead2campaign_gid:any;
  lspage:any;


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

  ngOnInit(): void {    
    const secretKey="storyboarderp"
  this.leadbank_gid = this.route.snapshot.paramMap.get('leadbank_gid');
this.leadbankcontact_gid = this.route.snapshot.paramMap.get('leadbankcontact_gid');
this.lead2campaign_gid = this.route.snapshot.paramMap.get('lead2campaign_gid');
this.lspage = this.route.snapshot.paramMap.get('lspage');
 
this.leadbank_gid = AES.decrypt(this.leadbank_gid,secretKey).toString(enc.Utf8);
this.leadbankcontact_gid = AES.decrypt(this.leadbankcontact_gid,secretKey).toString(enc.Utf8);
this.lead2campaign_gid = AES.decrypt(this.lead2campaign_gid,secretKey).toString(enc.Utf8);
this.lspage = AES.decrypt(this.lspage,secretKey).toString(enc.Utf8);
    const options: Options = {
      dateFormat: 'd-m-Y',    
    };

    flatpickr('.date-picker', options);

    var api = 'Einvoice/GetProductNamedropDown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productnamelist = this.responsedata.Getproductnamedropdown;
    });

    var api = 'Einvoice/GetProductNamedropDown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.productcodelist = this.responsedata.Getproductnamedropdown;
    });

    var api = 'Einvoice/GetcurrencyCodedropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.currency_list = this.responsedata.Getcurrencycodedropdown;
    });
    let params={
     leadbank_gid:this.leadbank_gid,
      
      }
    var api = 'Einvoice/GetCustomer360dropdowm';
    this.service.getparams(api,params).subscribe((result: any) => {
      this.responsedata = result;
      this.customernamelist = this.responsedata.GetCustomernamedropdown;
    });
    
    var api = 'Einvoice/GetBranchName';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.branch_list = this.responsedata.GetBranchnamedropdown;
    });

    var api = 'Einvoice/Gettaxnamedropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.tax_list = this.responsedata.Gettaxnamedropdown;
    });

    var url = 'Einvoice/GetTermsandConditions'
    this.service.get(url).subscribe((result: any) => {
      this.terms_list = result.terms_lists;
    });

    this.invoiceproductsummary();

    var api = 'SmrMstSalesConfig/GetAllChargesConfig';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.allchargeslist = this.responsedata.salesconfigalllist;
      this.addoncharges = this.allchargeslist[0].flag;
      this.invoicediscountamount = this.allchargeslist[1].flag;
      this.frieghtcharges = this.allchargeslist[2].flag;
      this.forwardingCharges = this.allchargeslist[3].flag;
      this.insurancecharges = this.allchargeslist[4].flag;

      if (this.allchargeslist[0].flag == 'Y') {
        this.addoncharges = 0;
      } else {
        this.addoncharges = this.allchargeslist[0].flag;
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

  invoiceproductsummary() {
    var api = 'Einvoice/GetInvoiceProductSummary';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.invoiceproductlist = this.responsedata.invoiceproductsummarylist;
      this.invoiceform.get("producttotalamount")?.setValue(this.responsedata.grand_total);
    });

    // const currentDate = new Date().toISOString().split('T')[0];
    // this.invoiceform.get('invoicedate')?.setValue(currentDate);

    const currentDate = new Date();
    const day = currentDate.getDate();
    const month = currentDate.getMonth() + 1;
    const year = currentDate.getFullYear();

    const formattedDate = `${day.toString().padStart(2, '0')}-${month.toString().padStart(2, '0')}-${year}`;
    this.invoiceform.get('invoicedate')?.setValue(formattedDate);
  }

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) {
    this.invoiceform = new FormGroup({
      customergid: new FormControl({ value: '', disabled: false }, Validators.required),
      customer_branch: new FormControl(''),
      customercontactperson: new FormControl(''),
      customercontactnumber: new FormControl(''),
      customeremailaddress: new FormControl(''),
      customeraddress: new FormControl(''),
      branchgid: new FormControl('',Validators.required),
      branch_name: new FormControl(''),
      gst: new FormControl(''),
      currencygid: new FormControl('', Validators.required),
      exchangerate: new FormControl(''),
      salestype: new FormControl('', Validators.required),
      mode_of_dispatch: new FormControl('',),
      remarks: new FormControl('',),
      invoiceref_no: new FormControl('',),
      invoicedate: new FormControl('', Validators.required),
      paymentterm: new FormControl('', Validators.required),
      duedate: new FormControl('', Validators.required),
      deliverydate: new FormControl('', Validators.required),
      payment: new FormControl(''),
      deliveryperiod: new FormControl(''),
      producttotalamount: new FormControl('',),
      addoncharges: new FormControl('',),
      invoicediscountamount: new FormControl('',),
      frieghtcharges: new FormControl('',),
      forwardingCharges: new FormControl('',),
      insurancecharges: new FormControl('',),
      roundoff: new FormControl('',),
      grandtotal: new FormControl('',[Validators.required]),
      template_gid: new FormControl(''),
      template_name: new FormControl(''),
      termsandconditions: new FormControl('',),
      
    })

    this.productform = new FormGroup({
      productgid: new FormControl(''),
      productgroup_gid: new FormControl(''),
      productcode: new FormControl(''),
      hsncode: new FormControl(''),
      hsndescription: new FormControl(''),
      productuom_gid: new FormControl(''),
      unitprice: new FormControl(''),
      mrp: new FormControl(''),
      quantity: new FormControl(''),
      discountpercentage: new FormControl(''),
      discountamount: new FormControl(''),
      taxname1: new FormControl(''),
      taxamount1: new FormControl(''),
      taxname2: new FormControl(''),
      taxamount2: new FormControl(''),
      totalamount: new FormControl(''),
    })
  }

  get branchControl() {
    return this.invoiceform.get('branchgid');
  }

  get invrefnoControl() {
    return this.invoiceform.get('invoice_ref_no');
  }

  get invoicedateControl() {
    return this.invoiceform.get('invoicedate');
  }

  get paytermControl() {
    return this.invoiceform.get('paymentterm');
  }

  get duedateControl() {
    return this.invoiceform.get('duedate');
  }

  get cusnameControl() {
    return this.invoiceform.get('customergid');
  }

  get connumControl() {
    return this.invoiceform.get('customercontactnumber');
  }

  get emailControl() {
    return this.invoiceform.get('customeremailaddress');
  }

  get custaddControl() {
    return this.invoiceform.get('customeraddress');
  }

  get deliverydateControl() {
    return this.invoiceform.get('deliverydate');
  }

  get currencyControl() {
    return this.invoiceform.get('currencygid');
  }

  get exchrateControl() {
    return this.invoiceform.get('exchangerate');
  }

  get salestypenameControl() {
    return this.invoiceform.get('salestype');
  }

  get mfdispControl() {
    return this.invoiceform.get('mode_of_dispatch');
  }

  get remarksControl() {
    return this.invoiceform.get('remarks');
  }

  get prodnameControl() {
    return this.productform.get('productgid');
  }

  onSubmit() {
  

if(this.invoiceproductlist!=null)
{
    var api = 'Einvoice/InvoiceSubmit';
    this.service.post(api, this.invoiceform.value).subscribe((result: any) => {
     
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
       
        const secretKey = 'storyboarderp';


        const lspage1 = AES.encrypt(this.lspage, secretKey).toString();
       
        if (this.lspage == 'MM-Total') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'MM-Upcoming') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'MM-Lapsed') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'MM-Longest') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'MM-New') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'MM-Prospect') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'MM-Potential') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'MM-mtd') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'MM-ytd') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'MM-Customer') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'MM-Drop') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'My-Today') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'My-New') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'My-Prospect') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'My-Potential') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'My-Customer') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'My-Drop') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'My-All') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else if (this.lspage == 'My-Upcoming') {
      
          this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);
      
        }
      
        else {
            this.router.navigate(['/einvoice/Invoice-Summary']);
            this.ToastrService.success(result.message);
        }
      
      }
    });
  }
  else{
    this.ToastrService.warning(" Kindly Add Atleast One Product")

  }
}

  OnChangeCurrency() {
    let currencyexchange_gid = this.invoiceform.get("currencygid")?.value;

    let param = {
      currencyexchange_gid: currencyexchange_gid
    }

    var url = 'Einvoice/GetOnChangeCurrency';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.currencylist = this.responsedata.GetOnChangeCurrency;
      this.invoiceform.get("exchangerate")?.setValue(this.currencylist[0].exchange_rate);
      this.currency_name = this.currencylist[0].currency_code;
    });
  }

  OnChangeBranchGSt() {
    let branch_gid = this.invoiceform.get("branchgid")?.value;
    
    let param = {
      branch_gid: branch_gid
    }

    var url = 'Einvoice/GetOnChangeBranch';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.branchgstlist = this.responsedata.GetOnChangeBranch;
      this.invoiceform.get("gst")?.setValue(this.branchgstlist[0].GST);
    });
  }

  GetOnChangeTerms() {    
    let template_gid = this.invoiceform.value.template_name;
    let param = {
      template_gid: template_gid
    }

    var url = 'Einvoice/GetOnChangeTerms';

    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.templatecontent_list = this.responsedata.terms_lists;
      this.invoiceform.get("termsandconditions")?.setValue(this.templatecontent_list[0].template_content);
      this.invoiceform.value.template_gid = result.terms_list[0].template_gid
    });
  }

  customerDetailsFetch() {
    let customer_gid = this.invoiceform.get('customergid')?.value;

    let param = {
      customer_gid: customer_gid
    }

    var url = 'Einvoice/GetOnChangeCustomer';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.customerdetails_list = this.responsedata.Getcustomeronchangedetails;
      this.invoiceform.get("customer_branch")?.setValue(this.customerdetails_list[0].customerbranchname);
      this.invoiceform.get("customercontactperson")?.setValue(this.customerdetails_list[0].customercontactname);
      this.invoiceform.get("customercontactnumber")?.setValue(this.customerdetails_list[0].mobile);
      this.invoiceform.get("customeremailaddress")?.setValue(this.customerdetails_list[0].email);
      this.invoiceform.get("customeraddress")?.setValue(this.customerdetails_list[0].address);
    });
  }

  productDetailsFetch() {
    let product_gid = this.productform.get('productgid')?.value;

    let param = {
      product_gid: product_gid
    }

    var url = 'Einvoice/GetOnChangeProduct';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.productdetails_list = this.responsedata.Getproductonchangedetails;
      this.productform.get("productgroup_gid")?.setValue(this.productdetails_list[0].productgroup_name);
      this.productform.get("productcode")?.setValue(this.productdetails_list[0].product_code);
      this.productform.get("hsncode")?.setValue(this.productdetails_list[0].hsn_code);
      this.productform.get("hsndescription")?.setValue(this.productdetails_list[0].hsn_description);
      this.productform.get("productuom_gid")?.setValue(this.productdetails_list[0].productuom_name);
      this.productform.get("mrp")?.setValue(this.productdetails_list[0].product_price);
    });
  }

  productSubmit() {
    console.log(this.productform)
    if(this.totalamount != 0) {
      var api = 'Einvoice/InvoicePostProduct';
      this.service.post(api, this.productform.value).subscribe((result: any) => {
        this.invoiceproductsummary();
        this.productform.reset();
      });
    }
    else {
      this.ToastrService.warning("Kindly fill the amount")
    }
    
  }

  prodtotalcal() {
    const subtotal = this.unitprice * this.quantity;
    this.discountamount = (subtotal * this.discountpercentage) / 100;
    this.totalamount = subtotal - this.discountamount;
  }

  taxAmount1() {
    let tax_gid = this.productform.get('taxname1')?.value;

    this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    console.log(this.taxpercentage);
    let tax_percentage = this.taxpercentage[0].tax_percentage;
    console.group(tax_percentage)

    this.taxamount1 = ((tax_percentage) * (this.totalamount) / 100);

    if (this.taxamount1 == undefined) {
      const subtotal = this.unitprice * this.quantity;
      this.discountamount = (subtotal * this.discountpercentage) / 100;
      this.totalamount = subtotal - this.discountamount;
    }
    else {
      this.totalamount = ((this.totalamount) + (+this.taxamount1));
    }
  }

  getDimensionsByFilter(id: any) {
    return this.tax_list.filter((x: { tax_gid: any; }) => x.tax_gid === id);
  }

  finaltotal() {
    this.grandtotal = ((this.producttotalamount) + (+this.addoncharges) + (+this.frieghtcharges) + (+this.forwardingCharges) + (+this.insurancecharges) + (+this.roundoff) - (+this.invoicediscountamount));
    this.invoiceform.get("grandtotal")?.setValue(this.grandtotal);

  }

  openModaldelete(parameter: string) {
    this.parameterValue = parameter
  }

  ondelete() {
    var url = 'Einvoice/GetDeleteInvoiceProductSummary'

    let param = {
      invoicedtl_gid: this.parameterValue
    }

    this.service.getparams(url, param).subscribe((result: any) => {
      if (result.status == true) {
        this.ToastrService.success(result.message)
        this.invoiceproductsummary()
      }
      else {
        this.ToastrService.warning(result.message)
      }
    });
  }

back() {  
   
    var api = 'Einvoice/GetDeleteInvoicebackProductSummary';

    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
    })
      const secretKey = 'storyboarderp';


  const lspage1 = AES.encrypt(this.lspage, secretKey).toString();
 
  if (this.lspage == 'MM-Total') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'MM-Upcoming') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'MM-Lapsed') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'MM-Longest') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'MM-New') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'MM-Prospect') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'MM-Potential') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'MM-mtd') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'MM-ytd') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'MM-Customer') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'MM-Drop') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'My-Today') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'My-New') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'My-Prospect') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'My-Potential') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'My-Customer') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'My-Drop') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'My-All') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else if (this.lspage == 'My-Upcoming') {

    this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

  }

  else {
      this.router.navigate(['/einvoice/Invoice-Summary']);
  }
    
  }
}
