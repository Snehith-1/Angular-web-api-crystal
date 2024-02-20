import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnMycampaignComponent } from './crm-trn-mycampaign.component';

describe('CrmTrnMycampaignComponent', () => {
  let component: CrmTrnMycampaignComponent;
  let fixture: ComponentFixture<CrmTrnMycampaignComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnMycampaignComponent]
    });
    fixture = TestBed.createComponent(CrmTrnMycampaignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
