import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmMstHrdocumentComponent } from './hrm-mst-hrdocument.component';

describe('HrmMstHrdocumentComponent', () => {
  let component: HrmMstHrdocumentComponent;
  let fixture: ComponentFixture<HrmMstHrdocumentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmMstHrdocumentComponent]
    });
    fixture = TestBed.createComponent(HrmMstHrdocumentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
