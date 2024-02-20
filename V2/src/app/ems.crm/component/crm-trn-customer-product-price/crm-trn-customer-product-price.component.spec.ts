import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnCustomerProductPriceComponent } from './crm-trn-customer-product-price.component';

describe('CrmTrnCustomerProductPriceComponent', () => {
  let component: CrmTrnCustomerProductPriceComponent;
  let fixture: ComponentFixture<CrmTrnCustomerProductPriceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnCustomerProductPriceComponent]
    });
    fixture = TestBed.createComponent(CrmTrnCustomerProductPriceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
