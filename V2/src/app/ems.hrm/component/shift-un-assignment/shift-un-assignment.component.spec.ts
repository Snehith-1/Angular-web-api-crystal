import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShiftUnAssignmentComponent } from './shift-un-assignment.component';

describe('ShiftUnAssignmentComponent', () => {
  let component: ShiftUnAssignmentComponent;
  let fixture: ComponentFixture<ShiftUnAssignmentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShiftUnAssignmentComponent]
    });
    fixture = TestBed.createComponent(ShiftUnAssignmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
