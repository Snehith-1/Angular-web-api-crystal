import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnBiometricSummaryComponent } from './hrm-trn-biometric-summary.component';

describe('HrmTrnBiometricSummaryComponent', () => {
  let component: HrmTrnBiometricSummaryComponent;
  let fixture: ComponentFixture<HrmTrnBiometricSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnBiometricSummaryComponent]
    });
    fixture = TestBed.createComponent(HrmTrnBiometricSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
