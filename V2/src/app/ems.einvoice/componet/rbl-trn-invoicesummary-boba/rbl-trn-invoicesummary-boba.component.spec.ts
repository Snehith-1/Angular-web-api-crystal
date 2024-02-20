import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RblTrnInvoicesummaryBobaComponent } from './rbl-trn-invoicesummary-boba.component';

describe('RblTrnInvoicesummaryBobaComponent', () => {
  let component: RblTrnInvoicesummaryBobaComponent;
  let fixture: ComponentFixture<RblTrnInvoicesummaryBobaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RblTrnInvoicesummaryBobaComponent]
    });
    fixture = TestBed.createComponent(RblTrnInvoicesummaryBobaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
