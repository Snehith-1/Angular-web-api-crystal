import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnEmployeeonboardViewComponent } from './hrm-trn-employeeonboard-view.component';

describe('HrmTrnEmployeeonboardViewComponent', () => {
  let component: HrmTrnEmployeeonboardViewComponent;
  let fixture: ComponentFixture<HrmTrnEmployeeonboardViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnEmployeeonboardViewComponent]
    });
    fixture = TestBed.createComponent(HrmTrnEmployeeonboardViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
