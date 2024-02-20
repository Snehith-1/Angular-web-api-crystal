import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RblTrnInvoiceviewBobaComponent } from './rbl-trn-invoiceview-boba.component';

describe('RblTrnInvoiceviewBobaComponent', () => {
  let component: RblTrnInvoiceviewBobaComponent;
  let fixture: ComponentFixture<RblTrnInvoiceviewBobaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RblTrnInvoiceviewBobaComponent]
    });
    fixture = TestBed.createComponent(RblTrnInvoiceviewBobaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
