import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnCommissionPayoutComponent } from './smr-trn-commission-payout.component';

describe('SmrTrnCommissionPayoutComponent', () => {
  let component: SmrTrnCommissionPayoutComponent;
  let fixture: ComponentFixture<SmrTrnCommissionPayoutComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnCommissionPayoutComponent]
    });
    fixture = TestBed.createComponent(SmrTrnCommissionPayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
