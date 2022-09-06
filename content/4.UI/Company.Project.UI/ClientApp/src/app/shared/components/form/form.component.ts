import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, AbstractControlOptions, ValidatorFn } from '@angular/forms';
import { MatFormFieldAppearance } from '@angular/material/form-field';
import { Dict } from '../../generics/models';
import { FormConfig, FormField } from './form.models';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {
  form: FormGroup = this.fb.group({});
  fields?: FormField[][];
  defaultAppareance: MatFormFieldAppearance = 'outline';
  @Input() config!: FormConfig;
  @Input() title?: string;
  @Input() subtitle?: string;
  @Output() submit = new EventEmitter();
  @Output() cancel = new EventEmitter();
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.title = this.title || this.config.title;
    this.subtitle = this.subtitle || this.config.subtitle;
    const current: Dict<any> = {};
    const generalValidators: ValidatorFn[] = [];
    this.config.fields.forEach(field => {
      const value = field.value || null;
      const validators = [];
      for (const key in field.validators) {
        if (field.validators.hasOwnProperty(key)) {
          validators.push(field.validators[key].validator);
        }
      }
      current[field.name] = [value, validators];
    });
    for (const key in this.config.validators) {
      if (this.config.validators.hasOwnProperty(key)) {
        generalValidators.push(this.config.validators[key].validator);
      }
    }
    const options: AbstractControlOptions  = { validators: generalValidators };
    this.fields = this.splitArray(this.config.fields, this.config.colsPerRow);
    this.form = this.fb.group(current, options);
  }

  splitArray<T>(array: Array<T>, l?: number): Array<Array<T>> {
    const limit = l || 1;
    const fields: Array<Array<T>> = [];
    for (let i = 0, len = array.length; i < len; i += limit) {
      fields.push(array.slice(i, i + limit));
    }
    return fields;
  }

  onSubmit() {
    if (this.form.valid) {
      console.error('Is valid', this.form.valid);
      this.submit.emit(this.form.value);
    }
  }

  onCancel() {
    this.cancel.emit(true);
  }

}
