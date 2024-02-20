import { Component,OnInit } from '@angular/core';
import { FormBuilder,FormControl,FormGroup,Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { SocketService } from 'src/app/ems.utilities/services/socket.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Title } from '@angular/platform-browser';
@Component({
  selector: 'app-hrm-mst-role-add',
  templateUrl: './hrm-mst-role-add.component.html',
  styleUrls: ['./hrm-mst-role-add.component.scss']
})
export class HrmMstRoleAddComponent {
  roleForm : FormGroup | any;
  countryList = [
    { MartialStatus: 'Married', },
    { MartialStatus: 'UnMarried', },
    { MartialStatus: 'Single',},
  ];
  reportingtoList: any;
  txtrolecode: any;
  txtroletitle: any;
  txtreportingto: any;
  txtprobationperiod: any;
  txtjobdescription: any;
  txtroleresponsible: any;
  constructor(private SocketService: SocketService,private fb: FormBuilder,public router:Router,private NgxSpinnerService: NgxSpinnerService,private ToastrService: ToastrService){
    this.roleForm = new FormGroup ({
      Role_Code :  new FormControl('',[Validators.required,Validators.pattern("^(?!\s*$).+")]),
      Role_Title :  new FormControl('',[Validators.required,Validators.pattern("^(?!\s*$).+")]),
      Probation_Period:  new FormControl(null,[Validators.required,Validators.pattern("^(?!\s*$).+")]),
      // Job_Description:  new FormControl(null,[Validators.required,
      // ]),
      Job_Description: new FormControl(null,

        [

          Validators.required,

          Validators.pattern(/^(?!\s*$).+/)

        ]),
      Reporting_to: new FormControl(),
      Role_Responsible:new FormControl(),
      
  })
  
}
  
  get JobDescription(){
    return this.roleForm.get('Job_Description')
  }

  get RoleTitle(){
    return this.roleForm.get('Role_Title')
  }

  get RoleCode(){
    return this.roleForm.get('Role_Code')
  }

  get ProbationPeriod(){
    return this.roleForm.get('Probation_Period')
  }

  ngOnInit(): void {
    var url = 'ManageRole/PopRoleReportingToAdd';
    this.SocketService.get(url).subscribe((result: any) => {
    this.reportingtoList = result.rolereporting_to;
      
    });


  }
  AddSubmit() {
    var params = {
      
      role_code: this.txtrolecode,
      role_name: this.txtroletitle,
      reportingto_gid: this.txtreportingto,
      probation_period: this.txtprobationperiod,
      job_description: this.txtjobdescription,
      role_responsible: this.txtroleresponsible,
      
    };
    this.NgxSpinnerService.show();
    var url = 'ManageRole/RoleAdd';
    this.SocketService.post(url, params).subscribe((result: any) => {
      if (result.status == true) {
        this.ToastrService.success(result.message)
        this.NgxSpinnerService.hide();
        this.router.navigate(['/hrm/HrmMstRoleSummary']);
      }
      else {
        this.ToastrService.warning(result.message)
        this.NgxSpinnerService.hide();
        this.router.navigate(['/hrm/HrmMstRoleSummary']);
      }
  }
  )}
  

  

  backbutton(){
    this.router.navigate(['/hrm/HrmMstRoleSummary']);
  }
  onadd(){}
}
