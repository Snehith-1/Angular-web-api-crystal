import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { AES } from 'crypto-js';
import { NgxSpinnerService } from 'ngx-spinner';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-smr-rpt-customerledgerreport',
  templateUrl: './smr-rpt-customerledgerreport.component.html',
  styleUrls: ['./smr-rpt-customerledgerreport.component.scss']
})
export class SmrRptCustomerledgerreportComponent {
  data:any;
  responsedata: any;
  customerledger_list:any[]=[];

  constructor(private formBuilder: FormBuilder, 
    private ToastrService: ToastrService, private router: ActivatedRoute, 
    private route: Router, public service: SocketService,
    public NgxSpinnerService:NgxSpinnerService,) {
    
  }
  ngOnInit(): void{
    this.CustomerledgerReportSummary();
  }

  CustomerledgerReportSummary(){
    var url = 'SmrRptCustomerledgerreport/GetCustomerledgerreportSummary'
    this.NgxSpinnerService.show();
    this.service.get(url).subscribe((result: any) => {
    $('#customerledger_list').DataTable().destroy();
     this.responsedata = result;
     this.customerledger_list = this.responsedata.customerledger_list;
     //console.log(this.entity_list)
     setTimeout(() => {
       $('#customerledger_list').DataTable()
     }, 1);
     this.NgxSpinnerService.hide();

    });
 }
  
  

 ondetail(customer_gid:any){
  debugger
  const secretKey = 'storyboarderp';
  const param = (customer_gid);
  const encryptedParam = AES.encrypt(param,secretKey).toString();
  this.route.navigate(['/smr/SmrRptCustomerledgerdetail',encryptedParam]) 
 }
 customerexportExcel(){}


}
