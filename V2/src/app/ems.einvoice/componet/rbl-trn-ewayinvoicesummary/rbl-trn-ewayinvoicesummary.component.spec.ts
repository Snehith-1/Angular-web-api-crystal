import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RblTrnEwayinvoicesummaryComponent } from './rbl-trn-ewayinvoicesummary.component';

describe('RblTrnEwayinvoicesummaryComponent', () => {
  let component: RblTrnEwayinvoicesummaryComponent;
  let fixture: ComponentFixture<RblTrnEwayinvoicesummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RblTrnEwayinvoicesummaryComponent]
    });
    fixture = TestBed.createComponent(RblTrnEwayinvoicesummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
