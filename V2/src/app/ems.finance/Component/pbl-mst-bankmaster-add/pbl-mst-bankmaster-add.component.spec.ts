import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PblMstBankmasterAddComponent } from './pbl-mst-bankmaster-add.component';

describe('PblMstBankmasterAddComponent', () => {
  let component: PblMstBankmasterAddComponent;
  let fixture: ComponentFixture<PblMstBankmasterAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PblMstBankmasterAddComponent]
    });
    fixture = TestBed.createComponent(PblMstBankmasterAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
