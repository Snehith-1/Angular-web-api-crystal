import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnNewmarketingComponent } from './crm-trn-newmarketing.component';

describe('CrmTrnNewmarketingComponent', () => {
  let component: CrmTrnNewmarketingComponent;
  let fixture: ComponentFixture<CrmTrnNewmarketingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnNewmarketingComponent]
    });
    fixture = TestBed.createComponent(CrmTrnNewmarketingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
