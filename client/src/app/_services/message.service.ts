import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Group } from '../_models/group';
import { Message } from '../_models/message';
import { Order } from '../_models/order';
import { User } from '../_models/user';


@Injectable({
  providedIn: 'root'
})
export class MessageService {
  startFrom = 0;
  receivedMessagesCount = 0;
  messagesCount = 0;

  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;

  private hubConnection?: HubConnection;
  messageThreadSourse? = new BehaviorSubject<Message[]>([])
  messageThread$ = this.messageThreadSourse?.asObservable();

  constructor(private http: HttpClient) { }

  getMessageThreads(){
    return this.http.get<Message[]>(this.baseUrl + 'message/list');
  }

  getMessageThreadInfo(recipientId: number, orderId: number){
    return this.http.get<{order: Order, recipient: User}>(this.baseUrl + `message/thread-info?recipientId=${recipientId}&orderId=${orderId}`)
  } 


  createHubConnection(user: User, recipientId: number, orderId: number) {
    this.startFrom = 0;
    this.messagesCount = 0;
    this.receivedMessagesCount = 0;
    console.log("createHubConnection")

    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'message?user=' + recipientId + '&order=' + orderId, {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build()

    this.hubConnection.start()
      .catch(error => console.log(error)).finally(() => {
        
      });


    this.hubConnection.on('ReceiveMessageThread', (messages: Array<any>) => {
      
      if(messages.length > 0){
        this.startFrom = messages[0].id;
        this.messageThreadSourse?.next(messages);
        this.messagesCount += messages.length;
      }
      else{

      }
      
      
    })

    this.hubConnection.on('MoreMessagesThreadReceived', (receivedMessages: Array<any>) => {

      this.messageThread$?.pipe(take(1)).subscribe(messages  => {
        console.log(receivedMessages)
        if(receivedMessages.length > 0){
          this.messageThreadSourse?.next([...receivedMessages, ...messages]);
          this.startFrom = receivedMessages[0].id;
          this.messagesCount += messages.length;
        }
        else{
          this.startFrom = 0;
        }
        this.receivedMessagesCount  = receivedMessages.length;
      })
    })

    this.hubConnection.on('NewMessage', message => {
      this.messageThread$?.pipe(take(1)).subscribe(messages  => {
        this.messageThreadSourse?.next([...messages, message]);
      })
    })

    this.hubConnection.on('UpdatedGroup', (group: Group) => {
      if(group.connections.some(x => x.userId === recipientId)) {
        this.messageThread$?.pipe(take(1)).subscribe(messages => {
          messages.forEach(message => {
              message.isRead = true;
          })
          this.messageThreadSourse?.next([...messages])
        })
      }
    })
  }

  stopHubConnection() {
    if (this.hubConnection) {
      this.messageThreadSourse?.next([]);
      this.hubConnection.stop();
    }
  }

  async sendMessage(recipientId: number, orderId:number , content: string){
    console.log(recipientId, orderId, content)
    return  this.hubConnection?.invoke(
      'SendMessage', 
      {
        recipientId, orderId, content
      })
      .catch(error => console.log(error));
  }

  async getMoreMessages(recipientId: number, orderId: number){
    if(this.startFrom !== 0)
      return  this.hubConnection?.invoke(
          'GetMoreMessages', 
          {
            recipientId, orderId, startFrom: this.startFrom 
          })
          .catch(error => console.log(error));
  }

}
