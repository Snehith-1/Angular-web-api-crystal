import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnRaiseEnquiryaddComponent } from './pmr-trn-raise-enquiryadd.component';

describe('PmrTrnRaiseEnquiryaddComponent', () => {
  let component: PmrTrnRaiseEnquiryaddComponent;
  let fixture: ComponentFixture<PmrTrnRaiseEnquiryaddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnRaiseEnquiryaddComponent]
    });
    fixture = TestBed.createComponent(PmrTrnRaiseEnquiryaddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
