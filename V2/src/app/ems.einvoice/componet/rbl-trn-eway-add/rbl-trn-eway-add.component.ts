import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { Options } from 'flatpickr/dist/types/options';
import flatpickr from 'flatpickr';

@Component({
  selector: 'app-rbl-trn-eway-add',
  templateUrl: './rbl-trn-eway-add.component.html',
  styleUrls: ['./rbl-trn-eway-add.component.scss']
})

export class RblTrnEwayAddComponent {

  ewaybillform: FormGroup | any;
  invoice_gid: any;
  responsedata: any;
  invoiceproductlist: any;
  ewaybillInvoicedata: any;

  ewaysuptype: any;
  ewaysubtype: any;
  ewaydoctype: any;
  ewaytranstype: any;
  ewaytransmode: any;
  ewayvehicletype: any;

  ngOnInit() {

    const options: Options = {
      dateFormat: 'd-m-Y',    
    };

    flatpickr('.date-picker', options);

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

    this.GetewaybillInvoicedata();
  }

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) {
    this.ewaybillform = new FormGroup({
      ewaybill_invoice_gid: new FormControl(''),
      ewaybill_supply_type: new FormControl('', [Validators.required]),
      ewaybill_sub_type: new FormControl('', [Validators.required]),
      ewaybill_document_type: new FormControl('', [Validators.required]),
      ewaybill_document_no: new FormControl('', [Validators.required]),
      ewaybill_document_date: new FormControl('', [Validators.required]),
      ewaybill_transaction_type: new FormControl('', [Validators.required]),
      ewaybill_bill_fromname: new FormControl(''),
      ewaybill_bill_fromgstin: new FormControl(''),
      ewaybill_bill_fromstate: new FormControl(''),
      ewaybill_dispatch_fromaddress: new FormControl(''),
      ewaybill_dispatch_fromplace: new FormControl(''),
      ewaybill_dispatch_frompincode: new FormControl(''),
      ewaybill_dispatch_fromstate: new FormControl(''),
      ewaybill_bill_toname: new FormControl(''),
      ewaybill_bill_togstin: new FormControl(''),
      ewaybill_bill_tostate: new FormControl(''),
      ewaybill_ship_toaddress: new FormControl(''),
      ewaybill_ship_toplace: new FormControl(''),
      ewaybill_ship_topincode: new FormControl(''),
      ewaybill_ship_tostate: new FormControl(''),
      ewaybill_irn: new FormControl(''),
      ewaybill_invoice_ref_no: new FormControl(''),
      ewaybill_invoice_amount: new FormControl(''),      
      ewaybill_transporter_id: new FormControl(''),
      ewaybill_transporter_name: new FormControl(''),
      ewaybill_approximate_distance: new FormControl('', [Validators.required]),
      ewaybill_transport_mode: new FormControl(''),
      ewaybill_vehicle_type: new FormControl(''),
      ewaybill_vehicle_no: new FormControl(''),
      ewaybill_transporter_doc_no: new FormControl(''),
      ewaybill_transporter_date: new FormControl(''),
    })
  }

  get ewaybillsupplytypeControl() {
    return this.ewaybillform.get('ewaybill_supply_type');
  }

  get ewaybillsubtypeControl() {
    return this.ewaybillform.get('ewaybill_sub_type');
  }

  get ewaybilldocumenttypeControl() {
    return this.ewaybillform.get('ewaybill_document_type');
  }
  
  get ewaybilldocumentnoControl() {
    return this.ewaybillform.get('ewaybill_document_no');
  }

  get ewaybilldocumentdateControl() {
    return this.ewaybillform.get('ewaybill_document_date');
  }

  get ewaybilltransactiontypeControl() {
    return this.ewaybillform.get('ewaybill_transaction_type');
  }

  get ewaybillapproximatedistanceControl() {
    return this.ewaybillform.get('ewaybill_approximate_distance');
  }

  GetewaybillInvoicedata() {
    const invoice_gid = this.route.snapshot.paramMap.get('invoice_gid');
    this.invoice_gid = invoice_gid;

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.invoice_gid, secretKey).toString(enc.Utf8);

    let param = {
      invoice_gid: deencryptedParam
    }

    var api = 'Einvoice/GetEInvoicedata';

    this.service.getparams(api, param).subscribe((result: any) => {
      this.responsedata = result;
      this.ewaybillInvoicedata = result;

      this.ewaybillform.get("ewaybill_bill_fromname")?.setValue(this.ewaybillInvoicedata.company_name);
      this.ewaybillform.get("ewaybill_bill_fromgstin")?.setValue(this.ewaybillInvoicedata.gst_no);
      this.ewaybillform.get("ewaybill_bill_fromstate")?.setValue(this.ewaybillInvoicedata.state);
      this.ewaybillform.get("ewaybill_dispatch_fromaddress")?.setValue(this.ewaybillInvoicedata.dispatchDetails_Address);
      this.ewaybillform.get("ewaybill_dispatch_fromplace")?.setValue(this.ewaybillInvoicedata.dispatchDetails_Loc);
      this.ewaybillform.get("ewaybill_dispatch_frompincode")?.setValue(this.ewaybillInvoicedata.dispatchDetails_Pin);
      this.ewaybillform.get("ewaybill_dispatch_fromstate")?.setValue(this.ewaybillInvoicedata.dispatchDetails_Stcd);
      this.ewaybillform.get("ewaybill_bill_toname")?.setValue(this.ewaybillInvoicedata.customer_name);
      this.ewaybillform.get("ewaybill_bill_togstin")?.setValue(this.ewaybillInvoicedata.gst_number);
      this.ewaybillform.get("ewaybill_bill_tostate")?.setValue(this.ewaybillInvoicedata.customer_state);      
      this.ewaybillform.get("ewaybill_ship_toaddress")?.setValue(this.ewaybillInvoicedata.shipDetails_Address);
      this.ewaybillform.get("ewaybill_ship_toplace")?.setValue(this.ewaybillInvoicedata.shipDetails_Loc);
      this.ewaybillform.get("ewaybill_ship_topincode")?.setValue(this.ewaybillInvoicedata.shipDetails_Pin);
      this.ewaybillform.get("ewaybill_ship_tostate")?.setValue(this.ewaybillInvoicedata.shipDetails_Stcd);
      this.ewaybillform.get("ewaybill_invoice_gid")?.setValue(this.ewaybillInvoicedata.invoice_gid);
      this.ewaybillform.get("ewaybill_irn")?.setValue(this.ewaybillInvoicedata.irn);
      this.ewaybillform.get("ewaybill_invoice_ref_no")?.setValue(this.ewaybillInvoicedata.invoice_refno);
      this.ewaybillform.get("ewaybill_invoice_amount")?.setValue(this.ewaybillInvoicedata.invoice_amount);
    });    
  }

  ewaybillSubmit() {
    var api = 'Ewaybill/Addewaybill';

    this.service.post(api, this.ewaybillform.value).subscribe((result: any) => {
      if(result.status == false)
      {        
        this.ToastrService.warning(result.message)       
      }
      else
      {
        this.router.navigate(['/einvoice/EwaySummary'])
        this.ToastrService.success(result.message)
      }
    })
  }

  back() {
    this.router.navigate(['/einvoice/EwaySummary']);
  }
}
