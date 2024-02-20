import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';




@Component({
  selector: 'app-pay-trn-loanview',
  templateUrl: './pay-trn-loanview.component.html',
  styleUrls: ['./pay-trn-loanview.component.scss']
})
export class PayTrnLoanviewComponent {
  loan: any;
  ViewLoanSummary_list:any [] = [];
  responsedata: any;

  constructor(private formBuilder: FormBuilder,private route:Router,private router:ActivatedRoute,public service :SocketService) { }

  ngOnInit(): void {
   const loan_gid =this.router.snapshot.paramMap.get('loan_gid');
    this.loan= loan_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.loan,secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.getViewLoanSummary(deencryptedParam);
  }
  getViewLoanSummary(loan_gid: any) {
    var url='PayTrnLoanSummary/getViewLoanSummary'
    let param = {
      loan_gid : loan_gid 
    }
    this.service.getparams(url,param).subscribe((result:any)=>{
    this.responsedata=result;
    this.ViewLoanSummary_list = result.getViewLoanSummary;   
    });
}
}
