import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-message-list',
  templateUrl: './message-list.component.html',
  styleUrls: ['./message-list.component.css']
})
export class MessageListComponent implements OnInit {
  user?: User;
  recipient?: User;
  orderId?: number;

  recipientId?: number;

  constructor(
    private accountService: AccountService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.recipientId = 5;
    this.orderId = 4;
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    this.loadMessages();
  }

  loadMessages(){
    if(true){
      this.messageService.createHubConnection(this.user, this.recipientId, this.orderId);
    }
  }

}
