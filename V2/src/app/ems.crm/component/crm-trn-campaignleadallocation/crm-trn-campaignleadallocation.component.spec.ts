import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnCampaignleadallocationComponent } from './crm-trn-campaignleadallocation.component';

describe('CrmTrnCampaignleadallocationComponent', () => {
  let component: CrmTrnCampaignleadallocationComponent;
  let fixture: ComponentFixture<CrmTrnCampaignleadallocationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnCampaignleadallocationComponent]
    });
    fixture = TestBed.createComponent(CrmTrnCampaignleadallocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
