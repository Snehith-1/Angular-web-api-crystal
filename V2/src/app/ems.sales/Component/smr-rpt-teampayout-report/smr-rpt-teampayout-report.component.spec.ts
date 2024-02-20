import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptTeampayoutReportComponent } from './smr-rpt-teampayout-report.component';

describe('SmrRptTeampayoutReportComponent', () => {
  let component: SmrRptTeampayoutReportComponent;
  let fixture: ComponentFixture<SmrRptTeampayoutReportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptTeampayoutReportComponent]
    });
    fixture = TestBed.createComponent(SmrRptTeampayoutReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
