import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnProbationhistoryComponent } from './hrm-trn-probationhistory.component';

describe('HrmTrnProbationhistoryComponent', () => {
  let component: HrmTrnProbationhistoryComponent;
  let fixture: ComponentFixture<HrmTrnProbationhistoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnProbationhistoryComponent]
    });
    fixture = TestBed.createComponent(HrmTrnProbationhistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
