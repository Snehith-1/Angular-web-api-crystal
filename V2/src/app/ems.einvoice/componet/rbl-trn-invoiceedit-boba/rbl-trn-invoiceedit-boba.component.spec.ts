import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RblTrnInvoiceeditBobaComponent } from './rbl-trn-invoiceedit-boba.component';

describe('RblTrnInvoiceeditBobaComponent', () => {
  let component: RblTrnInvoiceeditBobaComponent;
  let fixture: ComponentFixture<RblTrnInvoiceeditBobaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RblTrnInvoiceeditBobaComponent]
    });
    fixture = TestBed.createComponent(RblTrnInvoiceeditBobaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
