import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMstLeavegradeComponent } from './hrm-mst-leavegrade.component';

describe('HrmMstLeavegradeComponent', () => {
  let component: HrmMstLeavegradeComponent;
  let fixture: ComponentFixture<HrmMstLeavegradeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMstLeavegradeComponent]
    });
    fixture = TestBed.createComponent(HrmMstLeavegradeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
