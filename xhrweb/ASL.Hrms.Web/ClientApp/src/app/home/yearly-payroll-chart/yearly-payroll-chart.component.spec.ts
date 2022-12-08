import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { YearlyPayrollChartComponent } from './yearly-payroll-chart.component';

describe('YearlyPayrollChartComponent', () => {
  let component: YearlyPayrollChartComponent;
  let fixture: ComponentFixture<YearlyPayrollChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YearlyPayrollChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YearlyPayrollChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
