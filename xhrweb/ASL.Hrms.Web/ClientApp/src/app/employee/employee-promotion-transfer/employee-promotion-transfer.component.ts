import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SearchFilterComponent } from 'src/app/shared/components/search-filter/search-filter.component';
import { Department, Designation, Employee, EmployeeFilter, EmployeePromotionTransfer, EmployeeSalary, Grade, PaymentOption, SalaryStructure } from 'src/app/shared/models';
import { BranchService, CompanyService, DepartmentService, DesignationService, EmployeePromotionTransferService, EmployeeSalaryService, EmployeeService, PaymentOptionService, SalaryStructureService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import {TransferPromotionEnums} from '../../enums/transferPromotion'
import {ValueTypes} from '../../enums/benefit-deduction-calculation-type.enum';
import { ActivatedRoute } from '@angular/router';
import { AfterViewInit } from '@angular/core';
import { ThrowStmt } from '@angular/compiler';
import { GradeService } from 'src/app/shared/services/company/grade.service';
@Component({
  selector: 'app-employee-promotion-transfer',
  templateUrl: './employee-promotion-transfer.component.html',
  styleUrls: ['./employee-promotion-transfer.component.css']
})
export class EmployeePromotionTransferComponent implements OnInit,AfterViewInit {
  employees: Employee[];
  submitted:boolean = false;
  employeePromotionTransferForm: FormGroup;
  employeePromotionTransfer: EmployeePromotionTransfer= new EmployeePromotionTransfer();
  loading: boolean = false;
  selectedItems: any[];
  grades:Grade[]=[];
  newPaymentOption:number = 1;
  paymentOptions:PaymentOption[]=[]
  salaryStructures:SalaryStructure[]=[];
  employeeFilter: EmployeeFilter = new EmployeeFilter();
  settings:any;
  employeeDetails:any;
  showReasons:boolean = false;
  isEditMode:boolean = false;
  isFixed:boolean = false;
  employeePromotionTransferId:any;
  showIncrementRelatedFields:boolean = false;
  showTransferRelatedFields:boolean = false;
  companyId : any = localStorage.getItem('companyId');
  valueTypes:any = [{name:'Fixed',id:1},{name:'Percent',id:2}];
  incrementOnTypes:any = [{name:'Gross',id:10},{name:'Basic',id:20},{name:'Basic And Houserent',id:30}];
  reasons:any=[{name:'Increment',id:10},{name:'Adjustment',id:20},{name:'Promotion',id:30},{name:'Transfer',id:40},{name:'Promotion & Increment',id:50},
  {name:'Starting Salary',id:60},{name:'Revised Salary',id:70},{name:'Others',id:80},
]
  @ViewChild(SearchFilterComponent) searchFilter:SearchFilterComponent;
  itemList = [];
  companies: any;
  departments: Department[];
  branches: any;
  departmentId: any;
  linkedEntityId: any;
  designations: Designation[];
  employeeSalaryDetails: EmployeeSalary[];
  constructor(
    private employeeService: EmployeeService,
    private alertService: AlertService,
    private employeeSalaryService: EmployeeSalaryService,
    private formBuilder: FormBuilder,
    private employeePromotionTransferService: EmployeePromotionTransferService,
    private datePipe: DatePipe,
    private companyService: CompanyService,
    private branchService: BranchService,
    private departmentService: DepartmentService,
    private designationService: DesignationService,
    private gradeService: GradeService,
    private salarystructureService:SalaryStructureService,
    private paymentOptionService:PaymentOptionService,
    private route: ActivatedRoute

  ) { 
    
    let id =  this.route.snapshot.paramMap.get('id');
    if(id)
    this.isEditMode = true;
  }
  ngOnInit() {
   
    this.initializeEmployeeDropdown();
   
    
      this.buildForm();
    
   
  }
  ngAfterViewInit(): void{
  
    let id =  this.route.snapshot.paramMap.get('id');
    if(id){
      this.editMode(id);
    }
   
  }
  getAllCompanies():void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllBranchByCompanyId();
    }
  }
  onChangeBranch(data:any):void{
     this.getDepartmentByBranch(data);
     this.f.newPositionId.setValue(null);
     this.designations=[]
  }
  onChangeDepartment(departmentId: any) {
    this.departmentId = departmentId;
    this.linkedEntityId = departmentId;
 
    if (departmentId) {
      this.getAllDesignationByDepartmentId();
    }
  }
  getAllDesignationByDepartmentId():void{

    this.designationService.getAllDesignationByDepartmentId(this.departmentId).subscribe(res=>{
                this.designations = res;
                
    },()=>{})
  }
  getAllBranchByCompanyId() {
    this.branchService.getAllBranchByCompanyId(this.companyId).subscribe((result: any) => {
      this.branches = result;
      
      
      
    });
  }
