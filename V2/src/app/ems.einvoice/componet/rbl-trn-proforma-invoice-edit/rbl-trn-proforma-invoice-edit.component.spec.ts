import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RblTrnProformaInvoiceEditComponent } from './rbl-trn-proforma-invoice-edit.component';

describe('RblTrnProformaInvoiceEditComponent', () => {
  let component: RblTrnProformaInvoiceEditComponent;
  let fixture: ComponentFixture<RblTrnProformaInvoiceEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RblTrnProformaInvoiceEditComponent]
    });
    fixture = TestBed.createComponent(RblTrnProformaInvoiceEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
