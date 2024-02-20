import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-pmr-rpt-outstandingamountreport-summary',
  templateUrl: './pmr-rpt-outstandingamountreport-summary.component.html',
  styleUrls: ['./pmr-rpt-outstandingamountreport-summary.component.scss']
})
export class PmrRptOutstandingamountreportSummaryComponent {

  outstandingamountreport_list:any;
  responsedata: any;

  constructor(public service :SocketService,private route:Router,private ToastrService: ToastrService) {  
  }
  ngOnInit(): void {
    this.GetOutstandingAmountReportSummary();
  }
  GetOutstandingAmountReportSummary(){
    var url = 'PmrRptOutstandingAmountReport/GetOutstandingAmountReportSummary'
    this.service.get(url).subscribe((result: any) => {
  
      this.responsedata = result;
      this.outstandingamountreport_list = this.responsedata.Getoutstandingamountreport_list;
      setTimeout(() => {
        $('#outstandingamountreport_list').DataTable();
      }, 1);
    });
  }

  calculateTotal(): number {
    let totalAmount = 0;
    for (const data of this.outstandingamountreport_list) {
      totalAmount += parseFloat(data.invoice_amount);
    }
    return totalAmount;
  }

  calculateTotal1(): number {
    let totalAmount = 0;
    for (const data of this.outstandingamountreport_list) {
      totalAmount += parseFloat(data.payment_amount);
    }
    return totalAmount;
  }

  
  calculateTotal2(): number {
    let totalAmount = 0;
    for (const data of this.outstandingamountreport_list) {
      totalAmount += parseFloat(data.outstanding_amount);
    }
    return totalAmount;
  }

}


