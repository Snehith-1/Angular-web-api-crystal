import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnOpeninginvoiceSummaryComponent } from './pmr-trn-openinginvoice-summary.component';

describe('PmrTrnOpeninginvoiceSummaryComponent', () => {
  let component: PmrTrnOpeninginvoiceSummaryComponent;
  let fixture: ComponentFixture<PmrTrnOpeninginvoiceSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnOpeninginvoiceSummaryComponent]
    });
    fixture = TestBed.createComponent(PmrTrnOpeninginvoiceSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
