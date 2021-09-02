import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {
  firstDayIsSan = environment.firstDayIsSan;

  private _date: Date = new Date();

  public get date(): Date {
    return this._date;
  }


  constructor() { 
    this.date.setDate(15);
  }

  public getMonthCalendar(dayOfMonth: Date){
    let firstCalendarDate = this.getMonthCalendarFirstDay(dayOfMonth);

    return this.fillMonthDays(dayOfMonth, firstCalendarDate);
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
    let dateArr:Date[] = [];
    let month = dayOfMonth.getMonth();
    let isMonthEnd = false;
    let isLastDay = false;
    let lastDayOfWeek = this.firstDayIsSan ? 0 : 1;

    let tempDate = new Date(firstCalendarDate.getTime());
    while(!isMonthEnd || !isLastDay){
      dateArr.push(tempDate);

      tempDate = new Date(tempDate.getTime());
      tempDate.setDate(tempDate.getDate() + 1); 

      let tempMonthNumber = tempDate.getMonth();
      if(tempMonthNumber === 11 && month === 0)
        tempMonthNumber = -1;
      if(tempMonthNumber > month || month === 11 && tempMonthNumber === 0){
        isMonthEnd = true;
      }
      if(isMonthEnd && tempDate.getDay() === lastDayOfWeek){
        isLastDay = true;
      }
    }

    return dateArr;
  }


  //weekDays: string[] = ['Sun', 'Mon','Tue','Wed', 'Thu','Fri','Sat']
  weekDays: string[] = ['Sunday', 'Monday','Tuesday','Wednesday', 'Thursday','Friday','Saturday']

  getDaysNamesArray(){
    if(this.firstDayIsSan)
      return this.weekDays;
    else {
      let weekDays = this.weekDays.filter(x => x !== 'Sunday');
      weekDays.push('Sunday');
      return weekDays;
    }
      
  }
  
  getDayName(day: Date){
    return this.weekDays[day.getDay()]
  }

  months: string[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
  getMonthName(day: Date){
    return this.months[day.getMonth()];
  }

  public getGridStyles() {
    return {
      display: 'grid',
      'grid-template-columns': `repeat(7, 1fr)`
    };
  }
}
