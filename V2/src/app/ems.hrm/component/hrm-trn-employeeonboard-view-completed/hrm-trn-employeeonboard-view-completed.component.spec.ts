import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnEmployeeonboardViewCompletedComponent } from './hrm-trn-employeeonboard-view-completed.component';

describe('HrmTrnEmployeeonboardViewCompletedComponent', () => {
  let component: HrmTrnEmployeeonboardViewCompletedComponent;
  let fixture: ComponentFixture<HrmTrnEmployeeonboardViewCompletedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnEmployeeonboardViewCompletedComponent]
    });
    fixture = TestBed.createComponent(HrmTrnEmployeeonboardViewCompletedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
