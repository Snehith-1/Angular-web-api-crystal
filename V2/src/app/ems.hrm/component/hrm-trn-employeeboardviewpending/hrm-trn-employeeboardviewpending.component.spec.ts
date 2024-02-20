import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnEmployeeboardviewpendingComponent } from './hrm-trn-employeeboardviewpending.component';

describe('HrmTrnEmployeeboardviewpendingComponent', () => {
  let component: HrmTrnEmployeeboardviewpendingComponent;
  let fixture: ComponentFixture<HrmTrnEmployeeboardviewpendingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnEmployeeboardviewpendingComponent]
    });
    fixture = TestBed.createComponent(HrmTrnEmployeeboardviewpendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
