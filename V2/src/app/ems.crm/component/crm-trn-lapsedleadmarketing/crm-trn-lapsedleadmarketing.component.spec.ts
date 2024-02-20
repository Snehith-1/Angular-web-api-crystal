import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnLapsedleadmarketingComponent } from './crm-trn-lapsedleadmarketing.component';

describe('CrmTrnLapsedleadmarketingComponent', () => {
  let component: CrmTrnLapsedleadmarketingComponent;
  let fixture: ComponentFixture<CrmTrnLapsedleadmarketingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnLapsedleadmarketingComponent]
    });
    fixture = TestBed.createComponent(CrmTrnLapsedleadmarketingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
