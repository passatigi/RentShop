import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AdminProduct } from 'src/app/_models/adminModels/adminProduct';
import { AdminProductFeature } from 'src/app/_models/adminModels/adminProductFeature';

import { Category } from 'src/app/_models/category';
import { AdminHelperService } from 'src/app/_services/admin-helper.service';
import { DeveloperHelpService } from 'src/app/_services/developer-help.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  isAddNewFeatureCollapsed =  true;
  isSuccessfullyAdded = false;
 
  newProduct: AdminProduct = {};
  categories: Category[] = [];
  features: AdminProductFeature[] = [];
  selectedCategory?: Category;

  constructor(private helperService: AdminHelperService, private toastr: ToastrService, 
    
    public devHelp: DeveloperHelpService) { }

  ngOnInit(): void {
    this.newProduct.productFeatures = [];
    this.helperService.getCategories().subscribe(categories => {
      this.categories = categories as Category[];
    })
  }

  onSelectCategory(event:any){
    console.log(this.newProduct.categoryId)
    this.selectedCategory = this.categories.find(x => x.id == this.newProduct.categoryId);
    if(this.newProduct?.categoryId)
        this.helperService.getFeatures(this.newProduct.categoryId).subscribe(features => {
          this.features = features as AdminProductFeature[];
        })
  }

  addNewProduct(){
    console.log(this.newProduct)
    this.helperService.addProduct(this.newProduct).subscribe((id) => {
      this.isSuccessfullyAdded = true;
      if(this.newProduct)
        this.newProduct.id = <number>id;
      console.log(id)
      this.toastr.success("Product successfully added!")
    })
  }

}
