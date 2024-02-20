import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnCustomerCorporateComponent } from './smr-trn-customer-corporate.component';

describe('SmrTrnCustomerCorporateComponent', () => {
  let component: SmrTrnCustomerCorporateComponent;
  let fixture: ComponentFixture<SmrTrnCustomerCorporateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnCustomerCorporateComponent]
    });
    fixture = TestBed.createComponent(SmrTrnCustomerCorporateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
