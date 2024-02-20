import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule ,ReactiveFormsModule} from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { EmsFinanceRoutingModule } from './ems.finance-routing.module';
import { AccTrnCashbooksummaryComponent } from './Component/acc-trn-cashbooksummary/acc-trn-cashbooksummary.component';
import { AccTrnBankbooksummaryComponent } from './Component/acc-trn-bankbooksummary/acc-trn-bankbooksummary.component';
import { AccTrnBankbookComponent } from './Component/acc-trn-bankbook/acc-trn-bankbook.component'; 
import { AccMstBankmasterComponent } from './Component/acc-mst-bankmaster/acc-mst-bankmaster.component';
import { AccMstBankmasterAddComponent } from './Component/acc-mst-bankmaster-add/acc-mst-bankmaster-add.component';
import { AccTrnBankbookaddentryComponent } from './Component/acc-trn-bankbookaddentry/acc-trn-bankbookaddentry.component';
import { AccTrnCashbookSelectComponent } from './Component/acc-trn-cashbook-select/acc-trn-cashbook-select.component';
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
@NgModule({
  declarations: [
    AccTrnCashbooksummaryComponent,
    AccTrnBankbooksummaryComponent,
    AccTrnBankbookComponent, AccMstBankmasterComponent, AccMstBankmasterAddComponent, AccTrnBankbookaddentryComponent,
    AccTrnCashbookSelectComponent,AccTrnCashbookeditComponent,
    AccMstBankmasterEditComponent,
    AccMstOpeningbalanceComponent,
    AccMstCreditcardmasterSummaryComponent,
    AccMstGlcodeSummaryComponent,
    AccTrnCreditcardbookSummaryComponent,
    AccTrnJournalentrySummaryComponent,
    AccTrnSundrysalesSummaryComponent,
    AccTrnGstmanagementSummaryComponent,
    AccRptIncomeandEpenditureReportComponent,
    AccTrnReceivableinvoiceSummaryComponent

  ],
  imports: [
    CommonModule,
    EmsFinanceRoutingModule,NgSelectModule,
    FormsModule,ReactiveFormsModule
  ]
})
export class EmsFinanceModule { }
