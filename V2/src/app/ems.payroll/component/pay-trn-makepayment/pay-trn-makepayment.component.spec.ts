import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayTrnMakepaymentComponent } from './pay-trn-makepayment.component';

describe('PayTrnMakepaymentComponent', () => {
  let component: PayTrnMakepaymentComponent;
  let fixture: ComponentFixture<PayTrnMakepaymentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayTrnMakepaymentComponent]
    });
    fixture = TestBed.createComponent(PayTrnMakepaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
