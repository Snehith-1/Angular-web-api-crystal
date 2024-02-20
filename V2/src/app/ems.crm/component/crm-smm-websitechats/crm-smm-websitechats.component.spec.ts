import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmWebsitechatsComponent } from './crm-smm-websitechats.component';

describe('CrmSmmWebsitechatsComponent', () => {
  let component: CrmSmmWebsitechatsComponent;
  let fixture: ComponentFixture<CrmSmmWebsitechatsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmWebsitechatsComponent]
    });
    fixture = TestBed.createComponent(CrmSmmWebsitechatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
