import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  
  user?: User;
  recipient?: User;
  orderId?: number;

  recipientId?: number;

  constructor(
    private accountService: AccountService,
    private messageService: MessageService,
    public router: Router) { }

  ngOnInit(): void {
    
    console.log(this.router.getCurrentNavigation().extras.state)

    this.recipientId = 1;
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
