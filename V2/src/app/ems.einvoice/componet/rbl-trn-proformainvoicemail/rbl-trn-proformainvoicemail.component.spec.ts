import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RblTrnProformainvoicemailComponent } from './rbl-trn-proformainvoicemail.component';

describe('RblTrnProformainvoicemailComponent', () => {
  let component: RblTrnProformainvoicemailComponent;
  let fixture: ComponentFixture<RblTrnProformainvoicemailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RblTrnProformainvoicemailComponent]
    });
    fixture = TestBed.createComponent(RblTrnProformainvoicemailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
