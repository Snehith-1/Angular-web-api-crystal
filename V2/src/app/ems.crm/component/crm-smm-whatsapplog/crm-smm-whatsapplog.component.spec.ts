import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmWhatsapplogComponent } from './crm-smm-whatsapplog.component';

describe('CrmSmmWhatsapplogComponent', () => {
  let component: CrmSmmWhatsapplogComponent;
  let fixture: ComponentFixture<CrmSmmWhatsapplogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmWhatsapplogComponent]
    });
    fixture = TestBed.createComponent(CrmSmmWhatsapplogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
