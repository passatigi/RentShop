import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { DeliverymanSchedule } from 'src/app/_models/deliverymanModels/deliverymanSchedule';

@Component({
  selector: 'app-deliveryman-schedule-form',
  templateUrl: './deliveryman-schedule-form.component.html',
  styleUrls: ['./deliveryman-schedule-form.component.css']
})
export class DeliverymanScheduleFormComponent implements OnInit, OnChanges {
  @Input() schedule?: DeliverymanSchedule ;

  dayStart?: Date;
  dayEnd?: Date;

  constructor() { }
  
  ngOnInit(): void {

  }

  ngOnChanges() {
    this.dayStart = new Date(this.schedule.startDelivery.getTime());
    this.dayStart.setHours(0)
    this.dayStart.setMinutes(0)
    this.dayEnd = new Date(this.schedule.startDelivery.getTime());
    this.dayEnd.setHours(24)
    this.dayEnd.setMinutes(0)
  }
}
