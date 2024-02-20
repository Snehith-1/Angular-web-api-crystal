import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrMstTaxsummaryComponent } from './smr-mst-taxsummary.component';

describe('SmrMstTaxsummaryComponent', () => {
  let component: SmrMstTaxsummaryComponent;
  let fixture: ComponentFixture<SmrMstTaxsummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrMstTaxsummaryComponent]
    });
    fixture = TestBed.createComponent(SmrMstTaxsummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
