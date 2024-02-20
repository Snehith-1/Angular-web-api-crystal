import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnLeavegradeunassign2employeeComponent } from './hrm-trn-leavegradeunassign2employee.component';

describe('HrmTrnLeavegradeunassign2employeeComponent', () => {
  let component: HrmTrnLeavegradeunassign2employeeComponent;
  let fixture: ComponentFixture<HrmTrnLeavegradeunassign2employeeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnLeavegradeunassign2employeeComponent]
    });
    fixture = TestBed.createComponent(HrmTrnLeavegradeunassign2employeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
