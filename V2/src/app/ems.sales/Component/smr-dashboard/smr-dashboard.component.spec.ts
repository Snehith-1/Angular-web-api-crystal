import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrDashboardComponent } from './smr-dashboard.component';

describe('SmrDashboardComponent', () => {
  let component: SmrDashboardComponent;
  let fixture: ComponentFixture<SmrDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrDashboardComponent]
    });
    fixture = TestBed.createComponent(SmrDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
