import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RblTrnBobateainvoiceaddComponent } from './rbl-trn-bobateainvoiceadd.component';

describe('RblTrnBobateainvoiceaddComponent', () => {
  let component: RblTrnBobateainvoiceaddComponent;
  let fixture: ComponentFixture<RblTrnBobateainvoiceaddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RblTrnBobateainvoiceaddComponent]
    });
    fixture = TestBed.createComponent(RblTrnBobateainvoiceaddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
