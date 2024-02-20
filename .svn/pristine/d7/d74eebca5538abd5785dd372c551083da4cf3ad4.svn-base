import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { param } from 'jquery';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-smr-rpt-customerledgerinvoice',
  templateUrl: './smr-rpt-customerledgerinvoice.component.html',
  styleUrls: ['./smr-rpt-customerledgerinvoice.component.scss']
})
export class SmrRptCustomerledgerinvoiceComponent {
  data:any;
  responsedata: any;
  customer_gid:any;
  customerledgerinvoice_list:any[]=[];
  customerledgercount_list:any[]=[];
  customerledgerinvoicedetail_list:any[]=[];

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
    this.GetcustomerledgerinvoiceSummary(customer_gid)

}
GetcustomerledgerinvoiceSummary(customer_gid :any){
  debugger
    var url = 'SmrRptcustomerledgerdetail/GetCustomerledgerinvoice'
    this.NgxSpinnerService.show();
    let param ={
      customer_gid : customer_gid
    } 
        this.service.getparams(url,param).subscribe((result: any) => {
      $('#customerledgerinvoice_list').DataTable().destroy();
      this.responsedata = result;
      this.customerledgerinvoice_list = result.customerledgerinvoice_list;
      setTimeout(() => {
        $('#customerledgerinvoice_list').DataTable();
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
  ondetail(invoice_gid:any){
    var url = 'SmrRptcustomerledgerdetail/GetCustomerledgerinvoicedetail'
    let param ={
      invoice_gid : invoice_gid
    } 
        this.service.getparams(url,param).subscribe((result: any) => {
      $('#customerledgerinvoicedetail_list').DataTable().destroy();
      this.responsedata = result;
      this.customerledgerinvoicedetail_list = result.customerledgerinvoicedetail_list;
      setTimeout(() => {
        $('#customerledgerinvoicedetail_list').DataTable();
      }, 1);
  
  
    })
  }
  

}
