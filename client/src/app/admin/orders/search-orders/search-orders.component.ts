import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/_models/order';
import { OrderService } from 'src/app/_services/order.service';

@Component({
  selector: 'app-search-orders',
  templateUrl: './search-orders.component.html',
  styleUrls: ['./search-orders.component.css']
})
export class SearchOrdersComponent implements OnInit {
  orders: Order[] = [];

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.searchOrders('1');
  }

  searchOrders(searchString: string){
    this.orderService.getOrdersBySearchString(searchString).subscribe((orders) => {
      this.orders = <Order[]>orders;
    })
  }

}
