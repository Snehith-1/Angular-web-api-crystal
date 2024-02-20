import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnDropleadsComponent } from './crm-trn-dropleads.component';

describe('CrmTrnDropleadsComponent', () => {
  let component: CrmTrnDropleadsComponent;
  let fixture: ComponentFixture<CrmTrnDropleadsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnDropleadsComponent]
    });
    fixture = TestBed.createComponent(CrmTrnDropleadsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
