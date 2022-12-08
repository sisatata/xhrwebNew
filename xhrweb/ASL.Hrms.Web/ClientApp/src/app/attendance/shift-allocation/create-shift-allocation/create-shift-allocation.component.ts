import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ShiftAllocation } from 'src/app/shared/models';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CompanyService, BranchService, DepartmentService, DesignationService, ShiftAllocationService, EmployeeService, ShiftService, MonthCycleService, FinancialYearService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { MatSelectChange } from '@angular/material/select';
import { MatOption } from '@angular/material/core';

@Component({
  selector: 'app-create-shift-allocation',
  templateUrl: './create-shift-allocation.component.html',
  styleUrls: ['./create-shift-allocation.component.css']
})
export class CreateShiftAllocationComponent implements OnInit {
  //onShiftAssignmentCreateEvent:EventEmitter<any>=new EventEmitter();
  onShiftAllocationEditEvent: EventEmitter<any> = new EventEmitter();
  shiftAllocationCreateForm: FormGroup;
  submitted = false;
  isEditMode = false;
  shiftAllocation: ShiftAllocation;
  shiftAllocationId: any;
  companies: any;
  companyId: any;
  employees: any;
  employeeId: any;
  branches: any;
  branchId: any;
  departments: any;
  departmentId: any;
  designations: any;
  designationId: any;
  financialYearId: any;
  financialYears: any;
  financialYearName: any;
  monthCycleName: any;
  monthNumber: any;
  maxDayOfMonth: any;
  monthCycleId: any;
  monthCycles: any;
  shifts: any;
  shiftCode: any;
  shiftCD: any;
  shiftName: any;
  weeklyHoliDayId: any;
  weeklyHoliday: any;
  rotationalDayId: any;
  rotationalday: any;
  startDate: any;
  shiftstartPoint: any;
  date: Date;
  employeeIdList = [];

  constructor(
    private dialogRef: MatDialogRef<CreateShiftAllocationComponent>,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private alertService: AlertService,
    private branchService: BranchService,
    private departmentService: DepartmentService,
    private designationService: DesignationService,
    private shiftAllocationService: ShiftAllocationService,
    private employeeService: EmployeeService,
    private shiftService: ShiftService,
    private monthCycleService: MonthCycleService,
    private financialYearService: FinancialYearService,

    @Inject(MAT_DIALOG_DATA) data
  ) {

    this.shiftAllocation = new ShiftAllocation();

    var aa = new ShiftAllocation(data);
    if (isNaN(data)) {
      //debugger;
      this.shiftAllocation = new ShiftAllocation(data);
      this.companyId = this.shiftAllocation.companyId;
      this.branchId = this.shiftAllocation.branchId;
      this.departmentId = this.shiftAllocation.departmentId;
      this.designationId = this.shiftAllocation.designationId;
      this.financialYearId = this.shiftAllocation.financialYearId;
      this.monthCycleId = this.shiftAllocation.monthCycleId;
      this.monthNumber = this.shiftAllocation.dutyMonth;
      this.financialYearName = this.shiftAllocation.dutyYear;
      this.shiftCode = this.shiftAllocation.shiftCode;
      this.setMaxDayOfMonthByMonthNumber(this.shiftAllocation.dutyYear, this.shiftAllocation.dutyMonth);
      //console.log(this.maxDayOfMonth);
    } else {

    }
    this.buildForm();
    this.getAllBranchByCompanyId(this.companyId);
    this.getALLDepartmentByBranchId(this.branchId);
    this.getAllDesignationByDepartmentId();
    this.getAllFinancialYearByCompanyId();
    this.getAllMonthCycleByCompanyIdAndFinancialYear();
    this.getAllShiftByCompanyId();
    this.getAllEmployees();
    //console.log(this.monthNumber);
  }

