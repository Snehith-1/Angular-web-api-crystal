import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnProbationperiodComponent } from './hrm-trn-probationperiod.component';

describe('HrmTrnProbationperiodComponent', () => {
  let component: HrmTrnProbationperiodComponent;
  let fixture: ComponentFixture<HrmTrnProbationperiodComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnProbationperiodComponent]
    });
    fixture = TestBed.createComponent(HrmTrnProbationperiodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
