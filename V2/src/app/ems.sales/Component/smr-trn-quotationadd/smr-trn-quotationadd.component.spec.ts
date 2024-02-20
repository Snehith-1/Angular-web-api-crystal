import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnQuotationaddComponent } from './smr-trn-quotationadd.component';

describe('SmrTrnQuotationaddComponent', () => {
  let component: SmrTrnQuotationaddComponent;
  let fixture: ComponentFixture<SmrTrnQuotationaddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnQuotationaddComponent]
    });
    fixture = TestBed.createComponent(SmrTrnQuotationaddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
