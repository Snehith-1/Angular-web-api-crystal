import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../../environments/environment.development';
import { AES } from 'crypto-js';

@Component({
  selector: 'app-rbl-trn-proforma-invoice',
  templateUrl: './rbl-trn-proforma-invoice.component.html',
  styleUrls: ['./rbl-trn-proforma-invoice.component.scss']
})
export class RblTrnProformaInvoiceComponent {
  response_data: any;
  proformainvoice: any;
  company_code: any;
  parameterValue1 : any;
  invoice_gid : any;
  proformaproduct_list : any [] = [];



  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) { }

  ngOnInit(): void {
    var api = 'ProformaInvoice/GetProformaInvoiceSummary';
    this.service.get(api).subscribe((result: any) => {
      this.response_data = result;
      this.proformainvoice = this.response_data.proformainvoicesummary_list;
      setTimeout(() => {
        $('#proformainvoice').DataTable();
      }, 1);
    });
  }

  PrintPDF(invoice_gid: string) {
    debugger
    this.company_code = localStorage.getItem('c_code')
    window.location.href = "http://" + environment.host + "/Print/EMS_print/rbl_trn_proformainvoice.aspx?invoicegid=" + invoice_gid + "&companycode=" + this.company_code
  }

  raiseproforma() {
    this.router.navigate(['/einvoice/ProformaInvoiceAdd'])
  }
  editproformainvoice(params: string){
    debugger;
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/einvoice/ProformaInvoiceEdit',encryptedParam])
  }
  Mail(params : string)
  {
    debugger;
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/einvoice/ProformaInvoiceMail',encryptedParam])
  }

  proformaadvancereceipt(params: any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.router.navigate(['/einvoice/ProformaInvoiceAdvanceReceipt', encryptedParam])
  }

  Details(parameter: string,invoice_gid: string){
    this.parameterValue1 = parameter;
    this.invoice_gid = parameter;
  
    var url='ProformaInvoice/GetProductdetails'
      let param = {
        invoice_gid : invoice_gid 
      }
      this.service.getparams(url,param).subscribe((result:any)=>{
      this.response_data=result;
      this.proformaproduct_list = result.proformaproduct_list;   
      });
    }

    GetProductdetails() {
    
      var url = 'ProformaInvoice/GetProductdetails'
      this.service.get(url).subscribe((result: any) => {
        $('#proformaproduct_list').DataTable().destroy();
        this.response_data = result;
        this.proformaproduct_list = this.response_data.proformaproduct_list;
        setTimeout(() => {
          $('#proformaproduct_list').DataTable();
        }, 1);
    
    
      })
    }
    
  


}