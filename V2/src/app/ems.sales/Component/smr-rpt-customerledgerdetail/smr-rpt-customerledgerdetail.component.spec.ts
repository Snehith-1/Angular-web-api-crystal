import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptCustomerledgerdetailComponent } from './smr-rpt-customerledgerdetail.component';

describe('SmrRptCustomerledgerdetailComponent', () => {
  let component: SmrRptCustomerledgerdetailComponent;
  let fixture: ComponentFixture<SmrRptCustomerledgerdetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptCustomerledgerdetailComponent]
    });
    fixture = TestBed.createComponent(SmrRptCustomerledgerdetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
