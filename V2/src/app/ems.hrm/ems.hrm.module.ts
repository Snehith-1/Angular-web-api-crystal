import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmsUtilitiesModule } from '../ems.utilities/ems.utilities.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgApexchartsModule } from 'ng-apexcharts';
import { DataTablesModule } from 'angular-datatables';
import { NgSelectModule } from '@ng-select/ng-select';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { EmsHrmRoutingModule } from './ems.hrm-routing.module';
import { HrmTrnAdmincontrolComponent } from './component/hrm-trn-admincontrol/hrm-trn-admincontrol.component';
import { HrmMstEmployeeviewComponent } from './component/hrm-mst-employeeview/hrm-mst-employeeview.component';
import { HrmTrnEmployeeonboardComponent } from './component/hrm-trn-employeeonboard/hrm-trn-employeeonboard.component';
import { HrmTrnEmployeeonboardaddComponent } from './component/hrm-trn-employeeonboardadd/hrm-trn-employeeonboardadd.component';
import { HrmTrnEmployeeonboardEditComponent } from './component/hrm-trn-employeeonboard-edit/hrm-trn-employeeonboard-edit.component';
import { HrmTrnEmployeeonboardViewComponent } from './component/hrm-trn-employeeonboard-view/hrm-trn-employeeonboard-view.component';
import { HrmTrnEmployeeboardviewpendingComponent } from './component/hrm-trn-employeeboardviewpending/hrm-trn-employeeboardviewpending.component';
import { HrmMstSubfunctionComponent } from './component/hrm-mst-subfunction/hrm-mst-subfunction.component';
import { HrmMstBaselocationComponent } from './component/hrm-mst-baselocation/hrm-mst-baselocation.component';
import { HrmMstBloodgroupComponent } from './component/hrm-mst-bloodgroup/hrm-mst-bloodgroup.component';
import { HrmMstTeammasterComponent } from './component/hrm-mst-teammaster/hrm-mst-teammaster.component';
import { HrmMstUserprofileComponent } from './component/hrm-mst-userprofile/hrm-mst-userprofile.component';
import { HrmTrnEmployeeonboardViewCompletedComponent } from './component/hrm-trn-employeeonboard-view-completed/hrm-trn-employeeonboard-view-completed.component';
import { HrmMstRoleSummaryComponent } from './component/hrm-mst-role-summary/hrm-mst-role-summary.component';
import { HrmMstRoleAddComponent } from './component/hrm-mst-role-add/hrm-mst-role-add.component';
import { HrmMstRoleEditComponent } from './component/hrm-mst-role-edit/hrm-mst-role-edit.component';
import { HrmMstEntityComponent } from './component/hrm-mst-entity/hrm-mst-entity.component';
import { HrmMstBranchSummaryComponent } from './component/hrm-mst-branch-summary/hrm-mst-branch-summary.component';
import { HrmMstDepartmentSummaryComponent } from './component/hrm-mst-department-summary/hrm-mst-department-summary.component';
import { HrmMstDesignationComponent } from './component/hrm-mst-designation/hrm-mst-designation.component';
import { HrmMstHrdocumentComponent } from './component/hrm-mst-hrdocument/hrm-mst-hrdocument.component';
import { HrmMstTaskmasterComponent } from './component/hrm-mst-taskmaster/hrm-mst-taskmaster.component';
import { HrmMemberDashboardComponent } from './component/hrm-member-dashboard/hrm-member-dashboard.component';
import { HrmMemberMyleaveComponent } from './component/hrm-member-myleave/hrm-member-myleave.component';
import { HrmMemberApproveleaveComponent } from './component/hrm-member-approveleave/hrm-member-approveleave.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { HrmTrnProfileComponent } from './component/hrm-trn-profile/hrm-trn-profile.component';
import { HrmTrnIattendanceComponent } from './component/hrm-trn-iattendance/hrm-trn-iattendance.component';
import { HrmTrnProbationperiodComponent } from './component/hrm-trn-probationperiod/hrm-trn-probationperiod.component';
import { HrmTrnProbationhistoryComponent } from './component/hrm-trn-probationhistory/hrm-trn-probationhistory.component';
import { HrmTrnProbationleaveupdateComponent } from './component/hrm-trn-probationleaveupdate/hrm-trn-probationleaveupdate.component';
import { HrmTrnAppointmentorderComponent } from './component/hrm-trn-appointmentorder/hrm-trn-appointmentorder.component';
import { HrmTrnAppointmentordereditComponent } from './component/hrm-trn-appointmentorderedit/hrm-trn-appointmentorderedit.component';
import { HrmTrnAssetcustodianComponent } from './component/hrm-trn-assetcustodian/hrm-trn-assetcustodian.component';
import { HrmTrnAddassetcustodianComponent } from './component/hrm-trn-addassetcustodian/hrm-trn-addassetcustodian.component';
import { HrmTrnCompanypolicyComponent } from './component/hrm-trn-companypolicy/hrm-trn-companypolicy.component';
import { HrmMstEmployeeeditComponent } from './component/hrm-mst-employeeedit/hrm-mst-employeeedit.component';
import { HrmMstEmployeeaddComponent } from './component/hrm-mst-employeeadd/hrm-mst-employeeadd.component';
import { HrmTrnLeavebalancesummaryComponent } from './hrm-trn-leavebalancesummary/hrm-trn-leavebalancesummary.component';
import { HrmTrnLeavebalanceeditComponent } from './hrm-trn-leavebalanceedit/hrm-trn-leavebalanceedit.component';
import { HrmMemberMonthlyattendanceComponent } from './component/hrm-member-monthlyattendance/hrm-member-monthlyattendance.component';
import { HrmMemberOfficecalendarComponent } from './component/hrm-member-officecalendar/hrm-member-officecalendar.component';
import { HrmMstShifttypeComponent } from './component/hrm-mst-shifttype/hrm-mst-shifttype.component';
import { HrmMstAddshifttypeComponent } from './component/hrm-mst-addshifttype/hrm-mst-addshifttype.component';
import { HrmMstLeavetypeComponent } from './component/hrm-mst-leavetype/hrm-mst-leavetype.component';
import { HrmMstLeavegradeComponent } from './component/hrm-mst-leavegrade/hrm-mst-leavegrade.component';
import { HrmMstAddleavegradeComponent } from './component/hrm-mst-addleavegrade/hrm-mst-addleavegrade.component';
import { HrmMstAddholidaygradeComponent } from './component/hrm-mst-addholidaygrade/hrm-mst-addholidaygrade.component';
import { HrmTrnAddholidayasignComponent } from './component/hrm-trn-addholidayasign/hrm-trn-addholidayasign.component';
import { HrmTrnAttendancenonrollformanufacturingComponent } from './component/hrm-trn-attendancenonrollformanufacturing/hrm-trn-attendancenonrollformanufacturing.component';
import { HrmMstShiftassignmentComponent } from './component/hrm-mst-shiftassignment/hrm-mst-shiftassignment.component';
import { ShiftUnAssignmentComponent } from './component/shift-un-assignment/shift-un-assignment.component';
import { HrmTrnAddOfferLetterComponent } from './component/hrm-trn-add-offer-letter/hrm-trn-add-offer-letter.component';
import { HrmTrnOfferletterComponent } from './component/hrm-trn-offerletter/hrm-trn-offerletter.component';
import { HrmRptMonthlyattendanceReportComponent } from './component/hrm-rpt-monthlyattendance-report/hrm-rpt-monthlyattendance-report.component';

