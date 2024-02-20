import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnPotentialComponent } from './smr-trn-potential.component';

describe('SmrTrnPotentialComponent', () => {
  let component: SmrTrnPotentialComponent;
  let fixture: ComponentFixture<SmrTrnPotentialComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnPotentialComponent]
    });
    fixture = TestBed.createComponent(SmrTrnPotentialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
