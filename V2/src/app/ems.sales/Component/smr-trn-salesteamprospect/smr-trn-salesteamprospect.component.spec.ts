import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrTrnSalesteamprospectComponent } from './smr-trn-salesteamprospect.component';

describe('SmrTrnSalesteamprospectComponent', () => {
  let component: SmrTrnSalesteamprospectComponent;
  let fixture: ComponentFixture<SmrTrnSalesteamprospectComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrTrnSalesteamprospectComponent]
    });
    fixture = TestBed.createComponent(SmrTrnSalesteamprospectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
