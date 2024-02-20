import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrMstTermsconditionsaddComponent } from './pmr-mst-termsconditionsadd.component';

describe('PmrMstTermsconditionsaddComponent', () => {
  let component: PmrMstTermsconditionsaddComponent;
  let fixture: ComponentFixture<PmrMstTermsconditionsaddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrMstTermsconditionsaddComponent]
    });
    fixture = TestBed.createComponent(PmrMstTermsconditionsaddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
