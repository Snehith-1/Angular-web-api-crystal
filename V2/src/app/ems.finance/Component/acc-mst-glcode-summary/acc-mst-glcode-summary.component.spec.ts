import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccMstGlcodeSummaryComponent } from './acc-mst-glcode-summary.component';

describe('AccMstGlcodeSummaryComponent', () => {
  let component: AccMstGlcodeSummaryComponent;
  let fixture: ComponentFixture<AccMstGlcodeSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccMstGlcodeSummaryComponent]
    });
    fixture = TestBed.createComponent(AccMstGlcodeSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
