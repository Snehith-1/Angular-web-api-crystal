import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnProbationleaveupdateComponent } from './hrm-trn-probationleaveupdate.component';

describe('HrmTrnProbationleaveupdateComponent', () => {
  let component: HrmTrnProbationleaveupdateComponent;
  let fixture: ComponentFixture<HrmTrnProbationleaveupdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnProbationleaveupdateComponent]
    });
    fixture = TestBed.createComponent(HrmTrnProbationleaveupdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
