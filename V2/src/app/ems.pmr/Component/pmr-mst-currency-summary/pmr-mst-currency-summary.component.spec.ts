import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrMstCurrencySummaryComponent } from './pmr-mst-currency-summary.component';

describe('PmrMstCurrencySummaryComponent', () => {
  let component: PmrMstCurrencySummaryComponent;
  let fixture: ComponentFixture<PmrMstCurrencySummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrMstCurrencySummaryComponent]
    });
    fixture = TestBed.createComponent(PmrMstCurrencySummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