import { HrmTrnLeavegradeassign2employeeComponent } from './component/hrm-trn-leavegradeassign2employee/hrm-trn-leavegradeassign2employee.component';
import { HrmTrnLeavegradeunassign2employeeComponent } from './component/hrm-trn-leavegradeunassign2employee/hrm-trn-leavegradeunassign2employee.component';
import { HrmTrnManualregulationComponent } from './component/hrm-trn-manualregulation/hrm-trn-manualregulation.component';

@NgModule({
  declarations: [
    HrmTrnAdmincontrolComponent,
    HrmTrnAssetcustodianComponent,
    HrmTrnAddassetcustodianComponent,
    HrmTrnProfileComponent,
    HrmTrnCompanypolicyComponent,
    HrmTrnIattendanceComponent,
    HrmTrnProbationperiodComponent,
    HrmRptMonthlyattendanceReportComponent,
    HrmTrnProbationhistoryComponent,
    HrmTrnProbationleaveupdateComponent,
    HrmMstEmployeeviewComponent,
    HrmMstEmployeeeditComponent,
    HrmMstEmployeeaddComponent,
    HrmTrnAppointmentordereditComponent,
    HrmTrnAppointmentorderComponent,
    HrmTrnEmployeeonboardComponent,
    HrmTrnEmployeeonboardComponent,
    HrmTrnEmployeeonboardaddComponent,
    HrmTrnEmployeeonboardEditComponent,
    HrmTrnEmployeeonboardViewComponent,
    HrmTrnEmployeeboardviewpendingComponent,
    HrmMstSubfunctionComponent,
    HrmMstBaselocationComponent,
    HrmMstBloodgroupComponent,
    HrmMstTeammasterComponent,
    HrmMstUserprofileComponent,
    HrmTrnEmployeeonboardViewCompletedComponent,
    HrmMstRoleSummaryComponent,
    HrmMstRoleAddComponent,
    HrmMstRoleEditComponent,
    HrmMstEntityComponent,
    HrmMstBranchSummaryComponent,
    HrmMstDepartmentSummaryComponent,
    HrmMstDesignationComponent,
    HrmMstHrdocumentComponent,
    HrmMstTaskmasterComponent,
    HrmMemberDashboardComponent,
    HrmMemberMyleaveComponent,
    HrmMemberApproveleaveComponent,
    HrmTrnLeavebalancesummaryComponent,
    HrmTrnLeavebalanceeditComponent,
    HrmMemberMonthlyattendanceComponent,
    HrmMemberOfficecalendarComponent,
    HrmMstShifttypeComponent,
    HrmMstAddshifttypeComponent,
    HrmMstLeavetypeComponent,
    HrmMstLeavegradeComponent,
    HrmMstAddleavegradeComponent,    
    HrmMstAddholidaygradeComponent,
    HrmTrnAddholidayasignComponent,
    HrmTrnAttendancenonrollformanufacturingComponent,
    HrmMstShiftassignmentComponent,
    ShiftUnAssignmentComponent,
    HrmTrnAddOfferLetterComponent,
    HrmTrnOfferletterComponent,
    HrmTrnLeavegradeassign2employeeComponent,
    HrmTrnLeavegradeunassign2employeeComponent,
    HrmTrnManualregulationComponent
  ],
  imports: [
    CommonModule,EmsHrmRoutingModule,FormsModule, ReactiveFormsModule,EmsUtilitiesModule,
    NgApexchartsModule,DataTablesModule,
    NgSelectModule,AngularEditorModule,
    TabsModule
  ]
})
export class EmsHrmModule { }
