import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrMstProductAddComponent } from './pmr-mst-product-add.component';

describe('PmrMstProductAddComponent', () => {
  let component: PmrMstProductAddComponent;
  let fixture: ComponentFixture<PmrMstProductAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrMstProductAddComponent]
    });
    fixture = TestBed.createComponent(PmrMstProductAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
