import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SysMstTemplateSummaryComponent } from './sys-mst-template-summary.component';

describe('SysMstTemplateSummaryComponent', () => {
  let component: SysMstTemplateSummaryComponent;
  let fixture: ComponentFixture<SysMstTemplateSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SysMstTemplateSummaryComponent]
    });
    fixture = TestBed.createComponent(SysMstTemplateSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
