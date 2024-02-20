import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImsTrnDespatchViewComponent } from './ims-trn-despatch-view.component';

describe('ImsTrnDespatchViewComponent', () => {
  let component: ImsTrnDespatchViewComponent;
  let fixture: ComponentFixture<ImsTrnDespatchViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImsTrnDespatchViewComponent]
    });
    fixture = TestBed.createComponent(ImsTrnDespatchViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
