import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnCustomerenquiryeditComponent } from './smr-trn-customerenquiryedit.component';

describe('SmrTrnCustomerenquiryeditComponent', () => {
  let component: SmrTrnCustomerenquiryeditComponent;
  let fixture: ComponentFixture<SmrTrnCustomerenquiryeditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnCustomerenquiryeditComponent]
    });
    fixture = TestBed.createComponent(SmrTrnCustomerenquiryeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
