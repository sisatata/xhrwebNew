import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PayrollComponent } from './payroll.component';
import { SharedModule } from '../shared/shared.module';
import { MainLayoutModule } from '../layouts/main-layout/main-layout.module';
import { AuthGuard } from '../auth/services/auth-guard';
import { AuthModule } from '../auth/auth.module';
import { BankComponent } from './bank/bank.component';
import { CreateBankComponent } from './bank/create-bank/create-bank.component';
import { BankBranchComponent } from './bank-branch/bank-branch.component';
import { EmployeeBankAccountComponent } from './employee-bank-account/employee-bank-account.component';
import { PaymentOptionComponent } from './payment-option/payment-option.component';
import { SalaryStructureComponent } from './salary-structure/salary-structure.component';
import { SalaryStructureCompComponent } from './salary-structure-component/salary-structure-component.component';
import { CreateBankBranchComponent } from './bank-branch/create-bankbranch/create-bank-branch.component';
import { CreateEmployeeBankAccountComponent } from './employee-bank-account/create-employeebankaccount/create-employee-bank-account.component';
import { CreatePaymentOptionComponent } from './payment-option/create-paymentoption/create-payment-option.component';
import { CreateSalaryStructureComponent } from './salary-structure/create-salarystructure/create-salary-structure.component';
import { CreateSalaryStructureCompComponent } from './salary-structure-component/create-salarystructurecomponent/create-salary-structure-component.component';
import { CreateEmployeeSalaryComponent } from './employee-salary/create-employee-salary/create-employee-salary.component';
import { EmployeeSalaryComponent } from './employee-salary/employee-salary.component';
import { GenerateSalaryComponent } from './generate-salary/generate-salary.component';
import { EmpPaidSalaryComponent } from './emp-paid-salary/emp-paid-salary.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BenefitDeductionComponent } from './benefit-deduction/benefit-deduction.component';
import { CreateBenefitDeductionComponent } from './benefit-deduction/create-benefit-deduction/create-benefit-deduction.component';
import { BenefitDeductionConfigComponent } from './benefit-deduction/benefit-deduction-config/benefit-deduction-config.component';
import { CreateBenefitDeductionConfigComponent } from './benefit-deduction/benefit-deduction-config/create-benefit-deduction-config/create-benefit-deduction-config.component';
import { CreateEmployeeBenefitDeductionComponent } from './benefit-deduction/benefit-deduction-config/create-employee-benefit-deduction/create-employee-benefit-deduction.component';
import { IncomeTaxPayerTypeComponent } from './income-tax-payer-type/income-tax-payer-type.component';
import { CreateIncomeTaxPayerTypeComponent } from './income-tax-payer-type/create-income-tax-payer-type/create-income-tax-payer-type.component';
import { IncomeTaxSlabComponent } from './income-tax-slab/income-tax-slab.component';
import { CreateIncomeTaxSlabComponent } from './income-tax-slab/create-income-tax-slab/create-income-tax-slab.component';
import { IncomeTaxParameterComponent } from './income-tax-parameter/income-tax-parameter.component';
import { CreateIncomeTaxParameterComponent } from './income-tax-parameter/create-income-tax-parameter/create-income-tax-parameter.component';
import { IncomeTaxInvestmentComponent } from './income-tax-investment/income-tax-investment.component';
import { CreateIncomeTaxInvestmentComponent } from './income-tax-investment/create-income-tax-investment/create-income-tax-investment.component';
import { FestivalBonusComponent } from './festival-bonus/festival-bonus.component';
import { CreateFestivalBonusConfigComponent } from './festival-bonus/create-festival-bonus-config/create-festival-bonus-config.component';
import { FestivalBonusProcessComponent } from './festival-bonus-process/festival-bonus-process.component';
import { BillClaimComponent } from './bill-claim/bill-claim.component';
import { ApproveBillClaimComponent } from './bill-claim/approve-bill-claim/approve-bill-claim.component';
import { CreateBillClaimComponent } from './bill-claim/create-bill-claim/create-bill-claim.component';
import { EarnLeaveEncashmentSetupComponent } from './earn-leave-encashment-setup/earn-leave-encashment-setup.component';
import { CreateEarnLeaveEncashmentComponent } from './earn-leave-encashment-setup/create-earn-leave-encashment/create-earn-leave-encashment.component';

const routes: Routes = [
  { path: '', component: PayrollComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'EmployeeRole', 'SystemAdmin'] } },
  { path: 'bank', component: BankComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'bank-branch', component: BankBranchComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'emloyee-bank-account', component: EmployeeBankAccountComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin','EmployeeRole'] } },
  { path: 'payment-option', component: PaymentOptionComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'salary-structure', component: SalaryStructureComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin' ]} },
  { path: 'salary-structure-component', component: SalaryStructureCompComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'employee-salary', component: EmployeeSalaryComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin','EmployeeRole'] } },
  { path: 'generate-salary', component: GenerateSalaryComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'emp-paid-salary', component: EmpPaidSalaryComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },
  { path: 'benefit-deduction', component: BenefitDeductionComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },
  { path: 'income-tax-payer-type', component: IncomeTaxPayerTypeComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },
  { path: 'income-tax-slab', component: IncomeTaxSlabComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },
  { path: 'income-tax-parameter', component: IncomeTaxParameterComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },
  { path: 'income-tax-investment', component: IncomeTaxInvestmentComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },
  { path: 'festival-bonus', component: FestivalBonusComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },
  { path: 'bill-claim', component: BillClaimComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },
  { path: 'festival-bonus-process', component: FestivalBonusProcessComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },
  { path: 'earn-leave-encashment', component: EarnLeaveEncashmentSetupComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },




];

@NgModule({
  declarations: [PayrollComponent, BankComponent, CreateBankComponent,
    BankBranchComponent, CreateBankBranchComponent,
    EmployeeBankAccountComponent, CreateEmployeeBankAccountComponent,
    PaymentOptionComponent, CreatePaymentOptionComponent,
    SalaryStructureComponent, CreateSalaryStructureComponent,
    SalaryStructureCompComponent, CreateSalaryStructureCompComponent,
    EmployeeSalaryComponent,CreateEmployeeSalaryComponent, GenerateSalaryComponent, EmpPaidSalaryComponent, BenefitDeductionComponent, CreateBenefitDeductionComponent, BenefitDeductionConfigComponent, CreateBenefitDeductionConfigComponent, CreateEmployeeBenefitDeductionComponent, IncomeTaxPayerTypeComponent, CreateIncomeTaxPayerTypeComponent, IncomeTaxSlabComponent, CreateIncomeTaxSlabComponent, IncomeTaxParameterComponent, CreateIncomeTaxParameterComponent, IncomeTaxInvestmentComponent, CreateIncomeTaxInvestmentComponent, FestivalBonusComponent, CreateFestivalBonusConfigComponent, FestivalBonusProcessComponent, BillClaimComponent, ApproveBillClaimComponent, CreateBillClaimComponent, EarnLeaveEncashmentSetupComponent, CreateEarnLeaveEncashmentComponent ],
  imports: [
    SharedModule,
    AuthModule,
    MainLayoutModule,
    MatProgressSpinnerModule,
    RouterModule.forChild(routes)
  ],
  providers: [],
  entryComponents: [CreateBankComponent]
})
export class PayrollModule { }
