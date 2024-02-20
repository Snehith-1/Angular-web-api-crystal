import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnCustomerraiseenquiryComponent } from './smr-trn-customerraiseenquiry.component';

describe('SmrTrnCustomerraiseenquiryComponent', () => {
  let component: SmrTrnCustomerraiseenquiryComponent;
  let fixture: ComponentFixture<SmrTrnCustomerraiseenquiryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnCustomerraiseenquiryComponent]
    });
    fixture = TestBed.createComponent(SmrTrnCustomerraiseenquiryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
