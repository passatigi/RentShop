import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-deliveryman-schedule-form',
  templateUrl: './deliveryman-schedule-form.component.html',
  styleUrls: ['./deliveryman-schedule-form.component.css']
})
export class DeliverymanScheduleFormComponent implements OnInit {


  startTime: Date = new Date();
  endTime: Date = new Date();
 
  constructor() {
  }
  ngOnInit(): void {
    
  }
  

}
