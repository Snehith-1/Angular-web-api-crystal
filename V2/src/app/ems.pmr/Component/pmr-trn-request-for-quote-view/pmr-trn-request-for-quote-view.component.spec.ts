import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnRequestForQuoteViewComponent } from './pmr-trn-request-for-quote-view.component';

describe('PmrTrnRequestForQuoteViewComponent', () => {
  let component: PmrTrnRequestForQuoteViewComponent;
  let fixture: ComponentFixture<PmrTrnRequestForQuoteViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnRequestForQuoteViewComponent]
    });
    fixture = TestBed.createComponent(PmrTrnRequestForQuoteViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
