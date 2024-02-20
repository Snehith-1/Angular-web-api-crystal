import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrRptEmployeewisereportComponent } from './smr-rpt-employeewisereport.component';

describe('SmrRptEmployeewisereportComponent', () => {
  let component: SmrRptEmployeewisereportComponent;
  let fixture: ComponentFixture<SmrRptEmployeewisereportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrRptEmployeewisereportComponent]
    });
    fixture = TestBed.createComponent(SmrRptEmployeewisereportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
