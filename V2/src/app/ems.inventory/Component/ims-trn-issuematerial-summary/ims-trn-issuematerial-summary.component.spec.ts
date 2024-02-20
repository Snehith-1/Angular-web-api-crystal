import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImsTrnIssuematerialSummaryComponent } from './ims-trn-issuematerial-summary.component';

describe('ImsTrnIssuematerialSummaryComponent', () => {
  let component: ImsTrnIssuematerialSummaryComponent;
  let fixture: ComponentFixture<ImsTrnIssuematerialSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImsTrnIssuematerialSummaryComponent]
    });
    fixture = TestBed.createComponent(ImsTrnIssuematerialSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
