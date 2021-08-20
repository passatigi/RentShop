import { Injectable } from '@angular/core';
import { CartItem } from '../_models/cartItem';
import { RealProduct } from '../_models/realProduct';

@Injectable({
  providedIn: 'root'
})
export class SelectProductService {

  private _items: CartItem[] ;

  constructor() {
    this._items = JSON.parse(localStorage.getItem('items') ||'[]');
  }
  
  remove(item: CartItem) {
    const index = this._items.indexOf(item);
    this._items.splice(index,1);
    this.syncItems();
  }

  add(item: CartItem) { 
    this._items.push(item);
    this.syncItems();
  }

  get length() : number{
    return this._items.length
  }

  get items(){
    return this._items.slice(0)
  }

  syncItems(){
    localStorage.setItem('items',JSON.stringify(this._items)); // sync the data
  }
}
