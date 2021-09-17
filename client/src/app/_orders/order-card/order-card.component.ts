import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from 'src/app/_models/order';
import { Product } from 'src/app/_models/product';
import { ProductsService } from 'src/app/_services/products.service';


@Component({
  selector: 'app-order-card',
  templateUrl: './order-card.component.html',
  styleUrls: ['./order-card.component.css']
})
export class OrderCardComponent implements OnInit {
  @Output() editOrder = new EventEmitter<any>();

  @Input() order: Order;
  product: Product;
  isCollapsed = true;
  
  constructor(private productService: ProductsService) { }

  ngOnInit(): void {
    console.log(this.order)
    this.productService.getProductById(this.order.orderProducts[0].productId).subscribe( product => {
      this.product = product;
    })
  }

  edit(){
      this.editOrder.emit();
  }


}
