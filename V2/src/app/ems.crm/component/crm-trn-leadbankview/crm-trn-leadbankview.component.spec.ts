import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnLeadbankviewComponent } from './crm-trn-leadbankview.component';

describe('CrmTrnLeadbankviewComponent', () => {
  let component: CrmTrnLeadbankviewComponent;
  let fixture: ComponentFixture<CrmTrnLeadbankviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnLeadbankviewComponent]
    });
    fixture = TestBed.createComponent(CrmTrnLeadbankviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
