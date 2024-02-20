import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnPurchaseRequisitionComponent } from './pmr-trn-purchase-requisition.component';

describe('PmrTrnPurchaseRequisitionComponent', () => {
  let component: PmrTrnPurchaseRequisitionComponent;
  let fixture: ComponentFixture<PmrTrnPurchaseRequisitionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnPurchaseRequisitionComponent]
    });
    fixture = TestBed.createComponent(PmrTrnPurchaseRequisitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
