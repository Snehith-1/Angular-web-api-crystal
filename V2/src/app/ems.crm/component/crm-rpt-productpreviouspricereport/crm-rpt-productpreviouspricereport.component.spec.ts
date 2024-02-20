import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmRptProductpreviouspricereportComponent } from './crm-rpt-productpreviouspricereport.component';

describe('CrmRptProductpreviouspricereportComponent', () => {
  let component: CrmRptProductpreviouspricereportComponent;
  let fixture: ComponentFixture<CrmRptProductpreviouspricereportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmRptProductpreviouspricereportComponent]
    });
    fixture = TestBed.createComponent(CrmRptProductpreviouspricereportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
