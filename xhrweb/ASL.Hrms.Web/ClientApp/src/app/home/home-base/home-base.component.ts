import { DatePipe } from '@angular/common';
import { AfterViewInit, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatAccordion } from '@angular/material/expansion';
import { AuthService } from 'src/app/auth/services/auth.service';
import { CountChartService, EmployeeService } from 'src/app/shared/services';
import { ActiveEmployeeListComponent } from '../active-employee-list/active-employee-list.component';
import { EmployeeConfirmationListComponent } from '../employee-confirmation-list/employee-confirmation-list.component';
import { NewEmployeeListComponent } from '../new-employee-list/new-employee-list.component';
import { NoticeDetailsComponent } from '../notice-details/notice-details.component';
import { NotificationDetailsComponent } from '../notification-details/notification-details.component';
@Component({
  selector: 'app-home-base',
  templateUrl: './home-base.component.html',
  styleUrls: ['./home-base.component.css']
})
export class HomeBaseComponent implements OnInit, AfterViewInit {
  isAdmin: boolean = false;
  isEmployee: boolean = false;
  managerId: any;
  @ViewChild(MatAccordion) accordion: MatAccordion;
  companyId: any = localStorage.getItem('companyId');
  notifications: any;
  employees: number;
  billAmount: any;
  panelOpenState = false;
  currentYearNewEmployee: any;
  confirmedEmployees: any;
  currentYearName: any;
  employeesDetail: any;
  noticesDetail: any;
  single = [
  ];
  view: any[] = [1185, 900];
  colorScheme = {
    domain: ['#5AA454', '#E44D25', '#CFC0BB', '#7aa3e5', '#a8385d', '#aae3f5']
  };
  cardColor: string = '#FFFFFF';
  currentMonth: number;
  currentYear: number;
  currentDate: number;
  notices: any;
  currentMonthNewEmployee: any;
  ispayrollManager: any;
  constructor(
    private authService: AuthService,
    private countChartService: CountChartService,
    private employeeService: EmployeeService,
    private datePipe: DatePipe,
    private dialog: MatDialog,
  ) {
    this.isAdmin = this.authService.isAdmin;
    this.isEmployee = this.authService.isEmployee;
    this.managerId = this.authService.getLoggedInUserInfo().employeeId;
    this.ispayrollManager = this.authService.isPayrollManger;
  }
  ngAfterViewInit(): void {
    this.generateFullChart();
  }
 
