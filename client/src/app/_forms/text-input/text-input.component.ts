import { Component, Input, OnInit, Output, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, FormGroup, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements ControlValueAccessor {
  @Input() label: string = '';
  @Input() type = 'text';

  @Output() getFormControl(form: FormGroup, value: string) {
    return form?.controls[value] as FormControl;
  }


  constructor(@Self() public ngControl: NgControl) { 
    this.ngControl.valueAccessor = this;
  }
  writeValue(obj: any): void {
    
  }
  registerOnChange(fn: any): void {
    
  }
  registerOnTouched(fn: any): void {
    
  }

  get control(){
    return this.ngControl.control as FormControl;
 }

 
}
