import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RblTrnEwayAddComponent } from './rbl-trn-eway-add.component';

describe('RblTrnEwayAddComponent', () => {
  let component: RblTrnEwayAddComponent;
  let fixture: ComponentFixture<RblTrnEwayAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RblTrnEwayAddComponent]
    });
    fixture = TestBed.createComponent(RblTrnEwayAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
