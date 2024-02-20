import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccMstBankmasterEditComponent } from './acc-mst-bankmaster-edit.component';

describe('AccMstBankmasterEditComponent', () => {
  let component: AccMstBankmasterEditComponent;
  let fixture: ComponentFixture<AccMstBankmasterEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccMstBankmasterEditComponent]
    });
    fixture = TestBed.createComponent(AccMstBankmasterEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
