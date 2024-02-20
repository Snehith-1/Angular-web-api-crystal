import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SysMstTemplateEditComponent } from './sys-mst-template-edit.component';

describe('SysMstTemplateEditComponent', () => {
  let component: SysMstTemplateEditComponent;
  let fixture: ComponentFixture<SysMstTemplateEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SysMstTemplateEditComponent]
    });
    fixture = TestBed.createComponent(SysMstTemplateEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
