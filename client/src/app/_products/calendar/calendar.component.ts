import { Component, Input, OnInit } from '@angular/core';
import { CartItem } from 'src/app/_models/cartItem';
import { Segment } from 'src/app/_models/segment';
import { DateService } from '../../_services/date.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  days: Segment[];
  disabledDates: Date[] = [];
  minDate: Date;
  currentDate = new Date();
  @Input() item: CartItem;

  constructor(private dateService: DateService) { }

  ngOnInit(): void {
    this.dateService.getProductScheduleById(this.item.id).subscribe( result => {
      this.days = result.segments;
      this.minDate = this.addDays(this.currentDate, 1);
      this.addDisabledDays();
    }) 
  }

  addDisabledDays(){
    for (var i = 0; i < this.days.length; i++) {
      if (new Date(this.days[i].rentEnd) >= this.minDate) {
        for (var j = new Date(this.days[i].rentStart); j <= new Date(this.days[i].rentEnd); j = new Date(this.addDays(j, 1))){
          this.disabledDates.push(j);
        }
      }
    }
  }

  addDays(date: Date, count: number): Date{
    date = new Date(date.getTime() + (1000 * 60 * 60 * 24));
    return date;
  }
}
