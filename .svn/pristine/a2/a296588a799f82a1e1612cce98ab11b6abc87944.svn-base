import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
@Component({
  selector: 'app-smr-rpt-commissionpayout-report',
  templateUrl: './smr-rpt-commissionpayout-report.component.html',
  styleUrls: ['./smr-rpt-commissionpayout-report.component.scss']
})
export class SmrRptCommissionpayoutReportComponent {
  Commissionreport_list :any;
  response_data: any;
  GetCommissionPayout_List :any;
  parameterValue1: any;
  parameterValue: any;
  campaign_gid:any;
  sales_list :any;
  responsedata :any;
  invoicesummary_list :any;
  countlist :any;
  data:any;

  constructor(private formBuilder: FormBuilder,public route:ActivatedRoute,public service :SocketService,private router:Router,private ToastrService: ToastrService) {
    
    
    

  }
  
  ngOnInit(): void {debugger
    this.GetCommissionPayoutReport();
  }
  GetCommissionPayoutReport(){

    var api = 'SmrCommissionManagement/GetCommissionPayoutReport';
    this.service.get(api).subscribe((result:any) => {
      $('#product_list').DataTable().destroy();
      this.response_data = result;
      this.GetCommissionPayout_List = this.response_data.GetCommissionPayout_List;
      this.campaign_gid=this.response_data.GetCommissionPayout_List[0].campaign_gid
      setTimeout(()=>{  
        $('#GetCommissionPayout_List').DataTable();
      }, 1);
    });
  
  }
  // Details(parameter: string,campaign_gid: string){
  //   this.parameterValue1 = parameter;
  //   this.campaign_gid = parameter;
  
  //   var url='SmrCommissionManagement/GetCommissionPayoutReportDetails'
  //     let param = {
  //       campaign_gid : campaign_gid 
  //     }
  //     this.service.getparams(url,param).subscribe((result:any)=>{
  //     this.responsedata=result;
  //     this.invoicesummary_list = result.invoicesummary_list;   
  //     });
    
  // }

  Details(params:any){
    const secretKey = 'storyboarderp';
    const param = (params);
    const encryptedParam = AES.encrypt(param,secretKey).toString();
    this.router.navigate(['/smr/SmrRptDetailcommissionReport',encryptedParam]) 
  }

}