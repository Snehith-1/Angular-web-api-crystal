import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptCustomerledgerreportComponent } from './smr-rpt-customerledgerreport.component';

describe('SmrRptCustomerledgerreportComponent', () => {
  let component: SmrRptCustomerledgerreportComponent;
  let fixture: ComponentFixture<SmrRptCustomerledgerreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptCustomerledgerreportComponent]
    });
    fixture = TestBed.createComponent(SmrRptCustomerledgerreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
