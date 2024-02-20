import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnProfileComponent } from './hrm-trn-profile.component';

describe('HrmTrnProfileComponent', () => {
  let component: HrmTrnProfileComponent;
  let fixture: ComponentFixture<HrmTrnProfileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnProfileComponent]
    });
    fixture = TestBed.createComponent(HrmTrnProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
