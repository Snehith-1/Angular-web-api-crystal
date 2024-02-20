import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccTrnCashbooksummaryComponent } from './Component/acc-trn-cashbooksummary/acc-trn-cashbooksummary.component';
import { AccTrnBankbooksummaryComponent } from './Component/acc-trn-bankbooksummary/acc-trn-bankbooksummary.component'; 
import { AccTrnBankbookComponent } from './Component/acc-trn-bankbook/acc-trn-bankbook.component';
import { AccMstBankmasterAddComponent } from './Component/acc-mst-bankmaster-add/acc-mst-bankmaster-add.component';
import { AccMstBankmasterComponent } from './Component/acc-mst-bankmaster/acc-mst-bankmaster.component';
import { AccTrnBankbookaddentryComponent } from './Component/acc-trn-bankbookaddentry/acc-trn-bankbookaddentry.component';
import { AccTrnCashbookSelectComponent } from './Component/acc-trn-cashbook-select/acc-trn-cashbook-select.component';
// import { AccTrnAddcashbookComponent } from './Component/acc-trn-addcashbook/acc-trn-addcashbook.component';
import { AccTrnCashbookeditComponent } from './Component/acc-trn-cashbookedit/acc-trn-cashbookedit.component';
import { AccMstBankmasterEditComponent } from './Component/acc-mst-bankmaster-edit/acc-mst-bankmaster-edit.component';
import { AccMstOpeningbalanceComponent } from './Component/acc-mst-openingbalance/acc-mst-openingbalance.component';
import { AccMstCreditcardmasterSummaryComponent } from './Component/acc-mst-creditcardmaster-summary/acc-mst-creditcardmaster-summary.component';
import { AccMstGlcodeSummaryComponent } from './Component/acc-mst-glcode-summary/acc-mst-glcode-summary.component';
import { AccTrnCreditcardbookSummaryComponent } from './Component/acc-trn-creditcardbook-summary/acc-trn-creditcardbook-summary.component';
import { AccTrnJournalentrySummaryComponent } from './Component/acc-trn-journalentry-summary/acc-trn-journalentry-summary.component';
import { AccTrnSundrysalesSummaryComponent } from './Component/acc-trn-sundrysales-summary/acc-trn-sundrysales-summary.component';
import { AccTrnGstmanagementSummaryComponent } from './Component/acc-trn-gstmanagement-summary/acc-trn-gstmanagement-summary.component';
import { AccRptIncomeandEpenditureReportComponent } from './Component/acc-rpt-incomeand-ependiture-report/acc-rpt-incomeand-ependiture-report.component';
import { AccTrnReceivableinvoiceSummaryComponent } from './Component/acc-trn-receivableinvoice-summary/acc-trn-receivableinvoice-summary.component';
const routes: Routes = [
  { path: 'AccTrnCashbooksummary', component: AccTrnCashbooksummaryComponent},
  { path: 'AccTrnBankbooksummary', component: AccTrnBankbooksummaryComponent},
  { path: 'AccTrnBankbook/:bank_gid', component: AccTrnBankbookComponent},
  { path: 'AccMstBankMasterAdd', component: AccMstBankmasterAddComponent},
  { path: 'AccMstBankMasterSummary', component: AccMstBankmasterComponent},
  { path: 'AccTrnBankBookEntry/:bank_gid', component:AccTrnBankbookaddentryComponent},
  { path: 'AccTrnCashbookedit/:bank_gid', component:AccTrnCashbookeditComponent},
  // { path: 'AccTrnAddcashbook', component:AccTrnAddcashbookComponent}
  { path: 'AccTrnCashbookSelect/:branch_gid', component: AccTrnCashbookSelectComponent},
  { path: 'AccMstBankmasterEdit/:bank_gid', component: AccMstBankmasterEditComponent},
  { path: 'AccMstOpeningbalance', component: AccMstOpeningbalanceComponent},
  { path: 'AccMstCreditcardmasterSummary', component: AccMstCreditcardmasterSummaryComponent},
  { path: 'AccMstGlcodeSummary', component: AccMstGlcodeSummaryComponent},
  { path: 'AccTrnCreditcardbookSummary', component: AccTrnCreditcardbookSummaryComponent},
  { path: 'AccTrnJournalentrySummary', component: AccTrnJournalentrySummaryComponent},
  { path: 'AccTrnSundrysalesSummary', component: AccTrnSundrysalesSummaryComponent},
  { path: 'AccTrnGstmanagementSummary', component: AccTrnGstmanagementSummaryComponent},
  { path: 'AccRptIncomeandEpenditureReport', component: AccRptIncomeandEpenditureReportComponent},
  { path: 'AccTrnReceivableinvoiceSummary', component: AccTrnReceivableinvoiceSummaryComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmsFinanceRoutingModule { }
