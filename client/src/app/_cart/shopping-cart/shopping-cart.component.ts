import { Component, OnInit } from '@angular/core';
import { SelectProductService } from 'src/app/_services/select-product.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {

  constructor(private itemServ: SelectProductService) { }

  ngOnInit(): void {
  }

  get count(){
    return this.itemServ.items.length
  }
}
