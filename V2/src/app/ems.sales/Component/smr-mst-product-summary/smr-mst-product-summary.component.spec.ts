import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrMstProductSummaryComponent } from './smr-mst-product-summary.component';

describe('SmrMstProductSummaryComponent', () => {
  let component: SmrMstProductSummaryComponent;
  let fixture: ComponentFixture<SmrMstProductSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrMstProductSummaryComponent]
    });
    fixture = TestBed.createComponent(SmrMstProductSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
