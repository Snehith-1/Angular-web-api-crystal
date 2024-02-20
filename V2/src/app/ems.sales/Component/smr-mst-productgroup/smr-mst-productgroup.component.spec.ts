import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrMstProductgroupComponent } from './smr-mst-productgroup.component';

describe('SmrMstProductgroupComponent', () => {
  let component: SmrMstProductgroupComponent;
  let fixture: ComponentFixture<SmrMstProductgroupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrMstProductgroupComponent]
    });
    fixture = TestBed.createComponent(SmrMstProductgroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
