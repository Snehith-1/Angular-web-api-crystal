import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayTrnEmployeebankdetailsComponent } from './pay-trn-employeebankdetails.component';

describe('PayTrnEmployeebankdetailsComponent', () => {
  let component: PayTrnEmployeebankdetailsComponent;
  let fixture: ComponentFixture<PayTrnEmployeebankdetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayTrnEmployeebankdetailsComponent]
    });
    fixture = TestBed.createComponent(PayTrnEmployeebankdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
