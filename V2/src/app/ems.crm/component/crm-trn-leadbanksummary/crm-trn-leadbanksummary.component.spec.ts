import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnLeadbanksummaryComponent } from './crm-trn-leadbanksummary.component';

describe('CrmTrnLeadbanksummaryComponent', () => {
  let component: CrmTrnLeadbanksummaryComponent;
  let fixture: ComponentFixture<CrmTrnLeadbanksummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnLeadbanksummaryComponent]
    });
    fixture = TestBed.createComponent(CrmTrnLeadbanksummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
