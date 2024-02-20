import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { ExcelService } from 'src/app/Service/excel.service';

@Component({
  selector: 'app-pmr-rpt-vendorledgerreport',
  templateUrl: './pmr-rpt-vendorledgerreport.component.html',
  styleUrls: ['./pmr-rpt-vendorledgerreport.component.scss']
})
export class PmrRptVendorledgerreportComponent {

  vendorledger_list :any[]=[];
  responsedata:any;

  constructor(private formBuilder: FormBuilder, private excelService:ExcelService,
    private ToastrService: ToastrService, private router: ActivatedRoute, 
    private route: Router, public service: SocketService,
    public NgxSpinnerService:NgxSpinnerService,) {
    
  }
  ngOnInit(): void{
    this.VendorledgerReportSummary();
  }
  VendorledgerReportSummary(){
    var url = 'PmrRptVendorLedgerreport/GetVendorledgerReportSummary'
    this.NgxSpinnerService.show();
    this.service.get(url).subscribe((result: any) => {
    $('#vendorledger_list').DataTable().destroy();
     this.responsedata = result;
     this.vendorledger_list = this.responsedata.vendorledger_list;
     //console.log(this.entity_list)
     setTimeout(() => {
       $('#vendorledger_list').DataTable()
     }, 1);
     this.NgxSpinnerService.hide();

    });
  }
  vendorexportExcel(){
    const VendorExcel = this.vendorledger_list.map(item => ({
      VendorRefNo: item.vendor_refno || '',
      Vendor : item.vendor || '',
      VendorCode : item.vendor_code || '',
      VendorAddress : item.vendor_address || '',
      ContactDetails : item.contact_details || '',
      Products : item.products || '',
      OrderValue : item.order_value || '',
     
     
    }));
   
         
          this.excelService.exportAsExcelFile(VendorExcel, 'Vendor_Excel');
  }
}
