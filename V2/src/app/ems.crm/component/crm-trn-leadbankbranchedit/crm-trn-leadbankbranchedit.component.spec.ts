import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnLeadbankbrancheditComponent } from './crm-trn-leadbankbranchedit.component';

describe('CrmTrnLeadbankbrancheditComponent', () => {
  let component: CrmTrnLeadbankbrancheditComponent;
  let fixture: ComponentFixture<CrmTrnLeadbankbrancheditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnLeadbankbrancheditComponent]
    });
    fixture = TestBed.createComponent(CrmTrnLeadbankbrancheditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
