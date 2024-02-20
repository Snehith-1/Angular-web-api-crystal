import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmRptProductgroupreportComponent } from './crm-rpt-productgroupreport.component';

describe('CrmRptProductgroupreportComponent', () => {
  let component: CrmRptProductgroupreportComponent;
  let fixture: ComponentFixture<CrmRptProductgroupreportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmRptProductgroupreportComponent]
    });
    fixture = TestBed.createComponent(CrmRptProductgroupreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
