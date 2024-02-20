import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMstEmployeeviewComponent } from './hrm-mst-employeeview.component';

describe('HrmMstEmployeeviewComponent', () => {
  let component: HrmMstEmployeeviewComponent;
  let fixture: ComponentFixture<HrmMstEmployeeviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMstEmployeeviewComponent]
    });
    fixture = TestBed.createComponent(HrmMstEmployeeviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
