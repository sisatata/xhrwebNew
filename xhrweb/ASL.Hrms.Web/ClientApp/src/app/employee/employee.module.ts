import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { EmployeeComponent } from "./employee.component";
import { SharedModule } from "../shared/shared.module";
import { CreateEmployeeComponent } from "./create-employee/create-employee.component";
import { EmployeeDetailBaseComponent } from "./employee-detail-base/employee-detail-base.component";
import { EmployeeDetailComponent } from "./employee-detail/employee-detail.component";
import { EmployeeOtherInfoBaseComponent } from "./employee-other-info-base/employee-other-info-base.component";
import { EmployeeAddressComponent } from "./employee-address/employee-address.component";
import { EmployeeEmailComponent } from "./employee-email/employee-email.component";
import { EmployeePhoneComponent } from "./employee-phone/employee-phone.component";
import { CreateEmployeeAddressComponent } from "./employee-address/create-employee-address/create-employee-address.component";
import { CreateEmployeeEmailComponent } from "./employee-email/create-employee-email/create-employee-email.component";
import { CreateEmployeePhoneComponent } from "./employee-phone/create-employee-phone/create-employee-phone.component";
import { EmployeeExperienceComponent } from "./employee-experience/employee-experience.component";
import { CreateEmployeeExperienceComponent } from "./employee-experience/create-employee-experience/create-employee-experience.component";
import { EmployeeEducationComponent } from "./employee-education/employee-education.component";
import { CreateEmployeeEducationComponent } from "./employee-education/create-employee-education/create-employee-education.component";
import { CreateEmployeeFamilyMemberComponent } from "./employee-family-member/create-employee-family-member/create-employee-family-member.component";
import { EmployeeFamilyMemberComponent } from "./employee-family-member/employee-family-member.component";
import { EmployeeManagerComponent } from "./employee-manager/employee-manager.component";
import { CreateEmployeeManagerComponent } from "./employee-manager/create-employee-manager/create-employee-manager.component";
import { EmployeeEnrollmentComponent } from "./employee-enrollment/employee-enrollment.component";
import { MainLayoutModule } from "../layouts/main-layout/main-layout.module";
import { AuthGuard } from "../auth/services/auth-guard";
import { AuthModule } from "../auth/auth.module";
import { EditEmployeeStatusComponent } from "./employee-enrollment/edit-employee-status/edit-employee-status.component";
import { MatExpansionModule } from "@angular/material/expansion";
import { EmployeeCardComponent } from "./employee-card/employee-card.component";
import { CreateEmployeeCardComponent } from "./employee-card/create-employee-card/create-employee-card.component";
import { AngularMultiSelectModule } from "angular2-multiselect-dropdown";
import { EmployeePromotionTransferComponent } from "./employee-promotion-transfer/employee-promotion-transfer.component";
import { DataService } from "../shared/services/data.service";
import { EmployeePromotionTransferListComponent } from "./employee-promotion-transfer-list/employee-promotion-transfer-list.component";
import { ApproveOrRejectEmployeePromotionTransferComponent } from "./employee-promotion-transfer-list/approve-or-reject-employee-promotion-transfer/approve-or-reject-employee-promotion-transfer.component";
import { ImportEmployeeComponent } from "./import-employee/import-employee.component";
import { ImportFileHistoryComponent } from './import-file-history/import-file-history.component';
const routes: Routes = [
  {
    path: "list",
    component: EmployeeComponent,
    canActivate: [AuthGuard],
    data: { roles: ["Administrators", "SystemAdmin"] },
  },
  {
    path: "employee-details/:id/:name",
    component: EmployeeDetailBaseComponent,
    canActivate: [AuthGuard],
    data: { roles: ["Administrators", "EmployeeRole", "SystemAdmin"] },
  },
  {
    path: "employee-promotion-transfer",
    component: EmployeePromotionTransferComponent,
    canActivate: [AuthGuard],
    data: { roles: ["Administrators", "SystemAdmin"] },
  },
  {
    path: "employee-promotion-transfer-list",
    component: EmployeePromotionTransferListComponent,
    canActivate: [AuthGuard],
    data: { roles: ["Administrators", "SystemAdmin"] },
  },
  {
    path: "employee-file-import-history",
    component: ImportFileHistoryComponent,
    canActivate: [AuthGuard],
    data: { roles: ["Administrators", "SystemAdmin"] },
  },
];

@NgModule({
  declarations: [
    EmployeeComponent,
    CreateEmployeeComponent,
    EmployeeDetailBaseComponent,
    EmployeeDetailComponent,
    EmployeeOtherInfoBaseComponent,
    EmployeeAddressComponent,
    EmployeeEmailComponent,
    EmployeePhoneComponent,
    CreateEmployeeAddressComponent,
    CreateEmployeeEmailComponent,
    CreateEmployeePhoneComponent,
    EmployeeExperienceComponent,
    CreateEmployeeExperienceComponent,
    EmployeeEducationComponent,
    CreateEmployeeEducationComponent,
    EmployeeFamilyMemberComponent,
    CreateEmployeeFamilyMemberComponent,
    EditEmployeeStatusComponent,
    EmployeeManagerComponent,
    CreateEmployeeManagerComponent,
    EmployeeEnrollmentComponent,
    EmployeeCardComponent,
    CreateEmployeeCardComponent,
    EmployeePromotionTransferComponent,
    EmployeePromotionTransferListComponent,
    ApproveOrRejectEmployeePromotionTransferComponent,
    ImportEmployeeComponent,
    ImportFileHistoryComponent,
  ],
  imports: [
    SharedModule,
    AuthModule,
    MatExpansionModule,
    MainLayoutModule,
    AngularMultiSelectModule,
    RouterModule.forChild(routes),
  ],

  providers: [DataService],
  entryComponents: [
    CreateEmployeeComponent,
    CreateEmployeeAddressComponent,
    CreateEmployeeEmailComponent,
    CreateEmployeePhoneComponent,
    CreateEmployeeExperienceComponent,
    CreateEmployeeEducationComponent,
    CreateEmployeeFamilyMemberComponent,
    CreateEmployeeManagerComponent,
    EditEmployeeStatusComponent,
    ImportEmployeeComponent,
  ],
})
export class EmployeeModule {}
