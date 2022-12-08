import { AfterViewInit, Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/services/auth.service';
import { DatePipe } from '@angular/common';
import { NgxChartsModule } from "@swimlane/ngx-charts";
import { AttendanceChartService } from 'src/app/shared/services';
import { Observable } from 'rxjs';
import { ViewChild } from '@angular/core';
import { MatAccordion } from '@angular/material/expansion';
@Component({
  selector: 'app-attendance-chart',
  templateUrl: './attendance-chart.component.html',
  styleUrls: ['./attendance-chart.component.css']
})
export class AttendanceChartComponent implements OnInit, AfterViewInit {
  totalPresent: any;
  view: any[] = [375, 375];
  single = [
   
  ];
  // options
  gradient: boolean = true;
  showLegend: boolean = true;
  legendTitle:string="Legend";  // 3 25aaf5
  showLabels: boolean = false;
  isDoughnut: boolean = false;
  legendPosition: string = "below"; // 1bc47d  25aaf5 ff6161 0083a0  domain: ["#1bc47d ", "#ff6161", "#25aaf5","#0083a0"]
  colorScheme = {
    domain: ["#25aaf5 ", "#ff6161", "#52ccaf","#5c52cc"]
  };
  totalLate: any;
  totalAbsent: any;0x5252FF 
  totalLeave: any;
  loading: boolean = false;
  companyId: any = localStorage.getItem('companyId');
  managerId: string;
  constructor(private authService: AuthService, private datePipe: DatePipe, private chartService: AttendanceChartService) {
    this.managerId = this.authService.getLoggedInUserInfo().employeeId;
  }
  ngAfterViewInit(): void {
    this.getData();
  }
  ngOnInit(): void {
    const {width} = window.screen;
    this.view = width <= 768 ? [320, 375] : [375, 375];
    this.showLegend =  width <= 768 ? false : true;
   
  }
  generateChart(): void {
    this.single = [
      {
        name: "Present " ,
        value: this.totalPresent
      },
      {
        name: "Late " ,
        value: this.totalLate
      },
      {
        name: "Absent "  ,
        value: this.totalAbsent,
      },
      {
        name: "Leave " ,
        value: this.totalLeave
      }
    ];
  }
  getData() {
    let today = new Date();
    let punchDate = this.datePipe.transform(new Date(today), 'MM-dd-yyyy');
    this.loading = false;

    this.chartService.getAttendanceChartData(this.companyId, this.managerId,
      punchDate).subscribe(result => {
        this.totalAbsent = result.dailyAttendanceSummary.totalAbsent;
        this.totalLate = result.dailyAttendanceSummary.totalLate;
        this.totalPresent = result.dailyAttendanceSummary.totalPresent;
        this.totalLeave = result.dailyAttendanceSummary.totalLeave;
        this.generateChart();
      })
  }
}
