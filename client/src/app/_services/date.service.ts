import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { RealProductSchedule } from '../_models/realproductschedule';

@Injectable({
  providedIn: 'root'
})
export class DateService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getProductScheduleById(realProductid: number){
    return this.http.get<RealProductSchedule>(this.baseUrl + 'Product/shedule/' + realProductid)
  }
}
