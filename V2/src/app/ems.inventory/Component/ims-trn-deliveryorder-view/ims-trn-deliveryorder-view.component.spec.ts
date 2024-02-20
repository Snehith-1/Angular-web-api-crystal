import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImsTrnDeliveryorderViewComponent } from './ims-trn-deliveryorder-view.component';

describe('ImsTrnDeliveryorderViewComponent', () => {
  let component: ImsTrnDeliveryorderViewComponent;
  let fixture: ComponentFixture<ImsTrnDeliveryorderViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImsTrnDeliveryorderViewComponent]
    });
    fixture = TestBed.createComponent(ImsTrnDeliveryorderViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
