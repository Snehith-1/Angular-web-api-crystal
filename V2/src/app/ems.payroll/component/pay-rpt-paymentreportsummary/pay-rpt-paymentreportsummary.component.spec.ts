import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayRptPaymentreportsummaryComponent } from './pay-rpt-paymentreportsummary.component';

describe('PayRptPaymentreportsummaryComponent', () => {
  let component: PayRptPaymentreportsummaryComponent;
  let fixture: ComponentFixture<PayRptPaymentreportsummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayRptPaymentreportsummaryComponent]
    });
    fixture = TestBed.createComponent(PayRptPaymentreportsummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
