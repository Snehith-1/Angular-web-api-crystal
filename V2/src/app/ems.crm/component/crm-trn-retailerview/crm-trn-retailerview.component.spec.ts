import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnRetailerviewComponent } from './crm-trn-retailerview.component';

describe('CrmTrnRetailerviewComponent', () => {
  let component: CrmTrnRetailerviewComponent;
  let fixture: ComponentFixture<CrmTrnRetailerviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnRetailerviewComponent]
    });
    fixture = TestBed.createComponent(CrmTrnRetailerviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
