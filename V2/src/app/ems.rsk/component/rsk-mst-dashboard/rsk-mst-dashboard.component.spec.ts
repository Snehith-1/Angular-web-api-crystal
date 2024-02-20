import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RskMstDashboardComponent } from './rsk-mst-dashboard.component';

describe('RskMstDashboardComponent', () => {
  let component: RskMstDashboardComponent;
  let fixture: ComponentFixture<RskMstDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RskMstDashboardComponent]
    });
    fixture = TestBed.createComponent(RskMstDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
