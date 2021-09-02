import { ChangeDetectionStrategy, Component, ElementRef, HostListener, Input, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';


@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  
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

  


  isLoadingThread = false;

  constructor(public messageService: MessageService) { }

  ngOnInit(): void {

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
