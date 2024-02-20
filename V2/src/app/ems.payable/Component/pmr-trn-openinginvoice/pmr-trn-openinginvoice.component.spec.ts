import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnOpeninginvoiceComponent } from './pmr-trn-openinginvoice.component';

describe('PmrTrnOpeninginvoiceComponent', () => {
  let component: PmrTrnOpeninginvoiceComponent;
  let fixture: ComponentFixture<PmrTrnOpeninginvoiceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnOpeninginvoiceComponent]
    });
    fixture = TestBed.createComponent(PmrTrnOpeninginvoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
