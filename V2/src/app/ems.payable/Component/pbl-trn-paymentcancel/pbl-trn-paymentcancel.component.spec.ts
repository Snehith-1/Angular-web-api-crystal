import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PblTrnPaymentcancelComponent } from './pbl-trn-paymentcancel.component';

describe('PblTrnPaymentcancelComponent', () => {
  let component: PblTrnPaymentcancelComponent;
  let fixture: ComponentFixture<PblTrnPaymentcancelComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PblTrnPaymentcancelComponent]
    });
    fixture = TestBed.createComponent(PblTrnPaymentcancelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