  ngOnInit(): void {
    const cardChartWidth = window.screen.width ;
     if(cardChartWidth<=1199 && cardChartWidth > 1024){
      this.view = [600, 700];
     }
     
   else if(cardChartWidth<=1024){
      this.view = [300, 900];
     }
     else {
      this.view = [900, 300];
    }


    
     
     

  }
  onSelect(data: any): void {
    if (data.name === 'Notifications') {
      this.openNotificationsModal();
    }
    if (data.name === 'Notices') {
      this.openNoticesModal();
    }
    else if (data.name === 'New Employees ( current Year )' || data.name === 'New Employees ( current Month )') {
      this.openNewEmployeeModal(data.name);
    }
    else if (data.name === 'Active Employees') {
      this.openEmployeesModel();
    }
    else if (data.name === 'New Confirmed ( current Month )') {
      this.openConfirmedModal();
    }
  }
  openConfirmedModal(): void {
    const employeeConfirmedList = new MatDialogConfig();
    // Setting different dialog configurations
    employeeConfirmedList.disableClose = true;
    employeeConfirmedList.autoFocus = true;
    employeeConfirmedList.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    employeeConfirmedList.width =  `${dialogWidth}%`;
    employeeConfirmedList.autoFocus = false;
    employeeConfirmedList.maxHeight = "90vh";
    employeeConfirmedList.data = this.confirmedEmployees;
    const noticeDialog = this.dialog.open(EmployeeConfirmationListComponent, employeeConfirmedList);
    noticeDialog.afterClosed().subscribe(() => {
    });
  }
  openNotificationsModal(): void {
    const noticeList = new MatDialogConfig();
    // Setting different dialog configurations
    noticeList.disableClose = true;
    noticeList.autoFocus = true;
    noticeList.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    noticeList.width =  `${dialogWidth}%`;
    noticeList.autoFocus = false;
    noticeList.maxHeight = "90vh";
    noticeList.data = this.notifications;
    const noticeDialog = this.dialog.open(NotificationDetailsComponent, noticeList);
    noticeDialog.afterClosed().subscribe(() => {
    });
  }
  openNoticesModal(): void {
    const noticeList = new MatDialogConfig();
    // Setting different dialog configurations
    noticeList.disableClose = true;
    noticeList.autoFocus = true;
    noticeList.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    noticeList.width =  `${dialogWidth}%`;
    noticeList.autoFocus = false;
    noticeList.maxHeight = "90vh";
    noticeList.data = this.noticesDetail;
    const noticeDialog = this.dialog.open(NoticeDetailsComponent, noticeList);
    noticeDialog.afterClosed().subscribe(() => {
    });
  }
  openNewEmployeeModal(cardName: any): void {
    let newEmployees: any;
    newEmployees = cardName === 'New Employees ( current Year )' ? this.currentYearNewEmployee : this.currentMonthNewEmployee;
    newEmployees.isCurrentMonth = cardName === 'New Employees ( current Year )' ? false: true;
  
    const currentYearNewEmployeeList = new MatDialogConfig();
    // Setting different dialog configurations
    currentYearNewEmployeeList.disableClose = true;
    currentYearNewEmployeeList.autoFocus = true;
    currentYearNewEmployeeList.panelClass = "xHR-dialog";
    currentYearNewEmployeeList.data = newEmployees;
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    currentYearNewEmployeeList.width = `${dialogWidth}%`;
    currentYearNewEmployeeList.autoFocus = false;
    currentYearNewEmployeeList.maxHeight = "90vh";
    const noticeDialog = this.dialog.open(NewEmployeeListComponent, currentYearNewEmployeeList);
    noticeDialog.afterClosed().subscribe(() => {
    });
  }
  openEmployeesModel(): void {
    const employees = new MatDialogConfig();
    // Setting different dialog configurations
    employees.disableClose = true;
    employees.autoFocus = true;
    employees.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    employees.width = `${dialogWidth}%`;
    employees.autoFocus = false;
    employees.maxHeight = "90vh";
    employees.data = this.employeesDetail;
    const employeeDialog = this.dialog.open(ActiveEmployeeListComponent, employees);
    employeeDialog.afterClosed().subscribe(() => {
    });
  }
  generateFullChart(): void {
    var date = new Date(), y = date.getFullYear(), m = date.getMonth();
    var firstDay = new Date(y, m, 1);
    var lastDay = new Date(y, m + 1, 0);
    let stDate = this.datePipe.transform(new Date(firstDay), 'MM-dd-yyyy');
    let edDate = this.datePipe.transform(new Date(lastDay), 'MM-dd-yyyy');
    /// bill
    this.countChartService.getBillClaimChartAmount(this.companyId, stDate, edDate).subscribe(result => {
      this.billAmount = (result as any).approvedAmount;
      /// notice
      this.countChartService.getActiveNotice(this.companyId).subscribe(result => {
        this.notices = result.length;
        this.noticesDetail = result;
        /// notification
        this.countChartService.getNotificationsChartData(this.managerId).subscribe(result => {
          this.notifications = result.notificationList;
          /// employees
          this.employeeService.getAllEmployees().subscribe(result => {
            this.employees = result.length;
            this.employeesDetail = result;
            /// current year new employees
            this.countChartService.getCurrentYearNewEmployees(this.companyId, y).subscribe(result => {
              this.currentYearNewEmployee = result;
              this.countChartService.getNewConfirmedEmployees(this.companyId, stDate, edDate).subscribe(res => {
                this.confirmedEmployees = res;
                this.currentMonthNewEmployee = result.filter((item: { joiningDate: string | number | Date; }) => new Date(item.joiningDate).getMonth() === m);
                this.single = [
                  {
                    "name": "Notifications",
                    "value": this.notifications.length
                  },
                  {
                    "name": "Bill Amount",
                    "value": this.billAmount
                  },
                  {
                    "name": "Notices",
                    "value": this.notices ?? 0
                  },
                  {
                    "name": "Active Employees",
                    "value": this.employees
                  },
                  {
                    "name": "New Employees ( current Year )",
                    "value": result.length ?? 0
                  },
                  {
                    "name": "New Employees ( current Month )",
                    "value": this.currentMonthNewEmployee.length ?? 0
                  },
                  {
                    "name": "New Permanent Employees ( current Month )",
                    "value": this.confirmedEmployees.length ?? 0
                  }
                ];
              }, () => {
              });
            }, () => {
            })
          }, () => { });
        }, () => { })
      }, () => { })
    }, () => { })
  }
}
