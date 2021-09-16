import { HttpClient} from '@angular/common/http';
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

  getOrdersBySearchString(searchString: string){
    return this.http.get<Order[]>(this.baseUrl + 'adminhelper/search-orders/' + searchString)
  }


  addOrder(order: CreateOrder){
    return this.http.post(this.baseUrl + 'order/new', order)
  }
}
