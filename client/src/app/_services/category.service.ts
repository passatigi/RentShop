import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { CreateCategory } from '../_models/createCategory';
import { Category } from '../_models/category';


@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  getCategories(){
    return this.http.get(this.baseUrl + 'category');
  }

  addCategory(createCategory: CreateCategory){
    return this.http.post(this.baseUrl + 'category', createCategory);
  }

  updateCategory(category: Category){
    return this.http.put(this.baseUrl + 'category', category );
  }
}
