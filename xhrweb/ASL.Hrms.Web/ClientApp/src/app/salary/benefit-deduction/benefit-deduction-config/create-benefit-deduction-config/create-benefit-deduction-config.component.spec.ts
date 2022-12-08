import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBenefitDeductionConfigComponent } from './create-benefit-deduction-config.component';

describe('CreateBenefitDeductionConfigComponent', () => {
  let component: CreateBenefitDeductionConfigComponent;
  let fixture: ComponentFixture<CreateBenefitDeductionConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateBenefitDeductionConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateBenefitDeductionConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
