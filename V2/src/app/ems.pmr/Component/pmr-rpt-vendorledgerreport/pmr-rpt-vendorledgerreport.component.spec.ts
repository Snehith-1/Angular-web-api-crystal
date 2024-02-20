import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrRptVendorledgerreportComponent } from './pmr-rpt-vendorledgerreport.component';

describe('PmrRptVendorledgerreportComponent', () => {
  let component: PmrRptVendorledgerreportComponent;
  let fixture: ComponentFixture<PmrRptVendorledgerreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrRptVendorledgerreportComponent]
    });
    fixture = TestBed.createComponent(PmrRptVendorledgerreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
