import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomeTaxInvestmentComponent } from './income-tax-investment.component';

describe('IncomeTaxInvestmentComponent', () => {
  let component: IncomeTaxInvestmentComponent;
  let fixture: ComponentFixture<IncomeTaxInvestmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IncomeTaxInvestmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IncomeTaxInvestmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
