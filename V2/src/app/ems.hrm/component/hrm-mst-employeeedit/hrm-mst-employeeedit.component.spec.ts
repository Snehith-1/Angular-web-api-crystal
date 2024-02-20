import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMstEmployeeeditComponent } from './hrm-mst-employeeedit.component';

describe('HrmMstEmployeeeditComponent', () => {
  let component: HrmMstEmployeeeditComponent;
  let fixture: ComponentFixture<HrmMstEmployeeeditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMstEmployeeeditComponent]
    });
    fixture = TestBed.createComponent(HrmMstEmployeeeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
