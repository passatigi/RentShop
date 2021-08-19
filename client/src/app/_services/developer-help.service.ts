import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DeveloperHelpService {

  constructor() { }

  toString(object:any){
    return JSON.stringify(object)
  }
}
