import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnUpcomingmeetingsComponent } from './crm-trn-upcomingmeetings.component';

describe('CrmTrnUpcomingmeetingsComponent', () => {
  let component: CrmTrnUpcomingmeetingsComponent;
  let fixture: ComponentFixture<CrmTrnUpcomingmeetingsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnUpcomingmeetingsComponent]
    });
    fixture = TestBed.createComponent(CrmTrnUpcomingmeetingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
