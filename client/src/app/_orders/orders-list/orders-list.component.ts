import { Component, OnInit } from '@angular/core';
import { OrderDto } from 'src/app/_models/orderDto';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { OrderService } from 'src/app/_services/order.service';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrls: ['./orders-list.component.css']
})
export class OrdersListComponent implements OnInit {

  orders: OrderDto[] = [];

  user: User;

  constructor(private orderService: OrderService, private accountService: AccountService) { }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('user'));
    this.loadOrdersByUserId(this.user.id)
  }

  loadOrdersByUserId(id: number){
    this.orderService.getOrdersByUserId(id).subscribe(orders => {
      this.orders = orders;
    })
  }
}
