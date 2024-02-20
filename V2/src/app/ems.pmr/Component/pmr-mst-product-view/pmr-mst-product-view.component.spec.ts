import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrMstProductViewComponent } from './pmr-mst-product-view.component';

describe('PmrMstProductViewComponent', () => {
  let component: PmrMstProductViewComponent;
  let fixture: ComponentFixture<PmrMstProductViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrMstProductViewComponent]
    });
    fixture = TestBed.createComponent(PmrMstProductViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
