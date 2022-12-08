import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PayrollChartComponent } from './payroll-chart.component';

describe('PayrollChartComponent', () => {
  let component: PayrollChartComponent;
  let fixture: ComponentFixture<PayrollChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PayrollChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PayrollChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
