import { Component, OnInit, EventEmitter, ViewEncapsulation, Injector } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DesignationService, DepartmentService, BranchService, CompanyService, ShiftAllocationService, ShiftService, FinancialYearService, MonthCycleService, EmployeeService } from 'src/app/shared/services';
import { ShiftAllocation, Guid } from 'src/app/shared/models';
import { CreateShiftAllocationComponent } from './create-shift-allocation/create-shift-allocation.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { AlertService } from '../../shared/services/alert.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSelectChange } from '@angular/material/select';
import { MatOption } from '@angular/material/core';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';


@Component({
  selector: 'app-shift-allocation',
  templateUrl: './shift-allocation.component.html',
  styleUrls: ['./shift-allocation.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class ShiftAllocationComponent extends BaseAuthorizedComponent implements OnInit {
  onShiftAllocationCreateEvent: EventEmitter<any> = new EventEmitter();
  onShiftAllocationEditEvents: EventEmitter<any> = new EventEmitter();
  @BlockUI('shiftAllocation-list-section') blockUI: NgBlockUI
  shiftAllocations: ShiftAllocation[];
  shiftAllocation: ShiftAllocation;
  allocatedShifts: any;
  employees: any;
  employeeId?: any = Guid.empty;
  designations: any;
  designationId?: any = Guid.empty;
  shiftAllocationFilterFormGroup: FormGroup
  departments: any;
  departmentId?: any = Guid.empty;
  branches: any;
  branchId: any;
  companies: any;
  shifts: any;
  shiftId: any;
  financialYears: any;
  financialYearId: any = Guid.empty;
  financialYearName: any;
  monthCycles: any;
  monthCycleId: any = Guid.empty;;
  monthCycleName: any;
  monthNumber: any;
  maxDayOfMonth: any;
  totalDayListOfMonth: any[] = [];
  totalDayNumOfMonth: any[] = [];
  shortDay: any;
  shiftCode: any;
  weeklyHoliDayId: any;
  weeklyHoliday: any;
  rotationalDayId: any;
  rotationalday: any;
  startDate: any;
  shiftstartPoint: any;
  date: Date;
  employeeIdList = [];
  compIdList = [];
  finIdList = [];
  mcycleIdList = [];
  submitted: boolean;
  isEmployeeExist: boolean;
  selectAllChecked: boolean;
  //headerList: Array<{ id: number, name: string }> = [];

  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private employeeService: EmployeeService,
    private designationService: DesignationService,
    private departmentServce: DepartmentService,
    private branchService: BranchService,
    private companyService: CompanyService,
    private shiftAllocationService: ShiftAllocationService,
    private shiftService: ShiftService,
    private financialYearService: FinancialYearService,
    private monthCycleService: MonthCycleService,
    private alertService: AlertService,
    private injector: Injector
  ) {
    super(injector);
  }

  ngOnInit() {
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
    this.getAllEmployees();
    this.setInitialHeader();
  }


  ngOnChanges() {
    this.createMonthWiseDayHeaderList();
  }

  setInitialHeader() {
    var d = new Date();
    var month = d.getMonth();
    var year = d.getFullYear();
    this.financialYearName = year;
    this.monthCycleName = this.getMonthNameByMonthNumber(month);

    this.createMonthWiseDayHeaderList();

  }
  getMonthNameByMonthNumber(mm) {
    var months = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
    return months[mm];
  }
  rotationalDays = [
    { id: 1, value: '1' },
    { id: 2, value: '2' },
    { id: 3, value: '3' },
    { id: 4, value: '4' },
    { id: 5, value: '5' },
    { id: 6, value: '6' },
    { id: 7, value: '7' },
    { id: 8, value: '8' },
    { id: 9, value: '9' },
    { id: 10, value: '10' },
    { id: 11, value: '11' },
    { id: 12, value: '12' },
    { id: 13, value: '13' },
    { id: 14, value: '14' },
    { id: 15, value: '15' },
    { id: 16, value: '16' },
    { id: 17, value: '17' },
    { id: 18, value: '18' },
    { id: 19, value: '19' },
    { id: 20, value: '20' },
    { id: 21, value: '21' },
    { id: 22, value: '22' },
    { id: 23, value: '23' },
    { id: 24, value: '24' },
    { id: 25, value: '25' },
    { id: 26, value: '26' },
    { id: 27, value: '27' },
    { id: 28, value: '28' },
    { id: 29, value: '29' },
    { id: 30, value: '30' },
    { id: 31, value: '31' },
    { id: 32, value: 'Fixed' },
  ];

  weeklyHoliDays = [
    { id: 'Friday', value: 'Friday' },
    { id: 'Saturday', value: 'Saturday' },
    { id: 'Sunday', value: 'Sunday' },
    { id: 'Monday', value: 'Monday' },
    { id: 'Tuesday', value: 'Tuesday' },
    { id: 'Wednesday', value: 'Wednesday' },
    { id: 'Thursday', value: 'Thursday' },
  ];
  startDates = [
    { id: 1, value: '1' },
    { id: 2, value: '2' },
    { id: 3, value: '3' },
    { id: 4, value: '4' },
    { id: 5, value: '5' },
    { id: 6, value: '6' },
    { id: 7, value: '7' },
    { id: 8, value: '8' },
    { id: 9, value: '9' },
    { id: 10, value: '10' },
    { id: 11, value: '11' },
    { id: 12, value: '12' },
    { id: 13, value: '13' },
    { id: 14, value: '14' },
    { id: 15, value: '15' },
    { id: 16, value: '16' },
    { id: 17, value: '17' },
    { id: 18, value: '18' },
    { id: 19, value: '19' },
    { id: 20, value: '20' },
    { id: 21, value: '21' },
    { id: 22, value: '22' },
    { id: 23, value: '23' },
    { id: 24, value: '24' },
    { id: 25, value: '25' },
    { id: 26, value: '26' },
    { id: 27, value: '27' },
    { id: 28, value: '28' },
    { id: 29, value: '29' },
    { id: 30, value: '30' },
    { id: 31, value: '31' }
  ];

  get f() { return this.shiftAllocationFilterFormGroup.controls; }

  buildForm() {
    this.shiftAllocationFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      branchId: [this.branchId, Validators.required],
      departmentId: [this.departmentId],
      designationId: [this.designationId],
      employeeId: [this.employeeId],
      financialYearId: [this.financialYearId],
      financialYearName: [this.financialYearName],
      monthCycleId: [this.monthCycleId],
      monthCycleName: [this.monthCycleName],
      shiftCode: [this.shiftCode],
      rotationalDayId: [this.rotationalDayId],
      weeklyHoliDayId: [this.weeklyHoliDayId],
      startDate: [this.startDate]
    })
  }

  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllBranchByCompanyId();
      this.getAllShiftByCompanyId();
      this.getAllFinancialYearByCompanyId();
      //this.getShiftAllocationByCompanyAndBranch();
    }
  }

  onChangeBranch(branchId: any) {
    this.branchId = branchId;
    if (branchId) {
      this.getALLDepartmentByBranchId();
      this.getShiftAllocationByCompanyAndBranch();
    }
  }
  onChangeDepartment(departmentId: any) {
    this.departmentId = departmentId;
    if (departmentId) {
      this.getAllDesignationByDepartmentId();
      this.getShiftAllocationByCompanyAndBranch();
    }
  }

  onChangeDesignation(designationId: any) {
    this.designationId = designationId;
    if (designationId) {
      this.getShiftAllocationByCompanyAndBranch();
    }
  }

  onChangeEmployee(event: MatSelectChange) {
    var empName = (event.source.selected as MatOption).viewValue
    var empId = event.source.value;
    this.employeeId = empId;;
    if (empId) {
      this.getShiftAllocationByCompanyAndBranch();
    }
  }
  onChangeFinancialYears(event: MatSelectChange) {
    var financialYearName = (event.source.selected as MatOption).viewValue;
    var financialYearId = event.value;
    this.financialYearId = financialYearId;
    this.financialYearName = financialYearName;
    if (financialYearId) {
      this.getAllMonthCycleByCompanyIdAndFinancialYear();
      this.getShiftAllocationByCompanyAndBranch();
      this.employeeId = Guid.empty;
    }
  }
  onChangeMonthCycle(event: MatSelectChange) {
    var monthCycleName = (event.source.selected as MatOption).viewValue;
    var monthCycleId = event.source.value;
    this.monthCycleId = event.value;
    this.monthCycleName = monthCycleName;
    if (monthCycleId) {
      this.getShiftAllocationByCompanyAndBranch();
      this.createMonthWiseDayHeaderList();
      this.employeeId = Guid.empty;
    }
  }
  onChangeShift(event: MatSelectChange) {
    var shiftName = (event.source.selected as MatOption).viewValue;
    var shiftCD = event.source.value;
    this.shiftCode = shiftCD;

  }

  onChangeRotationalDay(rotationDay: any) {
    this.rotationalday = rotationDay;
  }
  onChangeWeeklyHoliDay(weeklyHoliday: any) {
    this.weeklyHoliday = weeklyHoliday;
    
  }
  onChangeStartDate(startDate: any) {
    this.startDate = startDate;
  }

  // checkEmployeeExist() {

  //   for (let i = 0; i < this.allocatedShifts.length; i++) {
  //     this.employeeIdList = this.allocatedShifts.filter(x => x.employeeId== this.employeeId);
  //     this.compIdList = this.allocatedShifts.filter(x => x.companyId);
  //     this.finIdList = this.allocatedShifts.filter(x => x.financialYearId);
  //     this.mcycleIdList = this.allocatedShifts.filter(x => x.monthCycleId);

  //     if (this.allocatedShifts.filter(x => x.employeeId == this.employeeId)) {
  //       this.isEmployeeExist = true;
  //     }
  //     else {
  //       this.isEmployeeExist = false;
  //     }
  //   }
  //   return this.isEmployeeExist;
  // }

  saveShiftAllocation() {

    this.submitted = true;
    if (this.shiftAllocationFilterFormGroup.invalid) {
      return;
    }
    if (this.rotationalday == undefined) {
      this.alertService.warn("You must select rotational day");
      return;
    }
    if (this.weeklyHoliday == undefined) {
      this.alertService.warn("You must select Weekly holiday");
      return
    }
    if (this.shiftCode == undefined) {
      this.alertService.warn("You must select shift name");
      return;
    }
    if (this.startDate == undefined) {
      this.alertService.warn("You must select start date");
      return;
    }

    if (!this.employeeIdList || this.employeeIdList.length == 0) {
      this.alertService.warn("Please select at least one employee");
      return;
    }

    // this.checkEmployeeExist();
    // if (this.isEmployeeExist == true) {
    //   this.editShiftAllocation();
    // }
    if (this.employeeIdList.length > 0) {
      this.setShiftFortheMultipleEmployee();
    }
    else if (this.employeeId != undefined && this.employeeId != Guid.empty) {
      this.createShiftAllocationForIndividualEmployee();
    }

  }

  editShiftAllocation() {

    let changeData = new ShiftAllocation(this.shiftAllocationFilterFormGroup.value);
    if (this.shiftAllocation !== null) {
      this.createShiftCodeList();
      this.shiftAllocation.companyId = changeData.companyId;
      this.shiftAllocation.branchId = changeData.branchId;
      this.shiftAllocation.employeeId = changeData.employeeId;
      this.shiftAllocation.financialYearId = changeData.financialYearId;
      this.shiftAllocation.monthCycleId = changeData.monthCycleId;
      this.shiftAllocation.dutyYear = this.financialYearName;
      this.shiftAllocation.dutyMonth = this.monthNumber;
      //this.shiftAllocation.weeklyHoliDayId = this.weeklyHoliDayId;
      this.shiftAllocation.primaryShiftId = '3fa85f64-5717-4562-b3fc-2c963f66afa6';
      this.shiftAllocation.rotationDay = changeData.rotationDay;

      this.setShiftAllocationId(this.shiftAllocation);
      this.shiftAllocationService.editShiftAllocation(this.shiftAllocation).subscribe((data: any) => {
        this.onShiftAllocationEditEvents.emit(this.shiftAllocation.id);
        this.alertService.success("Shift Allocation updated successfully");
      }, (error: any) => {
        this.alertService.error(error);
      });

    }
  }
  buildShiftAllocationObj(shiftCD: any[]) {
    this.shiftAllocation = new ShiftAllocation();
    this.shiftAllocation.c1 = shiftCD[0];
    this.shiftAllocation.c2 = shiftCD[1];
    this.shiftAllocation.c3 = shiftCD[2];
    this.shiftAllocation.c4 = shiftCD[3];
    this.shiftAllocation.c5 = shiftCD[4];
    this.shiftAllocation.c6 = shiftCD[5];
    this.shiftAllocation.c7 = shiftCD[6];
    this.shiftAllocation.c8 = shiftCD[7];
    this.shiftAllocation.c9 = shiftCD[8]
    this.shiftAllocation.c10 = shiftCD[9];
    this.shiftAllocation.c11 = shiftCD[10];
    this.shiftAllocation.c12 = shiftCD[11];
    this.shiftAllocation.c13 = shiftCD[12];
    this.shiftAllocation.c14 = shiftCD[13];
    this.shiftAllocation.c15 = shiftCD[14];
    this.shiftAllocation.c16 = shiftCD[15];
    this.shiftAllocation.c17 = shiftCD[16];
    this.shiftAllocation.c18 = shiftCD[17];
    this.shiftAllocation.c19 = shiftCD[18]
    this.shiftAllocation.c20 = shiftCD[19];
    this.shiftAllocation.c21 = shiftCD[20];
    this.shiftAllocation.c22 = shiftCD[21];
    this.shiftAllocation.c23 = shiftCD[22];
    this.shiftAllocation.c24 = shiftCD[23];
    this.shiftAllocation.c25 = shiftCD[24];
    this.shiftAllocation.c26 = shiftCD[25];
    this.shiftAllocation.c27 = shiftCD[26];
    this.shiftAllocation.c28 = shiftCD[27];
    this.shiftAllocation.c29 = shiftCD[28]
    this.shiftAllocation.c30 = shiftCD[29];
    this.shiftAllocation.c31 = shiftCD[30];

  }

  createShiftCodeList() {
    this.setMonthNumber(this.monthCycleName);
    var shiftStartDate = this.financialYearName + "/" + this.monthNumber + "/" + this.startDate;
    var today = new Date(shiftStartDate);
    let shiftCD = [];
    this.setMaxDayOfMonth(this.financialYearName, this.monthCycleName);
    let maxDay = this.maxDayOfMonth;
    if (this.startDate == 1) {
      this.shiftstartPoint = 0
    }
    if (this.startDate > 1) {
      this.shiftstartPoint = this.startDate - 1;
    }
    for (let i = this.shiftstartPoint; i < maxDay; i++) {
      var newday = today.toLocaleDateString('en-US', { weekday: 'long' })
      if (newday == this.weeklyHoliday || (this.weeklyHoliday && this.weeklyHoliday.find(x => x == newday))) {
        shiftCD[i] = "W";
      } else {
        shiftCD[i] = this.shiftCode;
      }
      today.setDate(today.getDate() + 1);
    }
    this.buildShiftAllocationObj(shiftCD);
  }

  createMonthWiseDayHeaderList() {
    this.totalDayListOfMonth = [];
    this.setMonthNumber(this.monthCycleName);
    this.setMaxDayOfMonth(this.financialYearName, this.monthCycleName);
    let maxDay = this.maxDayOfMonth;
    var dayStartDate = this.financialYearName + "/" + this.monthNumber + "/" + 1;
    var today = new Date(dayStartDate);
    for (let i = 0; i < maxDay; i++) {
      var newday = today.toLocaleDateString('en-US', { weekday: 'long' })
      this.shrinktheDayofMonth(newday);
      today.setDate(today.getDate() + 1);
      var obj = { dayNo: i + 1, day: this.shortDay };
      this.totalDayListOfMonth.push(obj);
    }

  }
  shrinktheDayofMonth(day: any) {
    if (day == "Saturday") {
      this.shortDay = "Sa"
    }
    if (day == "Sunday") {
      this.shortDay = "Su"
    }
    if (day == "Monday") {
      this.shortDay = "Mo"
    }
    if (day == "Tuesday") {
      this.shortDay = "Tu"
    }
    if (day == "Wednesday") {
      this.shortDay = "We"
    }
    if (day == "Thursday") {
      this.shortDay = "Th"
    }
    if (day == "Friday") {
      this.shortDay = "Fr"
    }

  }
  setMaxDayOfMonth(finYearName: any, monthName: any) {
    if (monthName == "January" || monthName == "March" || monthName == "May" || monthName == "July" || monthName == "August" || monthName == "October" || monthName == "December") {
      this.maxDayOfMonth = 31;
    }
    if (monthName == "April" || monthName == "June" || monthName == "September" || monthName == "November") {
      this.maxDayOfMonth = 30;
    }
    if (monthName == "February") {
      if ((finYearName % 4) == 0 && (finYearName % 100) != 0 || (finYearName % 400) == 0) {
        this.maxDayOfMonth = 29;
      } else {
        this.maxDayOfMonth = 28;
      }

    }
  }
  setMonthNumber(monthName: any) {
    if (monthName == "January") {
      this.monthNumber = 1;
    }
    if (monthName == "February") {
      this.monthNumber = 2;
    }
    if (monthName == "March") {
      this.monthNumber = 3;
    }
    if (monthName == "April") {
      this.monthNumber = 4;
    }
    if (monthName == "May") {
      this.monthNumber = 5;
    }
    if (monthName == "June") {
      this.monthNumber = 6;
    }
    if (monthName == "July") {
      this.monthNumber = 7;
    }
    if (monthName == "August") {
      this.monthNumber = 8;
    }
    if (monthName == "September") {
      this.monthNumber = 9;
    }
    if (monthName == "October") {
      this.monthNumber = 10;
    }
    if (monthName == "November") {
      this.monthNumber = 11;
    }
    if (monthName == "December") {
      this.monthNumber = 12;
    }

  }
  selection(event, index, item) {
    var isAlreadySelected = this.employeeIdList.find(x => x == item.employeeId);

    if (event.checked) {
      if (!isAlreadySelected) {
        this.employeeIdList.push(item.employeeId);
      }
    }
    else {
      if (isAlreadySelected) {
        this.employeeIdList = this.employeeIdList.filter(x => x != item.employeeId);
      }
    }
  }

  onChangeSelectAll(event) {
    this.employeeIdList = []; // removing all items even any one is selected

    if (event.checked) {
      this.allocatedShifts.forEach(obj => {
        obj.selected = true;

        var currentEmployeeId = obj.employeeId;

        var isAlreadySelected = this.employeeIdList.find(x => x == currentEmployeeId);

        if (!isAlreadySelected) {
          this.employeeIdList.push(currentEmployeeId);
        }

      });
    }
    else {
      // unchecking all selection

      this.employeeIdList = [];
      this.allocatedShifts.forEach(obj => {
        obj.selected = false;
      });
    }
  }
  setShiftFortheMultipleEmployee() {

    try {
      this.shiftAllocation = new ShiftAllocation(this.shiftAllocationFilterFormGroup.value);
      for (let i = 0; i < this.employeeIdList.length; i++) {
        this.createShiftCodeList();
        this.setMonthNumber(this.monthCycleName);
        this.shiftAllocation.companyId = this.companyId;
        this.shiftAllocation.branchId = this.branchId;
        this.shiftAllocation.financialYearId = this.financialYearId
        this.shiftAllocation.dutyYear = this.financialYearName;
        this.shiftAllocation.monthCycleId = this.monthCycleId;
        this.shiftAllocation.dutyMonth = this.monthNumber;
        this.shiftAllocation.employeeId = this.employeeIdList[i];
        this.shiftAllocation.shiftCode = this.shiftCode;
        //this.shiftAllocation.weeklyHoliDayId = this.weeklyHoliDayId;
        this.shiftAllocation.primaryShiftId = '3fa85f64-5717-4562-b3fc-2c963f66afa6';
        this.shiftAllocation.rotationDay = this.rotationalday;


        // this.shiftAllocationService.createShiftAllocation(this.shiftAllocation).subscribe((data: any) => {
        //   this.onShiftAllocationCreateEvent.emit(this.shiftAllocation.id);
        // });


        this.setShiftAllocationId(this.shiftAllocation);


        if (this.shiftAllocation.id) {
          this.shiftAllocationService.editShiftAllocation(this.shiftAllocation).subscribe((data: any) => {
            this.onShiftAllocationCreateEvent.emit(this.shiftAllocation.id);
            this.getShiftAllocationByCompanyAndBranch();
          }, (error: any) => {
            this.alertService.error(error);
          });
        }
        else {
          this.shiftAllocationService.createShiftAllocation(this.shiftAllocation).subscribe((data: any) => {
            this.onShiftAllocationCreateEvent.emit(this.shiftAllocation.id);
            this.getShiftAllocationByCompanyAndBranch();

          }, (error: any) => {
            this.alertService.error(error);
          });
        }
      }
    }
    catch (e) {

    }
    finally {
      this.selectAllChecked = false;
      this.employeeIdList = [];
      this.alertService.success("Shift assigned successfully");

    }


  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
    });
  }
  getAllShiftByCompanyId() {
    this.shiftService.getAllShiftByCompany(this.companyId).subscribe(result => {
      this.shifts = result;
    })
  }
  getAllFinancialYearByCompanyId() {
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;

      // var today = new Date();
      // var currentYear = today.getFullYear();

      // if (this.financialYears.find(x => x.financialYearName == currentYear)) {
      //   var currentFinancialYear = this.financialYears.find(x => x.financialYearName == currentYear);

      //   this.financialYearId = currentFinancialYear.id;

      //   this.getAllMonthCycleByCompanyIdAndFinancialYear();
      // }
    })
  }
  getAllMonthCycleByCompanyIdAndFinancialYear() {
    this.monthCycleService.getMonthCycleByCompanyAndFinancialYear(this.companyId, this.financialYearId).subscribe(result => {

      this.monthCycles = result;

      // var today = new Date();
      // var currentMonthName = today.toLocaleString('default', { month: 'long' });

      // if (this.monthCycles.find(x => x.monthCycleName == currentMonthName)) {
      //   var currentMonthCycle = this.monthCycles.find(x => x.monthCycleName == currentMonthName);

      //   this.monthCycleId = currentMonthCycle.id;
      // }

    })
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }

  getAllBranchByCompanyId() {
    this.branchService.getAllBranchByCompanyId(this.companyId).subscribe((result: any) => {
      this.branches = result;
    });
  }
  getALLDepartmentByBranchId() {
    this.departmentServce.getAllDepartmentByBranchId(this.branchId).subscribe((result: any) => {
      this.departments = result;
    });
  }
  getAllDesignationByDepartmentId() {
    this.designationService.getAllDesignationByDepartmentId(this.departmentId).subscribe(result => {
      this.designations = result;
    })
  }

  createShiftAllocationForIndividualEmployee() {
    this.shiftAllocation = new ShiftAllocation(this.shiftAllocationFilterFormGroup.value);
    this.createShiftCodeList();
    this.setMonthNumber(this.monthCycleName);
    this.shiftAllocation.companyId = this.companyId;
    this.shiftAllocation.branchId = this.branchId;
    this.shiftAllocation.financialYearId = this.financialYearId;
    this.shiftAllocation.dutyYear = this.financialYearName;
    this.shiftAllocation.monthCycleId = this.monthCycleId;
    this.shiftAllocation.dutyMonth = this.monthNumber;
    this.shiftAllocation.employeeId = this.employeeId;
    this.shiftAllocation.shiftCode = this.shiftCode;
    //this.shiftAllocation.weeklyHoliDayId = this.weeklyHoliDayId;
    this.shiftAllocation.primaryShiftId = '3fa85f64-5717-4562-b3fc-2c963f66afa6';
    this.shiftAllocation.rotationDay = this.rotationalday;



    this.setShiftAllocationId(this.shiftAllocation);


    if (this.shiftAllocation.id) {
      this.shiftAllocationService.editShiftAllocation(this.shiftAllocation).subscribe((data: any) => {
        this.onShiftAllocationEditEvents.emit(this.shiftAllocation.id);
        this.alertService.success("Shift assigned successfully");
        this.getShiftAllocationByCompanyAndBranch();
      }, (error: any) => {
        this.alertService.error(error);
      });
    }
    else {
      this.shiftAllocationService.createShiftAllocation(this.shiftAllocation).subscribe((data: any) => {
        this.onShiftAllocationCreateEvent.emit(this.shiftAllocation.id);
        this.alertService.success("Shift assigned successfully");
        this.getShiftAllocationByCompanyAndBranch();
      }, (error: any) => {
        this.alertService.error(error);
      });
    }

  }

  setShiftAllocationId(shift: ShiftAllocation) {
    var currentShiftAllocation = this.allocatedShifts.find(x => x.employeeId == shift.employeeId && x.companyId == shift.companyId
      && x.financialYearId == shift.financialYearId && x.monthCycleId == shift.monthCycleId);

    if (currentShiftAllocation) {
      shift.id = currentShiftAllocation.id;
    }
  }

  editShiftAllocations(shiftAllocation: ShiftAllocation) {

    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = shiftAllocation;
    editDialogConfig.panelClass = 'xHR-dialog';
    editDialogConfig.width = "80%";
    const outletEditDialog = this.dialog.open(CreateShiftAllocationComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onShiftAllocationEditEvent.subscribe((data) => {
      this.getShiftAllocationByCompanyAndBranch();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteShiftAllocation(shiftAllocation: ShiftAllocation): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the Shift Allocation " + shiftAllocation.shiftCode;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteShiftAssignment(shiftAllocation);
      }
    });
  }

  deleteShiftAssignment(shiftAllocation: ShiftAllocation) {
    this.shiftAllocationService.deleteShiftAllocation(shiftAllocation).subscribe((data) => {
      this.getShiftAllocationByCompanyAndBranch();
    }, (error) => {
      console.log(error);
    });

  }

  getShiftAllocationByCompanyAndBranch() {
    //this.setMonthNumber(this.monthCycleName);
    this.blockUI.start();
    this.shiftAllocationService.getShiftAllocationByCompanyAndBranch(this.companyId, this.branchId, this.financialYearId, this.monthCycleId, this.departmentId, this.designationId, this.employeeId).subscribe(result => {
      this.allocatedShifts = result;
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    })

  }


}
