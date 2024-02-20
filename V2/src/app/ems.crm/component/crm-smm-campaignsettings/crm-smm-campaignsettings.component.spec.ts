import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmCampaignsettingsComponent } from './crm-smm-campaignsettings.component';

describe('CrmSmmCampaignsettingsComponent', () => {
  let component: CrmSmmCampaignsettingsComponent;
  let fixture: ComponentFixture<CrmSmmCampaignsettingsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmCampaignsettingsComponent]
    });
    fixture = TestBed.createComponent(CrmSmmCampaignsettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
