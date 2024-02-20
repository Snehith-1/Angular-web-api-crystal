import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AmsMstAttributeComponent } from './ams-mst-attribute.component';

describe('AmsMstAttributeComponent', () => {
  let component: AmsMstAttributeComponent;
  let fixture: ComponentFixture<AmsMstAttributeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AmsMstAttributeComponent]
    });
    fixture = TestBed.createComponent(AmsMstAttributeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
