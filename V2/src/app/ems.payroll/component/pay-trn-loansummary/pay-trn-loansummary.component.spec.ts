import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayTrnLoansummaryComponent } from './pay-trn-loansummary.component';

describe('PayTrnLoansummaryComponent', () => {
  let component: PayTrnLoansummaryComponent;
  let fixture: ComponentFixture<PayTrnLoansummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayTrnLoansummaryComponent]
    });
    fixture = TestBed.createComponent(PayTrnLoansummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
