import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnPurchaseorderSummaryComponent } from './pmr-trn-purchaseorder-summary.component';

describe('PmrTrnPurchaseorderSummaryComponent', () => {
  let component: PmrTrnPurchaseorderSummaryComponent;
  let fixture: ComponentFixture<PmrTrnPurchaseorderSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnPurchaseorderSummaryComponent]
    });
    fixture = TestBed.createComponent(PmrTrnPurchaseorderSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
