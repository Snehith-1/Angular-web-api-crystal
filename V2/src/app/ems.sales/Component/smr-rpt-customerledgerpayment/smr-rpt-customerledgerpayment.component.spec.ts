import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptCustomerledgerpaymentComponent } from './smr-rpt-customerledgerpayment.component';

describe('SmrRptCustomerledgerpaymentComponent', () => {
  let component: SmrRptCustomerledgerpaymentComponent;
  let fixture: ComponentFixture<SmrRptCustomerledgerpaymentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptCustomerledgerpaymentComponent]
    });
    fixture = TestBed.createComponent(SmrRptCustomerledgerpaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
