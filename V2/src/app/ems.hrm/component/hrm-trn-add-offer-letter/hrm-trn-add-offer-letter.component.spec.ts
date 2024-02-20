import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnAddOfferLetterComponent } from './hrm-trn-add-offer-letter.component';

describe('HrmTrnAddOfferLetterComponent', () => {
  let component: HrmTrnAddOfferLetterComponent;
  let fixture: ComponentFixture<HrmTrnAddOfferLetterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnAddOfferLetterComponent]
    });
    fixture = TestBed.createComponent(HrmTrnAddOfferLetterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
