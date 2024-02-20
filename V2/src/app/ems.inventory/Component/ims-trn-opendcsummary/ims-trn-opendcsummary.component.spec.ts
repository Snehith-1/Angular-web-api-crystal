import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImsTrnOpendcsummaryComponent } from './ims-trn-opendcsummary.component';

describe('ImsTrnOpendcsummaryComponent', () => {
  let component: ImsTrnOpendcsummaryComponent;
  let fixture: ComponentFixture<ImsTrnOpendcsummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImsTrnOpendcsummaryComponent]
    });
    fixture = TestBed.createComponent(ImsTrnOpendcsummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
