import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnAppointmentorderComponent } from './hrm-trn-appointmentorder.component';

describe('HrmTrnAppointmentorderComponent', () => {
  let component: HrmTrnAppointmentorderComponent;
  let fixture: ComponentFixture<HrmTrnAppointmentorderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnAppointmentorderComponent]
    });
    fixture = TestBed.createComponent(HrmTrnAppointmentorderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
