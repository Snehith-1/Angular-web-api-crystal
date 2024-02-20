import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrMstVendorAdditionalinformationComponent } from './pmr-mst-vendor-additionalinformation.component';

describe('PmrMstVendorAdditionalinformationComponent', () => {
  let component: PmrMstVendorAdditionalinformationComponent;
  let fixture: ComponentFixture<PmrMstVendorAdditionalinformationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrMstVendorAdditionalinformationComponent]
    });
    fixture = TestBed.createComponent(PmrMstVendorAdditionalinformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
