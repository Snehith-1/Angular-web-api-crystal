import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnSalesorderviewComponent } from './smr-trn-salesorderview.component';

describe('SmrTrnSalesorderviewComponent', () => {
  let component: SmrTrnSalesorderviewComponent;
  let fixture: ComponentFixture<SmrTrnSalesorderviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnSalesorderviewComponent]
    });
    fixture = TestBed.createComponent(SmrTrnSalesorderviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
