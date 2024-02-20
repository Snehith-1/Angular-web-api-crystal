import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnIattendanceComponent } from './hrm-trn-iattendance.component';

describe('HrmTrnIattendanceComponent', () => {
  let component: HrmTrnIattendanceComponent;
  let fixture: ComponentFixture<HrmTrnIattendanceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnIattendanceComponent]
    });
    fixture = TestBed.createComponent(HrmTrnIattendanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
