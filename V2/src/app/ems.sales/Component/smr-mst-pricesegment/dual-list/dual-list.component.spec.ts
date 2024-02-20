import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DualListComponent } from './dual-list.component';

describe('DualListComponent', () => {
  let component: DualListComponent;
  let fixture: ComponentFixture<DualListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DualListComponent]
    });
    fixture = TestBed.createComponent(DualListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
