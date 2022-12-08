import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { timeStamp } from 'console';
import { LeaveApplication } from 'src/app/shared/models';
import { LeaveApplicationService, LeaveTypeService } from 'src/app/shared/services';
@Component({
  selector: 'app-leave-chart',
  templateUrl: './leave-chart.component.html',
  styleUrls: ['./leave-chart.component.css']
})
export class LeaveChartComponent implements OnInit, AfterViewInit {
  totalPresent: any;
  view: any[] = [375, 375];
  sickLeave: number = 0;
  casualLeave: number = 0;
  toDay: any;
  single = [
  ];
  // options
  gradient: boolean = true;
  showLegend: boolean = true;
  legendTitle: string = "Legend";
  showLabels: boolean = false;
  isDoughnut: boolean = false;
  legendPosition: string = "below";
  colorScheme = {
    domain: ["#5252FF", "#0ce8d6", "#e80cdd", "#e80c0c"]
  };
  totalLate: any;
  totalAbsent: any; 
  totalLeave: any;
  loading: boolean = false;
  companyId: any = localStorage.getItem('companyId');
  leaveTypes: any;
  leaveApplications: LeaveApplication[] = [];
  leaveChartEmpty: boolean;
  constructor(
    private leaveApplicationService: LeaveApplicationService,
    private datePipe: DatePipe
  ) { }
  ngAfterViewInit(): void {
    this.getAllLeaveTypeByCompanyId();
  }
  ngOnInit(): void {
    const {width} = window.screen;
    this.view = width <= 768 ? [320, 375] : [375, 375];
    this.showLegend =  width <= 768 ? false : true;
  }
  getAllLeaveTypeByCompanyId() {
    var currentDate = new Date();
    this.toDay = currentDate.getDate();
    currentDate.setDate(currentDate.getDate());
    let stDate = this.datePipe.transform(new Date(currentDate), 'MM-dd-yyyy');
    let edDate = this.datePipe.transform(new Date(new Date()), 'MM-dd-yyyy');
    this.leaveApplicationService.getAllLeaveApplicationByParameter(this.companyId, stDate, edDate, null, null, null).subscribe(result => {
      this.leaveApplications = result;
      this.loading = false;
      this.calculateLeave();
      // this.generateLeaveChart();
    }, () => {
      this.loading = false;
    })
  }
  calculateLeave(): void {
    for (let i = 0; i < this.leaveApplications.length; i++) {
      let edDate = new Date(this.leaveApplications[i].endDate);
      let getDate = edDate.getDate();
      if (this.leaveApplications[i].leaveTypeName === 'Sick Leave') {
        this.sickLeave++;
      }
      else if (this.leaveApplications[i].leaveTypeName === 'Casual Leave') {
        this.casualLeave++;
      }
    }
    this.single = [
      {
        name: "Sick",
        value: this.sickLeave
      },
      {
        name: "Casual",
        value: this.casualLeave
      }
    ];
    if (this.sickLeave === 0 && this.casualLeave === 0) {
      this.leaveChartEmpty = true
    }
  }
}
