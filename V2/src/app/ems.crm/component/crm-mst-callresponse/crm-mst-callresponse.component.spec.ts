import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmMstCallresponseComponent } from './crm-mst-callresponse.component';

describe('CrmMstCallresponseComponent', () => {
  let component: CrmMstCallresponseComponent;
  let fixture: ComponentFixture<CrmMstCallresponseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmMstCallresponseComponent]
    });
    fixture = TestBed.createComponent(CrmMstCallresponseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
