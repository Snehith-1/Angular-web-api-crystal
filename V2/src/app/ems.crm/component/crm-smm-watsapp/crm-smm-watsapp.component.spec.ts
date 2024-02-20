import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmWatsappComponent } from './crm-smm-watsapp.component';

describe('CrmSmmWatsappComponent', () => {
  let component: CrmSmmWatsappComponent;
  let fixture: ComponentFixture<CrmSmmWatsappComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmWatsappComponent]
    });
    fixture = TestBed.createComponent(CrmSmmWatsappComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
