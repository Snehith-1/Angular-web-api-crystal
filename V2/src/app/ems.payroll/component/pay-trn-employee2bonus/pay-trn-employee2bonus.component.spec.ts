import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayTrnEmployee2bonusComponent } from './pay-trn-employee2bonus.component';

describe('PayTrnEmployee2bonusComponent', () => {
  let component: PayTrnEmployee2bonusComponent;
  let fixture: ComponentFixture<PayTrnEmployee2bonusComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayTrnEmployee2bonusComponent]
    });
    fixture = TestBed.createComponent(PayTrnEmployee2bonusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
