import { ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';

export abstract class CustomValidators {
  public static mustBeEquals(fieldName1: string, fieldName2: string): ValidatorFn {
    return (form: AbstractControl): ValidationErrors | null => {
      const control1 = form.get(fieldName1);
      const control2 = form.get(fieldName2);
      if (control1?.value !== control2?.value) {
        return { mustBeEquals: true };
      }
      return null;
    };
  }
}
