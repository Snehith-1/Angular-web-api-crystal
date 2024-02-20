import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';




@Component({
  selector: 'app-rbl-trn-proforma-invoice-confirm',
  templateUrl: './rbl-trn-proforma-invoice-confirm.component.html',
  styleUrls: ['./rbl-trn-proforma-invoice-confirm.component.scss']
})

export class RblTrnProformaInvoiceConfirmComponent {

  proformainvoiceconfirmform: FormGroup | any;
  salesorder_gid: any;
  proformainvoicedata: any;
  responsedata: any;
  proformainvoiceproductlist: any;
  mdlperformainvoice:any;

  ngOnInit() {
   

    const salesorder_gid = this.route.snapshot.paramMap.get('directorder_gid');
    this.salesorder_gid = salesorder_gid;
  
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.salesorder_gid, secretKey).toString(enc.Utf8);
  
    let param = {
      directorder_gid: deencryptedParam
    }
    var api = 'ProformaInvoice/GetEditProformaInvoicedata';
    this.service.getparams(api, param).subscribe((result: any) => {
      this.responsedata = result;
      this.proformainvoiceproductlist = this.responsedata.editproformainvoice_list;
    });
    this.GetProformaInvoicedata();
    
      const options: Options = {
            dateFormat: 'd-m-Y',    
          };
          flatpickr('.date-picker', options);
      
  }

  constructor(private router: Router, private route: ActivatedRoute, private fb: FormBuilder, private service: SocketService,private ToastrService: ToastrService) {
    
    this.proformainvoiceconfirmform = new FormGroup({
      proforma_salesorder_gid: new FormControl(''),
      proforma_invoice_refno: new FormControl('', Validators.required),
      proforma_invoice_date: new FormControl('', Validators.required),
      proforma_invoice_payterm: new FormControl(''),
      proforma_invoice_due_date: new FormControl(''),
      proforma_invoice_raised_by: new FormControl(''),
      
      proforma_branch: new FormControl(''),
      proforma_cust_ref_no: new FormControl(''),
      proforma_so_ref_no: new FormControl(''),
      proforma_so_date: new FormControl(''),
      proforma_customer_name: new FormControl(''),
      proforma_contact_person: new FormControl(''),
      proforma_contact_no: new FormControl(''),
      proforma_email_address: new FormControl(''),
      proforma_address: new FormControl(''),
      proforma_remarks: new FormControl(''),

      proforma_sales_person: new FormControl(''),
      proforma_expected_start_date: new FormControl(''),
      proforma_estimated_arrival_time: new FormControl(''),
      proforma_freight_terms: new FormControl(''),
      proforma_payment_terms: new FormControl(''),
      proforma_currency: new FormControl(''),
      proforma_exchange_rate: new FormControl(''),

      proforma_product_gid: new FormControl(''),
      proforma_product_name: new FormControl(''),
      proforma_product_code: new FormControl(''),
      proforma_product_group: new FormControl(''),
      proforma_product_description: new FormControl(''),
      proforma_unit: new FormControl(''),
      proforma_unit_price: new FormControl(''),
      proforma_billed_quantity: new FormControl(''),
      proforma_mrp: new FormControl(''),
      proforma_discountpercentage: new FormControl(''),
      proforma_discountamount: new FormControl(''),
      proforma_taxname1: new FormControl(''),
      proforma_taxamount1: new FormControl(''),
      proforma_taxname2: new FormControl(''),
      proforma_taxamount2: new FormControl(''),
      proforma_total_amount: new FormControl(''),

      proforma_net_amount: new FormControl(''),
      proforma_overall_tax: new FormControl(''),
      proforma_total_amount_tax: new FormControl(''),
      proforma_maximum_addon_amount: new FormControl(''),
      proforma_maximum_addon_discount_amount: new FormControl(''),
      proforma_freight_charges: new FormControl(''),
      proforma_buy_back_scrap_charges: new FormControl(''),
      proforma_packing_forwarding_charges: new FormControl(''),
      proforma_insurance_charges: new FormControl(''),
      proforma_roundoff: new FormControl(''),
      proforma_grandtotal: new FormControl(''),
      proforma_advance_percentage: new FormControl(''),
      proforma_advance_amount: new FormControl(''),
      proforma_advance_roundoff: new FormControl(''),
      proforma_termsandconditions: new FormControl('')
    })
  }

  get proformainvoicerefnoControl() {
    return this.proformainvoiceconfirmform.get('proforma_invoice_refno');
  }
  get proformainvoicedateControl() {
    return this.proformainvoiceconfirmform.get('proforma_invoice_date');
  }

  GetProformaInvoicedata() {
    const salesorder_gid = this.route.snapshot.paramMap.get('directorder_gid');
    this.salesorder_gid = salesorder_gid;

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.salesorder_gid, secretKey).toString(enc.Utf8);

    let param = {
      directorder_gid: deencryptedParam
    }

    var api = 'ProformaInvoice/GetProformaInvoicedata';

    this.service.getparams(api, param).subscribe((result: any) => {
      this.responsedata= result;      
      this.proformainvoicedata = result;

      this.proformainvoiceconfirmform.get("proforma_salesorder_gid")?.setValue(this.proformainvoicedata.salesorder_gid);
      this.proformainvoiceconfirmform.get("proforma_so_date")?.setValue(this.proformainvoicedata.salesorder_date);
      this.proformainvoiceconfirmform.get("proforma_remarks")?.setValue(this.proformainvoicedata.salesorderdtl_gid);
      this.proformainvoiceconfirmform.get("proforma_sales_person")?.setValue(this.proformainvoicedata.user_firstname);
      this.proformainvoiceconfirmform.get("proforma_expected_start_date")?.setValue(this.proformainvoicedata.start_date);
      this.proformainvoiceconfirmform.get("proforma_estimated_arrival_time")?.setValue(this.proformainvoicedata.end_date);
      this.proformainvoiceconfirmform.get("proforma_payment_terms")?.setValue(this.proformainvoicedata.payment_terms);
      this.proformainvoiceconfirmform.get("proforma_currency")?.setValue(this.proformainvoicedata.payment_terms);
      this.proformainvoiceconfirmform.get("proforma_branch")?.setValue(this.proformainvoicedata.branch_name);
      this.proformainvoiceconfirmform.get("proforma_so_ref_no")?.setValue(this.proformainvoicedata.so_referencenumber);
      this.proformainvoiceconfirmform.get("proforma_cust_ref_no")?.setValue(this.proformainvoicedata.so_referenceno1);
      this.proformainvoiceconfirmform.get("proforma_freight_terms")?.setValue(this.proformainvoicedata.freight_terms);
      this.proformainvoiceconfirmform.get("proforma_product_name")?.setValue(this.proformainvoicedata.product_name);
      this.proformainvoiceconfirmform.get("proforma_product_group")?.setValue(this.proformainvoicedata.productgroup_name);
      this.proformainvoiceconfirmform.get("proforma_customer_name")?.setValue(this.proformainvoicedata.customer_name);
      this.proformainvoiceconfirmform.get("proforma_contact_person")?.setValue(this.proformainvoicedata.customer_contact_person);      
      this.proformainvoiceconfirmform.get("proforma_contact_no")?.setValue(this.proformainvoicedata.mobile);
      this.proformainvoiceconfirmform.get("proforma_email_address")?.setValue(this.proformainvoicedata.customer_email);
      this.proformainvoiceconfirmform.get("proforma_address")?.setValue(this.proformainvoicedata.customer_address);
      this.proformainvoiceconfirmform.get("proforma_product_gid")?.setValue(this.proformainvoicedata.product_gid);
      this.proformainvoiceconfirmform.get("proforma_total_amount")?.setValue(this.proformainvoicedata.product_price);
      this.proformainvoiceconfirmform.get("proforma_product_description")?.setValue(this.proformainvoicedata.display_field);
      this.proformainvoiceconfirmform.get("proforma_billed_quantity")?.setValue(this.proformainvoicedata.qty_quoted);
      this.proformainvoiceconfirmform.get("proforma_discountpercentage")?.setValue(this.proformainvoicedata.margin_percentage);
      this.proformainvoiceconfirmform.get("proforma_discountamount")?.setValue(this.proformainvoicedata.margin_amount);      
      this.proformainvoiceconfirmform.get("proforma_taxname1")?.setValue(this.proformainvoicedata.tax_name);      
      this.proformainvoiceconfirmform.get("proforma_taxamount1")?.setValue(this.proformainvoicedata.tax_amount);      
      this.proformainvoiceconfirmform.get("proforma_taxname2")?.setValue(this.proformainvoicedata.tax_name2);
      this.proformainvoiceconfirmform.get("proforma_taxamount2")?.setValue(this.proformainvoicedata.tax_amount2);
      this.proformainvoiceconfirmform.get("proforma_total_amount")?.setValue(this.proformainvoicedata.total_amount);
      this.proformainvoiceconfirmform.get("proforma_overall_tax")?.setValue(this.proformainvoicedata.gst_amount);
      this.proformainvoiceconfirmform.get("proforma_total_amount_tax")?.setValue(this.proformainvoicedata.final_amount);
      this.proformainvoiceconfirmform.get("proforma_maximum_addon_amount")?.setValue(this.proformainvoicedata.addon_charge);
      this.proformainvoiceconfirmform.get("proforma_maximum_addon_discount_amount")?.setValue(this.proformainvoicedata.additional_discount);
      this.proformainvoiceconfirmform.get("proforma_freight_charges")?.setValue(this.proformainvoicedata.freight_charges);
      this.proformainvoiceconfirmform.get("proforma_buy_back_scrap_charges")?.setValue(this.proformainvoicedata.buyback_charges);
      this.proformainvoiceconfirmform.get("proforma_packing_forwarding_charges")?.setValue(this.proformainvoicedata.packing_charges);
      this.proformainvoiceconfirmform.get("proforma_insurance_charges")?.setValue(this.proformainvoicedata.insurance_charges);
      this.proformainvoiceconfirmform.get("proforma_roundoff")?.setValue(this.proformainvoicedata.roundoff);      
      this.proformainvoiceconfirmform.get("proforma_grandtotal")?.setValue(this.proformainvoicedata.Grandtotal);
      this.proformainvoiceconfirmform.get("proforma_advance_amount")?.setValue(this.proformainvoicedata.total_price);
      this.proformainvoiceconfirmform.get("proforma_advance_roundoff")?.setValue(this.proformainvoicedata.roundoff);
      this.proformainvoiceconfirmform.get("proforma_termsandconditions")?.setValue(this.proformainvoicedata.termsandconditions);
    });
  }

  proformainvoicesubmit() {
    console.log(this.proformainvoiceconfirmform)

    var api = 'ProformaInvoice/ProformaInvoiceSubmit';
    this.service.post(api, this.proformainvoiceconfirmform.value).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.router.navigate(['/einvoice/ProformaInvoice']);
        this.ToastrService.success(result.message)
      }
      // this.router.navigate(['/einvoice/Invoice']);
    },
    );
  }

 back() {
    this.router.navigate(['/einvoice/ProformaInvoice']);
  }
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '12rem',
    minHeight: '0rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
  };
  
 
}

