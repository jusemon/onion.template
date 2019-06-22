import { ValidationErrors } from '@angular/forms';

export interface FormDataValidator {
    [name: string]: { message: string, validator: ValidationErrors };
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
    validators?: FormDataValidator;
}
