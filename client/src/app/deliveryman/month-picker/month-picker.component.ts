import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CalendarService } from 'src/app/_services/calendar.service';

@Component({
  selector: 'app-month-picker',
  templateUrl: './month-picker.component.html',
  styleUrls: ['./month-picker.component.css']
})
export class MonthPickerComponent implements OnInit {
  @Output() onChanged = new EventEmitter<any>();
  @Input() dayOfMonth?: Date;

  monthName: string = '';

  constructor(public calendarService: CalendarService) { }

  ngOnInit(): void {
    this.setMonthName();
  }

  setMonthName(){
    this.monthName = this.calendarService.getMonthName(this.dayOfMonth);
  }

  addMonth(value: number){
    this.dayOfMonth.setMonth(this.dayOfMonth.getMonth() + value);
    this.setMonthName();
    this.onChanged.emit();
  }

}
