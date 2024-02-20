import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnEmployeeonboardaddComponent } from './hrm-trn-employeeonboardadd.component';

describe('HrmTrnEmployeeonboardaddComponent', () => {
  let component: HrmTrnEmployeeonboardaddComponent;
  let fixture: ComponentFixture<HrmTrnEmployeeonboardaddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnEmployeeonboardaddComponent]
    });
    fixture = TestBed.createComponent(HrmTrnEmployeeonboardaddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
