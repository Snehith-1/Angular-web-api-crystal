import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnPotentialsComponent } from './crm-trn-potentials.component';

describe('CrmTrnPotentialsComponent', () => {
  let component: CrmTrnPotentialsComponent;
  let fixture: ComponentFixture<CrmTrnPotentialsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnPotentialsComponent]
    });
    fixture = TestBed.createComponent(CrmTrnPotentialsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
