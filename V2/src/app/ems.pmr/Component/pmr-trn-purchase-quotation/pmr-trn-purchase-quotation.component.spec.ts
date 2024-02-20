import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnPurchaseQuotationComponent } from './pmr-trn-purchase-quotation.component';

describe('PmrTrnPurchaseQuotationComponent', () => {
  let component: PmrTrnPurchaseQuotationComponent;
  let fixture: ComponentFixture<PmrTrnPurchaseQuotationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnPurchaseQuotationComponent]
    });
    fixture = TestBed.createComponent(PmrTrnPurchaseQuotationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
