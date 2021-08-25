import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-deliveryman-schedule-form',
  templateUrl: './deliveryman-schedule-form.component.html',
  styleUrls: ['./deliveryman-schedule-form.component.css']
})
export class DeliverymanScheduleFormComponent implements OnInit {
  @Input() day: Date;


  startTime: Date = new Date();
  endTime: Date = new Date();
 
  constructor() {
  }
  ngOnInit(): void {
    
  }
  

}
