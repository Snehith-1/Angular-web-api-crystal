import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-pmr-trn-invoice-directinvoice',
  templateUrl: './pmr-trn-invoice-directinvoice.component.html',
  styleUrls: ['./pmr-trn-invoice-directinvoice.component.scss']
})
export class PmrTrnInvoiceDirectinvoiceComponent {
  
  directinvoiceform: any;
  responsedata: any;
  branch_list: any;
  vendorlist: any;
  vendor_list:any;
  currency_list: any;
  currencylist: any;
  currency_name: any;
  direct_invoice_payterm:any;
  purchasetype_list: any;
  tax_list: any;
  taxpercentage: any;
  taxamount1: any;
  taxamount2: any;
  direct_invoice_additional_gid:any;
  direct_invoice_discount_gid:any;
  direct_invoice_taxname11:any;
  direct_invoice_taxname22:any;
  extraaddon_list: any;
  extradeduction_list: any;
  direct_invoice_priority:any;
  direct_invoice_amount: any;
  direct_invoice_total: number = 0;
  direct_invoice_grand_total: number = 0;
  direct_invoice_addon_amount: number = 0;
  direct_invoice_discount_amount: number = 0;
  direct_invoice_freight_charges: number = 0;
  direct_invoice_buyback_scrap_charges: number = 0;
  direct_invoice_extra_addon: number = 0;
  direct_invoice_extra_deduction: number = 0;
  direct_invoice_round_off: number = 0;
  direct_invoice_branchgid:any;
  direct_invoice_ven_name:any;
  direct_invoice_ven_contact_person:any;
  direct_invoice_currencygid:any;
  direct_invoice_account_gid:any;

