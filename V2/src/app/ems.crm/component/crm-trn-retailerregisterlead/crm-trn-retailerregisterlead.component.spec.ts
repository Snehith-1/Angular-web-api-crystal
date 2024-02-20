import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnRetailerregisterleadComponent } from './crm-trn-retailerregisterlead.component';

describe('CrmTrnRetailerregisterleadComponent', () => {
  let component: CrmTrnRetailerregisterleadComponent;
  let fixture: ComponentFixture<CrmTrnRetailerregisterleadComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnRetailerregisterleadComponent]
    });
    fixture = TestBed.createComponent(CrmTrnRetailerregisterleadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
