import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnRetaileraddComponent } from './crm-trn-retaileradd.component';

describe('CrmTrnRetaileraddComponent', () => {
  let component: CrmTrnRetaileraddComponent;
  let fixture: ComponentFixture<CrmTrnRetaileraddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnRetaileraddComponent]
    });
    fixture = TestBed.createComponent(CrmTrnRetaileraddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
