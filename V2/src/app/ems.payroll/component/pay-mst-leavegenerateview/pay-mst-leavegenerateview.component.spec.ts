import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayMstLeavegenerateviewComponent } from './pay-mst-leavegenerateview.component';

describe('PayMstLeavegenerateviewComponent', () => {
  let component: PayMstLeavegenerateviewComponent;
  let fixture: ComponentFixture<PayMstLeavegenerateviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayMstLeavegenerateviewComponent]
    });
    fixture = TestBed.createComponent(PayMstLeavegenerateviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
