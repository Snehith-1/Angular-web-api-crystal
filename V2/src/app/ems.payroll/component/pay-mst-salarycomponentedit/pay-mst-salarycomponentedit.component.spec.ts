import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayMstSalarycomponenteditComponent } from './pay-mst-salarycomponentedit.component';

describe('PayMstSalarycomponenteditComponent', () => {
  let component: PayMstSalarycomponenteditComponent;
  let fixture: ComponentFixture<PayMstSalarycomponenteditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayMstSalarycomponenteditComponent]
    });
    fixture = TestBed.createComponent(PayMstSalarycomponenteditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
