import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ValidationErrors, FormGroup, FormBuilder } from '@angular/forms';
import { FormConfig } from './form.models';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {
  form: FormGroup = this.fb.group({});
  @Input() config: FormConfig;
  @Input() title?: string;
  @Input() subtitle?: string;
  @Output() submit = new EventEmitter();
  @Output() cancel = new EventEmitter();
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.title = this.title || this.config.title;
    this.subtitle = this.subtitle || this.config.subtitle;
    const form = {};
    const generalValidators: ValidationErrors[] = [];
    this.config.fields.forEach(field => {
      const value = field.value || null;
      const validators = [];
      for (const key in field.validators) {
        if (field.validators.hasOwnProperty(key)) {
          validators.push(field.validators[key].validator);
        }
      }
      form[field.name] = [value, validators];
    });
    for (const key in this.config.validators) {
      if (this.config.validators.hasOwnProperty(key)) {
        generalValidators.push(this.config.validators[key].validator);
      }
    }
    this.form = this.fb.group(form, { validators: generalValidators});
  }

  onSubmit() {
    if (this.form.valid) {
      this.submit.emit(this.form.value);
    }
  }

  onCancel() {
    this.cancel.emit(true);
  }

}
