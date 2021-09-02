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

  constructor(
    private accountService: AccountService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  loadMessages(){
    if(this.user && this.recipient && this.orderId){
      this.messageService.createHubConnection(this.user, this.recipient.id, this.orderId);
    }
  }

}
