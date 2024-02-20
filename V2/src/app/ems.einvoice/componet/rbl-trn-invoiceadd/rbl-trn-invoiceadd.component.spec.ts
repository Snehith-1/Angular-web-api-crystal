import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RblTrninvoiceaddComponent } from './rbl-trn-invoiceadd.component';

describe('RblTrnInvoiceaddComponent', () => {
  let component: RblTrninvoiceaddComponent;
  let fixture: ComponentFixture<RblTrninvoiceaddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RblTrninvoiceaddComponent]
    });
    fixture = TestBed.createComponent(RblTrninvoiceaddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
