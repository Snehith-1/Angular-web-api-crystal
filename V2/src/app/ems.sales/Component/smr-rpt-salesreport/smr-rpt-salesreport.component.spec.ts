import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptSalesreportComponent } from './smr-rpt-salesreport.component';

describe('SmrRptSalesreportComponent', () => {
  let component: SmrRptSalesreportComponent;
  let fixture: ComponentFixture<SmrRptSalesreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptSalesreportComponent]
    });
    fixture = TestBed.createComponent(SmrRptSalesreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
