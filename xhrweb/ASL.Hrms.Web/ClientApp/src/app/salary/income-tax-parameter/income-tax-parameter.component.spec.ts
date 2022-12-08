import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomeTaxParameterComponent } from './income-tax-parameter.component';

describe('IncomeTaxParameterComponent', () => {
  let component: IncomeTaxParameterComponent;
  let fixture: ComponentFixture<IncomeTaxParameterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IncomeTaxParameterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IncomeTaxParameterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
