import { Directive, ElementRef, Input, SimpleChanges, OnChanges, HostListener, Self } from '@angular/core';
import { AbstractControl, NgControl } from '@angular/forms';

@Directive({
  selector: '[appTrim]'
})
export class TrimDirective {

  constructor(@Self() private ngControl: NgControl) {}

  @HostListener('change', ['$event.target.value'])
  onChange(value: any) {
    if (typeof(value) === 'string') {
      this.ngControl.control?.setValue(value.trim());
    }
  }
}
