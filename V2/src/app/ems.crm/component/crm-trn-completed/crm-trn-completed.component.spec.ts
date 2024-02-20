import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnCompletedComponent } from './crm-trn-completed.component';

describe('CrmTrnCompletedComponent', () => {
  let component: CrmTrnCompletedComponent;
  let fixture: ComponentFixture<CrmTrnCompletedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnCompletedComponent]
    });
    fixture = TestBed.createComponent(CrmTrnCompletedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
