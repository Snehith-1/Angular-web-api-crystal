import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AES } from 'crypto-js';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

@Component({
  selector: 'app-pay-trn-salarygrade-template',
  templateUrl: './pay-trn-salarygrade-template.component.html',
  styleUrls: ['./pay-trn-salarygrade-template.component.scss']
})
export class PayTrnSalarygradeTemplateComponent {
  responsedata: any;
  salarygradetemplate_list:any;
  parameterValue:any;
  componentdetails: any[] = []

  
  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private ToastrService: ToastrService, public service: SocketService) {
    
  }
  ngOnInit(): void {
  
    //// Summary Grid//////
       
       var url = 'PayTrnSalaryGrade/SalarygradeSummary'
       this.service.get(url).subscribe((result: any) => {
   
         this.responsedata = result;
         this.salarygradetemplate_list = this.responsedata.salarygrade_list;
         setTimeout(() => {
           $('#salary_list').DataTable();
         }, );   
   
       });
     }


    

  
    public onback(): void {

    }
    openDetailsDeduc(data: any): void {
      debugger;
      var api1 = 'PayTrnSalaryGrade/Getpopup';
   
      
      let params = {
        salarygradetemplate_gid: data.salarygradetemplate_gid,
        salarygradetype :"Deduction"
      };
   
      this.service.getparams(api1, params).subscribe((result: any) => {
          this.responsedata = result;
          this.componentdetails = this.responsedata.componentdetails;
      });

    }
    openDetailsOther(data: any): void {
      debugger;
      var api1 = 'PayTrnSalaryGrade/Getpopup';
   
      
      let params = {
        salarygradetemplate_gid: data.salarygradetemplate_gid,
        salarygradetype :"Other"
      };
   
      this.service.getparams(api1, params).subscribe((result: any) => {
          this.responsedata = result;
          this.componentdetails = this.responsedata.componentdetails;
      });

    }
    openDetails(data: any): void {
      debugger;
      var api1 = 'PayTrnSalaryGrade/Getpopup';
   
      
      let params = {
        salarygradetemplate_gid: data.salarygradetemplate_gid,
        salarygradetype :"Addition"
      };
   
      this.service.getparams(api1, params).subscribe((result: any) => {
          this.responsedata = result;
          this.componentdetails = this.responsedata.componentdetails;
      });
    }
    

    onedit(params:any){
      const secretKey = 'storyboarderp';
      const param = (params);
      const encryptedParam = AES.encrypt(param,secretKey).toString();
      this.router.navigate(['/payroll/PayTrnEditSalaryGradeTemplate',encryptedParam])
    }


}
