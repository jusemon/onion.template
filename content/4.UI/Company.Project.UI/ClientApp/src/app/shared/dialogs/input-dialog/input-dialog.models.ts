import { ValidationErrors } from '@angular/forms';

export interface InputDialogDataValidator {
    [name: string]: { message: string, validator: ValidationErrors };
}

export interface InputDialogData {
    content: string;
    fields: [
        {
            name: string,
            label: string,
            value?: string,
            icon?: string,
            validators: InputDialogDataValidator
        }
    ];
}

export interface InputDialogResponse {
    [x: string]: string;
}
