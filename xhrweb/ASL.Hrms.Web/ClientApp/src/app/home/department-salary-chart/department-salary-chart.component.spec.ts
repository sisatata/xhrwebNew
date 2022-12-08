import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentSalaryChartComponent } from './department-salary-chart.component';

describe('DepartmentSalaryChartComponent', () => {
  let component: DepartmentSalaryChartComponent;
  let fixture: ComponentFixture<DepartmentSalaryChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DepartmentSalaryChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentSalaryChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
