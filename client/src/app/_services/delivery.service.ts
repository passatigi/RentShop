import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DeliverymanSchedule } from '../_models/deliverymanModels/deliverymanSchedule';
import { Order } from '../_models/order';

@Injectable({
  providedIn: 'root' 
})
export class DeliveryService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getDeliverymanSchedule(year: number, month: number){
    return this.http.get<Array<any>>(this.baseUrl + 'deliveryman/schedule-month?year=' + year + '&month=' + (month + 1));
  }

  updateDeliverymanSchedule(schedule: DeliverymanSchedule){
    return this.http.put(this.baseUrl + 'deliveryman/schedule-day', schedule);
  }

  getDeliveryList(day: Date){
    console.log(day.toJSON())
    return this.http.get<Order[]>(this.baseUrl + 'deliveryman/delivery-list?date=' + day.toJSON());
  }

  updateOrderStatus(orderId: number, status: string){
    return this.http.put(this.baseUrl + 'deliveryman/delivery-list', {orderId: orderId, newStatus: status});
  }


}
