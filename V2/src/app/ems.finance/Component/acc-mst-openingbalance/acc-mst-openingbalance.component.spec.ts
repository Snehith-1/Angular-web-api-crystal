import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccMstOpeningbalanceComponent } from './acc-mst-openingbalance.component';

describe('AccMstOpeningbalanceComponent', () => {
  let component: AccMstOpeningbalanceComponent;
  let fixture: ComponentFixture<AccMstOpeningbalanceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccMstOpeningbalanceComponent]
    });
    fixture = TestBed.createComponent(AccMstOpeningbalanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
