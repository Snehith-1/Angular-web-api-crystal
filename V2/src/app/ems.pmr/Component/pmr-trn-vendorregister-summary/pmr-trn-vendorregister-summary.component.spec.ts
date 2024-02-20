import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnVendorregisterSummaryComponent } from './pmr-trn-vendorregister-summary.component';

describe('PmrTrnVendorregisterSummaryComponent', () => {
  let component: PmrTrnVendorregisterSummaryComponent;
  let fixture: ComponentFixture<PmrTrnVendorregisterSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnVendorregisterSummaryComponent]
    });
    fixture = TestBed.createComponent(PmrTrnVendorregisterSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
