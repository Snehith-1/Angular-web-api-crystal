import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptTodaySalesreportComponent } from './smr-rpt-today-salesreport.component';

describe('SmrRptTodaySalesreportComponent', () => {
  let component: SmrRptTodaySalesreportComponent;
  let fixture: ComponentFixture<SmrRptTodaySalesreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptTodaySalesreportComponent]
    });
    fixture = TestBed.createComponent(SmrRptTodaySalesreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
