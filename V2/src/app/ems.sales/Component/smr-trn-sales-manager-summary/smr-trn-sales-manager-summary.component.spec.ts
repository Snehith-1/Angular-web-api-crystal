import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnSalesManagerSummaryComponent } from './smr-trn-sales-manager-summary.component';

describe('SmrTrnSalesManagerSummaryComponent', () => {
  let component: SmrTrnSalesManagerSummaryComponent;
  let fixture: ComponentFixture<SmrTrnSalesManagerSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnSalesManagerSummaryComponent]
    });
    fixture = TestBed.createComponent(SmrTrnSalesManagerSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
