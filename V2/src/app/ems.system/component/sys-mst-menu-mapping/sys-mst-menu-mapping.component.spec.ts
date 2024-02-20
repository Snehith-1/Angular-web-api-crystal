import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SysMstMenuMappingComponent } from './sys-mst-menu-mapping.component';

describe('SysMstMenuMappingComponent', () => {
  let component: SysMstMenuMappingComponent;
  let fixture: ComponentFixture<SysMstMenuMappingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SysMstMenuMappingComponent]
    });
    fixture = TestBed.createComponent(SysMstMenuMappingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
