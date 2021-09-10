import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Category } from '../_models/category';
import { Product } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getSearchResult(searchString: string){
    return this.http.get<{ categories: Category[], products: Product[]}>(this.baseUrl + 'search/' + searchString);
  }
}
