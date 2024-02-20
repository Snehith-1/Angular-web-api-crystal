import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMemberApproveleaveComponent } from './hrm-member-approveleave.component';

describe('HrmMemberApproveleaveComponent', () => {
  let component: HrmMemberApproveleaveComponent;
  let fixture: ComponentFixture<HrmMemberApproveleaveComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMemberApproveleaveComponent]
    });
    fixture = TestBed.createComponent(HrmMemberApproveleaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
