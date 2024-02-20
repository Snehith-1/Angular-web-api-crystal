import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-rbl-trn-einvoice',
  templateUrl: './rbl-trn-einvoice.component.html',
  styleUrls: ['./rbl-trn-einvoice.component.scss']
})

export class RblTrnEinvoiceComponent {
  einvoicedtlform: FormGroup | any;
  invoice_gid: any;
  invoiceproductlist: any;
  responsedata: any;
  einvoicedata: any;

  mdltransdelIgst: any;
  mdltransdelregrev: any;
  mdltransdeltranstype: any;
  mdldocdeltype: any;

  ngOnInit(): void {
    const invoice_gid = this.route.snapshot.paramMap.get('invoice_gid');
    this.invoice_gid = invoice_gid;

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.invoice_gid, secretKey).toString(enc.Utf8);

    let param = {
      invoice_gid: deencryptedParam
    }

    var api = 'Einvoice/GetEditInvoiceProductSummary';
    this.service.getparams(api, param).subscribe((result: any) => {
      this.responsedata = result;
      this.invoiceproductlist = this.responsedata.editinvoiceproductsummarylist;
    });
    this.GetEInvoiceData();
  }

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) {
    this.einvoicedtlform = new FormGroup({
      invoice_gid: new FormControl(''),
      transactionDetails_TaxSch: new FormControl('', Validators.required),
      transactionDetails_SupTyp: new FormControl('', Validators.required),
      transactionDetails_RegRev: new FormControl('', Validators.required),
      transactionDetails_IgstOnIntra: new FormControl('', Validators.required),
      transactionDetails_trans_category: new FormControl('', Validators.required),
      transactionDetails_reverse_charge: new FormControl('', Validators.required),
      transactionDetails_tranaction_type: new FormControl('', Validators.required),
      documentDetails_Typ: new FormControl('', Validators.required),
      invoice_date: new FormControl('', Validators.required),
      invoicerefno: new FormControl('', Validators.required),
      documentDetails_payment_term: new FormControl('', Validators.required),
      documentDetails_due_date: new FormControl('', Validators.required),
      buyerDetails_Gstin: new FormControl('', Validators.required),
      buyerDetails_LglNm: new FormControl('', Validators.required),
      buyerDetails_cont_num: new FormControl('', Validators.required),
      buyerDetails_Address: new FormControl('', Validators.required),
      buyerDetails_Pin: new FormControl('', Validators.required),
      buyerDetails_remarks: new FormControl('', Validators.required),
      buyerDetails_Pos: new FormControl('', Validators.required),
      buyerDetails_cont_person: new FormControl('', Validators.required),
      buyerDetails_email: new FormControl('', Validators.required),
      buyerDetails_Loc: new FormControl('', Validators.required),
      buyerDetails_Stcd: new FormControl('', Validators.required),
      sellerDetails_Gstin: new FormControl('', Validators.required),
      sellerDetails_TrdNm: new FormControl('', Validators.required),
      sellerDetails_Loc: new FormControl('', Validators.required),
      sellerDetails_Stcd: new FormControl('', Validators.required),
      sellerDetails_email: new FormControl('', Validators.required),
      sellerDetails_LglNm: new FormControl('', Validators.required),
      sellerDetails_Address: new FormControl('', Validators.required),
      sellerDetails_Pin: new FormControl('', Validators.required),
      sellerDetails_cont_num: new FormControl('', Validators.required),
      dispatchDetails_Nm: new FormControl('', Validators.required),
      dispatchDetails_Loc: new FormControl('', Validators.required),
      dispatchDetails_Stcd: new FormControl('', Validators.required),
      dispatchDetails_Address: new FormControl('', Validators.required),
      dispatchDetails_Pin: new FormControl('', Validators.required),
      shipDetails_Gstin: new FormControl('', Validators.required),
      shipDetails_TrdNm: new FormControl('', Validators.required),
      shipDetails_Loc: new FormControl('', Validators.required),
      shipDetails_Stcd: new FormControl('', Validators.required),
      shipDetails_LglNm: new FormControl('', Validators.required),
      shipDetails_Address: new FormControl('', Validators.required),
      shipDetails_Pin: new FormControl('', Validators.required),     
      
      netamount: new FormControl(''),
      addoncharges: new FormControl(''),
      invoicediscountamount: new FormControl(''),      
      frieghtcharges: new FormControl(''),
      buybackcharges: new FormControl(''),
      packing_charges: new FormControl(''),
      insurancecharges: new FormControl(''),
      roundoff: new FormControl(''),
      invoice_amount: new FormControl(''),
    })
  }

  get transdlttaxschemeControl() {
    return this.einvoicedtlform.get('transactionDetails_TaxSch');
  }

  get transdltecommgstinControl() {
    return this.einvoicedtlform.get('transactionDetails_ecomm_gstin');
  }

  get transdlttranscatControl() {
    return this.einvoicedtlform.get('transactionDetails_trans_category');
  }

  get sellerDetails_sellergstinControl() {
    return this.einvoicedtlform.get('sellerDetails_Gstin');
  }

  get sellerDetails_tradenameControl() {
    return this.einvoicedtlform.get('sellerDetails_TrdNm');
  }

  get sellerDetails_legalnameControl() {
    return this.einvoicedtlform.get('sellerDetails_LglNm');
  }

  get sellerDetails_pincodeControl() {
    return this.einvoicedtlform.get('sellerDetails_Pin');
  }

  get dispatchDetails_nameControl() {
    return this.einvoicedtlform.get('dispatchDetails_Nm');
  }

  get dispatchDetails_locationControl() {
    return this.einvoicedtlform.get('dispatchDetails_Loc');
  }

  get dispatchDetails_stateControl() {
    return this.einvoicedtlform.get('dispatchDetails_Stcd');
  }

  get dispatchDetails_addressControl() {
    return this.einvoicedtlform.get('dispatchDetails_Address');
  }

  get dispatchDetails_pincodedisControl() {
    return this.einvoicedtlform.get('dispatchDetails_Pin');
  }

  get shipDetails_gstinControl() {
    return this.einvoicedtlform.get('shipDetails_Gstin');
  }

  get shipDetails_tradenameshipControl() {
    return this.einvoicedtlform.get('shipDetails_TrdNm');
  }

  get shipDetails_locationshipControl() {
    return this.einvoicedtlform.get('shipDetails_Loc');
  }

  get shipDetails_stateshipControl() {
    return this.einvoicedtlform.get('shipDetails_Stcd');
  }

  get shipDetails_legalnameshipControl() {
    return this.einvoicedtlform.get('shipDetails_LglNm');
  }

  get shipDetails_addressControl() {
    return this.einvoicedtlform.get('shipDetails_Address');
  }

  get shipDetails_pincodeControl() {
    return this.einvoicedtlform.get('shipDetails_Pin');
  }

  GetEInvoiceData() {
    const invoice_gid = this.route.snapshot.paramMap.get('invoice_gid');
    this.invoice_gid = invoice_gid;

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.invoice_gid, secretKey).toString(enc.Utf8);

    let param = {
      invoice_gid: deencryptedParam
    }

    var api = 'Einvoice/GetEInvoicedata';

    console.log(param);
    this.service.getparams(api, param).subscribe((result: any) => {
      this.responsedata = result;
      this.einvoicedata = result;
      this.einvoicedtlform.get("invoice_gid")?.setValue(this.einvoicedata.invoice_gid);
      this.einvoicedtlform.get("invoicerefno")?.setValue(this.einvoicedata.invoice_refno);
      this.einvoicedtlform.get("invoice_date")?.setValue(this.einvoicedata.invoice_date);
      this.einvoicedtlform.get("documentDetails_payment_term")?.setValue(this.einvoicedata.payment_term);
      this.einvoicedtlform.get("documentDetails_due_date")?.setValue(this.einvoicedata.payment_date);

      this.einvoicedtlform.get("buyerDetails_Gstin")?.setValue(this.einvoicedata.gst_number);
      this.einvoicedtlform.get("buyerDetails_Pos")?.setValue(this.einvoicedata.buyerDetails_Pos);
      this.einvoicedtlform.get("buyerDetails_LglNm")?.setValue(this.einvoicedata.customer_name);
      this.einvoicedtlform.get("buyerDetails_cont_person")?.setValue(this.einvoicedata.customer_contactperson);
      this.einvoicedtlform.get("buyerDetails_cont_num")?.setValue(this.einvoicedata.mobile);
      this.einvoicedtlform.get("buyerDetails_email")?.setValue(this.einvoicedata.customer_email);
      this.einvoicedtlform.get("buyerDetails_Address")?.setValue(this.einvoicedata.customer_address);
      this.einvoicedtlform.get("buyerDetails_Loc")?.setValue(this.einvoicedata.customer_city);
      this.einvoicedtlform.get("buyerDetails_Pin")?.setValue(this.einvoicedata.customer_pin);
      this.einvoicedtlform.get("buyerDetails_Stcd")?.setValue(this.einvoicedata.customer_state);
      this.einvoicedtlform.get("buyerDetails_remarks")?.setValue(this.einvoicedata.invoice_remarks);      
      
      this.einvoicedtlform.get("state")?.setValue(this.einvoicedata.state);
      
      this.einvoicedtlform.get("sellerDetails_Gstin")?.setValue(this.einvoicedata.gst_no);
      this.einvoicedtlform.get("sellerDetails_LglNm")?.setValue(this.einvoicedata.company_name);
      this.einvoicedtlform.get("sellerDetails_TrdNm")?.setValue(this.einvoicedata.company_name);
      this.einvoicedtlform.get("sellerDetails_Address")?.setValue(this.einvoicedata.company_address);
      this.einvoicedtlform.get("sellerDetails_cont_num")?.setValue(this.einvoicedata.sellercontact_number);
      this.einvoicedtlform.get("sellerDetails_email")?.setValue(this.einvoicedata.company_mail);
      this.einvoicedtlform.get("sellerDetails_Loc")?.setValue(this.einvoicedata.city);
      this.einvoicedtlform.get("sellerDetails_Pin")?.setValue(this.einvoicedata.postalcode);
      this.einvoicedtlform.get("sellerDetails_Stcd")?.setValue(this.einvoicedata.state);      
      
      this.einvoicedtlform.get("dispatchDetails_Nm")?.setValue(this.einvoicedata.dispatchDetails_Nm);
      this.einvoicedtlform.get("dispatchDetails_Address")?.setValue(this.einvoicedata.dispatchDetails_Address);
      this.einvoicedtlform.get("dispatchDetails_Loc")?.setValue(this.einvoicedata.dispatchDetails_Loc);
      this.einvoicedtlform.get("dispatchDetails_Pin")?.setValue(this.einvoicedata.dispatchDetails_Pin);
      this.einvoicedtlform.get("dispatchDetails_Stcd")?.setValue(this.einvoicedata.dispatchDetails_Stcd);

      this.einvoicedtlform.get("shipDetails_Gstin")?.setValue(this.einvoicedata.shipDetails_Gstin);
      this.einvoicedtlform.get("shipDetails_LglNm")?.setValue(this.einvoicedata.shipDetails_LglNm);
      this.einvoicedtlform.get("shipDetails_TrdNm")?.setValue(this.einvoicedata.shipDetails_TrdNm);
      this.einvoicedtlform.get("shipDetails_Address")?.setValue(this.einvoicedata.shipDetails_Address);
      this.einvoicedtlform.get("shipDetails_Loc")?.setValue(this.einvoicedata.shipDetails_Loc);
      this.einvoicedtlform.get("shipDetails_Pin")?.setValue(this.einvoicedata.shipDetails_Pin);
      this.einvoicedtlform.get("shipDetails_Stcd")?.setValue(this.einvoicedata.shipDetails_Stcd);      
      
      this.einvoicedtlform.get("netamount")?.setValue(this.einvoicedata.netamount);
      this.einvoicedtlform.get("addoncharges")?.setValue(this.einvoicedata.addoncharges);
      this.einvoicedtlform.get("invoicediscountamount")?.setValue(this.einvoicedata.invoicediscountamount);
      this.einvoicedtlform.get("frieghtcharges")?.setValue(this.einvoicedata.frieghtcharges);      
      this.einvoicedtlform.get("packing_charges")?.setValue(this.einvoicedata.packing_charges);
      this.einvoicedtlform.get("insurancecharges")?.setValue(this.einvoicedata.insurancecharges);
      this.einvoicedtlform.get("roundoff")?.setValue(this.einvoicedata.roundoff);
      this.einvoicedtlform.get("invoice_amount")?.setValue(this.einvoicedata.invoice_amount);
      // this.einvoicedtlform.get("buybackcharges")?.setValue(this.einvoicedata.buybackcharges);
    });
  }

  submit() {
    console.log(this.einvoicedtlform)

    var api = 'Einvoice/EinvoiceGeneration';
    this.service.post(api, this.einvoicedtlform.value).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message, undefined, {
          timeOut: 200000, positionClass: 'toast-bottom-right',
        })
      }
      else {
        this.ToastrService.success('IRN Generated successfully', 'Success', {
          positionClass: 'toast-top-center',
        });
        this.router.navigate(['/einvoice/Invoice']);
      }
    });
  }
  
  back() {
    this.router.navigate(['/einvoice/Invoice']);
  }
}
