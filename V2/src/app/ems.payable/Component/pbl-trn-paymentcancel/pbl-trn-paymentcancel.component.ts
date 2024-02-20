import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SelectionModel } from '@angular/cdk/collections';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { AES, enc } from 'crypto-js';
interface IPaymentcancel {
  payment_date: string;
  entered_by: string;
  email: string;
  contact_no: string;
  vendor_name: string;
  vendor_contact: string;
  vendor_address: string;
  fax: string;
  payment_remarks: string;
  payment_note: string;
  payment_mode: string;
  transaction_refno: string;
}
@Component({
  selector: 'app-pbl-trn-paymentcancel',
  templateUrl: './pbl-trn-paymentcancel.component.html',
  styleUrls: ['./pbl-trn-paymentcancel.component.scss']
})
export class PblTrnPaymentcancelComponent {
  SinglePaymentReport!: IPaymentcancel;
  responsedata: any;
  PaymentCancel!: IPaymentcancel;
  payment_gid:any;
  reactiveFormcancel!: FormGroup;
  vendor_lists: any[] = [];
  payment_cancel:any;
  payment_list: any[] = [];
  grandtotal: any;

  
  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    this.PaymentCancel = {} as IPaymentcancel;
  }

  ngOnInit(): void {
    this.reactiveFormcancel = new FormGroup({
    
      payment_date: new FormControl(''),
      entered_by: new FormControl(''),
      email: new FormControl(''),
      contact_no: new FormControl(''),
      vendor_name: new FormControl(''),
      vendor_contact: new FormControl(''),
      vendor_address: new FormControl(''),
      fax: new FormControl(''),
      payment_remarks: new FormControl(''),
      payment_note: new FormControl(''),
  
      
    
    
    });
   debugger
    this.payment_gid = this.route.snapshot.paramMap.get('payment_gid');
  
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.payment_gid, secretKey).toString(enc.Utf8);
    this.payment_gid = deencryptedParam;

    console.log(deencryptedParam)
    this.GetpaymentCancel(deencryptedParam) 

    const currentDate = new Date().toISOString().split('T')[0];
        this.reactiveFormcancel.get('payment_date')?.setValue(currentDate);

  }
  GetpaymentCancel(payment_gid:any){
    debugger
    var param={
      payment_gid :payment_gid
    } 
    var url = 'PblTrnPaymentRpt/GetpaymentCancel'
    this.service.getparams(url, param).subscribe((result: any) => {
      this.responsedata = result;
      this.vendor_lists = this.responsedata.paymentcancel;
      this. grandtotal=this.vendor_lists[0].payment_total

     });
     
     var url = 'PblTrnPaymentRpt/getPaymenamount'
     this.service.getparams(url, param).subscribe((result: any) => {
       this.responsedata = result;
       this.payment_list = this.responsedata.paymentamount_list;
      });

  }

  cancelclick(){

    var url='PblTrnPaymentRpt/paymentcancelsubmit'

    var param = {
      payment_gid:  this.payment_gid
    }
this.service.getparams(url,param).subscribe((result: any) => {
  if (result.status === false) {
    this.ToastrService.warning(result.message);
  } else {
    this.ToastrService.success(result.message);
    this.router.navigate(['/payable/PblTrnPaymentsummary'])
  }
    });
  }
  onback(){
     this.router.navigate(['/payable/PblTrnPaymentsummary']);
  }

  }
 




