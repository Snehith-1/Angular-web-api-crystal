import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-smr-rpt-salesorder-detailedreport',
  templateUrl: './smr-rpt-salesorder-detailedreport.component.html',
  styleUrls: ['./smr-rpt-salesorder-detailedreport.component.scss']
})
export class SmrRptSalesorderDetailedreportComponent {

  salesorderdetailedreport_List:any[]=[];
  response_data: any;

  constructor(private fb: FormBuilder,private route: ActivatedRoute,private router: Router,private service: SocketService,private ToastrService: ToastrService,) {} 
  


  ngOnInit(): void {
    this.GetSalesOrderDetailedReportSummary();
  }

  GetSalesOrderDetailedReportSummary(){

    var api = 'SmrRptSalesOrderDetailedReport/GetSmrTrnSalesorderDetailedReportsummary';
    this.service.get(api).subscribe((result:any) => {
      $('#product_list').DataTable().destroy();
      this.response_data = result;
      this.salesorderdetailedreport_List = result.salesorderdetail_list;
      setTimeout(()=>{  
        $('#salesorderdetail_list').DataTable();
      }, 1);
    });
  
  }

}
