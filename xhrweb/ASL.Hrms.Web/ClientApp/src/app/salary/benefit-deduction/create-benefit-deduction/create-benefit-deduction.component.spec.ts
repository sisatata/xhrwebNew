import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBenefitDeductionComponent } from './create-benefit-deduction.component';

describe('CreateBenefitDeductionComponent', () => {
  let component: CreateBenefitDeductionComponent;
  let fixture: ComponentFixture<CreateBenefitDeductionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateBenefitDeductionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateBenefitDeductionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
