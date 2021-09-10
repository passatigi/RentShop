import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { map, take } from 'rxjs/operators';
import { Address } from '../_models/address';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  register(model: User){
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: any) => {
        if(user){
          this.setCurrentUser(user);
        }
        return user;
      })
    )
  }

  login(model: User){
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: any) => {
        const user = response;
        if(user){
          this.setCurrentUser(user);
        }
        return user;
      })
    )
  }

  setCurrentUser(user: User){
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(undefined)
  }

  getDecodedToken(token: any) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  getUserAddresses(email:string){
    return this.http.get<Address[]>(this.baseUrl + 'account/getAddresses/'+ email )
  }

  addAddress(address: Address){
    return this.http.post(this.baseUrl + 'account/addAddress', address);
  }
  
  updateUserInfo(user: User){
    return this.http.put(this.baseUrl + 'account/updateUserInfo', user).pipe(
      map((response: any) => {
        this.logout()
        const user = response;
        if(user){
          this.setCurrentUser(user);
          console.log(user);
        }
        return user;
      })
    );
  }

  changePassword(currentPassword: string, newPassword:string){
    return this.http.put(this.baseUrl + 'account/changePassword?currentPassword='
      + currentPassword + '&newPassword=' + newPassword, '').pipe(
    );
  }

  deleteAddress(id:number){
    return this.http.delete(this.baseUrl + 'account/deleteAddress/'+ id);
  }

}
