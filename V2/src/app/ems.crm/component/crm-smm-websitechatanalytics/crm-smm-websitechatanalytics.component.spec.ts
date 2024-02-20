import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmWebsitechatanalyticsComponent } from './crm-smm-websitechatanalytics.component';

describe('CrmSmmWebsitechatanalyticsComponent', () => {
  let component: CrmSmmWebsitechatanalyticsComponent;
  let fixture: ComponentFixture<CrmSmmWebsitechatanalyticsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmWebsitechatanalyticsComponent]
    });
    fixture = TestBed.createComponent(CrmSmmWebsitechatanalyticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
