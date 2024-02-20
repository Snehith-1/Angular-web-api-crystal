import { Component, ElementRef, Renderer2 } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-rbl-trn-invoiceview-boba',
  templateUrl: './rbl-trn-invoiceview-boba.component.html',
  styleUrls: ['./rbl-trn-invoiceview-boba.component.scss']
})
export class RblTrnInvoiceviewBobaComponent {

  invoice_gid: any;
  responsedata: any;
  viewinvoicelist: any;
  grand_total: any;
  termsandconditions:any;
  termsandcondition:any;
  leadbank_gid:any;
  leadbankcontact_gid:any;
  lead2campaign_gid:any;
  lspage:any;


  constructor(private renderer: Renderer2, private el: ElementRef, public service: SocketService, private route: Router, private router: ActivatedRoute) {}

  ngOnInit(): void {
    debugger
    const invoice_gid = this.router.snapshot.paramMap.get('invoice_gid');
    this.invoice_gid = invoice_gid
    const leadbank_gid = this.router.snapshot.paramMap.get('leadbank_gid');
    this.leadbank_gid = leadbank_gid
    const lead2campaign_gid = this.router.snapshot.paramMap.get('lead2campaign_gid');
    this.lead2campaign_gid = lead2campaign_gid
    const lspage = this.router.snapshot.paramMap.get('lspage'); 
    this.lspage = lspage    

    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.invoice_gid, secretKey).toString(enc.Utf8);
    // this.leadbank_gid = AES.decrypt(this.leadbank_gid,secretKey).toString(enc.Utf8);
    // this.leadbankcontact_gid = AES.decrypt(this.leadbankcontact_gid,secretKey).toString(enc.Utf8);
    // this.lead2campaign_gid = AES.decrypt(this.lead2campaign_gid,secretKey).toString(enc.Utf8);
    // this.lspage = AES.decrypt(this.lspage,secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.viewinvoice(deencryptedParam);
  }

  // viewinvoice(invoice_gid: any) {
  //   debugger
  //   var url = 'Einvoice/viewinvoice';
  //   this.invoice_gid = invoice_gid
  //   var params={
  //     invoice_gid : invoice_gid
  //   }
  //   this.service.getparams(url, params).subscribe((result: any) => {
  //     this.responsedata = result;
  //     this.viewinvoicelist = result.viewinvoice_list;
  //     this.termsandconditions = result.viewinvoice_list[0].termsandconditions;
  //     console.log(this.viewinvoicelist);
  //   });
  // }
  
  viewinvoice(invoice_gid: any) {
    debugger
    var url = 'Einvoice/viewinvoice';
    this.invoice_gid = invoice_gid
    var params={
      invoice_gid : invoice_gid
    }
    this.service.getparams(url, params).subscribe((result: any) => {
      this.responsedata = result;
      this.viewinvoicelist = result.viewinvoice_list;
      this.termsandconditions = result.viewinvoice_list[0].termsandconditions;
      // Convert HTML to plain text
      this.termsandcondition = this.convertHtmlToText(this.termsandconditions);
      console.log(this.viewinvoicelist);
    });

  // back(){
    
  // }
}

convertHtmlToText(html: string): string {
    let temp = document.createElement("div");
    temp.innerHTML = html;
    return temp.textContent || temp.innerText || "";
}
// back() {  
   
//   const secretKey = 'storyboarderp';


//   const lspage1 = AES.encrypt(this.lspage, secretKey).toString();
 
//   if (this.lspage == 'MM-Total') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'MM-Upcoming') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'MM-Lapsed') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'MM-Longest') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'MM-New') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'MM-Prospect') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'MM-Potential') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'MM-mtd') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'MM-ytd') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'MM-Customer') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'MM-Drop') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'My-Today') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'My-New') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'My-Prospect') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'My-Potential') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'My-Customer') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'My-Drop') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'My-All') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else if (this.lspage == 'My-Upcoming') {

//     this.router.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

//   }

//   else {
//       this.router.navigate(['/einvoice/Invoice-Summary']);
//   }
  
// }
back() {  
   
  var api = 'Einvoice/GetDeleteInvoicebackProductSummary';

  this.service.get(api).subscribe((result: any) => {
    this.responsedata = result;
  })
    const secretKey = 'storyboarderp';


const lspage1 = AES.encrypt(this.lspage, secretKey).toString();

if (this.lspage == 'MM-Total') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'MM-Upcoming') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'MM-Lapsed') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'MM-Longest') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'MM-New') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'MM-Prospect') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'MM-Potential') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'MM-mtd') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'MM-ytd') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'MM-Customer') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'MM-Drop') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'My-Today') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'My-New') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'My-Prospect') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'My-Potential') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'My-Customer') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'My-Drop') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'My-All') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else if (this.lspage == 'My-Upcoming') {

  this.route.navigate(['/crm/CrmTrn360view',this.leadbank_gid,this.lead2campaign_gid,lspage1]);

}

else {
    this.route.navigate(['/einvoice/Invoice-Summary']);
}
  
}
}
