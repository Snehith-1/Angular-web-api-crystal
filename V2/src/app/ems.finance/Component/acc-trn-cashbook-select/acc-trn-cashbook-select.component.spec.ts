import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccTrnCashbookSelectComponent } from './acc-trn-cashbook-select.component';

describe('AccTrnCashbookSelectComponent', () => {
  let component: AccTrnCashbookSelectComponent;
  let fixture: ComponentFixture<AccTrnCashbookSelectComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccTrnCashbookSelectComponent]
    });
    fixture = TestBed.createComponent(AccTrnCashbookSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
