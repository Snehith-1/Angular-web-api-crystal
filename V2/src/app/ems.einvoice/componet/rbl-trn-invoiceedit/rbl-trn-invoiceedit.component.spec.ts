import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RblTrnInvoiceeditComponent } from './rbl-trn-invoiceedit.component';

describe('RblTrnInvoiceeditComponent', () => {
  let component: RblTrnInvoiceeditComponent;
  let fixture: ComponentFixture<RblTrnInvoiceeditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RblTrnInvoiceeditComponent]
    });
    fixture = TestBed.createComponent(RblTrnInvoiceeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
