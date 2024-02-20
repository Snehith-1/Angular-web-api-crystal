import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMstRoleEditComponent } from './hrm-mst-role-edit.component';

describe('HrmMstRoleEditComponent', () => {
  let component: HrmMstRoleEditComponent;
  let fixture: ComponentFixture<HrmMstRoleEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMstRoleEditComponent]
    });
    fixture = TestBed.createComponent(HrmMstRoleEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
