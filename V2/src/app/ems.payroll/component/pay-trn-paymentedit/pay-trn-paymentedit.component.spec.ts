import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayTrnPaymenteditComponent } from './pay-trn-paymentedit.component';

describe('PayTrnPaymenteditComponent', () => {
  let component: PayTrnPaymenteditComponent;
  let fixture: ComponentFixture<PayTrnPaymenteditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayTrnPaymenteditComponent]
    });
    fixture = TestBed.createComponent(PayTrnPaymenteditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
