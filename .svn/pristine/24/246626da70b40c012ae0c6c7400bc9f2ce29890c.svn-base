import { Component, DebugEventListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { FormGroup } from '@angular/forms';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-pmr-trn-request-for-quote-summary',
  templateUrl: './pmr-trn-request-for-quote-summary.component.html',
  styleUrls: ['./pmr-trn-request-for-quote-summary.component.scss']
})

export class PmrTrnRequestForQuoteSummaryComponent{
  
  Getrequestforquote_lists:any[]=[];
  responsedata: any;
  parameterValue1: any;
  company_code: any;
  enquiry_gid:any;
  Getrequestforquotegrid_lists:any[]=[];
 
  

  constructor(public service :SocketService,private router:Router,private ToastrService: ToastrService) {
    
  }


  ngOnInit(): void {
    this.GetRequestforQuoteSummary();
  }


  GetRequestforQuoteSummary(){
    var url = 'PmrTrnRequestforQuote/GetRequestforQuoteSummary'
    this.service.get(url).subscribe((result: any) => {
      $('#Getrequestforquote_lists').DataTable().destroy();
      this.responsedata = result;
      this.Getrequestforquote_lists = this.responsedata.Getrequestforquote_lists;
      setTimeout(() => {
        $('#Getrequestforquote_lists').DataTable();
      }, 1);
  
  
    });
  }
  onview(params:any){
    debugger
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
     this.router.navigate(['/pmr/PmrTrnRequestForQuoteView',encryptedParam])
     
  }

  onadd(){
    
    this.router.navigate(['/pmr/PmrTrnEnquiryAddSelect']);
  }
  Details(parameter: string,enquiry_gid: string){
    this.parameterValue1 = parameter;
    this.enquiry_gid = parameter;
    
    var url = 'PmrTrnRequestforQuote/GetRequestforQuoteSummarygrid'
    let param = {
      enquiry_gid: enquiry_gid
    }
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.Getrequestforquotegrid_lists = result.Getrequestforquotegrid_lists;
      console.log(this.Getrequestforquotegrid_lists)
      setTimeout(() => {
        $('#Getrequestforquotegrid_lists').DataTable();
      }, 1);
  
    });
  }

}

