import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Guid } from 'src/app/shared/models';
import { FinancialYearService, PayrollChartService } from 'src/app/shared/services';
@Component({
  selector: 'app-yearly-payroll-chart',
  templateUrl: './yearly-payroll-chart.component.html',
  styleUrls: ['./yearly-payroll-chart.component.css']
})
export class YearlyPayrollChartComponent implements OnInit, AfterViewInit {
  monthName: string;
  companyId: any = localStorage.getItem('companyId');
  monthCycleId: any = Guid.empty;
  single: any[];
  Yearlycost: any[];
  view: any[] = [375, 375];
  grossSalary: any;
  netPayableAmount: any;
  payableSalary: any;
  totalDeductedAmount: any;
  yearName: any;
  // options
  gradient: boolean = true;
  showLegend: boolean = true;
  showLabels: boolean = true;
  isDoughnut: boolean = false;
  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };
  yearlycost: { name: string; value: number; }[];
  constructor(
    private payrollChartService: PayrollChartService,
    private financialYearService: FinancialYearService,
  ) {
    let date = new Date();
    this.yearName = date.getFullYear();
  }
  ngAfterViewInit(): void {
    this.getCurrentFinancialYearId();
  }
  ngOnInit(): void {
    const {width} = window.screen;
    this.view = width <= 768 ? [320, 375] : [375, 375];
    this.showLegend =  width <= 768 ? false : true;
  }
  getCurrentFinancialYearId(): void {
    this.financialYearService.getCurrentFinancialYearIdByCompanyAndYearName(this.companyId, this.yearName).subscribe(result => {
      let yearId = (result as any).financialYearId;
      this.payrollChartService.getYearlyPayrollChartByCompanyIdAndYearId(this.companyId, yearId).subscribe(res => {
        if(isNaN(res && res?.netPayableAmount)){
          res.netPayableAmount = 0;
        }

        if(isNaN(res && res?.totalDeductedAmount)){
          res.totalDeductedAmount = 0;
        }
        var v1: number = +res?.netPayableAmount;
        var v2: number = +res?.totalDeductedAmount;
        this.yearlycost = [
          {
            "name": "Net Payable Salary (approx)",
            "value": v1?v1:0
          },
          {
            "name": "Total Deduction (approx)",
            "value": v2?v2:0
          }
        ];
      }, () => { })
    }, () => {
    })
  }
}
