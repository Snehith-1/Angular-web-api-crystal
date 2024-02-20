import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmFacebookpageComponent } from './crm-smm-facebookpage.component';

describe('CrmSmmFacebookpageComponent', () => {
  let component: CrmSmmFacebookpageComponent;
  let fixture: ComponentFixture<CrmSmmFacebookpageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmFacebookpageComponent]
    });
    fixture = TestBed.createComponent(CrmSmmFacebookpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
