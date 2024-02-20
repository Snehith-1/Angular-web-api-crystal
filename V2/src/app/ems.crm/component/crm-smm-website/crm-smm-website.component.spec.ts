import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmWebsiteComponent } from './crm-smm-website.component';

describe('CrmSmmWebsiteComponent', () => {
  let component: CrmSmmWebsiteComponent;
  let fixture: ComponentFixture<CrmSmmWebsiteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmWebsiteComponent]
    });
    fixture = TestBed.createComponent(CrmSmmWebsiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
