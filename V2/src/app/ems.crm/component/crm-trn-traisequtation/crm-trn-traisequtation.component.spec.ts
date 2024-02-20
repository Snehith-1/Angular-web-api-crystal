import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnTraisequtationComponent } from './crm-trn-traisequtation.component';

describe('CrmTrnTraisequtationComponent', () => {
  let component: CrmTrnTraisequtationComponent;
  let fixture: ComponentFixture<CrmTrnTraisequtationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnTraisequtationComponent]
    });
    fixture = TestBed.createComponent(CrmTrnTraisequtationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
