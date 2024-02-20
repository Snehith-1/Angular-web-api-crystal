import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnSales360Component } from './smr-trn-sales360.component';

describe('SmrTrnSales360Component', () => {
  let component: SmrTrnSales360Component;
  let fixture: ComponentFixture<SmrTrnSales360Component>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnSales360Component]
    });
    fixture = TestBed.createComponent(SmrTrnSales360Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
