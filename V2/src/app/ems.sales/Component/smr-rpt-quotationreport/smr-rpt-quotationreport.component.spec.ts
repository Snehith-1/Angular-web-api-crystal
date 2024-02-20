import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptQuotationreportComponent } from './smr-rpt-quotationreport.component';

describe('SmrRptQuotationreportComponent', () => {
  let component: SmrRptQuotationreportComponent;
  let fixture: ComponentFixture<SmrRptQuotationreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptQuotationreportComponent]
    });
    fixture = TestBed.createComponent(SmrRptQuotationreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
