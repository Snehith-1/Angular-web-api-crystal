import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ExcelService } from 'src/app/Service/excel.service';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';



@Component({
  selector: 'app-pmr-rpt-overallreport',
  templateUrl: './pmr-rpt-overallreport.component.html',
  styleUrls: ['./pmr-rpt-overallreport.component.scss']
})
export class PmrRptOverallreportComponent {

  overallreport_list:any[]=[];


  responsedata:any;

  constructor(private formBuilder: FormBuilder, private excelService:ExcelService,
    private ToastrService: ToastrService, private router: ActivatedRoute, 
    private route: Router, public service: SocketService,
    public NgxSpinnerService:NgxSpinnerService,) {
    
  }
  ngOnInit(): void{
    this.OverallReportSummary();
  }
  OverallReportSummary(){
    var url = 'PmrRptOverallReport/GetOverallReportSummary'
    this.NgxSpinnerService.show();
    this.service.get(url).subscribe((result: any) => {
    $('#overallreport_list').DataTable().destroy();
     this.responsedata = result;
     this.overallreport_list = this.responsedata.overallreport_list;
     //console.log(this.entity_list)
     setTimeout(() => {
       $('#overallreport_list').DataTable()
     }, 1);
     this.NgxSpinnerService.hide();

    });
  }
  reportexportExcel(){
    const OverallExcel = this.overallreport_list.map(item => ({
      PurchaseRequisitionRefNo: item.purchaseorder_gid || '',
      BranchName : item.branch_name || '',
      PurchaseOrderRefNo : item.purchaseorder_gid || '',
      GRNRefNo : item.grn_gid || '',
      InvoiceRefNo : item.invoice_gid || '',
      PaymentRefNo : item.payment_gid || '',
      OverAllStatus : item.purchaseorder_status || '',
     
     
    }));
   
         
          this.excelService.exportAsExcelFile(OverallExcel, 'OverallReport_Excel');
  }
 

}
