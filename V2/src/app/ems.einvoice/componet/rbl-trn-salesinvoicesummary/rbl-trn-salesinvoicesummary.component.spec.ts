import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RblTrnSalesinvoicesummaryComponent } from './rbl-trn-salesinvoicesummary.component';

describe('RblTrnSalesinvoicesummaryComponent', () => {
  let component: RblTrnSalesinvoicesummaryComponent;
  let fixture: ComponentFixture<RblTrnSalesinvoicesummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RblTrnSalesinvoicesummaryComponent]
    });
    fixture = TestBed.createComponent(RblTrnSalesinvoicesummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
