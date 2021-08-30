import { Component, Input, OnInit } from '@angular/core';
import { OrderDto } from 'src/app/_models/orderDto';

@Component({
  selector: 'app-order-card',
  templateUrl: './order-card.component.html',
  styleUrls: ['./order-card.component.css']
})
export class OrderCardComponent implements OnInit {

  @Input() orderDto: OrderDto;
  
  constructor() { }

  ngOnInit(): void {
  }

}
