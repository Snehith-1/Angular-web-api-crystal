import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMstTeammasterComponent } from './hrm-mst-teammaster.component';

describe('HrmMstTeammasterComponent', () => {
  let component: HrmMstTeammasterComponent;
  let fixture: ComponentFixture<HrmMstTeammasterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMstTeammasterComponent]
    });
    fixture = TestBed.createComponent(HrmMstTeammasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
