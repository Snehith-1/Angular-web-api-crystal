import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnEnquiryAddSelectComponent } from './pmr-trn-enquiry-add-select.component';

describe('PmrTrnEnquiryAddSelectComponent', () => {
  let component: PmrTrnEnquiryAddSelectComponent;
  let fixture: ComponentFixture<PmrTrnEnquiryAddSelectComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnEnquiryAddSelectComponent]
    });
    fixture = TestBed.createComponent(PmrTrnEnquiryAddSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
