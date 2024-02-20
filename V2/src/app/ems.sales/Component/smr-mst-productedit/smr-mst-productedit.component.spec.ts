import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmrMstProducteditComponent } from './smr-mst-productedit.component';

describe('SmrMstProducteditComponent', () => {
  let component: SmrMstProducteditComponent;
  let fixture: ComponentFixture<SmrMstProducteditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SmrMstProducteditComponent]
    });
    fixture = TestBed.createComponent(SmrMstProducteditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
