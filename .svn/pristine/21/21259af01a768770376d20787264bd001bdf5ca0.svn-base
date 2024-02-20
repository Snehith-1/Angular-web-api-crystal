import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AES, enc } from 'crypto-js';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-hrm-mst-employeeview',
  templateUrl: './hrm-mst-employeeview.component.html',
  styleUrls: ['./hrm-mst-employeeview.component.scss']
})
export class HrmMstEmployeeviewComponent{
  employee: any;
  employeeedit_list:any;
  
  constructor(private formBuilder: FormBuilder,private route:Router,private router:ActivatedRoute,public service :SocketService) { }

  ngOnInit(): void {
   const employee_gid =this.router.snapshot.paramMap.get('employee_gid');
    // console.log(termsconditions_gid)
    this.employee= employee_gid;
 
    const secretKey = 'storyboarderp';

    const deencryptedParam = AES.decrypt(this.employee,secretKey).toString(enc.Utf8);
    console.log(deencryptedParam)
    this.GetEditEmployeeSummary(deencryptedParam);
 
        
       
  }
  GetEditEmployeeSummary(employee_gid: any) {
    var url='HrmTrnAdmincontrol/GetEditEmployeeSummary'
    let param = {
      employee_gid : employee_gid 
    }
    this.service.getparams(url,param).subscribe((result:any)=>{
    this.employeeedit_list = result.GetEditEmployeeSummary;
    //console.log(this.employeeedit_list)

      
    });
  }
}
