import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AdminProduct } from '../_models/adminModels/adminProduct';
import { AdminProductFeature } from '../_models/adminModels/adminProductFeature';
import { AdminRealProduct } from '../_models/adminModels/adminRealProduct';


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

  addFeature(newFeature: AdminProductFeature){
    return this.http.post(this.baseUrl + 'adminhelper/features', newFeature);
  }

  addProduct(newProduct: AdminProduct){
    return this.http.post(this.baseUrl + 'adminhelper/products', newProduct);
  }

  updateProduct(product: AdminProduct){
    return this.http.put(this.baseUrl + 'adminhelper/products', product);
  }

  addRealProduct(realProduct:AdminRealProduct){
    return this.http.post(this.baseUrl + "adminhelper/real-products", realProduct);
  }
  
  updateRealProduct(realProduct:AdminRealProduct){
    return this.http.put(this.baseUrl + "adminhelper/real-products", realProduct);
  }

  getDetailedPhotos(productId: number){
    return this.http.get(this.baseUrl + "photo/photos/" + productId);
  }
  deletePhoto(photoid: number){
    return this.http.delete(this.baseUrl + "photo/delete-photo/" + photoid);

  }

}
