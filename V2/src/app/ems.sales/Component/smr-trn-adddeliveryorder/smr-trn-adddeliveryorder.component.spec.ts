import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnAdddeliveryorderComponent } from './smr-trn-adddeliveryorder.component';

describe('SmrTrnAdddeliveryorderComponent', () => {
  let component: SmrTrnAdddeliveryorderComponent;
  let fixture: ComponentFixture<SmrTrnAdddeliveryorderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnAdddeliveryorderComponent]
    });
    fixture = TestBed.createComponent(SmrTrnAdddeliveryorderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
