import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmMailcampaignsendstatusComponent } from './crm-smm-mailcampaignsendstatus.component';

describe('CrmSmmMailcampaignsendstatusComponent', () => {
  let component: CrmSmmMailcampaignsendstatusComponent;
  let fixture: ComponentFixture<CrmSmmMailcampaignsendstatusComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmMailcampaignsendstatusComponent]
    });
    fixture = TestBed.createComponent(CrmSmmMailcampaignsendstatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
