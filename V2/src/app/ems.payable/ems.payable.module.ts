import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgSelectModule } from '@ng-select/ng-select';
import { PmrTrnInvoiceAccountingaddconfirmComponent } from './Component/pmr-trn-invoice-accountingaddconfirm/pmr-trn-invoice-accountingaddconfirm.component';
import { PmrTrnInvoiceAddselectComponent } from './Component/pmr-trn-invoice-addselect/pmr-trn-invoice-addselect.component';
import { PmrTrnInvoiceSummaryComponent } from './Component/pmr-trn-invoice-summary/pmr-trn-invoice-summary.component';
import { EmsPayableRoutingModule } from './ems.payable-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PmrTrnOpeninginvoiceComponent } from './Component/pmr-trn-openinginvoice/pmr-trn-openinginvoice.component';
import { PmrTrnOpeninginvoiceSummaryComponent } from './Component/pmr-trn-openinginvoice-summary/pmr-trn-openinginvoice-summary.component';
import { PmrTrnInvoiceviewComponent } from './Component/pmr-trn-invoiceview/pmr-trn-invoiceview.component';
import { PblTrnPaymentsummaryComponent } from './Component/pbl-trn-paymentsummary/pbl-trn-paymentsummary.component';
import { PblTrnPaymentaddproceedComponent } from './Component/pbl-trn-paymentaddproceed/pbl-trn-paymentaddproceed.component';
import { PblTrnMultipleinvoice2singlepaymentComponent } from './Component/pbl-trn-multipleinvoice2singlepayment/pbl-trn-multipleinvoice2singlepayment.component';
import { PmrTrnInvoiceDirectinvoiceComponent } from './Component/pmr-trn-invoice-directinvoice/pmr-trn-invoice-directinvoice.component';
import { PblTrnPaymentcancelComponent } from './Component/pbl-trn-paymentcancel/pbl-trn-paymentcancel.component';
import { PblTrnPaymentviewComponent } from './Component/pbl-trn-paymentview/pbl-trn-paymentview.component';

@NgModule({
  declarations: [
    PmrTrnInvoiceSummaryComponent,
    PblTrnPaymentviewComponent,
    PmrTrnInvoiceAddselectComponent,
    PmrTrnInvoiceAccountingaddconfirmComponent,
    PmrTrnOpeninginvoiceComponent,
    PmrTrnOpeninginvoiceSummaryComponent,
    PmrTrnInvoiceviewComponent,
    PblTrnPaymentsummaryComponent,
    PblTrnPaymentaddproceedComponent,
    PblTrnMultipleinvoice2singlepaymentComponent,
    PblTrnPaymentcancelComponent,
    PmrTrnInvoiceDirectinvoiceComponent
    

  ],
  imports: [
    CommonModule,
    EmsPayableRoutingModule,
    FormsModule, ReactiveFormsModule,
    NgSelectModule
  ]
})
export class EmsPayableModule { }
