import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnRaiseRequisitionComponent } from './pmr-trn-raise-requisition.component';

describe('PmrTrnRaiseRequisitionComponent', () => {
  let component: PmrTrnRaiseRequisitionComponent;
  let fixture: ComponentFixture<PmrTrnRaiseRequisitionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnRaiseRequisitionComponent]
    });
    fixture = TestBed.createComponent(PmrTrnRaiseRequisitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
