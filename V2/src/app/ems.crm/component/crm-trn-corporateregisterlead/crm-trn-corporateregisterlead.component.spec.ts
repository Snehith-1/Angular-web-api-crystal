import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnCorporateregisterleadComponent } from './crm-trn-corporateregisterlead.component';

describe('CrmTrnCorporateregisterleadComponent', () => {
  let component: CrmTrnCorporateregisterleadComponent;
  let fixture: ComponentFixture<CrmTrnCorporateregisterleadComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnCorporateregisterleadComponent]
    });
    fixture = TestBed.createComponent(CrmTrnCorporateregisterleadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
