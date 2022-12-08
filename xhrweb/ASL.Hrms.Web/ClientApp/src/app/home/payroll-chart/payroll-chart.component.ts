import { AfterViewInit } from '@angular/core';
import { ViewChild } from '@angular/core';
import { ElementRef } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Guid } from 'src/app/shared/models';
import { FinancialYearService, PayrollChartService } from 'src/app/shared/services';
@Component({
  selector: 'app-payroll-chart',
  templateUrl: './payroll-chart.component.html',
  styleUrls: ['./payroll-chart.component.css']
})
export class PayrollChartComponent implements OnInit, AfterViewInit {
  monthName: string;
  companyId: any = localStorage.getItem('companyId');
  monthCycleId: any = Guid.empty;
  single: any[];
  Yearlycost: any[];
  view: any[] =[375, 375];
  grossSalary: any;
  netPayableAmount: any;
  payableSalary: any;
  totalDeductedAmount: any;
  yearName: any;
  @ViewChild('.total-label') myDiv: ElementRef;
  gradient: boolean = true;
  showLegend: boolean = true;
  showLabels: boolean = true;
  isDoughnut: boolean = false;
  months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };
  monthNumber: any;
  constructor(
    private payrollChartService: PayrollChartService,
    private financialYearService: FinancialYearService,
     private elementRef: ElementRef
  ) {
    let date = new Date();
    this.yearName = date.getFullYear();
    this.monthNumber = date.getMonth();
  }
  ngAfterViewInit(): void {
    this.getPayrollchartData();
    this.getCurrentFinancialYearId();
    let dom: HTMLElement = this.elementRef.nativeElement;
    let elements = dom.querySelectorAll('.total-label')[0];
    elements.innerHTML = 'Gross Salary';
  
  }
  ngOnInit(): void {
    const {width} = window.screen;
    this.view = width <= 768 ? [320, 375] : [375, 375];
    this.showLegend =  width <= 768 ? false : true;
  }
  getPayrollchartData(): void {
    this.payrollChartService.getPayrollChart(this.companyId, this.yearName, this.months[this.monthNumber - 1]).subscribe(result => {
      if(isNaN(result && result?.netPayableAmount)){
        result.netPayableAmount = 0;
       
      }
      if(isNaN(result && result?.totalDeductedAmount)){
        result.totalDeductedAmount = 0;
       
      }
      var v1: number = +result?.netPayableAmount;
      var v2: number = +result?.totalDeductedAmount;
      this.single = [
        {
          "name": "Net Payable Salary Department wise (approx)",
          "value": v1?v1:0
        },
        {
          "name": "Total Deduction (approx)",
          "value": v2?v2:0
        }
      ];
    }, () => {
    })
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
        this.Yearlycost = [
          {
            "name": "Net Payable Salary",
            "value": v1?v1:0
          },
          {
            "name": "Total Deduction",
            "value": v2?v2:0
          }
        ];
      }, () => { })
    }, () => {
    })
  }
}
