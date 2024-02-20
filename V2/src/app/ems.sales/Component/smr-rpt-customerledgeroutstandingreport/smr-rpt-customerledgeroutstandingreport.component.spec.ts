import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptCustomerledgeroutstandingreportComponent } from './smr-rpt-customerledgeroutstandingreport.component';

describe('SmrRptCustomerledgeroutstandingreportComponent', () => {
  let component: SmrRptCustomerledgeroutstandingreportComponent;
  let fixture: ComponentFixture<SmrRptCustomerledgeroutstandingreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptCustomerledgeroutstandingreportComponent]
    });
    fixture = TestBed.createComponent(SmrRptCustomerledgeroutstandingreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
