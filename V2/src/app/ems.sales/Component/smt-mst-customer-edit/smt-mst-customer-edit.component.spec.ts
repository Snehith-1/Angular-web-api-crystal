import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmtMstCustomerEditComponent } from './smt-mst-customer-edit.component';

describe('SmtMstCustomerEditComponent', () => {
  let component: SmtMstCustomerEditComponent;
  let fixture: ComponentFixture<SmtMstCustomerEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmtMstCustomerEditComponent]
    });
    fixture = TestBed.createComponent(SmtMstCustomerEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
