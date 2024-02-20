import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImsTrnDeliveryorderComponent } from './ims-trn-deliveryorder.component';

describe('ImsTrnDeliveryorderComponent', () => {
  let component: ImsTrnDeliveryorderComponent;
  let fixture: ComponentFixture<ImsTrnDeliveryorderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImsTrnDeliveryorderComponent]
    });
    fixture = TestBed.createComponent(ImsTrnDeliveryorderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
