import { Component, OnInit } from '@angular/core';
import { CartItem } from 'src/app/_models/cartItem';
import { RealProduct } from 'src/app/_models/realProduct';
import { SelectProductService } from 'src/app/_services/select-product.service';

@Component({
  selector: 'app-cart-detais',
  templateUrl: './cart-detais.component.html',
  styleUrls: ['./cart-detais.component.css']
})
export class CartDetaisComponent implements OnInit {

  items: CartItem[];

  constructor(private itemServ: SelectProductService) { }

  ngOnInit(): void {
    this.items = this.itemServ.items;
  }

}
