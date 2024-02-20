import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrMstProductEditComponent } from './pmr-mst-product-edit.component';

describe('PmrMstProductEditComponent', () => {
  let component: PmrMstProductEditComponent;
  let fixture: ComponentFixture<PmrMstProductEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrMstProductEditComponent]
    });
    fixture = TestBed.createComponent(PmrMstProductEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
