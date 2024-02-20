import { Component,OnInit } from '@angular/core';
import { FormBuilder,FormControl,FormGroup,Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-hrm-mst-role-edit',
  templateUrl: './hrm-mst-role-edit.component.html',
  styleUrls: ['./hrm-mst-role-edit.component.scss']
})
export class HrmMstRoleEditComponent {
  roleform : FormGroup | any;
  countryList = [
    { MartialStatus: 'Married', },
    { MartialStatus: 'UnMarried', },
    { MartialStatus: 'Single',},
  ];
  reportingtoList: any;
  
  role_gid: any;
  lstab: any;
  txtrolecode: any;
  txtroletitle: any;
  txtreportingto: any;
  txtprobationperiod: any;
  txtjobdescription: any;
  txtroleresponsible: any;
  constructor(private SocketService: SocketService,private fb: FormBuilder,public router:Router,private route:ActivatedRoute,private NgxSpinnerService: NgxSpinnerService,private ToastrService: ToastrService){
    this.roleform = new FormGroup ({
      Role_Title : new FormControl(null,[Validators.required,Validators.pattern("^(?!\s*$).+")]), 
      Probation_Period:new FormControl(null,[Validators.required,Validators.pattern("^(?!\s*$).+")]),
      // Job_Description:new FormControl(null,[Validators.required,Validators.pattern("^(?!\s*$).+")]),
      Job_Description: new FormControl(null,

        [

          Validators.required,

          Validators.pattern(/^(?!\s*$).+/)

        ]),
      Role_Code:new FormControl(''),
      Reporting_to: new FormControl(''),
      Role_Responsible:new FormControl('')
    })
  }

  ngOnInit(): void {
    var url = 'ManageRole/PopRoleReportingToAdd';
    this.SocketService.get(url).subscribe((result: any) => {
      this.reportingtoList = result.rolereporting_to; 
    });
    this.route.queryParams.subscribe(params => {
      this.role_gid = params['role_gid'];  //get the employee_gid into the employeependingsummary
      this.lstab = params['lstab'];
    });

    var url = 'ManageRole/RoleEdit';
    var params = {
      role_gid: this.role_gid,
    };   
    this.SocketService.getparams(url,params).subscribe((result: any) => {
      this.txtrolecode = result.role_code;
      this.txtroletitle = result.role_name;
      this.txtreportingto = result.reportingto_gid;
      this.txtprobationperiod = result.probation_period;
      this.txtjobdescription = result.job_description;
      this.txtroleresponsible = result.role_responsible;
    });
  }

  get JobDescription(){
    return this.roleform.get('Job_Description')
  }
  get RoleTitle(){
    return this.roleform.get('Role_Title')
  }
  get ProbationPeriod(){
    return this.roleform.get('Probation_Period')
  }

  role_update() {
    var params = {
      role_gid: this.role_gid,
      role_code: this.txtrolecode,
      role_name: this.txtroletitle,
      reportingto_gid: this.txtreportingto,
      probation_period: this.txtprobationperiod,
      job_description: this.txtjobdescription,
      role_responsible: this.txtroleresponsible,
    };
  
    this.NgxSpinnerService.show();
    var url = 'ManageRole/RoleUpdate';
    this.SocketService.post(url, params).subscribe((result: any) => {
      if (result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
        this.router.navigate(['/hrm/HrmMstRoleSummary']);
      }
      else {
        this.ToastrService.info(result.message)
        this.NgxSpinnerService.hide();
      }   
    }
  )}

  backbutton(){
    this.router.navigate(['/hrm/HrmMstRoleSummary']);
  }
}




