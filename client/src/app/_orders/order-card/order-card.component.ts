import { Component, Input, OnInit } from '@angular/core';
import { OrderDto } from 'src/app/_models/orderDto';
import { Product } from 'src/app/_models/product';
import { ProductsService } from 'src/app/_services/products.service';

@Component({
  selector: 'app-order-card',
  templateUrl: './order-card.component.html',
  styleUrls: ['./order-card.component.css']
})
export class OrderCardComponent implements OnInit {

  @Input() orderDto: OrderDto;
  product: Product;
  isCollapsed = true;
  
  constructor(private productService: ProductsService) { }

  ngOnInit(): void {
    this.productService.getProductById(this.orderDto.orderProducts[0].productId).subscribe( product => {
      this.product = product;
    })
    this.orderDto.totalPrice = 7;
  }

}
