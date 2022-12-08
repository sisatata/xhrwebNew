import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateIncomeTaxPayerTypeComponent } from './create-income-tax-payer-type.component';

describe('CreateIncomeTaxPayerTypeComponent', () => {
  let component: CreateIncomeTaxPayerTypeComponent;
  let fixture: ComponentFixture<CreateIncomeTaxPayerTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateIncomeTaxPayerTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateIncomeTaxPayerTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
