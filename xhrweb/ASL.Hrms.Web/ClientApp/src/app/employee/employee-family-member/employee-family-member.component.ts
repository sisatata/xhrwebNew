import { Component, OnInit, Input } from '@angular/core';
import { CompanyService, EmployeeFamilyMemberService, BranchService } from 'src/app/shared/services';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Router } from '@angular/router';
import { EmployeeFamilyMember } from '../../shared/models';
import { CreateEmployeeFamilyMemberComponent } from './create-employee-family-member/create-employee-family-member.component';
import { AlertService } from '../../shared/services/alert.service';
import { AuthService } from 'src/app/auth/services/auth.service';
@Component({
  selector: 'app-employee-family-member',
  templateUrl: './employee-family-member.component.html',
  styleUrls: ['./employee-family-member.component.css']
})
export class EmployeeFamilyMemberComponent implements OnInit {
  @Input() employeeId: any;
  @Input() employeeFullName: any;
  @BlockUI('employeeFamilyMember-list-section') blockUI: NgBlockUI;
  employeeFamilyMembers: EmployeeFamilyMember[];
  employeeFamilyMemberId: any;
  companies: any;
  submitted: boolean;
  isFormValid: boolean;
  errorMessages: any;
  isHRManager:boolean;
   isAdmin:boolean;
  constructor(private employeeFamilyMemberService: EmployeeFamilyMemberService,
    private branchService: BranchService,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private alertService: AlertService,
    private router: Router,
    private authService: AuthService,
    private dialog: MatDialog) { 
      this.isHRManager = this.authService.isHRManager;
      this.isAdmin = this.authService.isAdmin;
    }
  ngOnInit() {
    this.getAllCompanies();
    //this.buildForm();
    this.getAllEmployeeFamilyMembers();
  }
  onChangeCompany(companyId: any) {
    //this.companyId = companyId;
  }
  getEmployeeFamilyMembers() {
    //this.submitted = true;
    //if (this.employeeFamilyMemberFilterFormGroup.invalid) {
    //  return;
    //}
    this.getAllEmployeeFamilyMembers();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  getAllEmployeeFamilyMembers() {
    this.blockUI.start();
    this.employeeFamilyMemberService.getEmployeeFamilyMemberByEmployeeId(this.employeeId).subscribe(result => {
     
      this.employeeFamilyMembers = result; // no status
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    })
  }
  createEmployeeFamilyMember() {
    const createemployeeFamilyMemberDialogConfig = new MatDialogConfig;
    // Setting different dialog configurations
    createemployeeFamilyMemberDialogConfig.disableClose = true;
    createemployeeFamilyMemberDialogConfig.autoFocus = true;
    createemployeeFamilyMemberDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createemployeeFamilyMemberDialogConfig.width = `${dialogWidth}%`;
    var employeeFamilyMember = new EmployeeFamilyMember();
    employeeFamilyMember.employeeId = this.employeeId;
    createemployeeFamilyMemberDialogConfig.data = employeeFamilyMember;
    const createEmployeeFamilyMemberDialog = this.dialog.open(CreateEmployeeFamilyMemberComponent, createemployeeFamilyMemberDialogConfig)
    const successfullCreate = createEmployeeFamilyMemberDialog.componentInstance.onEmployeeFamilyMemberCreateEvent.subscribe((data) => {
      this.getAllEmployeeFamilyMembers();
    });
    createEmployeeFamilyMemberDialog.afterClosed().subscribe(() => {
    });
  }
  editEmployeeFamilyMember(employeeFamilyMember: EmployeeFamilyMember) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = employeeFamilyMember
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;

    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateEmployeeFamilyMemberComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onEmployeeFamilyMemberEditEvent.subscribe((data) => {
      this.getAllEmployeeFamilyMembers();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteEmployeeFamilyMember(employeeFamilyMember: EmployeeFamilyMember): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the employee Family Member " + employeeFamilyMember.familiyMemberName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteEmployeeFamilyMember(employeeFamilyMember);
      }
    });
  }
  deleteEmployeeFamilyMember(employeeFamilyMember: EmployeeFamilyMember) {
    this.employeeFamilyMemberService.deleteEmployeeFamilyMember(employeeFamilyMember).subscribe((data) => {
      this.alertService.success('Employee Family Member deleted successfully');
      this.getAllEmployeeFamilyMembers();
    },
      (error) => {
        this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }
}
