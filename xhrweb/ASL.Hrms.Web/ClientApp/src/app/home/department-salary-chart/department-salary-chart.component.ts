import { AfterViewInit, Component, OnInit } from '@angular/core';
import { truncate } from 'fs';
import { AuthService } from 'src/app/auth/services/auth.service';
import { PayrollChartService } from 'src/app/shared/services';
@Component({
  selector: 'app-department-salary-chart',
  templateUrl: './department-salary-chart.component.html',
  styleUrls: ['./department-salary-chart.component.css']
})
export class DepartmentSalaryChartComponent implements OnInit, AfterViewInit {
  yearName: any;
  monthName: any;
  monthNumber: any;
  single: any[];
  view: any[] = [375, 375];
  // options
  showXAxis: boolean = true;
  showYAxis: boolean = true;
  gradient: boolean = false;
  showLegend: boolean = true;
  showXAxisLabel: boolean = true;
  yAxisLabel: string = 'Salary';
  showYAxisLabel: boolean = true;
  xAxisLabel: string = 'Departments';
  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };
  loading: boolean = false;
  chartValue: any = []
  companyId: any = localStorage.getItem('companyId');
  months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
  constructor(
    private authService: AuthService,
    private payrollChartService: PayrollChartService,
  ) {
    var d = new Date();
    this.yearName = d.getFullYear();
    this.monthNumber = d.getMonth();
  }
  ngAfterViewInit(): void {
    this.loading = true;
    this.payrollChartService.getDepartmentMonthlySalary(this.companyId, this.yearName, this.months[this.monthNumber-1]).subscribe(result => {
      let data = result.filter(item => item.salary > 0);
      let uniqueValues = [...new Set(result.map((item: { departmentName: any; }) => item.departmentName))];
      uniqueValues.forEach(item => {
        let index = data.findIndex(x => x.departmentName === item);
        if (index > -1) {
          let value = +data[index].salary;
          let salary = value.toFixed(2);
          this.chartValue.push({
            "name": data[index].departmentName,
            "value": salary.toString()
          });
        }
        else {
          this.chartValue.push({
            "name": item,
            "value": 0
          });
        }
      });
      this.single = this.chartValue;
    }, () => {
    })
  }
  ngOnInit(): void {
    const {width} = window.screen;
    this.view = width <= 768 ? [320, 375] : [375, 375];
    this.showLegend =  width <= 768 ? false : true;
  }


}
