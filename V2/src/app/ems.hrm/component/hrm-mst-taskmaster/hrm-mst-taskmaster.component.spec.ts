import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMstTaskmasterComponent } from './hrm-mst-taskmaster.component';

describe('HrmMstTaskmasterComponent', () => {
  let component: HrmMstTaskmasterComponent;
  let fixture: ComponentFixture<HrmMstTaskmasterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMstTaskmasterComponent]
    });
    fixture = TestBed.createComponent(HrmMstTaskmasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
