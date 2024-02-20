import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImsTrnDeliveryacknowlegdementAddComponent } from './ims-trn-deliveryacknowlegdement-add.component';

describe('ImsTrnDeliveryacknowlegdementAddComponent', () => {
  let component: ImsTrnDeliveryacknowlegdementAddComponent;
  let fixture: ComponentFixture<ImsTrnDeliveryacknowlegdementAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImsTrnDeliveryacknowlegdementAddComponent]
    });
    fixture = TestBed.createComponent(ImsTrnDeliveryacknowlegdementAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
