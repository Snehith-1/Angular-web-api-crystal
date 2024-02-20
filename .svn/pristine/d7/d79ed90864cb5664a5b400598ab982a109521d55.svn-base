import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from '../../../ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { AES } from 'crypto-js';

@Component({
  selector: 'app-rbl-trn-salesinvoicesummary',
  templateUrl: './rbl-trn-salesinvoicesummary.component.html',
  styleUrls: ['./rbl-trn-salesinvoicesummary.component.scss']
})
export class RblTrnSalesinvoicesummaryComponent {
  salesinvoice: any;
  response_data :any;
  parameterValue: any;
  salesproduct_list: any[] = [];
  parameterValue1 : any;
  directorder_gid : any;


  constructor(private fb: FormBuilder,private route: ActivatedRoute,private router: Router,private service: SocketService, private ToastrService: ToastrService) {} 

  ngOnInit(): void {
    var api = 'Einvoice/SalesinvoiceSummary';
    this.service.get(api).subscribe((result:any) => {
      this.response_data = result;
      this.salesinvoice = this.response_data.salesinvoicesummary_list;
      setTimeout(()=>{  
        $('#salesinvoice').DataTable();
      }, 1);
    });
  }

  Details(parameter: string,directorder_gid: string){
    this.parameterValue1 = parameter;
    this.directorder_gid = parameter;
  
    var url='Einvoice/GetProductdetails'
      let param = {
        directorder_gid : directorder_gid 
      }
      this.service.getparams(url,param).subscribe((result:any)=>{
      this.response_data=result;
      this.salesproduct_list = result.salesproduct_list;   
      });
    
  }

  GetProductdetails() {
    
    var url = 'Einvoice/GetProductdetails'
    this.service.get(url).subscribe((result: any) => {
      $('#salesproduct_list').DataTable().destroy();
      this.response_data = result;
      this.salesproduct_list = this.response_data.salesproduct_list;
      setTimeout(() => {
        $('#salesproduct_list').DataTable();
      }, 1);
  
  
    })
  }
  

  


  invoiceaccounting(params: any,param1:any,param2:any) {
    const secretKey = 'storyboarderp';
    const param = (params);
    const parameter1 = (param1);
    const parameter2 = (param2);
    const leadbank_gid = AES.encrypt(parameter1, secretKey).toString();
    const lead2campaign_gid = AES.encrypt(parameter2, secretKey).toString();
    const lspage = "Invoice-Summary";



    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.router.navigate(['/einvoice/Invoiceaccountingaddconfirm', encryptedParam,leadbank_gid,lead2campaign_gid,lspage])
  }
  redirecttolist(){
    this.router.navigate(['/einvoice/Invoice-Summary'])
  }

}
