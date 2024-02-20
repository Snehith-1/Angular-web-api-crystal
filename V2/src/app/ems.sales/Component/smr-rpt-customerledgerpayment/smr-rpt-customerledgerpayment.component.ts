import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { param } from 'jquery';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-smr-rpt-customerledgerpayment',
  templateUrl: './smr-rpt-customerledgerpayment.component.html',
  styleUrls: ['./smr-rpt-customerledgerpayment.component.scss']
})
export class SmrRptCustomerledgerpaymentComponent {
  data:any;
  responsedata: any;
  customer_gid:any;
  customerledgerpayment_list:any[]=[];
  customerledgercount_list:any[]=[];
  customerledgerpaymentdetail_list:any[]=[];

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService,public NgxSpinnerService:NgxSpinnerService, private router: ActivatedRoute, private route: Router, public service: SocketService) {
  
  }
  ngOnInit(): void {
    debugger
    let customer_gid = this.router.snapshot.paramMap.get('customer_gid');
   
    this.customer_gid = customer_gid

    const secretKey = 'storyboarderp';

    const deencryptedParam = AES.decrypt(this.customer_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    customer_gid = deencryptedParam
    this.GetcustomerledgerpaymentSummary(customer_gid)

}
GetcustomerledgerpaymentSummary(customer_gid :any){
  debugger
    var url = 'SmrRptcustomerledgerdetail/GetCustomerledgerpayment'
    this.NgxSpinnerService.show();
    let param ={
      customer_gid : customer_gid
    } 
      this.service.getparams(url,param).subscribe((result: any) => {
      $('#customerledgerpayment_list').DataTable().destroy();
      this.responsedata = result;
      this.customerledgerpayment_list = result.customerledgerpayment_list;
      setTimeout(() => {
        $('#customerledgerpayment_list').DataTable();
      }, 1);
      this.NgxSpinnerService.hide();
  
  
    })
    
    
    var url  = 'SmrRptcustomerledgerdetail/Getcustomerledgercount';
    let params ={
      customer_gid : customer_gid
    }
    this.service.getparams(url,params).subscribe((result:any) => {
    this.responsedata = result;
    this.customerledgercount_list = this.responsedata.customerledgercount_list; 
    console.log(this.customerledgercount_list,'testdata');
    });
  
  
  }
  ondetail(payment_gid:any){
    var url = 'SmrRptcustomerledgerdetail/GetCustomerledgerpaymentdetail'
    let param ={
      payment_gid : payment_gid
    } 
        this.service.getparams(url,param).subscribe((result: any) => {
      $('#customerledgerpaymentdetail_list').DataTable().destroy();
      this.responsedata = result;
      this.customerledgerpaymentdetail_list = result.customerledgerpaymentdetail_list;
      setTimeout(() => {
        $('#customerledgerpaymentdetail_list').DataTable();
      }, 1);
  
  
    })
  }
  

}