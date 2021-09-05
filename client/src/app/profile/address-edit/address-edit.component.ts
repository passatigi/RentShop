import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Address } from 'src/app/_models/address';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-address-edit',
  templateUrl: './address-edit.component.html',
  styleUrls: ['./address-edit.component.css']
})
export class AddressEditComponent implements OnInit {

  @Input() addresses?: Address[]; 

  addAddressForm?: FormGroup;
  validationErrors: string[] = [];

  constructor(private fb: FormBuilder, private accountService: AccountService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initializeAddForm();
  }

  findAddress(addresses: Address[], id: number){
    return addresses.find(x => x.id === id);
  }

  initializeAddForm() {
    this.addAddressForm = this.fb.group({
      country: ['', Validators.required],
      city: ['', Validators.required],
      houseAddress: ['', Validators.required],
      postalCode: ['', Validators.required],
    })
  }

  addAddress(){
    this.accountService.addAddress(this.addAddressForm?.value)
        .subscribe(response => {
          let address = response as Address;
          console.log(address);
          this.addresses.push(address);
          this.toastr.success('Address added successfully');

          this.addAddressForm?.reset();
          
    })
  }
}
