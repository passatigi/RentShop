import { HostListener} from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {
  profileEditForm: FormGroup | undefined;
  passwordChangeForm: FormGroup | undefined;
  user: User = {} as User;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any){
    if (this.profileEditForm.dirty){
      $event.returnValue = true;
    } 
  }
  constructor(private accountService: AccountService, private toastr:ToastrService,
    private fb: FormBuilder) { 
    this.initializeForm();
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.profileEditForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', Validators.required],
      phoneNumber: ['', Validators.required],
    })
    this.passwordChangeForm = this.fb.group({
      currentPassword:['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    })
    this.passwordChangeForm.controls.password.valueChanges.subscribe(() => {
      this.passwordChangeForm?.controls.confirmPassword.updateValueAndValidity();
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.get(matchTo)?.value ? null : {isMatching: true}
    }
  }



  updateUserInfo(){
    this.accountService.updateUserInfo(this.profileEditForm?.value).subscribe(()=>{
      this.profileEditForm.reset(this.user);
      this.toastr.success('Profile updated successfully');
    }) 
  }


  changePassword(){
    this.accountService.changePassword(
        this.passwordChangeForm?.controls['currentPassword'].value,
        this.passwordChangeForm?.controls['password'].value)
      .subscribe(()=>{
      this.toastr.success('Password changed successfully');
    })
  }
}
