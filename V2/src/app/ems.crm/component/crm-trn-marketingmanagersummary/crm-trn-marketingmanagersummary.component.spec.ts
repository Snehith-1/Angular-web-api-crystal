import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnMarketingmanagersummaryComponent } from './crm-trn-marketingmanagersummary.component';

describe('CrmTrnMarketingmanagersummaryComponent', () => {
  let component: CrmTrnMarketingmanagersummaryComponent;
  let fixture: ComponentFixture<CrmTrnMarketingmanagersummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnMarketingmanagersummaryComponent]
    });
    fixture = TestBed.createComponent(CrmTrnMarketingmanagersummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
