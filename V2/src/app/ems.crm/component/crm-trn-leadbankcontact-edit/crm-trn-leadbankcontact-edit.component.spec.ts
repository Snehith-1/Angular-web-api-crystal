import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnLeadbankcontactEditComponent } from './crm-trn-leadbankcontact-edit.component';

describe('CrmTrnLeadbankcontactEditComponent', () => {
  let component: CrmTrnLeadbankcontactEditComponent;
  let fixture: ComponentFixture<CrmTrnLeadbankcontactEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnLeadbankcontactEditComponent]
    });
    fixture = TestBed.createComponent(CrmTrnLeadbankcontactEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
