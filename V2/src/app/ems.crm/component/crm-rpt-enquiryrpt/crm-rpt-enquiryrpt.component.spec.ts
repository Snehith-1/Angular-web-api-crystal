import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmRptEnquiryrptComponent } from './crm-rpt-enquiryrpt.component';

describe('CrmRptEnquiryrptComponent', () => {
  let component: CrmRptEnquiryrptComponent;
  let fixture: ComponentFixture<CrmRptEnquiryrptComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmRptEnquiryrptComponent]
    });
    fixture = TestBed.createComponent(CrmRptEnquiryrptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
