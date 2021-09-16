import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

import { Order } from '../_models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getOrdersByUserId(userId: number){
    return this.http.get<Order[]>(this.baseUrl + 'order/list/' + userId)
  }

  getOrdersBySearchString(searchString: string){
    return this.http.get<Order[]>(this.baseUrl + 'adminhelper/search-orders/' + searchString)
  }


  addOrder(order: Order){
    return this.http.post(this.baseUrl + 'order/new', order)
  }
}
