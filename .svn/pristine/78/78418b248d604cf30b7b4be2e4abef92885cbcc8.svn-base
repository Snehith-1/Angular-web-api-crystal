import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AES } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-pmr-trn-invoice-summary',
  templateUrl: './pmr-trn-invoice-summary.component.html',
  styleUrls: ['./pmr-trn-invoice-summary.component.scss']
})
export class PmrTrnInvoiceSummaryComponent {
  invoice_list: any[] = [];
  responsedata: any;

  constructor(public service :SocketService,private route:Router,
    public NgxSpinnerService:NgxSpinnerService,private ToastrService: ToastrService) {
    
    
  }
  ngOnInit(): void {
    this.GetInvoiceSummary();}

    GetInvoiceSummary(){
   
    var url='PmrTrnInvoice/GetInvoiceSummary'
    this.NgxSpinnerService.show();
    this.service.get(url).subscribe((result:any)=>{
      this.NgxSpinnerService.hide();
      this.responsedata=result;
      this.invoice_list = this.responsedata.invoice_lista;  
    
    
   
  });
  }
  
  onview(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.route.navigate(['/payable/PmrTrnInvoiceview',encryptedParam]) 
  }

  onedit(params:any){
    
  }

  openModaldelete(parameter: string) {
  }
}
