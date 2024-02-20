import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccTrnBankbooksummaryComponent } from './acc-trn-bankbooksummary.component';

describe('AccTrnBankbooksummaryComponent', () => {
  let component: AccTrnBankbooksummaryComponent;
  let fixture: ComponentFixture<AccTrnBankbooksummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccTrnBankbooksummaryComponent]
    });
    fixture = TestBed.createComponent(AccTrnBankbooksummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
