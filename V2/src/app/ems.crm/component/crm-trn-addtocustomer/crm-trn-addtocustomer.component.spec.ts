import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnAddtocustomerComponent } from './crm-trn-addtocustomer.component';

describe('CrmTrnAddtocustomerComponent', () => {
  let component: CrmTrnAddtocustomerComponent;
  let fixture: ComponentFixture<CrmTrnAddtocustomerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnAddtocustomerComponent]
    });
    fixture = TestBed.createComponent(CrmTrnAddtocustomerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
