import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeSalary, Guid } from 'src/app/shared/models';
import { CompanyService, EmployeeSalaryService, PaymentOptionService, EmployeeService, SalaryStructureService, DesignationService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { DatePipe } from '@angular/common';
import { GradeService } from 'src/app/shared/services/company/grade.service';
@Component({
  selector: 'create-employee-salary',
  templateUrl: './create-employee-salary.component.html',
  styleUrls: ['./create-employee-salary.component.css']
})
export class CreateEmployeeSalaryComponent implements OnInit {
  onEmployeeSalaryCreateEvent: EventEmitter<any> = new EventEmitter();
  onEmployeeSalaryEditEvent: EventEmitter<any> = new EventEmitter();
  @BlockUI('bank-list-section') blockUI: NgBlockUI;
  employeesalaryCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any;
  employees: any;
  employee: any;
  employeeId: any;
  branchName: any ='';
  departmentName: any = '';
  positionName: any = '';
  designationName:any ='';
  gradeName: any = '';
  departments: any;
  positions: any;
  branches: any;
  paymentOptions: any;
  salaryStructures: any;
  employeesalary: EmployeeSalary = new EmployeeSalary();
  employeesalaryId: number;
  isEditMode = false;
  Index: any;
  childList: any = [];
  hideme = [];
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
 grades:any;
  constructor(
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateEmployeeSalaryComponent>,
    private formBuilder: FormBuilder,
    private employeesalaryservice: EmployeeSalaryService,
    private paymentOptionService: PaymentOptionService,
    private salarystructureService: SalaryStructureService,
    private datePipe : DatePipe,
    private gradeService: GradeService,
    //private designationService: DesignationService,
    //private departmentService: DepartmentService,
    //private branchService: BranchService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.employeesalary = new EmployeeSalary();
    if (isNaN(data)) {
      this.employeesalary = new EmployeeSalary(data);
      this.companyId = this.employeesalary.companyId;
      this.employeeId = this.employeesalary.employeeId;
     
   

     
    } else {

    }
    if(this.employeesalary.id){
      this.isEditMode = true;
      this.branchName = this.employeesalary.branchName;
      this.departmentName = this.employeesalary.departmentName;
      this.designationName = this.employeesalary.designationName;
      this.gradeName = this.employeesalary.gradeName;
     // this.optionName = this.employeesalary.optionName;
    
      
    }
    this.buildForm();
    this.getAllCompanies();
    this.getAllEmployees();
    this.getAllPaymentOptionByCompanyId();
    this.getAllSalaryStructureByCompanyId();
    this.getAllGrades();
  
    //this.getAllDepartmentByBranchId();
    //this.getAllDesignationsByDepartmentId();
  }

  ngOnInit() {
    this.getAllCompanies();
    this.getAllEmployees();
    this.employeesalary.employeeId = this.employeeId;
   
    
  }

  //getAllDesignationsByDepartmentId() {
  //  this.designationService.getAllDesignationByDepartmentId(this.f.departmentId.value).subscribe(result => {
  //    this.positions = result;
  //  });
  //}
  //getAllBranchByCompanyId(companyId) {
  //  this.branchService.getAllBranchByCompanyId(companyId).subscribe((result: any) => {
  //    this.branches = result;
  //  });
  //}

  //getAllDepartmentByBranchId() {
  //  this.departmentService.getAllDepartmentByBranchId(this.branchId).subscribe(result => {
  //    this.departments = result;
  //  }, error => {
  //  })
  //}
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result ;
    })
  }

  //onChangeBranch() {
  //  this.branchId = this.f.branchId.value;
  //  if (this.branchId) {
  //    this.getAllDepartmentByBranchId();
  //  }
  //}

  //onChangeDepartment() {
  //  this.departmentId = this.f.departmentId.value;
  //  if (this.departmentId) {
  //    this.getAllDesignationsByDepartmentId();
  //  }
  //}

  public showChildList(index, structureId) {
    this.childList[index] = this.salaryStructures.find(x => x.id == structureId);// this.employees.find(x => x.id == this.employeeId);

    //this.salaryStructures
    //this.getAllBrancgesByBankId(index, bankId);
    this.hideme[index] = !this.hideme[index];
    this.Index = index;
  }

  //getAllBrancgesByBankId(index: any, bankId: any) {
  //  if (this.companyId) {
  //    //this.blockUI.start();
  //    this.bankBranchService.getAllBankBranchByBankId(bankId).subscribe(result => {
  //      this.childList[index] = result;
  //      //this.blockUI.stop();
  //    }, error => {
  //      this.blockUI.stop();
  //    })
  //  }
  //}

  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    //this.getAllBranchByCompanyId(this.companyId);
    this.getAllPaymentOptionByCompanyId();
    this.getAllSalaryStructureByCompanyId();
  }
  onChangeEmployee() {
    this.employeeId = this.f.employeeId.value;
    this.employee = this.employees.find(x => x.id == this.employeeId);
    this.branchName = this.employee.branchName;
    this.departmentName = this.employee.departmentName;
    this.designationName = this.employee.positionName;
    this.gradeName = this.employee.gradeName;
    this.employeesalary.gradeId = this.employee.gradeId;
   
   
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
     
    });
  }
  buildForm() {
    this.employeesalaryCreateForm = this.formBuilder.group({
      id: [this.employeesalary.id],
      employeeId: [this.employeesalary.employeeId],
      branchId: [this.employeesalary.branchId],
      departmentId: [this.employeesalary.departmentId],
      positionId: [this.employeesalary.positionId],
      gradeId: [this.employeesalary.gradeId],
      salaryStructureId: [this.employeesalary.salaryStructureId, [Validators.required]],   
      paymentOptionId: [this.employeesalary.paymentOptionId, [Validators.required]],
      grossSalary: [this.employeesalary.grossSalary, [Validators.required]],
      companyId: [this.employeesalary.companyId, [Validators.required]],
      startDate: [this.employeesalary.startDate, [Validators.required]],
      endDate: [this.employeesalary.endDate,[Validators.required]],

    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.employeesalaryCreateForm.invalid) {
      return;
    }
    if (this.employeesalary.id === undefined || this.employeesalary.id === null) {

      this.createEmployeeSalary();
    }
    else {
      this.editEmployeeSalary();
    }
    //this.dialogRef.close();
  }
  onChangeGrade(data:any):void{
  this.employee.gradeId = data.value;
  }
  getAllGrades() {
    this.gradeService.getGradeListByCompany(this.companyId).subscribe(result => {
      this.grades = result;
      this.buildForm();
    });
  }

  createEmployeeSalary() {
    this.employeesalary = new EmployeeSalary(this.employeesalaryCreateForm.value);
    this.employeesalary.branchId = this.employee.branchId;
    this.employeesalary.departmentId = this.employee.departmentId;
    this.employeesalary.positionId = this.employee.positionId;
    this.employeesalary.gradeId = this.employee.gradeId;
    this.loading = true;
    this.employeesalary.startDate = this.datePipe.transform(new Date(this.employeesalary.startDate), 'yyyy-MM-dd');
    this.employeesalary.endDate = this.datePipe.transform(new Date(this.employeesalary.endDate), 'yyyy-MM-dd');
    this.employeesalaryservice.createEmployeeSalary(this.employeesalary).subscribe((data: any) => {
      this.onEmployeeSalaryCreateEvent.emit(this.employeesalary.id);
      if((data as any).status === true){
        this.isFormValid = true;
        this.alertService.success('Employee Salary added successfully');
        this.dialogRef.close();

      }
      else{
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
     this.loading =false;
     
    }, (error: any) => {
      this.alertService.error(error);
      this.loading = false;
      

    });
  }


  editEmployeeSalary() {
    let newData = new EmployeeSalary(this.employeesalaryCreateForm.value);
 
    if (this.employeesalary !== null) {
      this.employeesalary.id = newData.id;
      this.employeesalary.employeeId = newData.employeeId;
      this.employeesalary.branchId = newData.branchId;
      this.employeesalary.departmentId = newData.departmentId;
      this.employeesalary.positionId = newData.positionId;
      this.employeesalary.gradeId = newData.gradeId;
      this.employeesalary.salaryStructureId = newData.salaryStructureId;
      this.employeesalary.paymentOptionId = newData.paymentOptionId;
      this.employeesalary.grossSalary = newData.grossSalary;
      this.employeesalary.companyId = newData.companyId;
      //this.employeesalary.startDate = newData.startDate;
     // this.employeesalary.endDate = newData.endDate;
      this.employeesalary.startDate = this.datePipe.transform(new Date(newData.startDate), 'yyyy-MM-dd');
    this.employeesalary.endDate = this.datePipe.transform(new Date(newData.endDate), 'yyyy-MM-dd');
      this.loading = true;
      this.employeesalaryservice.editEmployeeSalary(this.employeesalary).subscribe((data: any) => {
       
        this.onEmployeeSalaryEditEvent.emit(this.employeesalary.id);
         if((data as any).status === true){
           this.isFormValid = true;
           this.alertService.success('Employee Salary updated successfully');
           this.dialogRef.close();

         }
         else{
           this.isFormValid = false;
           this.errorMessages = (data as any).message;
         }
         this.loading = false;

      }, (error: any) => {
        this.alertService.error(error);
        this.loading = false;

      });
    }
  }
  getAllPaymentOptionByCompanyId() {
    this.blockUI.start();
    this.paymentOptionService.getAllPaymentOptionByCompanyId(this.companyId).subscribe(result => {
      this.paymentOptions = result;
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    })
  }

  getAllSalaryStructureByCompanyId() {
    this.blockUI.start();
    this.salarystructureService.getAllSalaryStructureByCompanyId(this.companyId).subscribe(result => {
      this.salaryStructures = result;
     
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    })

  }

  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);
  }
  get f() { return this.employeesalaryCreateForm.controls; }
}


