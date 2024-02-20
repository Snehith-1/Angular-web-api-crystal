import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, MaxLengthValidator, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import flatpickr from 'flatpickr';
import { Options } from 'flatpickr/dist/types/options';






@Component({
  selector: 'app-rbl-trn-proforma-invoice-edit',
  templateUrl: './rbl-trn-proforma-invoice-edit.component.html',
  styleUrls: ['./rbl-trn-proforma-invoice-edit.component.scss']
})
export class RblTrnProformaInvoiceEditComponent {
  invoice_gid:any;
  proformainvoiceeditform: FormGroup | any;
  invoice_list:any
  ProformaInvoiceEditlist: any[] = [];
  responsedata: any;
  constructor(private router: Router,
     private route: ActivatedRoute,
     private fb: FormBuilder, 
     private service: SocketService,
     private ToastrService: ToastrService) {
  }
  ngOnInit(): void {

    this.proformainvoiceeditform = new FormGroup({
      edit_proforma_invoice_refno: new FormControl(''),
      invoice_gid: new FormControl(''),
      edit_proforma_invoice_date: new FormControl(''),
      edit_proforma_invoice_payterm: new FormControl(''),
      edit_proforma_invoice_due_date: new FormControl(''),
      edit_proforma_invoice_so_reference_no: new FormControl(''),
      edit_proforma_invoice_raised_by: new FormControl(''),
      edit_proforma_invoice_branch: new FormControl(''),
      edit_proforma_invoice_cust_ref_no: new FormControl(''),
      edit_proforma_invoice_customer_name: new FormControl(''),
      edit_proforma_invoice_contact_person: new FormControl(''),
      edit_proforma_invoice_contact_no: new FormControl('',[Validators.pattern(/^[0-9]+$/), Validators.maxLength(10)]),
      edit_proforma_invoice_email_address: new FormControl('', [Validators.pattern('^([a-z0-9]+(?:[-.][a-z0-9]+)*)@([a-z0-9]+\\.[a-z]{2,20}(\\.[a-z]{2})?|\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\]|localhost)$')]),
      edit_proforma_invoice_address: new FormControl(''),
      edit_proforma_invoice_remarks: new FormControl(''),
      edit_proforma_invoice_sales_person: new FormControl(''),
      edit_proforma_invoice_expected_start_date: new FormControl(''),
      edit_proforma_invoice_estimated_arrival_time: new FormControl(''),
      edit_proforma_invoice_freight_terms: new FormControl(''),
      edit_proforma_invoice_payment_terms: new FormControl(''),
      edit_proforma_invoice_currency: new FormControl(''),
      edit_proforma_invoice_exchange_rate: new FormControl(''),
      edit_proforma_invoice_net_amount: new FormControl(''),
      edit_proforma_invoice_overall_tax: new FormControl(''),
      edit_proforma_invoice_total_amount_tax: new FormControl(''),
      edit_proforma_invoice_maximum_addon_amount: new FormControl(''),
      edit_proforma_invoice_maximum_addon_discount: new FormControl(''),
      edit_proforma_invoice_freight_charges: new FormControl(''),
      edit_proforma_invoice_buyback_charges: new FormControl(''),
      edit_proforma_invoice_packing_charges: new FormControl(''),
      edit_proforma_invoice_insurance_charges: new FormControl(''),
      edit_proforma_invoice_roundoff: new FormControl(''),
      edit_proforma_invoice_grandtotal: new FormControl(''),
      edit_proforma_invoice_advance_percentage: new FormControl(''),
      edit_proforma_invoice_advance_amount: new FormControl(''),
      edit_proforma_invoice_advance_roundoff: new FormControl(''),
      edit_proforma_invoice_termsandconditions: new FormControl(''),
      productgroup_name: new FormControl(''),
      product_name: new FormControl(''),
    });


debugger
    const invoice_gid = this.route.snapshot.paramMap.get('invoice_gid');
   
    this.invoice_gid = invoice_gid;

    const secretKey = 'storyboarderp';

    const deencryptedParam = AES.decrypt(this.invoice_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.summary(deencryptedParam)
    

   
  }


summary(invoice_gid: any)
{
  // debugger;
  // var url = 'ProformaInvoice/GetProformaInvoiceEditdata';
  // debugger;
  //   this.invoice_gid = invoice_gid
    
  //   var params={
  //     invoice_gid : invoice_gid
  //   }
    // this.service.getparams(url, params).subscribe((result: any) => {
    //   this.invoice_list = result.ProformaInvoiceEditlist;
debugger;
    var url = 'ProformaInvoice/GetProformaInvoiceEditdata'
    let param = {invoice_gid : invoice_gid}
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata=result;
      this.ProformaInvoiceEditlist = result.ProformaInvoiceEditlist;


      console.log(this.invoice_gid)
        console.log(this.ProformaInvoiceEditlist)
        this.proformainvoiceeditform.get("invoice_gid")?.setValue(this.ProformaInvoiceEditlist[0].invoice_gid); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_refno")?.setValue(this.ProformaInvoiceEditlist[0].invoice_refno); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_date")?.setValue(this.ProformaInvoiceEditlist[0].edit_proforma_invoice_date); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_payterm")?.setValue(this.ProformaInvoiceEditlist[0].invoice_date); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_due_date")?.setValue(this.ProformaInvoiceEditlist[0].payment_date); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_so_reference_no")?.setValue(this.ProformaInvoiceEditlist[0].so_referenceno1); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_raised_by")?.setValue(this.ProformaInvoiceEditlist[0].edit_proforma_invoice_raised_by); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_branch")?.setValue(this.ProformaInvoiceEditlist[0].branch_name); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_cust_ref_no")?.setValue(this.ProformaInvoiceEditlist[0].so_referencenumber); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_customer_name")?.setValue(this.ProformaInvoiceEditlist[0].customer_name); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_contact_person")?.setValue(this.ProformaInvoiceEditlist[0].customer_contactperson); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_contact_no")?.setValue(this.ProformaInvoiceEditlist[0].mobile); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_email_address")?.setValue(this.ProformaInvoiceEditlist[0].customer_email); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_address")?.setValue(this.ProformaInvoiceEditlist[0].customer_address); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_remarks")?.setValue(this.ProformaInvoiceEditlist[0].invoice_remarks); 
      this.proformainvoiceeditform.get("customer_address")?.setValue(this.ProformaInvoiceEditlist[0].edit_proforma_invoice_address); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_sales_person")?.setValue(this.ProformaInvoiceEditlist[0].user_firstname); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_expected_start_date")?.setValue(this.ProformaInvoiceEditlist[0].start_date); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_estimated_arrival_time")?.setValue(this.ProformaInvoiceEditlist[0].end_date); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_freight_terms")?.setValue(this.ProformaInvoiceEditlist[0].freight_terms); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_payment_terms")?.setValue(this.ProformaInvoiceEditlist[0].payment_terms); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_currency")?.setValue(this.ProformaInvoiceEditlist[0].currency_code); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_exchange_rate")?.setValue(this.ProformaInvoiceEditlist[0].exchange_rate); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_net_amount")?.setValue(this.ProformaInvoiceEditlist[0].total_price); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_overall_tax")?.setValue(this.ProformaInvoiceEditlist[0].gst_amount); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_total_amount_tax")?.setValue(this.ProformaInvoiceEditlist[0].total_amount); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_maximum_addon_amount")?.setValue(this.ProformaInvoiceEditlist[0].additionalcharges_amount); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_maximum_addon_discount")?.setValue(this.ProformaInvoiceEditlist[0].discount_amount); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_freight_charges")?.setValue(this.ProformaInvoiceEditlist[0].freight_charges); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_buyback_charges")?.setValue(this.ProformaInvoiceEditlist[0].buyback_charges); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_packing_charges")?.setValue(this.ProformaInvoiceEditlist[0].packing_charges); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_insurance_charges")?.setValue(this.ProformaInvoiceEditlist[0].insurance_charges); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_roundoff")?.setValue(this.ProformaInvoiceEditlist[0].roundoff); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_grandtotal")?.setValue(this.ProformaInvoiceEditlist[0].invoice_amount); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_advance_percentage")?.setValue(this.ProformaInvoiceEditlist[0].currency_code); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_advance_amount")?.setValue(this.ProformaInvoiceEditlist[0].invoicepercent_amount); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_advance_roundoff")?.setValue(this.ProformaInvoiceEditlist[0].advanceroundoff); 
      this.proformainvoiceeditform.get("edit_proforma_invoice_termsandconditions")?.setValue(this.ProformaInvoiceEditlist[0].termsandconditions); 
      this.proformainvoiceeditform.get("productgroup_name")?.setValue(this.ProformaInvoiceEditlist[0].productgroup_name);
      this.proformainvoiceeditform.get("product_name")?.setValue(this.ProformaInvoiceEditlist[0].product_name); 
      
      

    });
  }



  get editproformainvoicecontactnoControl() {
    return this.proformainvoiceeditform.get('edit_proforma_invoice_contact_no');
  }

  get editproformainvoiceemailControl() {
    return this.proformainvoiceeditform.get('edit_proforma_invoice_email_address');
  }

  back() {
    this.router.navigate(['/einvoice/ProformaInvoiceAdd']);
  }
  updateproformainvoice()
  {
    debugger;
    console.log(this.proformainvoiceeditform)

    var api = 'ProformaInvoice/UpdateProformainvoice';
    this.service.post(api, this.proformainvoiceeditform.value).subscribe((result: any) => {
      this.responsedata=result;
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

}
