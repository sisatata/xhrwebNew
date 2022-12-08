import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEmployeeBenefitDeductionComponent } from './create-employee-benefit-deduction.component';

describe('CreateEmployeeBenefitDeductionComponent', () => {
  let component: CreateEmployeeBenefitDeductionComponent;
  let fixture: ComponentFixture<CreateEmployeeBenefitDeductionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEmployeeBenefitDeductionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployeeBenefitDeductionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
