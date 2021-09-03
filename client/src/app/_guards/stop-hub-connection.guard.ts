import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MessageService } from '../_services/message.service';

@Injectable({
  providedIn: 'root'
})
export class StopHubConnectionGuard implements CanDeactivate<unknown> {
  constructor(private messageService: MessageService) { }

  canDeactivate():  boolean {
    //console.log("stpoppp!!")
    this.messageService.stopHubConnection();
    return true;
  }
  
}
