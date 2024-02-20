import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnQuotationHistoryComponent } from './smr-trn-quotation-history.component';

describe('SmrTrnQuotationHistoryComponent', () => {
  let component: SmrTrnQuotationHistoryComponent;
  let fixture: ComponentFixture<SmrTrnQuotationHistoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnQuotationHistoryComponent]
    });
    fixture = TestBed.createComponent(SmrTrnQuotationHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
