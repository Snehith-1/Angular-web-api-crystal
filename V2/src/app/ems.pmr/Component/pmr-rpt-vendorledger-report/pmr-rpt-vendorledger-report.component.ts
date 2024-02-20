import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { AES } from 'crypto-js';
import { NgxSpinnerService } from 'ngx-spinner';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-pmr-rpt-vendorledger-report',
  templateUrl: './pmr-rpt-vendorledger-report.component.html',
  styleUrls: ['./pmr-rpt-vendorledger-report.component.scss']
})
export class PmrRptVendorledgerReportComponent {
  data:any;
  responsedata: any;
  vendorledger_list:any[]=[];

  constructor(private formBuilder: FormBuilder, 
    private ToastrService: ToastrService, private router: ActivatedRoute, 
    private route: Router, public service: SocketService,
    public NgxSpinnerService:NgxSpinnerService,) {
    
  }

  ngOnInit(): void{
    this.PmrRptVendorledgerReportSummary();
  }

  PmrRptVendorledgerReportSummary(){
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
  
  

 ondetail(){

  }
  vendorexportExcel(){
    
  }

}
