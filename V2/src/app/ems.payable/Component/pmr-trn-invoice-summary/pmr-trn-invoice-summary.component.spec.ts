import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnInvoiceSummaryComponent } from './pmr-trn-invoice-summary.component';

describe('PmrTrnInvoiceSummaryComponent', () => {
  let component: PmrTrnInvoiceSummaryComponent;
  let fixture: ComponentFixture<PmrTrnInvoiceSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnInvoiceSummaryComponent]
    });
    fixture = TestBed.createComponent(PmrTrnInvoiceSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
