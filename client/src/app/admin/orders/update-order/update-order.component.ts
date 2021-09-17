import { Component, Input, OnInit } from '@angular/core';
import { Order } from 'src/app/_models/order';

@Component({
  selector: 'app-update-order',
  templateUrl: './update-order.component.html',
  styleUrls: ['./update-order.component.css']
})
export class UpdateOrderComponent implements OnInit {
  @Input() order: Order;

  constructor() { }

  ngOnInit(): void {
  }

  

}
