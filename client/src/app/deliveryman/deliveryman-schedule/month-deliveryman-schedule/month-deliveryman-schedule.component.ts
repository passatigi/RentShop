import { Component, ContentChild, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { DeliverymanSchedule } from 'src/app/_models/deliverymanModels/deliverymanSchedule';
import { CalendarService } from 'src/app/_services/calendar.service';
import { DeliveryService } from 'src/app/_services/delivery.service';
import { DeliverymanScheduleFormComponent } from '../deliveryman-schedule-form/deliveryman-schedule-form.component';

@Component({
  selector: 'app-month-deliveryman-schedule',
  templateUrl: './month-deliveryman-schedule.component.html',
  styleUrls: ['./month-deliveryman-schedule.component.css']
  
})
export class MonthDeliverymanScheduleComponent implements OnInit {
  date:Date = new Date();

  dateArr: Date[] = [];
  deliverymanSchedules: DeliverymanSchedule[] = [];


  constructor(  public calendarService: CalendarService,
                public deliveryService: DeliveryService,
                private toastr: ToastrService) { }

  ngOnInit(): void {
    this.fillDateArr();
  }

  fillDateArr(){
    this.dateArr = this.calendarService.getMonthCalendar(this.date);
    this.deliveryService.getDeliverymanSchedule(this.date.getFullYear(), this.date.getMonth())
      .subscribe((response) => {
        this.deliverymanSchedules = []
        for (const iterator of response) {
          this.deliverymanSchedules.push(
            <DeliverymanSchedule> 
            { 
              startDelivery: new Date(Date.parse(iterator.startDelivery)),
              endDelivery: new Date(Date.parse(iterator.endDelivery))
            }
          )
        }
         
        console.log(this.deliverymanSchedules)
      });
  }

  public getClass(day: Date) {
    let dayClass = day.getDay() === 0 || day.getDay() === 6 ? 'weekends' : 'weekday';
    dayClass += ' ' + (day.getMonth() !== this.date.getMonth() ? 'day-opacity' : '');
    return dayClass;
  }



  public onDateChanged(){
    this.fillDateArr();
  }

  isWorkDay(date: Date){
    let schedule = undefined;
    for (let x of this.deliverymanSchedules) {
      if(x.startDelivery.getDate() == date.getDate() && 
          x.startDelivery.getMonth() == date.getMonth() ){
            schedule = x;
            break;
          }
    }
    return schedule;
  }
  
  scheduleToEdit?: DeliverymanSchedule;
  openEditMenu(event: any, day: Date){
    let offsetLeft = 0;
    let offsetTop = 0;

    let el = event.srcElement;

    let num = 0;
    while(num != 2){
        offsetLeft += el.offsetLeft;
        offsetTop += el.offsetTop;
        el = el.parentElement;
        num++;
    }
    console.log( { offsetTop:offsetTop , offsetLeft:offsetLeft });
    this.coordinateX = offsetLeft - 120;
    this.coordinateY =   offsetTop - 190;
    
    let scheduleBefore = this.isWorkDay(day);
    
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

  coordinateX: number;
  coordinateY: number; 

  updateSchedule(){
    if(this.scheduleToEdit){
      this.deliveryService.updateDeliverymanSchedule(this.scheduleToEdit).subscribe(() => {
        this.deliverymanSchedules.push(this.scheduleToEdit);
        this.scheduleToEdit = undefined;
        this.toastr.success("Successfully updated")
      });
    }
  }
}
