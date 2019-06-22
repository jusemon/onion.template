import { ValidatorFn, FormGroup, AbstractControl } from '@angular/forms';

export abstract class CustomValidators {
  public static mustBeEquals(fieldName1: string, fieldName2: string): ValidatorFn {
    return (form: FormGroup): { [key: string]: boolean } | null => {
      const control1: AbstractControl = form.controls[fieldName1];
      const control2: AbstractControl = form.controls[fieldName2];
      if (control1.value !== control2.value) {
        return { mustBeEquals: true };
      }
      return null;
    };
  }
}
