import { Component, OnInit } from '@angular/core';
import { AdminProduct } from 'src/app/_models/adminProduct';
import { Category } from 'src/app/_models/category';
import { AdminHelperService } from 'src/app/_services/admin-helper.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
toString(object:any){
  return JSON.stringify(object)
}

 newProduct: AdminProduct = {};

 categories: Category[] = [];
  constructor(private helperService: AdminHelperService) { }

  ngOnInit(): void {
    this.helperService.getCategories().subscribe(categories => {
      this.categories = categories as Category[];
    })
  }

  

}
