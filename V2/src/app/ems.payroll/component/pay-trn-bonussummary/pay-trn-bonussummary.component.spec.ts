import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayTrnBonussummaryComponent } from './pay-trn-bonussummary.component';

describe('PayTrnBonussummaryComponent', () => {
  let component: PayTrnBonussummaryComponent;
  let fixture: ComponentFixture<PayTrnBonussummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayTrnBonussummaryComponent]
    });
    fixture = TestBed.createComponent(PayTrnBonussummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
