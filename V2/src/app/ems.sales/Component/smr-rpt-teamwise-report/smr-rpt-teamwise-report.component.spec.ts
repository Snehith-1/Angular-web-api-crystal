import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptTeamwiseReportComponent } from './smr-rpt-teamwise-report.component';

describe('SmrRptTeamwiseReportComponent', () => {
  let component: SmrRptTeamwiseReportComponent;
  let fixture: ComponentFixture<SmrRptTeamwiseReportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptTeamwiseReportComponent]
    });
    fixture = TestBed.createComponent(SmrRptTeamwiseReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
