import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnDropComponent } from './smr-trn-drop.component';

describe('SmrTrnDropComponent', () => {
  let component: SmrTrnDropComponent;
  let fixture: ComponentFixture<SmrTrnDropComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnDropComponent]
    });
    fixture = TestBed.createComponent(SmrTrnDropComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
