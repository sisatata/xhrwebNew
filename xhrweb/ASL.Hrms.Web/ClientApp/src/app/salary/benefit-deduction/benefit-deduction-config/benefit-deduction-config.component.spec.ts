import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BenefitDeductionConfigComponent } from './benefit-deduction-config.component';

describe('BenefitDeductionConfigComponent', () => {
  let component: BenefitDeductionConfigComponent;
  let fixture: ComponentFixture<BenefitDeductionConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BenefitDeductionConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BenefitDeductionConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
