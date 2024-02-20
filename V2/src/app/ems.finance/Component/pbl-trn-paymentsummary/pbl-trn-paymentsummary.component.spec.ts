import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PblTrnPaymentsummaryComponent } from './pbl-trn-paymentsummary.component';

describe('PblTrnPaymentsummaryComponent', () => {
  let component: PblTrnPaymentsummaryComponent;
  let fixture: ComponentFixture<PblTrnPaymentsummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PblTrnPaymentsummaryComponent]
    });
    fixture = TestBed.createComponent(PblTrnPaymentsummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
