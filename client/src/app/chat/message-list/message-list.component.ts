import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { parse } from '@fortawesome/fontawesome-svg-core';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-message-list',
  templateUrl: './message-list.component.html',
  styleUrls: ['./message-list.component.css']
})
export class MessageListComponent implements OnInit {
  data$: Observable<object>;
  
  
  recipient?: User;

  orderId?: number;
  recipientId?: number;

  constructor(
    public route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(
      (queryParam: any) => {
          this.orderId =  parseInt(queryParam['orderId']) ;
          this.recipientId = parseInt(queryParam['recipientId']);
      });

    console.log(this.orderId, this.recipientId)

    

  }


}
