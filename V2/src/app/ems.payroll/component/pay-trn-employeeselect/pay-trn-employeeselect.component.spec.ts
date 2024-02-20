import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayTrnEmployeeselectComponent } from './pay-trn-employeeselect.component';

describe('PayTrnEmployeeselectComponent', () => {
  let component: PayTrnEmployeeselectComponent;
  let fixture: ComponentFixture<PayTrnEmployeeselectComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayTrnEmployeeselectComponent]
    });
    fixture = TestBed.createComponent(PayTrnEmployeeselectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
