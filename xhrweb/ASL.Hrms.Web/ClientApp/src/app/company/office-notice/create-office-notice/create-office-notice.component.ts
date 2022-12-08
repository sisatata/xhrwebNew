import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { OfficeNotice } from '../../../shared/models';
import { OfficeNoticeService, CompanyService } from '../../../shared/services';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { error } from 'protractor';
import { RouteConfigLoadEnd } from '@angular/router';
import { AlertService } from '../../../shared/services/alert.service';

@Component({
  selector: 'app-create-office-notice',
  templateUrl: './create-office-notice.component.html',
  styleUrls: ['./create-office-notice.component.css']
})
export class CreateOfficeNoticeComponent implements OnInit {

  onOfficeNoticeCreateEvent: EventEmitter<number> = new EventEmitter();
  onOfficeNoticeEditEvent: EventEmitter<number> = new EventEmitter();

  officeNoticeCreateForm: FormGroup
  submitted = false;
  officeNoticeId: any;
  companies: any;
  officeNotice: OfficeNotice;
  isEditMode = false;

  constructor(
    private companyService: CompanyService,
    private officeNoticeService: OfficeNoticeService,
    private dialogRef: MatDialogRef<CreateOfficeNoticeComponent>,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    this.officeNotice = new OfficeNotice();
    if (isNaN(data)) {
      this.officeNotice = new OfficeNotice(data);
    }
    if (this.officeNotice.id) {
      this.isEditMode = true;
    }
    else {
      this.isEditMode = false;

    }
    this.buildForm();

  }

  ngOnInit() {
    this.getAllCompanies();
  }

  buildForm() {
    this.officeNoticeCreateForm = this.formBuilder.group({
      subject: [this.officeNotice.subject, [Validators.required]],
      details: [this.officeNotice.details, [Validators.required]],
      startDate: [this.officeNotice.startDate],
      endDate: [this.officeNotice.endDate],
      isGeneral: [this.officeNotice.isGeneral],
      isSectionSpecific: [this.officeNotice.isSectionSpecific],
      applicableSections: [this.officeNotice.applicableSections],
      publishDate: [this.officeNotice.publishDate],
      isPublished: [this.officeNotice.isPublished],
      companyId: [this.officeNotice.companyId],
    })
  }

  onSubmit() {
    this.submitted = true;
    if (this.officeNoticeCreateForm.invalid) {
      return;

    }
    if (this.officeNotice.id === undefined) {
      this.createOfficeNotice();
    }
    else {
      this.editOfficeNotice();
    }
    this.dialogRef.close();
  }
  editOfficeNotice() {
    let newData = new OfficeNotice(this.officeNoticeCreateForm.value);
    if (this.officeNotice !== null) {
      this.officeNotice.applicableSections = newData.applicableSections;
      this.officeNotice.companyId = newData.companyId;
      this.officeNotice.details = newData.details;
      this.officeNotice.endDate = newData.endDate;
      this.officeNotice.isGeneral = newData.isGeneral;
      this.officeNotice.isPublished = newData.isPublished;
      this.officeNotice.isSectionSpecific = newData.isSectionSpecific;
      this.officeNotice.publishDate = newData.publishDate;
      this.officeNotice.startDate = newData.startDate;
      this.officeNotice.subject = newData.subject;
      this.officeNoticeService.updateOfficeNotice(this.officeNotice).subscribe((data: any) => {
        this.onOfficeNoticeEditEvent.emit(this.officeNotice.id);
        this.alertService.success("Office Notice updated successfully");
      },
        (error: any) => {
          this.showErrorMessage(error);
        });
    }
  }
  createOfficeNotice() {

    this.officeNotice = new OfficeNotice(this.officeNoticeCreateForm.value);
    this.officeNoticeService.createOfficeNotice(this.officeNotice).subscribe((data: any) => {
      this.onOfficeNoticeCreateEvent.emit(this.officeNotice.id);
      this.alertService.success("Office Notice added successfully");
    }, (error: any) => {
      this.showErrorMessage(error);
    });
  }

  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);

  }
  get f() { return this.officeNoticeCreateForm.controls; }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
}
