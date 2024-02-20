import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImsTrnRaiseDeliveryorderComponent } from './ims-trn-raise-deliveryorder.component';

describe('ImsTrnRaiseDeliveryorderComponent', () => {
  let component: ImsTrnRaiseDeliveryorderComponent;
  let fixture: ComponentFixture<ImsTrnRaiseDeliveryorderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImsTrnRaiseDeliveryorderComponent]
    });
    fixture = TestBed.createComponent(ImsTrnRaiseDeliveryorderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
