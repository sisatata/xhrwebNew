import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { Company, EarnLeaveEncashment } from 'src/app/shared/models';
import { CompanyService, EarnLeaveEncashmentService, EmployeeSalaryProcessService } from 'src/app/shared/services';
import { CreateEarnLeaveEncashmentComponent } from './create-earn-leave-encashment/create-earn-leave-encashment.component';
@Component({
  selector: 'app-earn-leave-encashment-setup',
  templateUrl: './earn-leave-encashment-setup.component.html',
  styleUrls: ['./earn-leave-encashment-setup.component.css']
})
export class EarnLeaveEncashmentSetupComponent extends BaseAuthorizedComponent implements OnInit {
companyId:any = localStorage.getItem('companyId');
loading:boolean = false;
earnLeaveEncashmentFilterFormGroup: FormGroup;
earnLeaveEncashments: EarnLeaveEncashment[]=[];
companies: Company[]=[];
  constructor(
    private injector: Injector,
    private companyService: CompanyService,
    private formBuilder: FormBuilder,
    private dialog: MatDialog,
    private earnLeaveEncashmentService: EarnLeaveEncashmentService,
    
  ) {
    super(injector);
   }
  ngOnInit(): void {
    this.getLeaveEncashmentByCompanyId();
    this.buildForm();
    this.getAllCompanies();
  
  }
  getLeaveEncashmentByCompanyId():void{
    this.loading = true;
    this.earnLeaveEncashmentService.getEarnLeaveEncashmentByCompany(this.companyId).subscribe(res=>{
              this.earnLeaveEncashments = res;
              // console.log( this.earnLeaveEncashments);
              this.totalItems = res.length;
              this.generateTotalItemsText();
              this.loading = false;
    },()=>{
          this.loading = false;
    });
  }
  
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  buildForm():void{
    this.earnLeaveEncashmentFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
    });
  }
  createEarnLeaveEncashment():void{
    const createEarcnLeaveEncashmentDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createEarcnLeaveEncashmentDialogConfig.disableClose = true;
    createEarcnLeaveEncashmentDialogConfig.autoFocus = true;
    createEarcnLeaveEncashmentDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    createEarcnLeaveEncashmentDialogConfig.width = `${dialogWidth}%`;
    var employeeEarnLeaveEncashment = new EarnLeaveEncashment();
    
    const createEmployeeLeaveEncashmentDialog = this.dialog.open(
      CreateEarnLeaveEncashmentComponent,
      createEarcnLeaveEncashmentDialogConfig
    );
    const successFullEdit =
    createEmployeeLeaveEncashmentDialog.componentInstance.onEmployeeEarnLeaveEncashmentCreateEventCreateEvent.subscribe(
        (data) => {
          this.getLeaveEncashmentByCompanyId();
        }
      );
      createEmployeeLeaveEncashmentDialog.afterClosed().subscribe(() => {});
  }
  get f() {
    return this.earnLeaveEncashmentFilterFormGroup.controls;
  }
}
