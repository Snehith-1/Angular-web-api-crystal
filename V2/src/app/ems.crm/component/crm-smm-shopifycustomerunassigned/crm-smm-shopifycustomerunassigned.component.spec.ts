import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmShopifycustomerunassignedComponent } from './crm-smm-shopifycustomerunassigned.component';

describe('CrmSmmShopifycustomerunassignedComponent', () => {
  let component: CrmSmmShopifycustomerunassignedComponent;
  let fixture: ComponentFixture<CrmSmmShopifycustomerunassignedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmShopifycustomerunassignedComponent]
    });
    fixture = TestBed.createComponent(CrmSmmShopifycustomerunassignedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
