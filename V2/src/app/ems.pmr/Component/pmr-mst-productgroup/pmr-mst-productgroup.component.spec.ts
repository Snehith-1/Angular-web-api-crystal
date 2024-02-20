import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrMstProductgroupComponent } from './pmr-mst-productgroup.component';

describe('PmrMstProductgroupComponent', () => {
  let component: PmrMstProductgroupComponent;
  let fixture: ComponentFixture<PmrMstProductgroupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrMstProductgroupComponent]
    });
    fixture = TestBed.createComponent(PmrMstProductgroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
