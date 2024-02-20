import { Component, DebugEventListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';
import { FormGroup } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-pmr-trn-purchaseorder-summary',
  templateUrl: './pmr-trn-purchaseorder-summary.component.html',
  styleUrls: ['./pmr-trn-purchaseorder-summary.component.scss']
})
export class PmrTrnPurchaseorderSummaryComponent{
  
  purchaseorder_list:any[]=[];
  responsedata: any;
  parameterValue1: any;
  company_code: any;
 
  

  constructor(public service :SocketService,private router:Router,private route:Router, private ToastrService: ToastrService,public NgxSpinnerService:NgxSpinnerService) {
    
  }


  ngOnInit(): void {
    this.GetPurchaseOrderSummary();
    this.purchaseorder_list.sort((a,b) => {
      return new (b.created_date) - new (a.created_date); 
    });
  }
  Mail(params : string)
  {
    debugger;
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/pmr/PmrTrnPurchaseordermail',encryptedParam])
  }

  		
  PrintPDF(purchaseorder_gid: any) {
    // API endpoint URL
    const api = 'PmrTrnPurchaseOrder/GetPurchaseOrderRpt';
    this.NgxSpinnerService.show()
    let param = {
      purchaseorder_gid:purchaseorder_gid
    } 
    this.service.getparams(api,param).subscribe((result: any) => {
      if(result!=null){
        this.service.filedownload1(result);
      }
      this.NgxSpinnerService.hide()
    });
  }

  GetPurchaseOrderSummary(){
    // this.NgxSpinnerService.show();
    var url = 'PmrTrnPurchaseOrder/GetPurchaseOrderSummary'
    this.service.get(url).subscribe((result: any) => {
      $('#purchaseorder_list').DataTable().destroy();
      this.responsedata = result;
      this.purchaseorder_list = this.responsedata.GetPurchaseOrder_lists;
      console.log(this.purchaseorder_list )
      // setTimeout(() => {
      //   $('#purchaseorder_list').DataTable();
      // }, 1);
  
      // this.NgxSpinnerService.hide();
    });
  }

  onadd(){
    
    this.router.navigate(['/pmr/PmrTrnDirectpoAdd']);
  }
  onview(params:any){
    debugger
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/pmr/PmrTrnPurchaseOrderView',encryptedParam])
     
  }


  

}

