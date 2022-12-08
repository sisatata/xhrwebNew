import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomeTaxPayerTypeComponent } from './income-tax-payer-type.component';

describe('IncomeTaxPayerTypeComponent', () => {
  let component: IncomeTaxPayerTypeComponent;
  let fixture: ComponentFixture<IncomeTaxPayerTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IncomeTaxPayerTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IncomeTaxPayerTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
