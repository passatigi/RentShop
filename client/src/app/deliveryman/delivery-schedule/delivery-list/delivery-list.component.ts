import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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

  constructor(private deliveryService: DeliveryService,
              private route: ActivatedRoute,
              public helpService: DeveloperHelpService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe( params => {
      this.date = new Date(parseInt(params['date']));

      this.deliveryService.getDeliveryList(this.date).subscribe((list) => {
        this.orders = list;
        console.log(this.orders);
      })
    })
  }

}
