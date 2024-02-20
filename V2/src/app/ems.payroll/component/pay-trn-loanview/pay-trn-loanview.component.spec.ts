import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayTrnLoanviewComponent } from './pay-trn-loanview.component';

describe('PayTrnLoanviewComponent', () => {
  let component: PayTrnLoanviewComponent;
  let fixture: ComponentFixture<PayTrnLoanviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayTrnLoanviewComponent]
    });
    fixture = TestBed.createComponent(PayTrnLoanviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
