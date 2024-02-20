import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMemberOfficecalendarComponent } from './hrm-member-officecalendar.component';

describe('HrmMemberOfficecalendarComponent', () => {
  let component: HrmMemberOfficecalendarComponent;
  let fixture: ComponentFixture<HrmMemberOfficecalendarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMemberOfficecalendarComponent]
    });
    fixture = TestBed.createComponent(HrmMemberOfficecalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
