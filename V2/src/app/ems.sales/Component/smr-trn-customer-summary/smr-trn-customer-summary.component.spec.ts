import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnCustomerSummaryComponent } from './smr-trn-customer-summary.component';

describe('SmrTrnCustomerSummaryComponent', () => {
  let component: SmrTrnCustomerSummaryComponent;
  let fixture: ComponentFixture<SmrTrnCustomerSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnCustomerSummaryComponent]
    });
    fixture = TestBed.createComponent(SmrTrnCustomerSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
