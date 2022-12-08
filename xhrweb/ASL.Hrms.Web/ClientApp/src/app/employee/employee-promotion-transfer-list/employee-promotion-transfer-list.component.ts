import { Injector } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { Company, EmployeePromotionTransfer } from 'src/app/shared/models';
import { CompanyService, EmployeePromotionTransferService } from 'src/app/shared/services';
import { ApproveOrRejectEmployeePromotionTransferComponent } from './approve-or-reject-employee-promotion-transfer/approve-or-reject-employee-promotion-transfer.component';

@Component({
  selector: 'app-employee-promotion-transfer-list',
  templateUrl: './employee-promotion-transfer-list.component.html',
  styleUrls: ['./employee-promotion-transfer-list.component.css']
})
export class EmployeePromotionTransferListComponent extends BaseAuthorizedComponent implements OnInit {
  employeePromotionTransfers: EmployeePromotionTransfer[]=[];
  employeePromotionTransfersList: EmployeePromotionTransfer[]=[];
  submitted:boolean = false;
  companyId:any = localStorage.getItem('companyId');
  employeeProromotionTransferFilterFormGroup: FormGroup;
  companies: Company[]=[];
  loading:boolean = false;
  employeePromotionTransferModalData: any = {id:null,type:0};
  approvalStatuses:any[] = [{id:0,value:'All'},{id:'3',value:'Approved'},{id:'9',value:'Declined'},{id:'1',value:'Pending'}]
  constructor(private dialog: MatDialog,
    private employeePromotionTransferService: EmployeePromotionTransferService,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private injector : Injector
    ) { 

      super(injector);
    }

  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.getEmployeePromotionTransferByCompany();
  }
  getEmployeePromotionTransferByCompany():void{
    this.loading= true;
    this.employeePromotionTransferService.getEmployeePromotionTransferByCompanyId(this.companyId).subscribe(res=>{
                this.loading = false;
                this.employeePromotionTransfers = res;
               
                this.employeePromotionTransfersList = res;
                this.totalItems = res.length;
                this.generateTotalItemsText();
    },()=>{

    })

  }
  buildForm():void {
    this.employeeProromotionTransferFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      approvalStatus:[]
    });
  }
  getAllCompanies():void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  approveOrRejectEmployeePromotionTransfer(data:EmployeePromotionTransfer, type:number):void{
    const approveOrRejectEmployeePromotionTransfer = new MatDialogConfig();
    
    // Setting different dialog configurations
    approveOrRejectEmployeePromotionTransfer.disableClose = true;
    approveOrRejectEmployeePromotionTransfer.autoFocus = true;
    approveOrRejectEmployeePromotionTransfer.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    approveOrRejectEmployeePromotionTransfer.width = `${dialogWidth}%`;
    this.employeePromotionTransferModalData.id = data.id;
    this.employeePromotionTransferModalData.type = type;
    approveOrRejectEmployeePromotionTransfer.data = this.employeePromotionTransferModalData;
    const createemployeePhoneDialog = this.dialog.open(ApproveOrRejectEmployeePromotionTransferComponent, approveOrRejectEmployeePromotionTransfer);
    const successfullCreate = createemployeePhoneDialog.componentInstance.onEmployeeApproveOrRejectCreateEvent.subscribe((data) => {
      this.getEmployeePromotionTransferByCompany();
    });
    createemployeePhoneDialog.afterClosed().subscribe(() => {
    });  
  }
  onChangApprovalStatus(value:any):void{
    if(value === '3' || value === '9'){
    const data = this.employeePromotionTransfersList.filter(x=>x.approvalStatus === value.toString());
    this.employeePromotionTransfers = data;
    this.totalItems = data.length;
    this.generateTotalItemsText();
    }
    else if(value === '1'){
      const data = this.employeePromotionTransfersList.filter(x=>x.approvalStatus !== '3' && x.approvalStatus !== '9');
      this.employeePromotionTransfers = data;
      this.totalItems = data.length;
    this.generateTotalItemsText();
      
    }
    else{
      this.employeePromotionTransfers = this.employeePromotionTransfersList;
      this.totalItems = this.employeePromotionTransfers.length;
    this.generateTotalItemsText();
    }
    
  }
  navigateWithState(id:any):void {
    this.router.navigate(['/employee/employee-promotion-transfer',  {id: id } ]);
  }
 
  get f() { return this.employeeProromotionTransferFilterFormGroup.controls; }
}
