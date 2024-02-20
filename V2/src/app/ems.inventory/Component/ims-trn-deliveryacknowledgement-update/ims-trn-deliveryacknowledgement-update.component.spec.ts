import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImsTrnDeliveryacknowledgementUpdateComponent } from './ims-trn-deliveryacknowledgement-update.component';

describe('ImsTrnDeliveryacknowledgementUpdateComponent', () => {
  let component: ImsTrnDeliveryacknowledgementUpdateComponent;
  let fixture: ComponentFixture<ImsTrnDeliveryacknowledgementUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImsTrnDeliveryacknowledgementUpdateComponent]
    });
    fixture = TestBed.createComponent(ImsTrnDeliveryacknowledgementUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
