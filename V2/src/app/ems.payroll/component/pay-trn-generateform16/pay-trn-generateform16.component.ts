import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AES, enc } from 'crypto-js';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-pay-trn-generateform16',
  templateUrl: './pay-trn-generateform16.component.html',
  styleUrls: ['./pay-trn-generateform16.component.scss']
})
export class PayTrnGenerateform16Component {
  response_data: any;
  assessmentsummarylist: any;
  generateformsummarylist: any;
  assessment_gid: any;
  company_code: any;

  constructor(private fb: FormBuilder, private ToastrService: ToastrService,
    private route: ActivatedRoute, private router: Router, private service: SocketService,
    public NgxSpinnerService: NgxSpinnerService) {
  }

  ngOnInit() {
    const assessment_gid = this.route.snapshot.paramMap.get('assessment_gid');
    this.assessment_gid = assessment_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.assessment_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam);

    var api = 'PayMstAssessmentSummary/Getassessmentyear';
    let param = {
      assessment_gid: deencryptedParam
    }

    this.NgxSpinnerService.show();
    this.service.getparams(api, param).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      this.response_data = result;
      this.assessmentsummarylist = this.response_data.assessmentsummary_list;
    });

    this.Getgenerateformsummary();
  }

  Getgenerateformsummary() {
    const assessment_gid = this.route.snapshot.paramMap.get('assessment_gid');
    this.assessment_gid = assessment_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.assessment_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam);

    var url = 'PayMstAssessmentSummary/Getgenerateformsummary';
    let param = {
      assessment_gid : deencryptedParam
    };
    this.service.getparams(url, param).subscribe((result: any) => {
      this.response_data = result;
      this.generateformsummarylist = this.response_data.generateformsummary_list;

      setTimeout(() => {
        $('#generateform').DataTable();
      }, 1);
    });
  }

  fillform(params: any,params1:any) {
    debugger;
    const secretKey = 'storyboarderp';

    const param = (params+ '+' +params1);
    const encryptedParam = AES.encrypt(param, secretKey).toString();
    this.router.navigate(['/payroll/paymstform16employeedetails', encryptedParam])
  }

  back() {
    this.router.navigate(['/payroll/PayMstAssessmentsummary'])
  }
  PrintPDF(assessment_gid: string,employee_gid:string) {
    this.company_code = localStorage.getItem('c_code')
    window.location.href = "http://" + environment.host + "/Print/EMS_print/pay_trn_form16.aspx?assessment_gid=" + assessment_gid +"&employee_gid=" + employee_gid + "&companycode=" + this.company_code
  }
}
