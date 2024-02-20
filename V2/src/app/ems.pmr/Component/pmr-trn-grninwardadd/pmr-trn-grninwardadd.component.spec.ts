import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmrTrnGrninwardaddComponent } from './pmr-trn-grninwardadd.component';

describe('PmrTrnGrninwardaddComponent', () => {
  let component: PmrTrnGrninwardaddComponent;
  let fixture: ComponentFixture<PmrTrnGrninwardaddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmrTrnGrninwardaddComponent]
    });
    fixture = TestBed.createComponent(PmrTrnGrninwardaddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
