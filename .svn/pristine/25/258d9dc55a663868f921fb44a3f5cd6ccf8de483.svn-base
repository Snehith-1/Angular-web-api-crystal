import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';

@Component({
  selector: 'app-pmr-trn-request-for-quote-view',
  templateUrl: './pmr-trn-request-for-quote-view.component.html',
  styleUrls: ['./pmr-trn-request-for-quote-view.component.scss']
})
export class PmrTrnRequestForQuoteViewComponent  {

  requestquote_list: any [] = [];
  requestquotedetails_list: any [] = [];
  requestview:any;
  responsedata: any;
  constructor(private fb: FormBuilder,private route: ActivatedRoute,private router: Router,private service: SocketService,private ToastrService: ToastrService,) {
  }
  ngOnInit(): void {
  const requestview =this.route.snapshot.paramMap.get('enquiry_gid');
  this.requestview= requestview;
  const secretKey = 'storyboarderp';
  const deencryptedParam = AES.decrypt(this.requestview,secretKey).toString(enc.Utf8);
  console.log(deencryptedParam)
  this.GetViewRequestForQuotation(deencryptedParam);
  }

  GetViewRequestForQuotation(enquiry_gid: any) {
    var url='PmrTrnRequestforQuote/GetRequestForQuotationView'
    let param = {
      enquiry_gid : enquiry_gid 
    }
    this.service.getparams(url,param).subscribe((result:any)=>{
    this.responsedata=result;
    this.requestquote_list = result.RFQview_list; 
    this.requestquotedetails_list = result.RFQview_list

    });
  }
}
