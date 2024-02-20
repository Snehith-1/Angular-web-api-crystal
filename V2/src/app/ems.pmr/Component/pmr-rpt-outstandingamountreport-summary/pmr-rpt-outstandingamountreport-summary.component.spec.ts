import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrRptOutstandingamountreportSummaryComponent } from './pmr-rpt-outstandingamountreport-summary.component';

describe('PmrRptOutstandingamountreportSummaryComponent', () => {
  let component: PmrRptOutstandingamountreportSummaryComponent;
  let fixture: ComponentFixture<PmrRptOutstandingamountreportSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrRptOutstandingamountreportSummaryComponent]
    });
    fixture = TestBed.createComponent(PmrRptOutstandingamountreportSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
