import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmCampaignMailmanagementsummaryComponent } from './crm-campaign-mailmanagementsummary.component';

describe('CrmCampaignMailmanagementsummaryComponent', () => {
  let component: CrmCampaignMailmanagementsummaryComponent;
  let fixture: ComponentFixture<CrmCampaignMailmanagementsummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmCampaignMailmanagementsummaryComponent]
    });
    fixture = TestBed.createComponent(CrmCampaignMailmanagementsummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
