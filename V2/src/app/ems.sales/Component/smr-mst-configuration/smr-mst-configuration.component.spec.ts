import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrMstConfigurationComponent } from './smr-mst-configuration.component';

describe('SmrMstConfigurationComponent', () => {
  let component: SmrMstConfigurationComponent;
  let fixture: ComponentFixture<SmrMstConfigurationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrMstConfigurationComponent]
    });
    fixture = TestBed.createComponent(SmrMstConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
