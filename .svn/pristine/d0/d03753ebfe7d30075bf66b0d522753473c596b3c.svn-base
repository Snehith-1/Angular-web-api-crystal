import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { SelectionModel } from '@angular/cdk/collections';
import { AES, enc } from 'crypto-js';

export class IEmployee {
  employeeselect_list: string[] = [];
  assessment_gid:any;
  assignempsummary_list: any[] = [];
  selectedEmployeeList: any[] = []
}

@Component({
  selector: 'app-pay-trn-assignemployee2form16',
  templateUrl: './pay-trn-assignemployee2form16.component.html',
  styleUrls: ['./pay-trn-assignemployee2form16.component.scss']
})
export class PayTrnAssignemployee2form16Component {

  selection = new SelectionModel<IEmployee>(true, []);
  employeeselect_list: any[] = [];
  assignempForm: FormGroup | any;
  assignempsummarylist: any[] = [];
  assessment_gid: any;
  response_data: any;
  assessmentsummarylist: any;
  CurObj: IEmployee = new IEmployee();
  pick: Array<any> = [];
  selectedEmployeeList: any;  

  constructor(private formBuilder: FormBuilder, private ToastrService: ToastrService,
    private route: ActivatedRoute, private router: Router, private service: SocketService,
    public NgxSpinnerService: NgxSpinnerService) {

      this.assignempForm = this.formBuilder.group({
     
      });
  }

  ngOnInit() {
    const assessment_gid = this.route.snapshot.paramMap.get('assessment_gid');
    this.assessment_gid = assessment_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.assessment_gid, secretKey).toString(enc.Utf8);
    console.log(deencryptedParam);
    
    this.assignempsummary_list(deencryptedParam);

    let param = {
      assessment_gid: deencryptedParam
    }

    var api = 'PayMstAssessmentSummary/Getassessmentyear';
    this.NgxSpinnerService.show();
    this.service.getparams(api, param).subscribe((result: any) => {
      this.NgxSpinnerService.hide();
      this.response_data = result;
      this.assessmentsummarylist = this.response_data.assessmentsummary_list;
    });
  }

  hasSelectedEmployees(): boolean {
    return this.selectedEmployeeList.length > 0;
  }

  assignempsummary_list(assessment_gid: any) {
    var url = 'PayMstAssessmentSummary/Getassignempsummary';
    let param = {
      assessment_gid: assessment_gid,
    };
    this.service.getparams(url, param).subscribe((result: any) => {
      this.response_data = result;
      this.assignempsummarylist = this.response_data.assignempsummary_list;
    });
  }

  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.employeeselect_list.forEach((row: IEmployee) => this.selection.select(row));
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.employeeselect_list.length;
    return numSelected === numRows;
  }

  onSubmit() {
    const assessment_gid = this.route.snapshot.paramMap.get('assessment_gid');
    this.assessment_gid = assessment_gid;
    const secretKey = 'storyboarderp';
    const deencryptedParam = AES.decrypt(this.assessment_gid, secretKey).toString(enc.Utf8);
    this.pick = this.selection.selected
    this.CurObj.assignempsummary_list = this.pick
    this.CurObj.assessment_gid=deencryptedParam
    
    if ( this.CurObj.assignempsummary_list.length === 0) {
      this.ToastrService.warning("Select Atleast one Employee");
      return;
    }

    debugger;
    const url = 'PayMstAssessmentSummary/Postassignemployee';
      this.service.post(url, this.CurObj).subscribe((result: any) => {
        if (result.status === false) {
          this.ToastrService.warning(result.message)
          this.router.navigate(['/payroll/PayMstAssessmentsummary']);
        } else {
          this.ToastrService.success(result.message);
        }
      });
  }

  back() {
    this.router.navigate(['/payroll/PayMstAssessmentsummary'])
  }
}
