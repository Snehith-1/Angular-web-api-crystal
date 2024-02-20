import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnSalesorderSummaryComponent } from './smr-trn-salesorder-summary.component';

describe('SmrTrnSalesorderSummaryComponent', () => {
  let component: SmrTrnSalesorderSummaryComponent;
  let fixture: ComponentFixture<SmrTrnSalesorderSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnSalesorderSummaryComponent]
    });
    fixture = TestBed.createComponent(SmrTrnSalesorderSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
