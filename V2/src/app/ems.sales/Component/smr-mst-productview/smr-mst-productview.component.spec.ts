import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrMstProductviewComponent } from './smr-mst-productview.component';

describe('SmrMstProductviewComponent', () => {
  let component: SmrMstProductviewComponent;
  let fixture: ComponentFixture<SmrMstProductviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrMstProductviewComponent]
    });
    fixture = TestBed.createComponent(SmrMstProductviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
