import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrMstProductaddComponent } from './smr-mst-productadd.component';

describe('SmrMstProductaddComponent', () => {
  let component: SmrMstProductaddComponent;
  let fixture: ComponentFixture<SmrMstProductaddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrMstProductaddComponent]
    });
    fixture = TestBed.createComponent(SmrMstProductaddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
