import { Component } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser(){
    let userStr = localStorage.getItem('user');
    if(userStr){
      const user: User = JSON.parse(userStr);
      if(user){
        this.accountService.setCurrentUser(user);
      }
    }
  }
}
