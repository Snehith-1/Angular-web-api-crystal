import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMemberMyleaveComponent } from './hrm-member-myleave.component';

describe('HrmMemberMyleaveComponent', () => {
  let component: HrmMemberMyleaveComponent;
  let fixture: ComponentFixture<HrmMemberMyleaveComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMemberMyleaveComponent]
    });
    fixture = TestBed.createComponent(HrmMemberMyleaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
