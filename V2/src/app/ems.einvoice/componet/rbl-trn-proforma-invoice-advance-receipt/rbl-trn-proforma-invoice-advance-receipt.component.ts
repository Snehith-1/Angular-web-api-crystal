import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';



@Component({
  selector: 'app-rbl-trn-proforma-invoice-advance-receipt',
  templateUrl: './rbl-trn-proforma-invoice-advance-receipt.component.html',
  styleUrls: ['./rbl-trn-proforma-invoice-advance-receipt.component.scss']
})

export class RblTrnProformaInvoiceAdvanceReceiptComponent {
  proformaadvancerptform: FormGroup | any;
  invoice_gid: any;
  responsedata: any;
  proformainvoiceadvancedata: any;
  modeofpayment_list: any;
  productdata_list: any;
  mdlReceiptmode:any;
 
  constructor(private router: Router, private route: ActivatedRoute, private fb: FormBuilder, private service: SocketService,private ToastrService: ToastrService) { 
    this.proformaadvancerptform = new FormGroup({
      proforma_advrpt_salesorder_gid: new FormControl(''),
      proforma_advrpt_invoice_gid: new FormControl(''),
      proforma_advrpt_invoice_ref_no: new FormControl(''),
      proforma_advrpt_invoice_date: new FormControl(''),
      proforma_advrpt_order_ref_no: new FormControl(''),
      proforma_advrpt_order_date: new FormControl(''),
      proforma_advrpt_customer_gid: new FormControl(''),
      proforma_advrpt_company_name: new FormControl(''),
      proforma_advrpt_contact_person: new FormControl(''),
      proforma_advrpt_contact_no: new FormControl(''),
      proforma_advrpt_email_address: new FormControl(''),
      proforma_advrpt_company_address: new FormControl(''),
      proforma_advrpt_department: new FormControl(''),
      proforma_advrpt_order_reference: new FormControl(''),
      proforma_advrpt_remarks: new FormControl(''),
      proforma_advrpt_branch_gid: new FormControl(''),
      proforma_advrpt_payment: new FormControl(''),
      proforma_advrpt_delivery_period: new FormControl(''),
      proforma_advrpt_net_amount: new FormControl(''),
      proforma_advrpt_order_refno: new FormControl(''),
      proforma_advrpt_addon_charges: new FormControl(''),
      proforma_advrpt_additional_discount: new FormControl(''),
      proforma_advrpt_hold_with_tax: new FormControl(''),
      proforma_advrpt_grand_total: new FormControl(''),
      proforma_advrpt_existing_advance: new FormControl(''),
      proforma_advrpt_outstanding_adv_amount: new FormControl(''),
      proforma_advrpt_advance: new FormControl(''),
      proforma_advrpt_payment_mode: new FormControl('',[Validators.required]),
      proforma_advrpt_termsandconditions: new FormControl(''),

      proforma_advrpt_product_group: new FormControl(''),
      proforma_advrpt_product_name: new FormControl(''),
      proforma_advrpt_product_unit: new FormControl(''),
      proforma_advrpt_product_quantity: new FormControl(''),
      proforma_advrpt_product_unitprice: new FormControl(''),
      proforma_advrpt_product_discount: new FormControl(''),
      proforma_advrpt_product_tax: new FormControl(''),
      proforma_advrpt_product_date: new FormControl(''),
      proforma_advrpt_product_amount: new FormControl(''),
    });
  }
    
  ngOnInit() {    
    const invoice_gid = this.route.snapshot.paramMap.get('invoice_gid');
    this.invoice_gid = invoice_gid;

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.invoice_gid, secretKey).toString(enc.Utf8);

    let param = {
      invoice_gid: deencryptedParam
    }

    var api = 'ProformaInvoice/GetProformaInvoiceProductdata';
    this.service.getparams(api,param).subscribe((result: any) => {
      this.productdata_list = result.MdlProformaInvoiceProductdata;
    });
    this.GetProformaInvoiceAdvancedata();

