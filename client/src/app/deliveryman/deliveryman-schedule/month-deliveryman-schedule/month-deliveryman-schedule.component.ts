import { Component, ContentChild, OnInit, TemplateRef } from '@angular/core';
import { CalendarService } from 'src/app/_services/calendar.service';

@Component({
  selector: 'app-month-deliveryman-schedule',
  templateUrl: './month-deliveryman-schedule.component.html',
  styleUrls: ['./month-deliveryman-schedule.component.css']
  
})
export class MonthDeliverymanScheduleComponent implements OnInit {
  date:Date = new Date();

  dateArr: Date[] = [];

  constructor(public calendarService: CalendarService) { }

  ngOnInit(): void {
    this.fillDateArr();
  }

  fillDateArr(){
    this.dateArr = this.calendarService.getMonthCalendar(this.date);
  }

  public getClass(day: Date) {
    let dayClass = day.getDay() === 0 || day.getDay() === 6 ? 'weekends' : 'weekday';
    dayClass += ' ' + (day.getMonth() !== this.date.getMonth() ? 'day-opacity' : '');
    return dayClass;
  }

  public onDateChanged(){
    this.fillDateArr();
  }
}
