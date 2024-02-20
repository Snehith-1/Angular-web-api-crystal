import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PmrTrnInvoiceSummaryComponent } from './Component/pmr-trn-invoice-summary/pmr-trn-invoice-summary.component';
import { PmrTrnInvoiceAccountingaddconfirmComponent } from './Component/pmr-trn-invoice-accountingaddconfirm/pmr-trn-invoice-accountingaddconfirm.component';
import { PmrTrnInvoiceAddselectComponent } from './Component/pmr-trn-invoice-addselect/pmr-trn-invoice-addselect.component';
import { PmrTrnOpeninginvoiceComponent } from './Component/pmr-trn-openinginvoice/pmr-trn-openinginvoice.component';
import { PmrTrnOpeninginvoiceSummaryComponent } from './Component/pmr-trn-openinginvoice-summary/pmr-trn-openinginvoice-summary.component';
import { PmrTrnInvoiceviewComponent } from './Component/pmr-trn-invoiceview/pmr-trn-invoiceview.component';
import { PblTrnPaymentsummaryComponent } from './Component/pbl-trn-paymentsummary/pbl-trn-paymentsummary.component';
import { PblTrnPaymentaddproceedComponent } from './Component/pbl-trn-paymentaddproceed/pbl-trn-paymentaddproceed.component';
import { PblTrnMultipleinvoice2singlepaymentComponent } from './Component/pbl-trn-multipleinvoice2singlepayment/pbl-trn-multipleinvoice2singlepayment.component';
import { PmrTrnInvoiceDirectinvoiceComponent } from './Component/pmr-trn-invoice-directinvoice/pmr-trn-invoice-directinvoice.component';
import { PblTrnPaymentcancelComponent } from './Component/pbl-trn-paymentcancel/pbl-trn-paymentcancel.component';
import { PblTrnPaymentviewComponent } from './Component/pbl-trn-paymentview/pbl-trn-paymentview.component';

const routes: Routes = [
  { path: 'PmrTrnInvoice', component: PmrTrnInvoiceSummaryComponent},
  { path: 'PmrTrnInvoiceAccountingaddconfirm/:vendor_gid', component: PmrTrnInvoiceAccountingaddconfirmComponent},
   { path: 'PmrTrnInvoiceAddselect', component: PmrTrnInvoiceAddselectComponent},
   { path: 'PmrTrnOpeningInvoice', component: PmrTrnOpeninginvoiceComponent},
   { path: 'PmrTrnOpeninginvoiceSummary', component: PmrTrnOpeninginvoiceSummaryComponent},
   { path: 'PmrTrnInvoiceview/:invoice_gid', component: PmrTrnInvoiceviewComponent},
   { path: 'PblTrnPaymentsummary', component: PblTrnPaymentsummaryComponent},
   { path: 'PblTrnPaymentAddProceed', component: PblTrnPaymentaddproceedComponent},
   { path: 'PblTrnMultipleinvoice2singlepayment/:vendor_gid', component: PblTrnMultipleinvoice2singlepaymentComponent},
   { path: 'PmrTrnDirectInvoice', component: PmrTrnInvoiceDirectinvoiceComponent},
   { path: 'PblTrnPaymentCancel/:payment_gid', component: PblTrnPaymentcancelComponent},
   { path: 'PblTrnPaymentview/:payment_gid', component: PblTrnPaymentviewComponent},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmsPayableRoutingModule { }
