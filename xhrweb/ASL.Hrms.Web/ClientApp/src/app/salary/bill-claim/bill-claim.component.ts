import { DatePipe } from '@angular/common';
import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { SearchFilterComponent } from 'src/app/shared/components/search-filter/search-filter.component';
import { BillClaim } from 'src/app/shared/models';
import { BenefitBillClaimService, CompanyService, EmployeeService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { ApproveBillClaimComponent } from './approve-bill-claim/approve-bill-claim.component';
import { CreateBillClaimComponent } from './create-bill-claim/create-bill-claim.component';
@Component({
  selector: 'app-bill-claim',
  templateUrl: './bill-claim.component.html',
  styleUrls: ['./bill-claim.component.css']
})
export class BillClaimComponent extends BaseAuthorizedComponent implements OnInit {
  @ViewChild(SearchFilterComponent) searchFilter: SearchFilterComponent;
  billClaims: BillClaim[] = [];
  billClaim: BillClaim;
  attendanceId: any;
  branchId: any;
  companies: any;
  branches: any;
  billClaimsFilterFormGroup: FormGroup;
  submitted: boolean;
  endDate: any;
  startDate: any;
  employeeId: any;
  isAdmin: any;
  startDateInitialValue: any;
  endDateInitialValue: any;
  companyId: any = localStorage.getItem('companyId');
  employees: any;
  loading: boolean = false;
  searched: boolean = true;
  inputValue: string = '';
  allEmployees: any;
  isPayrollManager: boolean = false;
  showSearchAsEmployeeButton: boolean = true;
  isEmployee: boolean = false;
  managerId: any;
  showDetails: boolean = false;
  isReportingManager: boolean= false;
  constructor(
    private alertService: AlertService,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private dialog: MatDialog, private injector: Injector, private datePipe: DatePipe,
    private benefitBillClaimService: BenefitBillClaimService,
    private employeeService: EmployeeService,
  ) {
    super(injector);
    this.isAdmin = this.authService.isAdmin;
    this.isPayrollManager = this.authService.isPayrollManger;
    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
    this.isReportingManager = this.authService.isReportingManager;
    this.isEmployee = this.authService.isEmployee;
    this.managerId = this.authService.getLoggedInUserInfo().employeeId;
    this.buildForm()
  }
  ngOnInit(): void {
    this.setDates();
    this.getAllEmployees();
    
  }
  ngAfterViewInit():void{
    if (this.isReportingManager)
    this.search();
  else
    this.getBenefitBillClaimsByEmployee();
  }
  getAllEmployees(): void {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
      this.employees.unshift({ id: 1, fullName: 'Select Employee' });
    });
  }
  buildForm() {
    this.billClaimsFilterFormGroup = this.formBuilder.group({
      startDate: [this.startDate, Validators.required],
      endDate: [this.endDate, Validators.required],
      employeeId: [this.employeeId],
      companyId: [this.companyId]
    });
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.billClaimsFilterFormGroup.invalid) {
      return;
    }
    else if (this.billClaimsFilterFormGroup.value.employeeId === null || this.billClaimsFilterFormGroup.value.employeeId === '1') {
     /// this.getBenefitBillClaimsByManager();
     this.search(true)
    }
  }
  getBenefitBillClaimsByManager(): void {
    if (this.employeeId !== this.authService.getLoggedInUserInfo().employeeId) {
      this.getBenefitBillClaimsByEmployee();
      return;
    }
    this.startDate = this.f.startDate.value;
    this.endDate = this.f.endDate.value;
    this.startDate = this.datePipe.transform(new Date(this.startDate), 'yyyy-MM-dd');
    this.endDate = this.datePipe.transform(new Date(this.endDate), 'yyyy-MM-dd');
    this.loading = true;
    this.benefitBillClaimService.getAllBenefitBillClaimByEmployeesByManagerId(this.employeeId, this.startDate, this.endDate).subscribe(res => {
      this.loading = false;
      this.billClaims = res;
      this.totalItems = this.billClaims.length;
      this.generateTotalItemsText();
    }, () => {
    })
  }
  getBenefitBillClaimsByEmployee(): void {
    this.startDate = this.f.startDate.value;
    this.endDate = this.f.endDate.value;
    this.startDate = this.datePipe.transform(new Date(this.startDate), 'yyyy-MM-dd');
    this.endDate = this.datePipe.transform(new Date(this.endDate), 'yyyy-MM-dd');
    this.loading = true;
    this.benefitBillClaimService.getAllBenefitBillClaimByEmployeesByEmployeeId(this.employeeId, this.startDate, this.endDate).subscribe(res => {
      this.loading = false;
      this.billClaims = (res as any).benefitBillClaims;
      this.totalItems = this.billClaims.length;
      this.showDetails = false;
      this.generateTotalItemsText();
    }, () => {
    })
  }
  setDates(): void {
    var oneMonthAgoDate = new Date();
    var oneDayAgoDate = new Date();
    var today = new Date();
    oneDayAgoDate.setDate(oneDayAgoDate.getDate() - 1);
    oneMonthAgoDate.setMonth(oneMonthAgoDate.getMonth() - 1);
    this.f.startDate.setValue(oneMonthAgoDate);
    this.f.endDate.setValue(today);
    if (this.isReportingManager) {
      this.f.startDate.setValue(oneDayAgoDate);
    }
  }
  get f() { return this.billClaimsFilterFormGroup.controls; }
  onChangeEmployee() {
    this.employeeId = this.f.employeeId.value;
    this.showSearchAsEmployeeButton = this.employeeId === this.authService.getLoggedInUserInfo().employeeId;
    this.emptyTable();
  }
  onChangeStartDate(): void {
    this.emptyTable();
  }
  onChangeEndDate(): void {
    this.emptyTable();
  }
  emptyTable(): void {
    this.billClaims = [];
    this.totalItems = 0;
    this.searched = false;
    this.loading = false;
  }
  editOrApproveBillClaims(billClaim: any): void {
    if ((billClaim.employeeId === this.authService.getLoggedInUserInfo().employeeId)) {
      this.editBillClaims(billClaim);
    }
    else {
      const approveDialogConfig = new MatDialogConfig();
      approveDialogConfig.disableClose = true;
      approveDialogConfig.autoFocus = true;
      approveDialogConfig.data = billClaim;
      approveDialogConfig.panelClass = 'xHR-dialog';
      const dialogWidth = window.screen.width <= 576 ? 90: 50;
      approveDialogConfig.width = `${dialogWidth}%`;
      const outletEditDialog = this.dialog.open(ApproveBillClaimComponent, approveDialogConfig);
      const successFullEdit = outletEditDialog.componentInstance.onBillApproveOrDeclinedEvent.subscribe((data) => {
        //this.getBenefitBillClaimsByManager();
        this.search();
      });
      outletEditDialog.afterClosed().subscribe(() => {
      });
    }
  }
  createBillClaims(billClaim: any): void {
    const billClaimDialogConfig = new MatDialogConfig();
    billClaimDialogConfig.disableClose = true;
    billClaimDialogConfig.autoFocus = true;
    let newData = new BillClaim();
    newData.employeeId = this.authService.getLoggedInUserInfo().employeeId;
    newData.companyId = this.companyId;
    billClaimDialogConfig.data = billClaim;
    billClaimDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    billClaimDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateBillClaimComponent, billClaimDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onBillCreateEvent.subscribe((data) => {
     // this.getBenefitBillClaimsByManager();
     this.search();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }
  editBillClaims(billClaim: any): void {
    const billClaimDialogConfig = new MatDialogConfig();
    billClaimDialogConfig.disableClose = true;
    billClaimDialogConfig.autoFocus = true;
    billClaimDialogConfig.data = billClaim;
    billClaimDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    billClaimDialogConfig.width =  `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateBillClaimComponent, billClaimDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onBillEditEvent.subscribe((data) => {
      this.getBenefitBillClaimsByEmployee();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }
  search(searched?:boolean):void{
    this.loading = true;
    let form = this.searchFilter.employeeFilterFormGroup;
    this.billClaim = new BillClaim();
    this.billClaim.companyId = this.companyId;
    this.billClaim.branchIds = form.value.branchId;
    this.billClaim.departmentIds = form.value.departmentId;
    this.billClaim.designationIds = form.value.designationId;
    this.billClaim.searchText = form.value.searchText;
    this.billClaim.managerId = this.employeeId;
    this.billClaim.startTime = this.datePipe.transform(new Date(this.billClaimsFilterFormGroup.value.startDate), 'yyyy-MM-dd');
    this.billClaim.endTime = this.datePipe.transform(new Date(this.billClaimsFilterFormGroup.value.endDate), 'yyyy-MM-dd');
    this.benefitBillClaimService.getAllBenefitBillClaim(this.billClaim).subscribe(res=>{
        this.loading = false;
        this.billClaims = res;
        this.totalItems = res.length;
        this.showDetails = true;
        if(searched===true)
        this.currentPage = 1;
        this.generateTotalItemsText();
    },()=>{
      this.loading = false;
    })
  
  }
}
