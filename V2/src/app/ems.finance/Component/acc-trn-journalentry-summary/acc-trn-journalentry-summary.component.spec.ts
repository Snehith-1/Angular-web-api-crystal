import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccTrnJournalentrySummaryComponent } from './acc-trn-journalentry-summary.component';

describe('AccTrnJournalentrySummaryComponent', () => {
  let component: AccTrnJournalentrySummaryComponent;
  let fixture: ComponentFixture<AccTrnJournalentrySummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccTrnJournalentrySummaryComponent]
    });
    fixture = TestBed.createComponent(AccTrnJournalentrySummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
