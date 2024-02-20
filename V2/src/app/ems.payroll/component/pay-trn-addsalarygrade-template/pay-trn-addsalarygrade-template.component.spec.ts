import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayTrnAddsalarygradeTemplateComponent } from './pay-trn-addsalarygrade-template.component';

describe('PayTrnAddsalarygradeTemplateComponent', () => {
  let component: PayTrnAddsalarygradeTemplateComponent;
  let fixture: ComponentFixture<PayTrnAddsalarygradeTemplateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayTrnAddsalarygradeTemplateComponent]
    });
    fixture = TestBed.createComponent(PayTrnAddsalarygradeTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
