import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CreateOrder } from '../_models/createOrder';
import { Order } from '../_models/order';
import { OrderDto } from '../_models/orderDto';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getOrdersByUserId(userId: number){
    return this.http.get<OrderDto[]>(this.baseUrl + 'order/list/' + userId)
  }


  addOrder(order: CreateOrder){
    console.log(JSON.stringify(order));
    let q = 1;
    return this.http.post(this.baseUrl + 'order/new', order);
  }
}
