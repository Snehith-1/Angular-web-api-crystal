import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmTrnNewtaskComponent } from './crm-trn-newtask.component';

describe('CrmTrnNewtaskComponent', () => {
  let component: CrmTrnNewtaskComponent;
  let fixture: ComponentFixture<CrmTrnNewtaskComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmTrnNewtaskComponent]
    });
    fixture = TestBed.createComponent(CrmTrnNewtaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
