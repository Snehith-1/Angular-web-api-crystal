import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrmTrnOfferletterComponent } from './hrm-trn-offerletter.component';

describe('HrmTrnOfferletterComponent', () => {
  let component: HrmTrnOfferletterComponent;
  let fixture: ComponentFixture<HrmTrnOfferletterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HrmTrnOfferletterComponent]
    });
    fixture = TestBed.createComponent(HrmTrnOfferletterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
