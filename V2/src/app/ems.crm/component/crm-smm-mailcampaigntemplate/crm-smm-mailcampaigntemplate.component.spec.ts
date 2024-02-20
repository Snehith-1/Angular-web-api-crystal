import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmMailcampaigntemplateComponent } from './crm-smm-mailcampaigntemplate.component';

describe('CrmSmmMailcampaigntemplateComponent', () => {
  let component: CrmSmmMailcampaigntemplateComponent;
  let fixture: ComponentFixture<CrmSmmMailcampaigntemplateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmMailcampaigntemplateComponent]
    });
    fixture = TestBed.createComponent(CrmSmmMailcampaigntemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
