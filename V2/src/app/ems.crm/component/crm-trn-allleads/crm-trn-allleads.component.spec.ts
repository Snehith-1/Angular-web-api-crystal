import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnAllleadsComponent } from './crm-trn-allleads.component';

describe('CrmTrnAllleadsComponent', () => {
  let component: CrmTrnAllleadsComponent;
  let fixture: ComponentFixture<CrmTrnAllleadsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnAllleadsComponent]
    });
    fixture = TestBed.createComponent(CrmTrnAllleadsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
