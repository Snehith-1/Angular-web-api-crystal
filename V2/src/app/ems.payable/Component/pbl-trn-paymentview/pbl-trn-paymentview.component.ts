import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';


@Component({
  selector: 'app-pbl-trn-paymentview',
  templateUrl: './pbl-trn-paymentview.component.html',
  styleUrls: ['./pbl-trn-paymentview.component.scss']
})
export class PblTrnPaymentviewComponent {
  
  payment_lists: any;
  paymentview: any;
  payment_gid:any;
  responsedata: any;
  payment_list: any;
  payment_remarks: any;
  
  constructor(private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    
  }
   
    ngOnInit(): void {1
      this.paymentview= this.route.snapshot.paramMap.get('payment_gid');
      const secretKey = 'storyboarderp';
      const deencryptedParam = AES.decrypt(this.paymentview,secretKey).toString(enc.Utf8);
      console.log(deencryptedParam)
      this.GetPaymentview(deencryptedParam); 
      

    }
    GetPaymentview(payment_gid: any) {
      var url='PblTrnPaymentRpt/GetPaymentview'
      let param = {
        payment_gid : payment_gid,
      }
      this.service.getparams(url,param).subscribe((result:any)=>{
      this.payment_lists = result.paymentlists;
      //console.log(this.employeeedit_list)
  
    });
    
    var url = 'PblTrnPaymentRpt/getPaymenamount'
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.payment_list = this.responsedata.paymentamount_list;
     });
    }
    onback(){
      this.router.navigate(['/payable/PblTrnPaymentsummary'])
    }
}
