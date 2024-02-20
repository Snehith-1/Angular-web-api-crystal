import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMstSubfunctionComponent } from './hrm-mst-subfunction.component';

describe('HrmMstSubfunctionComponent', () => {
  let component: HrmMstSubfunctionComponent;
  let fixture: ComponentFixture<HrmMstSubfunctionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMstSubfunctionComponent]
    });
    fixture = TestBed.createComponent(HrmMstSubfunctionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
