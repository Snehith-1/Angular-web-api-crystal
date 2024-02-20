import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES, enc } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

interface Employeeport {
 
  user_gid: string;
  user_name: any;

}


@Component({
  selector: 'app-smr-rptemployeewise-report',
  templateUrl: './smr-rptemployeewise-report.component.html',
  styleUrls: ['./smr-rptemployeewise-report.component.scss']
})
export class SmrRptemployeewiseReportComponent {
  GetCommissionPayout_List :any;
  response_data: any;
  responsedata: any;
  campaign :any;
  mdlUserName :any;
  reactiveform: FormGroup | any;
  report: Employeeport;
  sales_list:any;
  
  constructor(private formBuilder: FormBuilder,public route:ActivatedRoute,public service :SocketService,private router:Router,private ToastrService: ToastrService) {
    this.report = {} as Employeeport;
    
    

  }


  ngOnInit(): void {

    this.reactiveform = new FormGroup({
      user_name: new FormControl(''),

    })

    var url = 'SmrTrnQuotation/GetPersonDtl'
    
    this.service.get(url).subscribe((result:any)=>{
      this.sales_list = result.GetPersonDt;
     });
    this.GetSalesperson();
    
 
 
   }
GetSalesperson(){
  let user_name = this.reactiveform.value.user_name;

  if(user_name==null || user_name==""){
    user_name='all'
  }
    const params = {
      user_gid: user_name
    };
    const url2 = 'SmrCommissionManagement/GetCommissionEmpwisePayoutReport';
    this.service.getparams(url2, params).subscribe((result) => {
      this.responsedata = result;
      this.GetCommissionPayout_List = this.responsedata.GetCommissionPayout_List;
      setTimeout(() => {
        $('#GetCommissionPayout_List').DataTable();
      }, 1);
    });

}
}