  ngOnInit() {
    this.getAllCompanies();
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
    { id: 'Thirsday', value: 'Thirsday' },
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


  onChangeCompany() {

    this.companyId = this.f.companyId.value;
    if (this.companyId) {
      this.getAllBranchByCompanyId(this.companyId);
      this.getAllFinancialYearByCompanyId();
      this.getAllShiftByCompanyId();
    }
  }

  onChangeBranch() {
    this.branchId = this.f.branchId.value;
    if (this.branchId) {
      this.getALLDepartmentByBranchId(this.branchId);
      this.getAllEmployees();
      this.getAllShiftByCompanyId();
      this.getAllFinancialYearByCompanyId();
    }
  }

  onChangeDepartment() {
    this.departmentId = this.f.departmentId.value;
    if (this.departmentId) {
      this.getAllDesignationByDepartmentId();
    }

  }
  onChangeDesignation() {
    this.designationId = this.f.designationId.value;
  }
  onChangeEmployee(event: MatSelectChange) {
    var empName = (event.source.selected as MatOption).viewValue
    var empId = event.source.value;
    this.employeeId = empId;
  }
  onChangeFinancialYears(event: MatSelectChange) {
    var financialYearName = (event.source.selected as MatOption).viewValue;
    var financialYearId = event.source.value
    this.financialYearId = financialYearId;
    this.financialYearName = financialYearName;
    if (financialYearId) {
      this.getAllMonthCycleByCompanyIdAndFinancialYear();
    }
  }
  onChangeMonthCycle(event: MatSelectChange) {
    var monthCycleName = (event.source.selected as MatOption).viewValue;
    var monthCycleId = event.source.value;
    this.monthCycleId = monthCycleId;
    this.monthCycleName = monthCycleName;
  }
  onChangeShift(event: MatSelectChange) {
    var shiftName = (event.source.selected as MatOption).viewValue;
    var shiftCD = event.source.value;
    this.shiftCode = shiftCD;
    this.shiftName = shiftName;

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
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  getAllFinancialYearByCompanyId() {
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;
    })
  }
  getAllBranchByCompanyId(companyId) {
    this.branchService.getAllBranchByCompanyId(companyId).subscribe((result: any) => {
      this.branches = result;
    });
  }
  getALLDepartmentByBranchId(branchId) {
    this.departmentService.getAllDepartmentByBranchId(branchId).subscribe((result: any) => {
      this.departments = result;
    });
  }
  getAllDesignationByDepartmentId() {
    this.designationService.getAllDesignationByDepartmentId(this.departmentId).subscribe(result => {
      this.designations = result;
    })
  }
  getAllMonthCycleByCompanyIdAndFinancialYear() {
    this.monthCycleService.getMonthCycleByCompanyAndFinancialYear(this.companyId, this.financialYearId).subscribe(result => {
      this.monthCycles = result;
    })
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
    });
  }
  getAllShiftByCompanyId() {
    this.shiftService.getAllShiftByCompany(this.companyId).subscribe(result => {
      this.shifts = result;
      var weekend = { shiftCode: 'W' };
      var holiday = { shiftCode: 'H' };
      this.shifts.push(weekend);
      this.shifts.push(holiday);
    })
  }
  buildForm() {
    this.shiftAllocationCreateForm = this.formBuilder.group({
      companyId: [this.shiftAllocation.companyId],
      branchId: [this.shiftAllocation.branchId],
      departmentId: [this.shiftAllocation.departmentId],
      designationId: [this.shiftAllocation.designationId],
      financialYearId: [this.shiftAllocation.financialYearId],
      financialYearName: [this.financialYearName],
      monthCycleId: [this.shiftAllocation.monthCycleId],
      monthCycleName: [this.monthCycleName],
      employeeId: [this.shiftAllocation.employeeId],
      shiftName: [this.shiftAllocation.shiftName],
      rotationalDayId: [this.rotationalDayId],
      weeklyHoliDayId: [this.weeklyHoliDayId],
      startDate: [this.startDate],
      c1: [this.shiftAllocation.c1],
      c2: [this.shiftAllocation.c2],
      c3: [this.shiftAllocation.c3],
      c4: [this.shiftAllocation.c4],
      c5: [this.shiftAllocation.c5],
      c6: [this.shiftAllocation.c6],
      c7: [this.shiftAllocation.c7],
      c8: [this.shiftAllocation.c8],
      c9: [this.shiftAllocation.c9],
      c10: [this.shiftAllocation.c10],
      c11: [this.shiftAllocation.c11],
      c12: [this.shiftAllocation.c12],
      c13: [this.shiftAllocation.c13],
      c14: [this.shiftAllocation.c14],
      c15: [this.shiftAllocation.c15],
      c16: [this.shiftAllocation.c16],
      c17: [this.shiftAllocation.c17],
      c18: [this.shiftAllocation.c18],
      c19: [this.shiftAllocation.c19],
      c20: [this.shiftAllocation.c20],
      c21: [this.shiftAllocation.c21],
      c22: [this.shiftAllocation.c22],
      c23: [this.shiftAllocation.c23],
      c24: [this.shiftAllocation.c24],
      c25: [this.shiftAllocation.c25],
      c26: [this.shiftAllocation.c26],
      c27: [this.shiftAllocation.c27],
      c28: [this.shiftAllocation.c28],
      c29: [this.shiftAllocation.c29],
      c30: [this.shiftAllocation.c30],
      c31: [this.shiftAllocation.c31],

    })
  }

  onSubmit() {

    this.submitted = true;
    if (this.shiftAllocationCreateForm.invalid) {
      return;
    }
    if (this.shiftAllocation.id != undefined) {
      this.shiftAllocationId = this.shiftAllocation.id;
      this.editShiftAllocation();
      this.dialogRef.close();
    }
    else{
      this.createShiftAllocation();
      this.dialogRef.close();
    }

  }


  createShiftAllocation() {
    this.shiftAllocation = new ShiftAllocation(this.shiftAllocationCreateForm.value);
    if (this.shiftAllocation !== null) {
      this.shiftAllocation.primaryShiftId = '3fa85f64-5717-4562-b3fc-2c963f66afa6';
      this.shiftAllocationService.createShiftAllocation(this.shiftAllocation).subscribe((data: any) => {
        this.onShiftAllocationEditEvent.emit(this.shiftAllocation.id);
        this.alertService.success("Shift Allocation created successfully");
      }, (error: any) => {
        this.alertService.error(error);
      });
    }
  }

  editShiftAllocation() {

    let changeData = new ShiftAllocation(this.shiftAllocationCreateForm.value);
    if (this.shiftAllocation !== null) {
      
      this.shiftAllocation.id = this.shiftAllocationId;
      this.shiftAllocation.c1 = changeData.c1;
      this.shiftAllocation.c2 = changeData.c2;
      this.shiftAllocation.c3 = changeData.c3;
      this.shiftAllocation.c4 = changeData.c4;
      this.shiftAllocation.c5 = changeData.c5;
      this.shiftAllocation.c6 = changeData.c6;
      this.shiftAllocation.c7 = changeData.c7;
      this.shiftAllocation.c8 = changeData.c8;
      this.shiftAllocation.c9 = changeData.c9;
      this.shiftAllocation.c10 = changeData.c10;
      this.shiftAllocation.c11 = changeData.c11;
      this.shiftAllocation.c12 = changeData.c12;
      this.shiftAllocation.c13 = changeData.c13;
      this.shiftAllocation.c14 = changeData.c14;
      this.shiftAllocation.c15 = changeData.c15;
      this.shiftAllocation.c16 = changeData.c16;
      this.shiftAllocation.c17 = changeData.c17;
      this.shiftAllocation.c18 = changeData.c18;
      this.shiftAllocation.c19 = changeData.c19;
      this.shiftAllocation.c20 = changeData.c20;
      this.shiftAllocation.c21 = changeData.c21;
      this.shiftAllocation.c22 = changeData.c22;
      this.shiftAllocation.c23 = changeData.c23;
      this.shiftAllocation.c24 = changeData.c24;
      this.shiftAllocation.c25 = changeData.c25;
      this.shiftAllocation.c26 = changeData.c26;
      this.shiftAllocation.c27 = changeData.c27;
      this.shiftAllocation.c28 = changeData.c28;
      this.shiftAllocation.c29 = changeData.c29;
      this.shiftAllocation.c30 = changeData.c30;
      this.shiftAllocation.c31 = changeData.c31;
      this.shiftAllocation.companyId = changeData.companyId;
      this.shiftAllocation.employeeId = changeData.employeeId;
      this.shiftAllocation.branchId = changeData.branchId;
      this.shiftAllocation.financialYearId = changeData.financialYearId;
      this.shiftAllocation.monthCycleId = changeData.monthCycleId;
      this.shiftAllocation.dutyYear = this.financialYearName;
      this.shiftAllocation.dutyMonth = this.monthNumber;
      this.shiftAllocation.primaryShiftId = '3fa85f64-5717-4562-b3fc-2c963f66afa6';
      this.shiftAllocation.rotationDay = 1;   // This is default 1 here
      this.shiftAllocationService.editShiftAllocation(this.shiftAllocation).subscribe((data: any) => {
        this.onShiftAllocationEditEvent.emit(this.shiftAllocation.id);
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

  setMaxDayOfMonthByMonthNumber(finYearName: any, monthNumber: any) {
    if (monthNumber == 1 || monthNumber == 3 || monthNumber == 5 || monthNumber == 7 || monthNumber == 8 || monthNumber == 10 || monthNumber == 12) {
      this.maxDayOfMonth = 31;
    }
    if (monthNumber == 4 || monthNumber == 6 || monthNumber == 9 || monthNumber == 11) {
      this.maxDayOfMonth = 30;
    }
    if (monthNumber == 2) {
      if ((finYearName % 4) == 0 && (finYearName % 100) != 0 || (finYearName % 400) == 0) {
        this.maxDayOfMonth = 29;
      } else {
        this.maxDayOfMonth = 28;
      }

    }
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
  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);

  }

  get f() { return this.shiftAllocationCreateForm.controls; }

}
