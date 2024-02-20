import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnQuotationSummaryComponent } from './smr-trn-quotation-summary.component';

describe('SmrTrnQuotationSummaryComponent', () => {
  let component: SmrTrnQuotationSummaryComponent;
  let fixture: ComponentFixture<SmrTrnQuotationSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnQuotationSummaryComponent]
    });
    fixture = TestBed.createComponent(SmrTrnQuotationSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
