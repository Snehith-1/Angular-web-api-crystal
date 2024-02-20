import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnVendorregisterEditComponent } from './pmr-trn-vendorregister-edit.component';

describe('PmrTrnVendorregisterEditComponent', () => {
  let component: PmrTrnVendorregisterEditComponent;
  let fixture: ComponentFixture<PmrTrnVendorregisterEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnVendorregisterEditComponent]
    });
    fixture = TestBed.createComponent(PmrTrnVendorregisterEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
