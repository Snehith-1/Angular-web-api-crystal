import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-pmr-rpt-purchaseorderdetailedreport',
  templateUrl:'./pmr-rpt-purchaseorderdetailedreport.component.html',
  styleUrls: ['./pmr-rpt-purchaseorderdetailedreport.component.scss']
})
export class PmrRptPurchaseorderdetailedreportComponent {
  PmrRptPurchaseorderdetailedreport_lists:any[]=[]
  responsedata: any;
  constructor(private formBuilder: FormBuilder,public service :SocketService,private route:Router,private ToastrService: ToastrService,private NgxSpinnerService:NgxSpinnerService) {
  }

  ngOnInit(): void {

    this.GetPmrRptPurchaseorderdetailedreportSummary();
  }
  GetPmrRptPurchaseorderdetailedreportSummary(){
    this.NgxSpinnerService.show();
    var url='PmrRptPurchaseorderdetailedreport/GetPmrRptPurchaseorderdetailedreportSummary'
    this.service.get(url).subscribe((result:any)=>{
      this.responsedata=result;
      this.PmrRptPurchaseorderdetailedreport_lists = result.PmrRptPurchaseorderdetailedreport_lists;  
      setTimeout(()=>{   
        $('#PmrRptPurchaseorderdetailedreport_lists').DataTable();
      }, 1);
      this.NgxSpinnerService.hide();
    });
   
}
}


