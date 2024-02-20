import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptCustomerledgerinvoiceComponent } from './smr-rpt-customerledgerinvoice.component';

describe('SmrRptCustomerledgerinvoiceComponent', () => {
  let component: SmrRptCustomerledgerinvoiceComponent;
  let fixture: ComponentFixture<SmrRptCustomerledgerinvoiceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptCustomerledgerinvoiceComponent]
    });
    fixture = TestBed.createComponent(SmrRptCustomerledgerinvoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
