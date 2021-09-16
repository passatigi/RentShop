import { Component, OnInit} from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Address } from '../_models/address';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  appUser: User;
  addresses: Address[]

  constructor(private accountService: AccountService, private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.appUser = user);
  }

  ngOnInit(): void { 
    this.loadAddresses();
  }

  loadAddresses(){
    this.accountService.getUserAddresses(this.appUser.email).subscribe(addresses => {
      this.addresses = addresses;
    })
  }

  deleteAddress(id:number){
    this.accountService.deleteAddress(id).subscribe(() => {
      this.addresses = this.addresses.filter(x => x.id !== id);
      this.toastr.success("Successfully deleted");
    })
  }

}
