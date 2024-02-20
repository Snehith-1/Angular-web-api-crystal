import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnQuotationmailComponent } from './smr-trn-quotationmail.component';

describe('SmrTrnQuotationmailComponent', () => {
  let component: SmrTrnQuotationmailComponent;
  let fixture: ComponentFixture<SmrTrnQuotationmailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnQuotationmailComponent]
    });
    fixture = TestBed.createComponent(SmrTrnQuotationmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