    var api1 = 'ProformaInvoice/GetProformaInvoicemodeofpayment';
    this.service.get(api1).subscribe((result: any) => {
      this.modeofpayment_list = result.GetProformaInvoicemodeofpaymentlist;
    });
      const options: Options = {
            dateFormat: 'd-m-Y',    
          };
          flatpickr('.date-picker', options);
      
  }

  get proformaadvrptpaymentControl() {
    return this.proformaadvancerptform.get('proforma_advrpt_payment_mode');
  }

  GetProformaInvoiceAdvancedata() {

    const invoice_gid = this.route.snapshot.paramMap.get('invoice_gid');
    this.invoice_gid = invoice_gid;

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.invoice_gid, secretKey).toString(enc.Utf8);

    let param = {
      invoice_gid: deencryptedParam
    }

    var api = 'ProformaInvoice/GetProformaInvoiceAdvancedata';
    this.service.getparams(api, param).subscribe((result: any) => {
      this.responsedata = result;
      this.proformainvoiceadvancedata = result;

      this.proformaadvancerptform.get("proforma_advrpt_salesorder_gid")?.setValue(this.proformainvoiceadvancedata.salesorder_gid);
      this.proformaadvancerptform.get("proforma_advrpt_invoice_gid")?.setValue(this.proformainvoiceadvancedata.invoice_gid);
      this.proformaadvancerptform.get("proforma_advrpt_invoice_ref_no")?.setValue(this.proformainvoiceadvancedata.invoice_refno);
      this.proformaadvancerptform.get("proforma_advrpt_invoice_date")?.setValue(this.proformainvoiceadvancedata.invoice_date);
      this.proformaadvancerptform.get("proforma_advrpt_order_ref_no")?.setValue(this.proformainvoiceadvancedata.so_referenceno1);
      this.proformaadvancerptform.get("proforma_advrpt_order_date")?.setValue(this.proformainvoiceadvancedata.salesorder_date);
      this.proformaadvancerptform.get("proforma_advrpt_customer_gid")?.setValue(this.proformainvoiceadvancedata.customer_gid);
      this.proformaadvancerptform.get("proforma_advrpt_company_name")?.setValue(this.proformainvoiceadvancedata.customer_name);
      this.proformaadvancerptform.get("proforma_advrpt_contact_person")?.setValue(this.proformainvoiceadvancedata.customer_contact_person);
      this.proformaadvancerptform.get("proforma_advrpt_contact_no")?.setValue(this.proformainvoiceadvancedata.customer_mobile);
      this.proformaadvancerptform.get("proforma_advrpt_email_address")?.setValue(this.proformainvoiceadvancedata.customer_email);
      this.proformaadvancerptform.get("proforma_advrpt_company_address")?.setValue(this.proformainvoiceadvancedata.customer_address_so);
      this.proformaadvancerptform.get("proforma_advrpt_remarks")?.setValue(this.proformainvoiceadvancedata.so_remarks);
      this.proformaadvancerptform.get("proforma_advrpt_branch_gid")?.setValue(this.proformainvoiceadvancedata.branch_gid);
      this.proformaadvancerptform.get("proforma_advrpt_payment")?.setValue(this.proformainvoiceadvancedata.payment_days);
      this.proformaadvancerptform.get("proforma_advrpt_delivery_period")?.setValue(this.proformainvoiceadvancedata.delivery_days);
      this.proformaadvancerptform.get("proforma_advrpt_net_amount")?.setValue(this.proformainvoiceadvancedata.total_value);
      this.proformaadvancerptform.get("proforma_advrpt_order_refno")?.setValue(this.proformainvoiceadvancedata.so_referenceno1);
      this.proformaadvancerptform.get("proforma_advrpt_addon_charges")?.setValue(this.proformainvoiceadvancedata.addon_charge);
      this.proformaadvancerptform.get("proforma_advrpt_additional_discount")?.setValue(this.proformainvoiceadvancedata.additional_discount);
      this.proformaadvancerptform.get("proforma_advrpt_grand_total")?.setValue(this.proformainvoiceadvancedata.Grandtotal);
      this.proformaadvancerptform.get("proforma_advrpt_existing_advance")?.setValue(this.proformainvoiceadvancedata.salesorder_advance);
      this.proformaadvancerptform.get("proforma_advrpt_outstanding_adv_amount")?.setValue(this.proformainvoiceadvancedata.outstandingadvance);
      this.proformaadvancerptform.get("proforma_advrpt_termsandconditions")?.setValue(this.proformainvoiceadvancedata.termsandconditions);
      // this.proformaadvancerptform.get("proforma_advrpt_product_group")?.setValue(this.proformainvoiceadvancedata.productgroup_name);
      this.proformaadvancerptform.get("proforma_advrpt_product_name")?.setValue(this.proformainvoiceadvancedata.product_name);
      this.proformaadvancerptform.get("proforma_advrpt_product_unit")?.setValue(this.proformainvoiceadvancedata.productuom_name);
      this.proformaadvancerptform.get("proforma_advrpt_product_quantity")?.setValue(this.proformainvoiceadvancedata.qty_invoice);
      this.proformaadvancerptform.get("proforma_advrpt_product_unitprice")?.setValue(this.proformainvoiceadvancedata.product_price);
      this.proformaadvancerptform.get("proforma_advrpt_product_discount")?.setValue(this.proformainvoiceadvancedata.discount_amount);
      this.proformaadvancerptform.get("proforma_advrpt_product_tax")?.setValue(this.proformainvoiceadvancedata.tax_amount);
      this.proformaadvancerptform.get("proforma_advrpt_product_amount")?.setValue(this.proformainvoiceadvancedata.product_total);
    })
  }

  AdvrptProformaInvoiceSubmit() {
    var api = 'ProformaInvoice/AdvrptProformaInvoiceSubmit';
    this.service.post(api, this.proformaadvancerptform.value).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.router.navigate(['/einvoice/ProformaInvoice']);
        this.ToastrService.success(result.message)
      }
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
