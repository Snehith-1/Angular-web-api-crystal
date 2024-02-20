import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayTrnLoaneditComponent } from './pay-trn-loanedit.component';

describe('PayTrnLoaneditComponent', () => {
  let component: PayTrnLoaneditComponent;
  let fixture: ComponentFixture<PayTrnLoaneditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PayTrnLoaneditComponent]
    });
    fixture = TestBed.createComponent(PayTrnLoaneditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
