import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-pmr-trn-invoiceview',
  templateUrl: './pmr-trn-invoiceview.component.html',
  styleUrls: ['./pmr-trn-invoiceview.component.scss']
})
export class PmrTrnInvoiceviewComponent {

  invoice_lists: any;
  invoiceview: any;
  invoice_gid:any;

  constructor(private router:ActivatedRoute,public service :SocketService ) { 

  }
  ngOnInit(): void {1
    
        this.invoiceview= this.router.snapshot.paramMap.get('invoice_gid');
        const secretKey = 'storyboarderp';
        const deencryptedParam = AES.decrypt(this.invoiceview,secretKey).toString(enc.Utf8);
        console.log(deencryptedParam)
        this.GetPmrTrnInvoiceview(deencryptedParam);    
      }
      GetPmrTrnInvoiceview(invoice_gid: any) {
        var url='PmrTrnInvoice/GetPmrTrnInvoiceview'
        let param = {
          invoice_gid : invoice_gid
        }
        this.service.getparams(url,param).subscribe((result:any)=>{
        this.invoice_lists = result.invoice_lists;
        //console.log(this.employeeedit_list)
    
      });
    
      }
}
