import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccTrnCashbookeditComponent } from './acc-trn-cashbookedit.component';

describe('AccTrnCashbookeditComponent', () => {
  let component: AccTrnCashbookeditComponent;
  let fixture: ComponentFixture<AccTrnCashbookeditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccTrnCashbookeditComponent]
    });
    fixture = TestBed.createComponent(AccTrnCashbookeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
