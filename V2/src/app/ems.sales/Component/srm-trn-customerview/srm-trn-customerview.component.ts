import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-srm-trn-customerview',
  templateUrl: './srm-trn-customerview.component.html',
  styleUrls: ['./srm-trn-customerview.component.scss']
})
export class SrmTrnCustomerviewComponent {
  Viewcustomersummary_list:any [] = [];
  smrcustomerbranch_list :any[]=[];
  customer: any;
  responsedata: any;

  constructor(private formBuilder: FormBuilder,private route:Router,private router:ActivatedRoute,public service :SocketService) { }

  ngOnInit(): void {
   const customer_gid =this.router.snapshot.paramMap.get('customer_gid');
    this.customer= customer_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.customer,secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.GetViewcustomerSummary(deencryptedParam);
    this.GetSmrTrnCustomerBranchSummary(deencryptedParam);
  }

  GetViewcustomerSummary(customer_gid: any) {
    var url='SmrTrnCustomerSummary/GetViewcustomerSummary'
    let param = {
      customer_gid : customer_gid 
    }
    this.service.getparams(url,param).subscribe((result:any)=>{
    this.responsedata=result;
    this.Viewcustomersummary_list = result.postcustomer_list;   
    });
  }
  GetSmrTrnCustomerBranchSummary(customer_gid: any) {
    debugger
    var url = 'SmrTrnCustomerSummary/GetSmrTrnCustomerBranchSummary'
    
    let param = {
      customer_gid : customer_gid
        }
    this.service.getparams(url,param).subscribe((result: any) => {
       this.responsedata = result;
       this.smrcustomerbranch_list = result.smrcustomerbranch_list;
    });
  }
  
}
