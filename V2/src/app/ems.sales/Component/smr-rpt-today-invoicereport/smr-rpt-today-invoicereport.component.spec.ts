import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptTodayInvoicereportComponent } from './smr-rpt-today-invoicereport.component';

describe('SmrRptTodayInvoicereportComponent', () => {
  let component: SmrRptTodayInvoicereportComponent;
  let fixture: ComponentFixture<SmrRptTodayInvoicereportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptTodayInvoicereportComponent]
    });
    fixture = TestBed.createComponent(SmrRptTodayInvoicereportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
