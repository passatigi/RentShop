import { HttpClient, HttpParams } from '@angular/common/http';
import { identifierModuleUrl } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Product } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getProductsByCaregoryId(id: number){
    let httpParams = new HttpParams().set('CategoryId', id);
    return this.http.get<Product[]>(this.baseUrl + 'Product', {params: httpParams});
  }

  getProductById(id: number){
    return this.http.get<Product>(this.baseUrl + 'Product/detail/' + id)
  }
}
