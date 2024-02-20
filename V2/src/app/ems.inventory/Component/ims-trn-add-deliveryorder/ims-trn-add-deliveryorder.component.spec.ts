import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImsTrnAddDeliveryorderComponent } from './ims-trn-add-deliveryorder.component';

describe('ImsTrnAddDeliveryorderComponent', () => {
  let component: ImsTrnAddDeliveryorderComponent;
  let fixture: ComponentFixture<ImsTrnAddDeliveryorderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImsTrnAddDeliveryorderComponent]
    });
    fixture = TestBed.createComponent(ImsTrnAddDeliveryorderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
