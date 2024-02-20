import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayableDashboardComponent } from './payable-dashboard.component';

describe('PayableDashboardComponent', () => {
  let component: PayableDashboardComponent;
  let fixture: ComponentFixture<PayableDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayableDashboardComponent]
    });
    fixture = TestBed.createComponent(PayableDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
