import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnAppointmentordereditComponent } from './hrm-trn-appointmentorderedit.component';

describe('HrmTrnAppointmentordereditComponent', () => {
  let component: HrmTrnAppointmentordereditComponent;
  let fixture: ComponentFixture<HrmTrnAppointmentordereditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnAppointmentordereditComponent]
    });
    fixture = TestBed.createComponent(HrmTrnAppointmentordereditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
