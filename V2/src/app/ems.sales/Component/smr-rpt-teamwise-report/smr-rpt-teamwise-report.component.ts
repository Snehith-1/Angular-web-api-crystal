import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-smr-rpt-teamwise-report',
  templateUrl: './smr-rpt-teamwise-report.component.html',
  styleUrls: ['./smr-rpt-teamwise-report.component.scss']
})
export class SmrRptTeamwiseReportComponent {
  responsedata: any;
  constructor(private formBuilder: FormBuilder,private route:Router,private router:ActivatedRoute,public service :SocketService) { }

  invoicesummary_list:any;



  GetViewcustomerSummary(campaign_gid: any) {
    var url='SmrCommissionManagement/GetCommissionPayoutReportDetails'
    let param = {
      campaign_gid : campaign_gid 
    }
    this.service.getparams(url,param).subscribe((result:any)=>{
    this.responsedata=result;
    this.invoicesummary_list = result.invoicesummary_list;   
    });
  }
}
