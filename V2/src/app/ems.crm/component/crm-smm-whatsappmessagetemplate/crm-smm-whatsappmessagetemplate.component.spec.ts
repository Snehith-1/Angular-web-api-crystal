import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmWhatsappmessagetemplateComponent } from './crm-smm-whatsappmessagetemplate.component';

describe('CrmSmmWhatsappmessagetemplateComponent', () => {
  let component: CrmSmmWhatsappmessagetemplateComponent;
  let fixture: ComponentFixture<CrmSmmWhatsappmessagetemplateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmWhatsappmessagetemplateComponent]
    });
    fixture = TestBed.createComponent(CrmSmmWhatsappmessagetemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
