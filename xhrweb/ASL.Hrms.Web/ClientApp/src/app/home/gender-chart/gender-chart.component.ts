import { AfterViewInit, Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services';
@Component({
  selector: 'app-gender-chart',
  templateUrl: './gender-chart.component.html',
  styleUrls: ['./gender-chart.component.css']
})
export class GenderChartComponent implements OnInit, AfterViewInit {
  single: any[];
  multi: any[];
  view: any[] = [375, 375];
  // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  legendPosition: string = "below";
  xAxisLabel = 'Gender';
  showYAxisLabel = true;
  yAxisLabel = 'Number';
  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };
  employees: any;
  constructor(
    private employeeService: EmployeeService
  ) { }
  ngAfterViewInit(){
    this.getAllEmployees();
  }
  ngOnInit(): void {
    const {width} = window.screen;
    this.view = width <= 768 ? [320, 375] : [375, 375];
    this.showLegend =  width <= 768 ? false : true;
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
      this.generateGenderChart(result);
    }, () => {
    });
  }
  generateGenderChart(employees: any): void {
    let male = 0;
    let female = 0;
    employees.forEach(function (value) {
      if (value.genderName === 'Female') {
        female++;
      }
      else if (value.genderName === 'Male')
        male++;
    });
    this.single = [
      {
        name: 'Female ',
        value: female
      },
      {
        name: 'Male ' ,
        value: male
      }
    ]
  }
}
