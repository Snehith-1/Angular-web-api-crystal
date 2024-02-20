import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmShopifycustomerComponent } from './crm-smm-shopifycustomer.component';

describe('CrmSmmShopifycustomerComponent', () => {
  let component: CrmSmmShopifycustomerComponent;
  let fixture: ComponentFixture<CrmSmmShopifycustomerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmShopifycustomerComponent]
    });
    fixture = TestBed.createComponent(CrmSmmShopifycustomerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
