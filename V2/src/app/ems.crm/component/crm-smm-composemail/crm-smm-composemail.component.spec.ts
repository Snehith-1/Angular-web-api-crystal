import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmComposemailComponent } from './crm-smm-composemail.component';

describe('CrmSmmComposemailComponent', () => {
  let component: CrmSmmComposemailComponent;
  let fixture: ComponentFixture<CrmSmmComposemailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmComposemailComponent]
    });
    fixture = TestBed.createComponent(CrmSmmComposemailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
