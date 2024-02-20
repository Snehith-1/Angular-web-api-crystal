import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayTrnEditsalarygradeTemplateComponent } from './pay-trn-editsalarygrade-template.component';

describe('PayTrnEditsalarygradeTemplateComponent', () => {
  let component: PayTrnEditsalarygradeTemplateComponent;
  let fixture: ComponentFixture<PayTrnEditsalarygradeTemplateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayTrnEditsalarygradeTemplateComponent]
    });
    fixture = TestBed.createComponent(PayTrnEditsalarygradeTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
