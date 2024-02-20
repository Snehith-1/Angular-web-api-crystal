import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SysMstModulemanagerComponent } from './sys-mst-modulemanager.component';

describe('SysMstModulemanagerComponent', () => {
  let component: SysMstModulemanagerComponent;
  let fixture: ComponentFixture<SysMstModulemanagerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SysMstModulemanagerComponent]
    });
    fixture = TestBed.createComponent(SysMstModulemanagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
