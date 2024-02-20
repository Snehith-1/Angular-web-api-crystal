import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayMstForm16employeedetailComponent } from './pay-mst-form16employeedetail.component';

describe('PayMstForm16employeedetailComponent', () => {
  let component: PayMstForm16employeedetailComponent;
  let fixture: ComponentFixture<PayMstForm16employeedetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayMstForm16employeedetailComponent]
    });
    fixture = TestBed.createComponent(PayMstForm16employeedetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
