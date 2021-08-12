import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup | undefined;

  constructor(private fb: FormBuilder, private accountService: AccountService, private routher: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
    })

  }

  login() {
    this.accountService.login(this.loginForm?.value).subscribe(response => {
      this.routher.navigateByUrl('/')
    }, error => console.log(error))
  }
}
