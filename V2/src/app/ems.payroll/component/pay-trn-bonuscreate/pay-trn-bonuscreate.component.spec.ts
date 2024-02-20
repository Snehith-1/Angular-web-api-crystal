import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayTrnBonuscreateComponent } from './pay-trn-bonuscreate.component';

describe('PayTrnBonuscreateComponent', () => {
  let component: PayTrnBonuscreateComponent;
  let fixture: ComponentFixture<PayTrnBonuscreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayTrnBonuscreateComponent]
    });
    fixture = TestBed.createComponent(PayTrnBonuscreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
