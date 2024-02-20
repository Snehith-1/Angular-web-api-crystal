import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnRaiseEnquiryComponent } from './pmr-trn-raise-enquiry.component';

describe('PmrTrnRaiseEnquiryComponent', () => {
  let component: PmrTrnRaiseEnquiryComponent;
  let fixture: ComponentFixture<PmrTrnRaiseEnquiryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnRaiseEnquiryComponent]
    });
    fixture = TestBed.createComponent(PmrTrnRaiseEnquiryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
