import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'confirmation-with-text',
  templateUrl: './confirmation-dialog-with-text.component.html',
  styleUrls: ['./confirmation-dialog-with-text.component.css']
})

export class ConfirmationWithTextComponent implements OnInit {

  confirmationMessage: string;
  confirmationWithTextForm: FormGroup;
  submitted: boolean = false;


  constructor(private dialogRef: MatDialogRef<ConfirmationWithTextComponent>,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) data) {

    if (data !== null) {
      this.confirmationMessage = data;
    }
  }

  ngOnInit() {
    this.buildForm();
  }



  buildForm() {
    this.confirmationWithTextForm = this.formBuilder.group({
      note: ['', [Validators.maxLength(500)]],
    });
  }

  confirm() {
    this.submitted = true;
    this.dialogRef.close(this.confirmationWithTextForm.value);
  }
  close() {
    this.dialogRef.close();
  }

  get f() { return this.confirmationWithTextForm.controls; }

}


