import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayMstEditemployee2gradeassignComponent } from './pay-mst-editemployee2gradeassign.component';

describe('PayMstEditemployee2gradeassignComponent', () => {
  let component: PayMstEditemployee2gradeassignComponent;
  let fixture: ComponentFixture<PayMstEditemployee2gradeassignComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayMstEditemployee2gradeassignComponent]
    });
    fixture = TestBed.createComponent(PayMstEditemployee2gradeassignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
