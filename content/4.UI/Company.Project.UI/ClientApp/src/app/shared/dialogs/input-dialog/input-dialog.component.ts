import { Component, OnInit, Inject, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Dict } from '../../generics/models';
import { InputDialogData } from './input-dialog.models';

@Component({
  selector: 'app-input-dialog',
  templateUrl: './input-dialog.component.html',
  styleUrls: ['./input-dialog.component.scss'],
})
export class InputDialogComponent implements OnInit {
  form: FormGroup = this.fb.group({});
  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<InputDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: InputDialogData) { }

  ngOnInit() {
    const form: Dict<any> = {};
    this.data.fields.forEach(field => {
      const value = field.value || null;
      const validators = [];
      for (const key in field.validators) {
        if (field.validators.hasOwnProperty(key)) {
          const element = field.validators[key];
          validators.push(element.validator);
        }
      }
      form[field.name] = [value, validators];
    });
    this.form = this.fb.group(form);
  }

  onSubmit() {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
