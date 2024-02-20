import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesteamManagerListComponent } from './salesteam-manager-list.component';

describe('SalesteamManagerListComponent', () => {
  let component: SalesteamManagerListComponent;
  let fixture: ComponentFixture<SalesteamManagerListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SalesteamManagerListComponent]
    });
    fixture = TestBed.createComponent(SalesteamManagerListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
