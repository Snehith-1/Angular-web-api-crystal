import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SysMstEmployeeEditComponent } from './sys-mst-employee-edit.component';

describe('SysMstEmployeeEditComponent', () => {
  let component: SysMstEmployeeEditComponent;
  let fixture: ComponentFixture<SysMstEmployeeEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SysMstEmployeeEditComponent]
    });
    fixture = TestBed.createComponent(SysMstEmployeeEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
