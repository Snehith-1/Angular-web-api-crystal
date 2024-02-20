import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SysRptEmployeereportComponent } from './sys-rpt-employeereport.component';

describe('SysRptEmployeereportComponent', () => {
  let component: SysRptEmployeereportComponent;
  let fixture: ComponentFixture<SysRptEmployeereportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SysRptEmployeereportComponent]
    });
    fixture = TestBed.createComponent(SysRptEmployeereportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
