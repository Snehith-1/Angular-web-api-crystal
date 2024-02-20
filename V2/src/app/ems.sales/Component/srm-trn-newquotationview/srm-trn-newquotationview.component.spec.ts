import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SrmTrnNewquotationviewComponent } from './srm-trn-newquotationview.component';

describe('SrmTrnNewquotationviewComponent', () => {
  let component: SrmTrnNewquotationviewComponent;
  let fixture: ComponentFixture<SrmTrnNewquotationviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SrmTrnNewquotationviewComponent]
    });
    fixture = TestBed.createComponent(SrmTrnNewquotationviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
