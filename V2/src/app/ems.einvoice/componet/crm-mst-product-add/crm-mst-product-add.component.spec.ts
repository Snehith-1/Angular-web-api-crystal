import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmMstProductAddComponent } from './crm-mst-product-add.component';

describe('CrmMstProductAddComponent', () => {
  let component: CrmMstProductAddComponent;
  let fixture: ComponentFixture<CrmMstProductAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmMstProductAddComponent]
    });
    fixture = TestBed.createComponent(CrmMstProductAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
