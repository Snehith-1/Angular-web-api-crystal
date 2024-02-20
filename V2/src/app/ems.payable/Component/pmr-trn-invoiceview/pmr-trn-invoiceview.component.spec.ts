import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnInvoiceviewComponent } from './pmr-trn-invoiceview.component';

describe('PmrTrnInvoiceviewComponent', () => {
  let component: PmrTrnInvoiceviewComponent;
  let fixture: ComponentFixture<PmrTrnInvoiceviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnInvoiceviewComponent]
    });
    fixture = TestBed.createComponent(PmrTrnInvoiceviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
