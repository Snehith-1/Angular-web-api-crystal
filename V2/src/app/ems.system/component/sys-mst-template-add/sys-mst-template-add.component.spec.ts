import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SysMstTemplateAddComponent } from './sys-mst-template-add.component';

describe('SysMstTemplateAddComponent', () => {
  let component: SysMstTemplateAddComponent;
  let fixture: ComponentFixture<SysMstTemplateAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SysMstTemplateAddComponent]
    });
    fixture = TestBed.createComponent(SysMstTemplateAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
