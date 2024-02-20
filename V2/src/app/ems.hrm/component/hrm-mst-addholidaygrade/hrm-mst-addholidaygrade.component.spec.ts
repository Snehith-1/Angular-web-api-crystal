import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMstAddholidaygradeComponent } from './hrm-mst-addholidaygrade.component';

describe('HrmMstAddholidaygradeComponent', () => {
  let component: HrmMstAddholidaygradeComponent;
  let fixture: ComponentFixture<HrmMstAddholidaygradeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMstAddholidaygradeComponent]
    });
    fixture = TestBed.createComponent(HrmMstAddholidaygradeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
