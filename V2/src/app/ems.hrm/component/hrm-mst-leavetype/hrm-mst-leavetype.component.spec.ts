import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMstLeavetypeComponent } from './hrm-mst-leavetype.component';

describe('HrmMstLeavetypeComponent', () => {
  let component: HrmMstLeavetypeComponent;
  let fixture: ComponentFixture<HrmMstLeavetypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMstLeavetypeComponent]
    });
    fixture = TestBed.createComponent(HrmMstLeavetypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
