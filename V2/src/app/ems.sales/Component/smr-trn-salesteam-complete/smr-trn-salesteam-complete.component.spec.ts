import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnSalesteamCompleteComponent } from './smr-trn-salesteam-complete.component';

describe('SmrTrnSalesteamCompleteComponent', () => {
  let component: SmrTrnSalesteamCompleteComponent;
  let fixture: ComponentFixture<SmrTrnSalesteamCompleteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnSalesteamCompleteComponent]
    });
    fixture = TestBed.createComponent(SmrTrnSalesteamCompleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
