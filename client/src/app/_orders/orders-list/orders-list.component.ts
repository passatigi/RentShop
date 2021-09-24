import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/_models/order';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { OrderService } from 'src/app/_services/order.service';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrls: ['./orders-list.component.css']
})
export class OrdersListComponent implements OnInit {

  orders: Order[] = [];

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
