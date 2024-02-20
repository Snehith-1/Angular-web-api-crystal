import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnTrasieinvoiceComponent } from './crm-trn-trasieinvoice.component';

describe('CrmTrnTrasieinvoiceComponent', () => {
  let component: CrmTrnTrasieinvoiceComponent;
  let fixture: ComponentFixture<CrmTrnTrasieinvoiceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnTrasieinvoiceComponent]
    });
    fixture = TestBed.createComponent(CrmTrnTrasieinvoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
