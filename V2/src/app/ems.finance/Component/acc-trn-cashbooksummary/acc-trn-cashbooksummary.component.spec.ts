import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccTrnCashbooksummaryComponent } from './acc-trn-cashbooksummary.component';

describe('AccTrnCashbooksummaryComponent', () => {
  let component: AccTrnCashbooksummaryComponent;
  let fixture: ComponentFixture<AccTrnCashbooksummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccTrnCashbooksummaryComponent]
    });
    fixture = TestBed.createComponent(AccTrnCashbooksummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
