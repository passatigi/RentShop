import { Component, ContentChild, OnInit, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-month-deliveryman-schedule',
  templateUrl: './month-deliveryman-schedule.component.html',
  styleUrls: ['./month-deliveryman-schedule.component.css']
  
})
export class MonthDeliverymanScheduleComponent implements OnInit {
  @ContentChild(TemplateRef) template: TemplateRef<any>;
  
  firstDayIsSan = false;
  date?:Date = new Date();

  dateArr: Date[] = [];

  constructor() { }

  ngOnInit(): void {
    
    let dayOfMonth = this.date;

    let firstCalendarDate = this.getMonthCalendarFirstDay(dayOfMonth);

    this.fillMonthDays(dayOfMonth, firstCalendarDate);
  }

  private getMonthCalendarFirstDay(dayOfMonth: Date){
    let firstDay = new Date(dayOfMonth.getTime());

    firstDay.setDate(1);

    let diff = firstDay.getDay();

    if(!this.firstDayIsSan){
      if(diff === 0){
        diff += 6;
      }
      else{
        diff -=1 ;
      }
    }
    
    firstDay.setDate(firstDay.getDate() - diff)

    return firstDay;
  }

  private fillMonthDays(dayOfMonth: Date, firstCalendarDate: Date){
    let month = dayOfMonth.getMonth();
    let isMonthEnd = false;
    let isLastDay = false;
    let lastDayOfWeek = this.firstDayIsSan ? 0 : 1;
    let tempDate = new Date(firstCalendarDate.getTime());

    while(!isMonthEnd || !isLastDay){
      this.dateArr.push(tempDate);
      tempDate = new Date(tempDate.getTime());
      tempDate.setDate(tempDate.getDate() + 1); 
      if(tempDate.getMonth() > month){
        isMonthEnd = true;
      }
      if(isMonthEnd && tempDate.getDay() === lastDayOfWeek){
        isLastDay = true;
      }
    }
  }


  public getStyles() {
    return {
      display: 'grid',
      'grid-template-columns': `repeat(7, 1fr)`
    };
  }

  public getClass(day: Date) {
    let dayClass = day.getDay() === 0 || day.getDay() === 6 ? 'weekends' : 'weekday';
    dayClass += ' ' + (day.getMonth() !== this.date.getMonth() ? 'day-opacity' : '');
    return dayClass;
  }

  weekDays: string[] = ['Sun', 'Mon','Tue','Wed', 'Thu','Fri','Sat']
  //weekDays: string[] = ['Sunday', 'Monday','Tuesday','Wednesday', 'Thursday','Friday','Saturday']

  getDayName(day: Date){
    return this.weekDays[day.getDay()]
  }
}
