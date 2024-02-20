import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmLinkedaccountComponent } from './crm-smm-linkedaccount.component';

describe('CrmSmmLinkedaccountComponent', () => {
  let component: CrmSmmLinkedaccountComponent;
  let fixture: ComponentFixture<CrmSmmLinkedaccountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmLinkedaccountComponent]
    });
    fixture = TestBed.createComponent(CrmSmmLinkedaccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
