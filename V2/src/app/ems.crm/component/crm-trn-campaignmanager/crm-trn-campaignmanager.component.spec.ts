import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnCampaignmanagermanagerComponent } from './crm-trn-campaignmanager.component';

describe('CrmTrnCampaignmanagermanagerComponent', () => {
  let component: CrmTrnCampaignmanagermanagerComponent;
  let fixture: ComponentFixture<CrmTrnCampaignmanagermanagerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnCampaignmanagermanagerComponent]
    });
    fixture = TestBed.createComponent(CrmTrnCampaignmanagermanagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
