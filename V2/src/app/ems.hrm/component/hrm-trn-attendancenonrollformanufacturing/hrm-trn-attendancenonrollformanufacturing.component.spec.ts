import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnAttendancenonrollformanufacturingComponent } from './hrm-trn-attendancenonrollformanufacturing.component';

describe('HrmTrnAttendancenonrollformanufacturingComponent', () => {
  let component: HrmTrnAttendancenonrollformanufacturingComponent;
  let fixture: ComponentFixture<HrmTrnAttendancenonrollformanufacturingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnAttendancenonrollformanufacturingComponent]
    });
    fixture = TestBed.createComponent(HrmTrnAttendancenonrollformanufacturingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