getDepartmentByBranch(branchId:any):void{
  this.departmentService.getAllDepartmentByBranchId(branchId).subscribe(res=>{
    this.departments =res;
   
     // this.getAllDesignationByDepartmentId();
    
  },()=>{
    
  })
}
getAllGrades():void {
  this.gradeService.getGradeListByCompany(this.companyId).subscribe(result => {
    this.grades = result;
  });
}
getAllSalaryStructureByCompanyId():void {
 
  this.salarystructureService.getAllSalaryStructureByCompanyId(this.companyId).subscribe(result => {
    this.salaryStructures = result;
   
    
  }, error => {
   
  })

}
getAllPaymentOptionByCompanyId():void {
 
  this.paymentOptionService.getAllPaymentOptionByCompanyId(this.companyId).subscribe(result => {
    this.paymentOptions = result;
    
  }, error => {
   
  })
}
  initializeEmployeeDropdown():void{
    this.selectedItems = [];
    const width = window.screen.width > 768 ? '2': '1';
    this.settings = {
      text: "Select employees",
      enableSearchFilter: true,
      unSelectAllText:"Un Select All",
      classes: "myclass custom-class",
      labelKey:"fullName",
      primaryKey:"id",
      badgeShowLimit:width,
      showCheckbox:true,
      disabled: this.isEditMode === true? true:false,
      enableFilterSelectAll:true,
      maxHeight:"200",
      singleSelection: true
      
  };
  }
  search():void{
    let form = this.searchFilter.employeeFilterFormGroup;
    this.employeeFilter.branchIds = form.value.branchId;
    this.employeeFilter.departmentIds = form.value.departmentId;
    this.employeeFilter.designationIds = form.value.designationId;
    this.loading = true;
    this.employeeService.getAllEmployeeFilteredList(this.employeeFilter).subscribe(res=>{
                this.employees = (res as any).data;
                this.loading = false;
                this.itemList = this.employees;
               // this.selectedItems = this.isEditMode === true? this.
    },()=>{
      this.loading = false;
    })
  }
  onItemSelect(id:any):void{
    this.loading = true;
    this.employeeSalaryService.getEmployeeSalaryByEmployeeId(id).subscribe(res=>{
           this.loading = false;
           this.employeeSalaryDetails = res;
        
           this.showReasons = true;
          let currentYear = new Date().getFullYear();
          let currentMonth = new Date().getMonth();
          let currentDate = new Date().getDate();
          this.employeeDetails = this.employeeSalaryDetails.filter(x=> new Date(x.startDate)<= new Date(currentYear, currentMonth, currentDate) &&
              new Date(x.endDate)>= new Date(currentYear, currentMonth, currentDate)
           );
         
          
          
    },()=>{
    })
  }
  onItemDeselect(data:any):void{
    this.showReasons = false;
    this.employeeDetails=[];
    this.showIncrementRelatedFields = false;
    this.showTransferRelatedFields = false;
    this.f.incrementTypeId.setValue('');
    this.clearSalaryFields();
    this.clearTransferFields();
  }
  buildForm():void{
    
  

    this.employeePromotionTransferForm = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      employeeId:[this.employeePromotionTransfer.employeeId],
      incrementTypeId:[this.employeePromotionTransfer.incrementTypeId,[ Validators.required]],
      previousCompanyId:[this.employeePromotionTransfer.previousCompanyId],
      previousBranchId:[this.employeePromotionTransfer.previousBranchId],
      previousDepartmentId:[this.employeePromotionTransfer.previousDepartmentId],
      previousPositionId:[this.employeePromotionTransfer.previousPositionId],
      previousBasic:[this.employeePromotionTransfer.previousBasic],
      startDate:[this.employeePromotionTransfer.startDate, [Validators.required]],
      previousGradeId:[this.employeePromotionTransfer.previousGradeId],
      previousSalaryStructureId:[this.employeePromotionTransfer.previousSalaryStructureId],
      previousPaymentOptionId:[this.employeePromotionTransfer.previousPaymentOptionId]
    });
   
  }
  addFormControlTransfer():void{
    this.setTransferFields();  
    this.employeePromotionTransferForm.addControl('newCompanyId',this.formBuilder.control(this.employeePromotionTransfer.newCompanyId, Validators.required));
    this.employeePromotionTransferForm.addControl('newBranchId',this.formBuilder.control(this.employeePromotionTransfer.newBranchId, Validators.required));
    this.employeePromotionTransferForm.addControl('newPositionId',this.formBuilder.control(this.employeePromotionTransfer.newPositionId, Validators.required));
    this.employeePromotionTransferForm.addControl('newDepartmentId',this.formBuilder.control(this.employeePromotionTransfer.newDepartmentId, Validators.required));
    
  }
  addFormControlSalary():void{
    this.setSalaryFields();
    this.employeePromotionTransferForm.addControl('proposedDate',this.formBuilder.control(this.employeePromotionTransfer.proposedDate, Validators.required));
    this.employeePromotionTransferForm.addControl('incrementValue',this.formBuilder.control(this.employeePromotionTransfer.incrementValue, Validators.required));
    this.employeePromotionTransferForm.addControl('incrementOn',this.formBuilder.control(this.employeePromotionTransfer.incrementOn,[Validators.required]));
    this.employeePromotionTransferForm.addControl('incrementValueTypeId',this.formBuilder.control(this.employeePromotionTransfer.incrementValueTypeId, Validators.required));
    this.employeePromotionTransferForm.addControl('newGradeId',this.formBuilder.control(this.employeePromotionTransfer.newGradeId, Validators.required));
    this.employeePromotionTransferForm.addControl('newSalaryStructureId',this.formBuilder.control(this.employeePromotionTransfer.newSalaryStructureId, Validators.required));
    this.employeePromotionTransferForm.addControl('newPaymentOptionId',this.formBuilder.control(this.employeePromotionTransfer.newPaymentOptionId, Validators.required));
    
  }
  onChangeReasons(data:any):void{
   if(data === TransferPromotionEnums.Increment  || data === TransferPromotionEnums.Adjustment || data === TransferPromotionEnums.RevisedSalary){
    this.clearTransferFields();   
    this.addFormControlSalary();
       this.showIncrementRelatedFields = true;
       this.showTransferRelatedFields = false;
      
   }
   else if(data === TransferPromotionEnums.Transfer){
     this.clearSalaryFields();
     this.showIncrementRelatedFields = false;
     this.addFormControlTransfer();
     this.showTransferRelatedFields = true;
     this.onChangeCompany(this.companyId);
   }
   else {
     this.addFormControlSalary();
     this.addFormControlTransfer();
     this.showIncrementRelatedFields = true;
     this.showTransferRelatedFields = true;
   }
  
  }
  onSubmit():void{
    this.submitted = true;
    if(this.employeePromotionTransferForm.invalid){
      return ;
    }
    if(this.isEditMode){
      this.updateEmployeePromotionTransfer();
    }
    else{
    this.createEmployeePromotionTransfer();
    }
  }
  createEmployeePromotionTransfer():void{
    this.loading = true;
    this.employeePromotionTransfer = new EmployeePromotionTransfer(this.employeePromotionTransferForm.value);
    this.employeePromotionTransfer.employeeId = this.employeePromotionTransferForm.value.employeeId[0].id;
    if(this.employeePromotionTransfer.startDate){
    this.employeePromotionTransfer.startDate = this.datePipe.transform(new Date(this.employeePromotionTransfer.startDate), 'yyyy-MM-dd');
    }
    this.employeePromotionTransfer.previousCompanyId = this.companyId;
    this.employeePromotionTransfer.previousBranchId = this.employeeDetails[0].branchId;
    this.employeePromotionTransfer.previousDepartmentId = this.employeeDetails[0].departmentId;
    this.employeePromotionTransfer.previousPositionId = this.employeeDetails[0].positionId;
    this.employeePromotionTransfer.previousGross = this.employeeDetails[0].grossSalary;
    this.employeePromotionTransfer.previousHouserent = this.employeeDetails[0].employeeSalaryComponentList[0].componentAmount;
    this.employeePromotionTransfer.previousBasic = this.employeeDetails[0].employeeSalaryComponentList[1].componentAmount;
    this.employeePromotionTransfer.previousGradeId = this.employeeDetails[0].gradeId;
    this.employeePromotionTransfer.previousSalaryStructureId = this.employeeDetails[0].salaryStructureId;
    this.employeePromotionTransfer.previousPaymentOptionId = this.employeeDetails[0].paymentOptionId;

  if(this.employeePromotionTransfer.proposedDate){
    this.employeePromotionTransfer.proposedDate = this.datePipe.transform(new Date(this.employeePromotionTransfer.proposedDate ), 'yyyy-MM-dd');
  }
    this.employeePromotionTransferService.createEmployeePromotionTransfer(this.employeePromotionTransfer).subscribe(res=>{
                        this.loading = false;
                        this.alertService.success('Update successful');
    },()=>{
   this.loading = false;
    })
  }

  updateEmployeePromotionTransfer():void{
    this.loading = true;
    this.employeePromotionTransfer = new EmployeePromotionTransfer(this.employeePromotionTransferForm.value);
    this.employeePromotionTransfer.id = this.employeePromotionTransferId;
    this.employeePromotionTransfer.employeeId = this.employeePromotionTransferForm.value.employeeId[0].id;
    if(this.employeePromotionTransfer.startDate){
    this.employeePromotionTransfer.startDate = this.datePipe.transform(new Date(this.employeePromotionTransfer.startDate), 'yyyy-MM-dd');
    }
    this.employeePromotionTransfer.previousCompanyId = this.companyId;
    this.employeePromotionTransfer.previousBranchId = this.employeeDetails[0].branchId;
    this.employeePromotionTransfer.previousDepartmentId = this.employeeDetails[0].departmentId;
    this.employeePromotionTransfer.previousPositionId = this.employeeDetails[0].positionId;
    this.employeePromotionTransfer.previousGross = this.employeeDetails[0].grossSalary;
    this.employeePromotionTransfer.previousHouserent = this.employeeDetails[0].employeeSalaryComponentList[0].componentAmount;
    this.employeePromotionTransfer.previousBasic = this.employeeDetails[0].employeeSalaryComponentList[1].componentAmount;
    this.employeePromotionTransfer.previousGradeId = this.employeeDetails[0].gradeId;
    this.employeePromotionTransfer.previousSalaryStructureId = this.employeeDetails[0].salaryStructureId;
    this.employeePromotionTransfer.previousPaymentOptionId = this.employeeDetails[0].paymentOptionId;
  if(this.employeePromotionTransfer.proposedDate){
    this.employeePromotionTransfer.proposedDate = this.datePipe.transform(new Date(this.employeePromotionTransfer.proposedDate ), 'yyyy-MM-dd');
  }
    this.employeePromotionTransferService.updateEmployeePromotionTransfer(this.employeePromotionTransfer).subscribe(res=>{
                        this.loading = false;
                        this.alertService.success('Update successful');
    },()=>{
   this.loading = false;
    })
  }

  onChangeValueTypes(data:any):void{
    if(data === ValueTypes.Fixed){
      this.isFixed = true;
      this.f.incrementOn.clearValidators();
      this.employeePromotionTransferForm.get("incrementOn").updateValueAndValidity();
      this.f.incrementOn.setValue('');
    }
    else{
      this.isFixed = false;
      this.f.incrementOn.setValidators([Validators.required]);
    } this.employeePromotionTransferForm.get("incrementOn").updateValueAndValidity();
  }
  get f() {
    return this.employeePromotionTransferForm.controls;
  }
  setTransferFields():void{
    this.getAllCompanies();
    this.onChangeCompany(this.companyId);
  }
  setSalaryFields():void{
    this.getAllGrades();
    this.getAllSalaryStructureByCompanyId();
    this.getAllPaymentOptionByCompanyId();
  }
  clearTransferFields():void{
    this.f.newCompanyId?.setValue(null);
    this.f.newBranchId?.setValue(null);
    this.f.newPositionId?.setValue(null);
    this.f.newDepartmentId?.setValue(null);
    this.employeePromotionTransferForm.removeControl('newCompanyId');
    this.employeePromotionTransferForm.removeControl('newBranchId');
    this.employeePromotionTransferForm.removeControl('newPositionId');
    this.employeePromotionTransferForm.removeControl('newDepartmentId');
  }
  clearSalaryFields():void{
    this.f.proposedDate?.setValue(null);
    this.f.incrementValue?.setValue(null);
    this.f.incrementValueTypeId?.setValue(null);
    this.f.startDate?.setValue(null);
    this.f.newGradeId?.setValue(null);
    this.f.newSalaryStructureId?.setValue(null);
    this.f.newPaymentOptionId?.setValue(null);
    this.employeePromotionTransferForm.removeControl('proposedDate');
    this.employeePromotionTransferForm.removeControl('incrementValue');
    this.employeePromotionTransferForm.removeControl('incrementValueTypeId');
    this.employeePromotionTransferForm.removeControl('newGradeId');
    this.employeePromotionTransferForm.removeControl('newSalaryStructureId');
    this.employeePromotionTransferForm.removeControl('newPaymentOptionId');
    //this.employeePromotionTransferForm.removeControl('startDate');

    /**
     * 
     *  this.employeePromotionTransferForm.addControl('newGradeId',this.formBuilder.control(this.employeePromotionTransfer.newGradeId, Validators.required));
    this.employeePromotionTransferForm.addControl('newSalaryStructureId',this.formBuilder.control(this.employeePromotionTransfer.newSalaryStructureId, Validators.required));
    this.employeePromotionTransferForm.addControl('newPaymentOptionId',this.formBuilder.control(this.employeePromotionTransfer.newPaymentOptionId, Validators.required));
    
     */
  }
  editMode(id:any):void{
    this.loading = true;
    this.initializeEmployeeDropdown();
    this.employeePromotionTransferService.getEmployeePromotionTransferById(id).subscribe(res=>{
     // this.initializeEmployeeDropdown();
                 this.loading = false;
                // this.search();
                this.employeePromotionTransfer= res;
                this.onItemSelect(res.employeeId);
                this.employeePromotionTransferId = res.id;
                this.employeeService.getAllEmployees().subscribe(response=>{
                        this.employees = response;   
                        this.itemList = this.employees;
                        
                        this.selectedItems =[];
                        let employee = this.employees.filter(x=>x.id === res.employeeId);
                        this.selectedItems=employee;
                        this.buildForm();
                        this.showReasons = true;
                       if(this.employeePromotionTransfer.newCompanyId){
                        this.addFormControlTransfer();
                         this.showTransferRelatedFields = true;
                        
                          this.onChangeCompany(this.employeePromotionTransfer.newCompanyId);
                          this.onChangeBranch(this.employeePromotionTransfer.newBranchId);
                          this.onChangeDepartment(this.employeePromotionTransfer.newDepartmentId);
                          this.f.newPositionId.setValue(this.employeePromotionTransfer.newPositionId);
                       } 
                       if(this.employeePromotionTransfer.proposedDate){
                        this.addFormControlSalary();
                         this.showIncrementRelatedFields = true;
                         this.isFixed =  this.employeePromotionTransfer.incrementValueTypeId === ValueTypes.Fixed ? true:false;
                         this.onChangeValueTypes(this.employeePromotionTransfer.incrementValueTypeId)
                        
                       }
                        
                       
                },()=>{})
                
               //  this.search();

    },()=>{
      this.loading = false;
    })
    
  }
  
}
