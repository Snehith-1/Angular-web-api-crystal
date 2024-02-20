import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnEmployeeonboardComponent } from './hrm-trn-employeeonboard.component';

describe('HrmTrnEmployeeonboardComponent', () => {
  let component: HrmTrnEmployeeonboardComponent;
  let fixture: ComponentFixture<HrmTrnEmployeeonboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnEmployeeonboardComponent]
    });
    fixture = TestBed.createComponent(HrmTrnEmployeeonboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
