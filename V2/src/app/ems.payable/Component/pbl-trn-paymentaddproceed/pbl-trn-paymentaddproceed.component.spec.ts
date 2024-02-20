import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PblTrnPaymentaddproceedComponent } from './pbl-trn-paymentaddproceed.component';

describe('PblTrnPaymentaddproceedComponent', () => {
  let component: PblTrnPaymentaddproceedComponent;
  let fixture: ComponentFixture<PblTrnPaymentaddproceedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PblTrnPaymentaddproceedComponent]
    });
    fixture = TestBed.createComponent(PblTrnPaymentaddproceedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
