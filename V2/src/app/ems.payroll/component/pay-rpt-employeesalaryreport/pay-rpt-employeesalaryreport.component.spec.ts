import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayRptEmployeesalaryreportComponent } from './pay-rpt-employeesalaryreport.component';

describe('PayRptEmployeesalaryreportComponent', () => {
  let component: PayRptEmployeesalaryreportComponent;
  let fixture: ComponentFixture<PayRptEmployeesalaryreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayRptEmployeesalaryreportComponent]
    });
    fixture = TestBed.createComponent(PayRptEmployeesalaryreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