  ngOnInit(): void {
    const options: Options = {
      dateFormat: 'd-m-Y',    
    };
    flatpickr('.date-picker', options);

    var api = 'PmrTrnDirectInvoice/GetBranchName';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.branch_list = this.responsedata.GetBranchnamedropdown;
    });

    var api = 'PmrTrnDirectInvoice/GetVendorNamedropDown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.vendor_list = this.responsedata.GetVendornamedropdown;
    });

    var api = 'PmrTrnDirectInvoice/GetcurrencyCodedropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.currency_list = this.responsedata.Getcurrencycodedropdown;
    });

    var api = 'PmrTrnDirectInvoice/GetPurchaseTypedropDown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.purchasetype_list = this.responsedata.GetPurchaseTypedropDown;
    });

    var api = 'PmrTrnDirectInvoice/Gettaxnamedropdown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.tax_list = this.responsedata.Gettaxnamedropdown;
    });

    var api = 'PmrTrnDirectInvoice/GetExtraAddondropDown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.extraaddon_list = this.responsedata.GetExtraAddondropDown;
    });

    var api = 'PmrTrnDirectInvoice/GetExtraDeductiondropDown';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.extradeduction_list = this.responsedata.GetExtraDeductiondropDown;
    });
    const currentDate = new Date();
    const day = currentDate.getDate();
    const month = currentDate.getMonth() + 1;
    const year = currentDate.getFullYear();
 
    const formattedDate = `${day.toString().padStart(2, '0')}-${month.toString().padStart(2, '0')}-${year}`;
    this.directinvoiceform.get('direct_invoice_date')?.setValue(formattedDate);
    this.directinvoiceform.get("direct_invoice_due_date")?.setValue(formattedDate);
    this.directinvoiceform.get("direct_invoice_type")?.setValue('Direct Invoice');
    this.direct_invoice_payterm='0';
  }

  constructor(private router: Router, private route: 
    ActivatedRoute, private fb: FormBuilder, private service: SocketService, 
    private ToastrService: ToastrService,
    public NgxSpinnerService:NgxSpinnerService) {
    
    this.directinvoiceform = new FormGroup({
      direct_invoice_refno: new FormControl('',[Validators.required]),
      direct_invoice_date: new FormControl('',[Validators.required]),
      direct_invoice_type: new FormControl(''),
      direct_invoice_payterm: new FormControl(''),
      direct_invoice_due_date: new FormControl(''),
      
      direct_invoice_branchgid: new FormControl(''),
      direct_invoice_ven_ref_no: new FormControl(''),      
      direct_invoice_ven_name: new FormControl('',[Validators.required]),
      direct_invoice_ven_contact_person: new FormControl('',[Validators.required]),
      direct_invoice_ph_no: new FormControl(''),
      direct_invoice_ven_address: new FormControl(''),
      direct_invoice_remarks: new FormControl(''),
    
      direct_invoice_currencycode: new FormControl(''),
      direct_invoice_currencygid: new FormControl('',[Validators.required]),
      direct_invoice_exchange_rate: new FormControl('',[Validators.required]),
      direct_invoice_account_gid: new FormControl('',[Validators.required]),
      
      direct_invoice_amount: new FormControl('',[Validators.pattern(/^[0-9]+$/), Validators.required]),
      direct_invoice_taxname1: new FormControl(''),
      direct_invoice_taxamount1: new FormControl(''),
      direct_invoice_taxname2: new FormControl(''),
      direct_invoice_taxamount2: new FormControl(''),
      direct_invoice_total: new FormControl(''),
      direct_invoice_description: new FormControl('',[Validators.required]),

      direct_invoice_addon_amount: new FormControl(''),
      direct_invoice_discount_amount: new FormControl(''),
      direct_invoice_freight_charges: new FormControl(''),
      direct_invoice_buyback_scrap_charges: new FormControl(''),
      direct_invoice_additional_gid: new FormControl(''),
      direct_invoice_extra_addon: new FormControl(''),
      direct_invoice_discount_gid: new FormControl(''),
      direct_invoice_extra_deduction: new FormControl(''),
      direct_invoice_round_off: new FormControl(''),
      direct_invoice_grand_total: new FormControl(''),
      direct_invoice_priority: new FormControl('')
    })
  }

  get directinvoicerefnoControl() {
    return this.directinvoiceform.get('direct_invoice_refno');
  }

  get directinvoicedateControl() {
    return this.directinvoiceform.get('direct_invoice_date');
  }

  get directinvoicevennameControl() {
    return this.directinvoiceform.get('direct_invoice_ven_name');
  }

  get directinvoicevenconperControl() {
    return this.directinvoiceform.get('direct_invoice_ven_contact_person');
  }

  get directinvoicecurrencyControl() {
    return this.directinvoiceform.get('direct_invoice_currencygid');
  }

  get directinvoiceexchangerateControl() {
    return this.directinvoiceform.get('direct_invoice_exchange_rate');
  }

  get directinvoicepurtypeControl() {
    return this.directinvoiceform.get('direct_invoice_account_gid');
  }

  get directinvoicedescriptionControl() {
    return this.directinvoiceform.get('direct_invoice_description');
  }

  get directinvoiceamountControl() {
    return this.directinvoiceform.get('direct_invoice_amount');
  }

  getDimensionsByFilter(id: any) {
    return this.tax_list.filter((x: { tax_gid: any; }) => x.tax_gid === id);
  }

  OnChangeVendor() {
    debugger;
    let vendor_gid = this.directinvoiceform.get("direct_invoice_ven_name")?.value;

    let param = {
      vendor_gid: vendor_gid
    }

    var url = 'PmrTrnDirectInvoice/GetOnChangeVendor';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.vendorlist = this.responsedata.GetOnChangeVendor;
      this.directinvoiceform.get("direct_invoice_ph_no")?.setValue(this.vendorlist[0].phone);
      this.directinvoiceform.get("direct_invoice_ven_address")?.setValue(this.vendorlist[0].address);
    });
  }

  OnChangeCurrency() {
    debugger;
    let currencyexchange_gid = this.directinvoiceform.get("direct_invoice_currencygid")?.value;

    let param = {
      currencyexchange_gid: currencyexchange_gid
    }

    var url = 'Einvoice/GetOnChangeCurrency';
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.currencylist = this.responsedata.GetOnChangeCurrency;
      this.directinvoiceform.get("direct_invoice_exchange_rate")?.setValue(this.currencylist[0].exchange_rate);
      this.directinvoiceform.get("direct_invoice_currencycode")?.setValue(this.currencylist[0].currency_code);
      this.currency_name = this.currencylist[0].currency_code;
    });
  }

  taxAmount1() {
    let tax_gid = this.directinvoiceform.get('direct_invoice_taxname1')?.value;

    this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    console.log(this.taxpercentage);
    let tax_percentage = this.taxpercentage[0].tax_percentage;
    console.group(tax_percentage)

    this.taxamount1 = ( (this.direct_invoice_amount) * (tax_percentage) / 100);
    
    if (this.taxamount1 == undefined) {
      
    }
    else {
      const direct_invoice_amount = parseFloat(this.direct_invoice_amount);   
      this.direct_invoice_total = ((direct_invoice_amount ||0) + (+this.taxamount1 ||0) + (+this.taxamount2 ||0));
    }
  }

  taxAmount2() {
    let tax_gid = this.directinvoiceform.get('direct_invoice_taxname1')?.value;

    this.taxpercentage = this.getDimensionsByFilter(tax_gid);
    console.log(this.taxpercentage);
    let tax_percentage = this.taxpercentage[0].tax_percentage;
    console.group(tax_percentage)

    this.taxamount2 = ( (this.direct_invoice_amount) * (tax_percentage) / 100);
    
    if (this.taxamount2 == undefined) {
      
    }
    else {
      const direct_invoice_amount = parseFloat(this.direct_invoice_amount); 
      this.direct_invoice_total = ((direct_invoice_amount ||0) + (+this.taxamount1 ||0) + (+this.taxamount2 ||0));
       }
  }

  finaltotal() {
    const direct_invoice_amount = parseFloat(this.direct_invoice_amount) || 0;
    const taxAmount1 = parseFloat(this.taxamount1) || 0;
    const taxAmount2 = parseFloat(this.taxamount2) || 0;
    
    this.direct_invoice_total = ((direct_invoice_amount) + (taxAmount1) + (taxAmount2));
  }

  grandtotal() {
    this.direct_invoice_grand_total = ((this.direct_invoice_total) + (+this.direct_invoice_addon_amount) - (+this.direct_invoice_discount_amount) + (+this.direct_invoice_freight_charges) + (+this.direct_invoice_buyback_scrap_charges) + (+this.direct_invoice_extra_addon) + (+this.direct_invoice_extra_deduction) + (+this.direct_invoice_round_off))
  }  

  directinvoicesubmit(event: Event) {
    window.scrollTo({ top: 0, behavior: 'smooth' });
    debugger
    let f=0;
    if(this.directinvoiceform.value.direct_invoice_refno==null ||
       this.directinvoiceform.value.direct_invoice_branchgid==null ||
       this.directinvoiceform.value.direct_invoice_ven_name==null ||
       this.directinvoiceform.value.direct_invoice_ven_contact_person==null ||
       this.directinvoiceform.value.direct_invoice_currencygid==null||
       this.directinvoiceform.value.direct_invoice_amount==null ||
       this.directinvoiceform.value.direct_invoice_account_gid==null 
        ){
      f=1
    }
    if(f==0){
    var api = 'PmrTrnDirectInvoice/directinvoicesubmit';
    this.NgxSpinnerService.show();
    this.service.post(api, this.directinvoiceform.value).subscribe((result: any) => {
      this.NgxSpinnerService.hide(); 
      if (result.status == true) {
        this.ToastrService.success(result.message);
        this.router.navigate(['/payable/PmrTrnInvoice']);
      }
      else {
        this.ToastrService.warning(result.message)
      }
    }); 
  }
  else {
    this.ToastrService.warning("Fill All Mandatory Fields")
   
  }   
  }
  
  back() {
    this.router.navigate(['/payable/PmrTrnInvoice']);
  }
}
