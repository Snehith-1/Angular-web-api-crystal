import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnGrninwardViewComponent } from './pmr-trn-grninward-view.component';

describe('PmrTrnGrninwardViewComponent', () => {
  let component: PmrTrnGrninwardViewComponent;
  let fixture: ComponentFixture<PmrTrnGrninwardViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnGrninwardViewComponent]
    });
    fixture = TestBed.createComponent(PmrTrnGrninwardViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
