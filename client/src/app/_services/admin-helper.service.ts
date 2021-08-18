import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NewFeature } from '../_models/newFeature';

@Injectable({
  providedIn: 'root'
})
export class AdminHelperService {
  baseUrl = environment.apiUrl

  constructor(private http: HttpClient) { }

  getCategories(){
    return this.http.get(this.baseUrl + 'adminhelper/categories');
  }

  getFeatures(categoryId: number){
    return this.http.get(this.baseUrl + 'adminhelper/features/' + categoryId);
  }

  addFeature(newFeature: NewFeature){
    return this.http.post(this.baseUrl + 'adminhelper/features', newFeature);
  }

}
