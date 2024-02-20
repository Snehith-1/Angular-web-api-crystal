import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrDashboardComponent } from './pmr-dashboard.component';

describe('PmrDashboardComponent', () => {
  let component: PmrDashboardComponent;
  let fixture: ComponentFixture<PmrDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrDashboardComponent]
    });
    fixture = TestBed.createComponent(PmrDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
