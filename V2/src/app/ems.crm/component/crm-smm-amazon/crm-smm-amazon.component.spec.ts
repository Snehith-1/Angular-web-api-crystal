import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmSmmAmazonComponent } from './crm-smm-amazon.component';

describe('CrmSmmAmazonComponent', () => {
  let component: CrmSmmAmazonComponent;
  let fixture: ComponentFixture<CrmSmmAmazonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CrmSmmAmazonComponent]
    });
    fixture = TestBed.createComponent(CrmSmmAmazonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
