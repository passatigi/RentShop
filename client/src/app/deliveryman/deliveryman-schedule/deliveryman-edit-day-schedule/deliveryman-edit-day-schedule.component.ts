import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { DeliverymanSchedule } from 'src/app/_models/deliverymanModels/deliverymanSchedule';
import { CalendarService } from 'src/app/_services/calendar.service';
import { DeliveryService } from 'src/app/_services/delivery.service';

@Component({
  selector: 'app-deliveryman-edit-day-schedule',
  templateUrl: './deliveryman-edit-day-schedule.component.html',
  styleUrls: ['./deliveryman-edit-day-schedule.component.css']
})
export class DeliverymanEditDayScheduleComponent implements OnInit {
  @Input() scheduleToEdit: DeliverymanSchedule;
  @Input() deliverymanSchedules: DeliverymanSchedule[];

  constructor(
              public calendarService: CalendarService,
              public deliveryService: DeliveryService,
              private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  updateSchedule(){
    if(this.scheduleToEdit){
      this.deliveryService.updateDeliverymanSchedule(this.scheduleToEdit).subscribe(() => {
        let newSchedule = this.deliverymanSchedules
          .find(x => x.startDelivery.getDate() === this.scheduleToEdit.startDelivery.getDate());

        if(!newSchedule){
          this.deliverymanSchedules.push(this.scheduleToEdit);
        }

        this.toastr.success("Successfully updated")
      });
    }
  }

  deleteSchedule(){

  }

}
