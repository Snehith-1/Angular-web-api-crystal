import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmWacustomizebulkmessageComponent } from './crm-smm-wacustomizebulkmessage.component';

describe('CrmSmmWacustomizebulkmessageComponent', () => {
  let component: CrmSmmWacustomizebulkmessageComponent;
  let fixture: ComponentFixture<CrmSmmWacustomizebulkmessageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmWacustomizebulkmessageComponent]
    });
    fixture = TestBed.createComponent(CrmSmmWacustomizebulkmessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
