import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { parse } from '@fortawesome/fontawesome-svg-core';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { Message } from 'src/app/_models/message';
import { Order } from 'src/app/_models/order';
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
  order?: Order;

  orderId?: number;
  recipientId?: number;

  constructor(
    private messageService: MessageService,
    public route: ActivatedRoute, 
    private accountService: AccountService,
    private router: Router) { }

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
    this.getThreadInfo();
  }


  openChat(message: Message){
    let recipientId = message.recipientId === this.user.id ? message.senderId : message.recipientId;
    if(this.orderId ===  message.orderId &&
      this.recipientId === recipientId){

      }
      else{
        this.orderId =  message.orderId;
        this.recipientId = recipientId;
        this.getThreadInfo();
        this.router.navigate(
          [], 
          {
            relativeTo: this.route,
            queryParams: {recipientId:this.recipientId,
                        orderId: this.orderId },
            queryParamsHandling: 'merge'
          });
      }
    
  }

  getThreadInfo(){
    this.messageService.getMessageThreadInfo(this.recipientId, this.orderId).subscribe((obj) => {
      this.order = obj.order;
      this.recipient = obj.recipient;
    })
  }
}
