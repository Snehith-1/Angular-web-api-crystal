import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnAddholidayasignComponent } from './hrm-trn-addholidayasign.component';

describe('HrmTrnAddholidayasignComponent', () => {
  let component: HrmTrnAddholidayasignComponent;
  let fixture: ComponentFixture<HrmTrnAddholidayasignComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnAddholidayasignComponent]
    });
    fixture = TestBed.createComponent(HrmTrnAddholidayasignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
