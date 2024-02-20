import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptEnquiryreportComponent } from './smr-rpt-enquiryreport.component';

describe('SmrRptEnquiryreportComponent', () => {
  let component: SmrRptEnquiryreportComponent;
  let fixture: ComponentFixture<SmrRptEnquiryreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptEnquiryreportComponent]
    });
    fixture = TestBed.createComponent(SmrRptEnquiryreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
