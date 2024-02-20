import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrRptPurchaseorderdetailedreportComponent } from './pmr-rpt-purchaseorderdetailedreport.component';

describe('PmrRptPurchaseorderdetailedreportComponent', () => {
  let component: PmrRptPurchaseorderdetailedreportComponent;
  let fixture: ComponentFixture<PmrRptPurchaseorderdetailedreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrRptPurchaseorderdetailedreportComponent]
    });
    fixture = TestBed.createComponent(PmrRptPurchaseorderdetailedreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
