import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SysMstUserprofileComponent } from './sys-mst-userprofile.component';

describe('SysMstUserprofileComponent', () => {
  let component: SysMstUserprofileComponent;
  let fixture: ComponentFixture<SysMstUserprofileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SysMstUserprofileComponent]
    });
    fixture = TestBed.createComponent(SysMstUserprofileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
