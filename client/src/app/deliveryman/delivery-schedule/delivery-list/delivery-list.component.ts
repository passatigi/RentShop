
import { DOCUMENT } from '@angular/common';

import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Order } from 'src/app/_models/order';
import { DeliveryService } from 'src/app/_services/delivery.service';
import { DeveloperHelpService } from 'src/app/_services/developer-help.service';


@Component({
  selector: 'app-delivery-list',
  templateUrl: './delivery-list.component.html',
  styleUrls: ['./delivery-list.component.css']
})
export class DeliveryListComponent implements OnInit {
  date?: Date;

  orders: Order[] = [];
  document: any;

  constructor(private deliveryService: DeliveryService,
              private route: ActivatedRoute,
              public helpService: DeveloperHelpService,
              private toastr: ToastrService,
              @Inject(DOCUMENT) document) 
              { 
                this.document = document;
              }

  ngOnInit(): void {
    this.route.queryParams.subscribe( params => {
      this.date = new Date(parseInt(params['date']));
      this.date.setHours(15, 0, 0);

      this.deliveryService.getDeliveryList(this.date).subscribe((list) => {
        this.getTotalPrices(list);
        this.orders = list;
        console.log(this.orders);
        
      })
    })
  }

  oneDay = 24 * 60 * 60 * 1000;

  changeStringsToDates(object: any){
    console.log(typeof object)
    let array = Object.getOwnPropertyNames(object);
    console.log(array)
    for (const iterator of array) {
      if(iterator.includes('Date')){
        object[iterator] = new Date(object[iterator]);
      }
    }
  }

  getTotalPrices(orders: Order[]){
    for (const order of orders) {
      this.changeStringsToDates(order);
      const diffDays = Math.round(Math.abs((order.requiredReturnDate.getTime() - order.requiredDate.getTime()) / this.oneDay));
      let dayPrice = 0;
      for (const product of order.realProducts) {
        dayPrice += product.rentPrice;
      }
      order.totalPrice = dayPrice * diffDays;
    }
  }

  openInfo(orderId: number){
    let el = this.document.getElementById("hidden-info-" + orderId);

    if(el.style.visibility === 'visible'){
      el.style.visibility = 'collapse';
    }
    else{
      el.style.visibility = 'visible';
    }
  }

  submitOrder(order:Order){
    if(order.status === 'Awaiting delivery'){
      this.deliveryService.updateOrderStatus(order.id, 'Delivered').subscribe(() => {
          order.status = 'Delivered';
          this.toastr.success("Successfully changed");
      });
    }
    else if(order.status === 'Delivered'){
      this.deliveryService.updateOrderStatus(order.id, 'Returned').subscribe(() => {
          order.status = 'Returned';
          this.toastr.success("Successfully changed");
      });
    }
  }

  isCompleted(order: Order){
    if(order.requiredDate.toDateString() === this.date.toDateString() && order.status !== 'Awaiting delivery'){
      return true;
    }
    else if(order.requiredReturnDate.toDateString() === this.date.toDateString() 
            && order.status !== 'Delivered' && order.status !== 'Awaiting delivery'){
      return true;
    }
    return false;
  }
}
