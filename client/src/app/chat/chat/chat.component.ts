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
export class ChatComponent implements OnInit, AfterViewChecked  {
  user?: User;
  
  @ViewChild('messageForm') messageForm?: NgForm;

  @Input() recipientId?: number;
  @Input() orderId?: number;


  @ViewChild('scrollMe') private myScrollContainer?: ElementRef;
  @ViewChildren('messagesNgFor') messagesNgFor?: QueryList<any>;

  isSmallScreen = false;
  isFirstScroll = true;
  @HostListener('window:resize', ['$event']) resizeHandler(event: any) { 
    this.isSmallScreen = event.target.innerWidth < 1000;
  }

  
  messageContent?: string;
  loading = false;
  height = 500;

  lastMessageId = 0;

  


  isLoadingThread = false;

  constructor(public messageService: MessageService,
              private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.messageService.createHubConnection(this.user, this.recipientId, this.orderId);
    });

    this.messageService.messageThread$.pipe(distinctUntilChanged())
        .subscribe(messages => {
          console.log(messages)
          setTimeout(() => {
            this.onMessagesChange(messages);
          }, 0)
        });
  }
  onMessagesChange(messages: Message[]){
    if(messages){
      if(messages[messages.length - 1].id > this.lastMessageId){
        this.lastMessageId = messages[messages.length - 1].id;
        this.scrollToBottom();
      }
    }
  }

  ngAfterViewChecked(){
    //console.log("ngAfterViewChecked")
    this.isSmallScreen = (window.innerWidth) < 1000;
    this.isLoadingThread = false;
  }


  sendMessage(){
    this.loading = true;
    if(this.recipientId && this.orderId && this.messageContent)
      this.messageService.sendMessage(this.recipientId, this.orderId, this.messageContent).then(() => {
        this.messageForm?.reset();
        this.scrollToBottom();
      }).then(() => this.loading = false);
  }



  lastScrollHeight = 0;

  onScroll(){
    if(this.messageService.startFrom !== 0 && 
      this.myScrollContainer && 
      this.myScrollContainer.nativeElement.scrollTop === 0
      ){
      this.isLoadingThread = true;
      this.lastScrollHeight = this.myScrollContainer.nativeElement.scrollHeight - this.myScrollContainer.nativeElement.scrollTop;
      setTimeout(() => {
        if(this.recipientId)
        this.messageService.getMoreMessages(this.recipientId, this.orderId).then(() => {
          if(this.myScrollContainer){
            this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight - this.lastScrollHeight;
          }
        });
      }, 1000)

    }
  }
    autoBottomScroll(){
      if(this.isFirstScroll){
        this.isFirstScroll = false;
        this.scrollToBottom();
      }
    }

    scrollToBottom(): void {
        try {
          if(this.myScrollContainer){
            this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
          }
        } catch(err) { }                 
    }

    getMessageClass(message: Message){
      let messageClass = !message.isRead && message.recipientId === this.recipientId ? "unread" : "read";
      messageClass += message.recipientId === this.recipientId ? "sent" : "received";

      return messageClass;
    }

}
