import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateIncomeTaxInvestmentComponent } from './create-income-tax-investment.component';

describe('CreateIncomeTaxInvestmentComponent', () => {
  let component: CreateIncomeTaxInvestmentComponent;
  let fixture: ComponentFixture<CreateIncomeTaxInvestmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateIncomeTaxInvestmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateIncomeTaxInvestmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
