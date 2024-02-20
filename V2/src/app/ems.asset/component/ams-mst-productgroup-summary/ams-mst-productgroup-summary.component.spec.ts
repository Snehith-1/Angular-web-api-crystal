import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AmsMstProductgroupSummaryComponent } from './ams-mst-productgroup-summary.component';

describe('AmsMstProductgroupSummaryComponent', () => {
  let component: AmsMstProductgroupSummaryComponent;
  let fixture: ComponentFixture<AmsMstProductgroupSummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AmsMstProductgroupSummaryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AmsMstProductgroupSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
