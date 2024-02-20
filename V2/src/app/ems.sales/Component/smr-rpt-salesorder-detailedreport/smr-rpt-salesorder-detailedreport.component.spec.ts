import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptSalesorderDetailedreportComponent } from './smr-rpt-salesorder-detailedreport.component';

describe('SmrRptSalesorderDetailedreportComponent', () => {
  let component: SmrRptSalesorderDetailedreportComponent;
  let fixture: ComponentFixture<SmrRptSalesorderDetailedreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptSalesorderDetailedreportComponent]
    });
    fixture = TestBed.createComponent(SmrRptSalesorderDetailedreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
