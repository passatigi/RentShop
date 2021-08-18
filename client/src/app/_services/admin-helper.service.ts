import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AdminProduct } from '../_models/adminProduct';
import { NewFeature } from '../_models/newFeature';
import { NewProduct } from '../_models/newProduct';

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

  addProduct(newProduct: AdminProduct){
    return this.http.post(this.baseUrl + 'adminhelper/products', newProduct);
  }

}
