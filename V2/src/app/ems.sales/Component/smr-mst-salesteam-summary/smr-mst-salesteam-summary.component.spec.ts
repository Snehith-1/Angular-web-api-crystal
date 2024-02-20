import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SmrMstSalesteamSummaryComponent } from './smr-mst-salesteam-summary.component';

describe('SmrMstSalesteamSummaryComponent', () => {
  let component: SmrMstSalesteamSummaryComponent;
  let fixture: ComponentFixture<SmrMstSalesteamSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrMstSalesteamSummaryComponent]
    });
    fixture = TestBed.createComponent(SmrMstSalesteamSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
