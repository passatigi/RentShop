import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { parse } from '@fortawesome/fontawesome-svg-core';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { Message } from 'src/app/_models/message';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-message-list',
  templateUrl: './message-list.component.html',
  styleUrls: ['./message-list.component.css']
})
export class MessageListComponent implements OnInit {
  user: User;
  messageThreadList: Message[] = [];
  data$: Observable<object>;
  
  
  recipient?: User;

  orderId?: number;
  recipientId?: number;

  constructor(
    private messageService: MessageService,
    public route: ActivatedRoute, 
    private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe((user) => {
      this.user = user;
    })
    this.messageService.getMessageThreads().subscribe((messages) => {
      this.messageThreadList = messages;
      console.log(messages)
    });

    this.route.queryParams.subscribe(
      (queryParam: any) => {
          this.orderId =  parseInt(queryParam['orderId']) ;
          this.recipientId = parseInt(queryParam['recipientId']);
      });

    console.log(this.orderId, this.recipientId)

  }


  openChat(message: Message){
    this.orderId =  message.orderId ;
    this.recipientId = message.recipientId === this.user.id ? message.senderId : message.recipientId;
  }
}
