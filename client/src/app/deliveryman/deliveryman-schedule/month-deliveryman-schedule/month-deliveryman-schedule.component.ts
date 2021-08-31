import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { DeliverymanSchedule } from 'src/app/_models/deliverymanModels/deliverymanSchedule';
import { CalendarService } from 'src/app/_services/calendar.service';
import { DeliveryService } from 'src/app/_services/delivery.service';

@Component({
  selector: 'app-month-deliveryman-schedule',
  templateUrl: './month-deliveryman-schedule.component.html',
  styleUrls: ['./month-deliveryman-schedule.component.css']
  
})
export class MonthDeliverymanScheduleComponent implements OnInit {
  date?:Date;

  dateArr: Date[] = [];
  deliverymanSchedules: DeliverymanSchedule[] = [];

  scheduleToEdit?: DeliverymanSchedule;

  coordinateX: number = 0;
  coordinateY: number = 100; 


  constructor(  public calendarService: CalendarService,
                public deliveryService: DeliveryService
                ) { }

  ngOnInit(): void {
    this.date = this.calendarService.date;
    
    this.fillDateArr();
  }

  fillDateArr(){
    this.dateArr = this.calendarService.getMonthCalendar(this.date);

    this.deliveryService.getDeliverymanSchedule(this.date.getFullYear(), this.date.getMonth())
      .subscribe((response) => {
        console.log(response)
        this.deliverymanSchedules = []
        for (const iterator of response) {
          this.deliverymanSchedules.push(
            <DeliverymanSchedule> 
            { 
              startDelivery: new Date(iterator.startDelivery),
              endDelivery: new Date(iterator.endDelivery)
            }
          )
        }
        console.log(this.deliverymanSchedules);
        
      });
  }

  public onDateChanged(){
    this.fillDateArr();
  }

  public getClass(day: Date) {
    let dayClass = day.getDay() === 0 || day.getDay() === 6 ? 'weekends' : 'weekday';
    dayClass += ' ' + (day.getMonth() !== this.date.getMonth() ? 'day-opacity' : '');
    return dayClass;
  }

  getWorkDay(date: Date){
    let schedule = undefined;
    for (let x of this.deliverymanSchedules) {
      if(x.startDelivery.getDate() == date.getDate() && 
          x.startDelivery.getMonth() == date.getMonth()){
            schedule = x;
            break;
          }
    }
    return schedule;
  }
  
  openEditMenu(event: any, day: Date){
    let scheduleBefore = this.getWorkDay(day);
    
    if(!scheduleBefore){
      scheduleBefore = {
        startDelivery: new Date(day.getTime()),
        endDelivery: new Date(day.getTime())
      }
      scheduleBefore.startDelivery.setHours(10);
      scheduleBefore.startDelivery.setMinutes(0);
      scheduleBefore.endDelivery.setHours(17);
      scheduleBefore.endDelivery.setMinutes(0);
    }

    this.scheduleToEdit = scheduleBefore;
  }

}
