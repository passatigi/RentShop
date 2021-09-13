import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { SelectProductService } from '../_services/select-product.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  isCollapsed = true;

  constructor(public accountService: AccountService, 
    private itemServ: SelectProductService) { }

  ngOnInit(): void {
  }

  get rpCount(){
    return this.itemServ.items.length
  }

  searchString(form: NgForm){
    console.log(form.value.search)
  }

}
