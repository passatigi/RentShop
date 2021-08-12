import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';



@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup | undefined;

  validationErrors: string[] = [];

  constructor(private accountService: AccountService, 
    private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    })
    this.registerForm.controls.password.valueChanges.subscribe(() => {
      this.registerForm?.controls.confirmPassword.updateValueAndValidity();
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.get(matchTo)?.value ? null : {isMatching: true}
    }
  }

  register(){
    this.accountService.register(this.registerForm?.value).subscribe(response =>{
      this.router.navigateByUrl('/');;
    }, error => {
      this.validationErrors = error;
    })
  }
}
