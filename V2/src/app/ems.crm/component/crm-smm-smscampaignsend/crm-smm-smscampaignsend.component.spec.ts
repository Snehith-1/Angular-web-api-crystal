import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmSmscampaignsendComponent } from './crm-smm-smscampaignsend.component';

describe('CrmSmmSmscampaignsendComponent', () => {
  let component: CrmSmmSmscampaignsendComponent;
  let fixture: ComponentFixture<CrmSmmSmscampaignsendComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmSmscampaignsendComponent]
    });
    fixture = TestBed.createComponent(CrmSmmSmscampaignsendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
