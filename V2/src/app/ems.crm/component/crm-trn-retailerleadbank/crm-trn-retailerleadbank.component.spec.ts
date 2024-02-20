import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnRetailerleadbankComponent } from './crm-trn-retailerleadbank.component';

describe('CrmTrnRetailerleadbankComponent', () => {
  let component: CrmTrnRetailerleadbankComponent;
  let fixture: ComponentFixture<CrmTrnRetailerleadbankComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnRetailerleadbankComponent]
    });
    fixture = TestBed.createComponent(CrmTrnRetailerleadbankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
