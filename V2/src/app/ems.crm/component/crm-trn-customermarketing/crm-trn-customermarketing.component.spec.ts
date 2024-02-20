import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnCustomermarketingComponent } from './crm-trn-customermarketing.component';

describe('CrmTrnCustomermarketingComponent', () => {
  let component: CrmTrnCustomermarketingComponent;
  let fixture: ComponentFixture<CrmTrnCustomermarketingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnCustomermarketingComponent]
    });
    fixture = TestBed.createComponent(CrmTrnCustomermarketingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
