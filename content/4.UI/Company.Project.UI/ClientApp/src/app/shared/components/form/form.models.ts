import { ValidationErrors, ValidatorFn } from '@angular/forms';
import { MatFormFieldAppearance } from '@angular/material/form-field';

export interface FormDataValidator {
    [name: string]: { message: string, validator: ValidationErrors };
}

export interface FormDataValidatorFn {
    [name: string]: { message: string, validator: ValidatorFn };
}

export interface FormResponse {
    [x: string]: string;
}

export interface FormField {
    name: string;
    label: string;
    type?: string;
    value?: string;
    icon?: string;
    validators: FormDataValidator;
}

export interface FormConfig {
    fields: FormField[];
    title?: string;
    subtitle?: string;
    appearance?: MatFormFieldAppearance;
    colsPerRow?: number;
    validators?: FormDataValidatorFn;
}
