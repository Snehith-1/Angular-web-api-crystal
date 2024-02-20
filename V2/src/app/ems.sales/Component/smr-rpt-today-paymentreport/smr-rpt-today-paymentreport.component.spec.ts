import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptTodayPaymentreportComponent } from './smr-rpt-today-paymentreport.component';

describe('SmrRptTodayPaymentreportComponent', () => {
  let component: SmrRptTodayPaymentreportComponent;
  let fixture: ComponentFixture<SmrRptTodayPaymentreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptTodayPaymentreportComponent]
    });
    fixture = TestBed.createComponent(SmrRptTodayPaymentreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
