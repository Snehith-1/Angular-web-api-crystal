import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RblTrnInvoiceviewComponent } from './rbl-trn-invoiceview.component';

describe('RblTrnInvoiceviewComponent', () => {
  let component: RblTrnInvoiceviewComponent;
  let fixture: ComponentFixture<RblTrnInvoiceviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RblTrnInvoiceviewComponent]
    });
    fixture = TestBed.createComponent(RblTrnInvoiceviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
