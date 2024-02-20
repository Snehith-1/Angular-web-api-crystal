import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { param } from 'jquery';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-smr-rpt-customerledgerdetail',
  templateUrl: './smr-rpt-customerledgerdetail.component.html',
  styleUrls: ['./smr-rpt-customerledgerdetail.component.scss']
})
export class SmrRptCustomerledgerdetailComponent {
  data:any;
  responsedata: any;
  customer_gid:any;
  customerledgersalesorder_list:any[]=[];
  customerledgercount_list:any[]=[];
  customerledgersalesorderdetail_list:any[]=[];

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
    this.GetcustomerledgersalesorderSummary(customer_gid)

}
GetcustomerledgersalesorderSummary(customer_gid :any){
  debugger
  // const customer_refno = this.router.snapshot.paramMap.get('customer_refno');
  //   this.customer_refno = customer_refno;
  //   const secretKey = 'storyboarderp';
  //   const deencryptedParam = AES.decrypt(this.customer_refno, secretKey).toString(enc.Utf8);
  //   let param = {
  //     customer_refno : deencryptedParam
  //     }

  
    var url = 'SmrRptcustomerledgerdetail/GetCustomerledgersalesorder'
    this.NgxSpinnerService.show();
    let param ={
      customer_gid : customer_gid
    } 
        this.service.getparams(url,param).subscribe((result: any) => {
      $('#customerledgersalesorder_list').DataTable().destroy();
      this.responsedata = result;
      this.customerledgersalesorder_list = result.customerledgersalesorder_list;
      setTimeout(() => {
        $('#customerledgersalesorder_list').DataTable();
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
  ondetail(salesorder_gid:any){
    var url = 'SmrRptcustomerledgerdetail/GetCustomerledgersalesorderdetail'
    let param ={
      salesorder_gid : salesorder_gid
    } 
        this.service.getparams(url,param).subscribe((result: any) => {
      $('#customerledgersalesorderdetail_list').DataTable().destroy();
      this.responsedata = result;
      this.customerledgersalesorderdetail_list = result.customerledgersalesorderdetail_list;
      setTimeout(() => {
        $('#customerledgersalesorderdetail_list').DataTable();
      }, 1);
  
  
    })
  }
  
   
  
}

  


