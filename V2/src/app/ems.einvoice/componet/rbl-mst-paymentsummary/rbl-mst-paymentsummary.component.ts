import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../../environments/environment.development';
import { AES } from 'crypto-js';
@Component({
  selector: 'app-rbl-mst-paymentsummary',
  templateUrl: './rbl-mst-paymentsummary.component.html',
  styleUrls: ['./rbl-mst-paymentsummary.component.scss']
})
export class RblMstPaymentsummaryComponent {

  receipt: any;
  responsedata : any;
  parameterValue : any;
  company_code : any;
  parameterValue1: any;
  invoice_list : any[] = [];
  invoice_gid: any;


  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private service: SocketService, private ToastrService: ToastrService) { }

  ngOnInit(): void {
    var api = 'Receipt/GetReceiptSummary';
    this.service.get(api).subscribe((result: any) => {
      this.responsedata = result;
      this.receipt = this.responsedata.receiptsummary_list;
      setTimeout(() => {
        $('#receipt').DataTable();
      }, 1);
    });
  }

  openModaldelete(parameter: string) {
    debugger;
    this.parameterValue = parameter
  }

  ondelete() {
    debugger;
    console.log(this.parameterValue);
    var url3 = 'Receipt/deleteReceiptSummary'
    this.service.getid(url3, this.parameterValue).subscribe((result: any) => {
      if (result.status == false) {
        this.ToastrService.warning(result.message)
      }
      else {
        this.ToastrService.success(result.message)
      }
    });
  }

  Details(parameter: string,invoice_gid: string){
    this.parameterValue1 = parameter;
    this.invoice_gid = parameter;
  
    var url='Receipt/Getreceiptdetails'
      let param = {
        invoice_gid : invoice_gid 
      }
      this.service.getparams(url,param).subscribe((result:any)=>{
      this.responsedata=result;
      this.invoice_list = result.invoice_list;   
      });
    
  }

  Getreceiptdetails() {
    
    var url = 'Receipt/Getreceiptdetails'
    this.service.get(url).subscribe((result: any) => {
      $('#invoice_list').DataTable().destroy();
      this.responsedata = result;
      this.invoice_list = this.responsedata.invoice_list;
      setTimeout(() => {
        $('#invoice_list').DataTable();
      }, 1);
  
  
    })
  }
  

 addreceipt() {
    this.router.navigate(['/einvoice/ReceiptAdd'])
  }

 PrintPDF(payment_gid: string,payment_type:string) {
    this.company_code = localStorage.getItem('c_code')
    window.location.href = "http://" + environment.host + "/Print/EMS_print/rbl_mst_paymentsummaryreceiptrpt.aspx?payment_gid=" + payment_gid + "&companycode=" + this.company_code+ "&payment_type=" + payment_type
  }

}
