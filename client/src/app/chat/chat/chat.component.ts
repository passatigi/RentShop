import { identifierModuleUrl } from '@angular/compiler';
import { AfterViewChecked, Component, ElementRef, HostListener, Input, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { NgForm } from '@angular/forms';
import { distinctUntilChanged, take } from 'rxjs/operators';
import { Message } from 'src/app/_models/message';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MessageService } from 'src/app/_services/message.service';


@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit  {
  @Input() recipientId?: number;
  @Input() orderId?: number;

  @ViewChild('messageForm') messageForm?: NgForm;
  @ViewChild('scrollMe') private myScrollContainer?: ElementRef;

  user?: User;
  
  isAnyNewMessages = false;
  isBottomScrolled = true;
  isLoadingThread = false;
  loadingSend = false;
  
  lastScrollHeight = 0;
  lastMessageId = 0;
  messageContent?: string;
  
  constructor(public messageService: MessageService,
              private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.messageService.createHubConnection(this.user, this.recipientId, this.orderId);
    });

    this.messageService.messageThread$.pipe(distinctUntilChanged())
        .subscribe(messages => {
          //console.log(messages)
          setTimeout(() => {
            this.onMessagesChange(messages);
          }, 0)
        });
  }

  onMessagesChange(messages: Message[]){
    if(messages.length > 0){
      if(messages[messages.length - 1].id > this.lastMessageId){
        this.lastMessageId = messages[messages.length - 1].id;
        if(this.isBottomScrolled)
          this.scrollToBottom();
        else{
          this.isAnyNewMessages = true;
        }
      }
      else{
        this.isLoadingThread = false;
      }
    }
  }

  sendMessage(){
    this.loadingSend = true;
    if(this.recipientId && this.orderId && this.messageContent)
      this.messageService
      .sendMessage(this.recipientId, this.orderId, this.messageContent)
      .then(() => {
        this.messageForm?.reset();
        this.scrollToBottom();
      })
      .then(() => this.loadingSend = false);
  }

  onScroll(){
    if(!this.myScrollContainer) return;

    let el = this.myScrollContainer.nativeElement;
    
    //console.log(el.scrollTop, el.offsetHeight,el.scrollHeight)
    if(Math.ceil(el.scrollTop) + el.offsetHeight === el.scrollHeight){
      this.isBottomScrolled = true;
      this.isAnyNewMessages = false;
    }
    else{
      this.isBottomScrolled = false;
    }
    if(this.messageService.startFrom !== 0 && el.scrollTop === 0){
      this.isLoadingThread = true;
      this.lastScrollHeight = el.scrollHeight - el.scrollTop;
      setTimeout(() => {
        if(this.recipientId)
        this.messageService.getMoreMessages(this.recipientId, this.orderId).then(() => {
          el.scrollTop = el.scrollHeight - this.lastScrollHeight;
        });
      }, 1000)

    }
  }

  scrollToBottom(): void {
    if(this.myScrollContainer){
      this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
    }              
  }

  getMessageClass(message: Message){
    let messageClass = !message.isRead && message.recipientId === this.recipientId ? "unread" : "read";
    messageClass += message.recipientId === this.recipientId ? "sent" : "received";

    return messageClass;
  }

}
