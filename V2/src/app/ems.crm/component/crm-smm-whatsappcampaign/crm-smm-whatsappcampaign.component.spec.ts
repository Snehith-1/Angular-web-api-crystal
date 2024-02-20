import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmWhatsappcampaignComponent } from './crm-smm-whatsappcampaign.component';

describe('CrmSmmWhatsappcampaignComponent', () => {
  let component: CrmSmmWhatsappcampaignComponent;
  let fixture: ComponentFixture<CrmSmmWhatsappcampaignComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmWhatsappcampaignComponent]
    });
    fixture = TestBed.createComponent(CrmSmmWhatsappcampaignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
