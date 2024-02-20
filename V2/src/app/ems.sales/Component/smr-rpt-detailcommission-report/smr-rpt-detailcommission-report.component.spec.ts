import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptDetailcommissionReportComponent } from './smr-rpt-detailcommission-report.component';

describe('SmrRptDetailcommissionReportComponent', () => {
  let component: SmrRptDetailcommissionReportComponent;
  let fixture: ComponentFixture<SmrRptDetailcommissionReportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptDetailcommissionReportComponent]
    });
    fixture = TestBed.createComponent(SmrRptDetailcommissionReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
