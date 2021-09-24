import { Route } from '@angular/compiler/src/core';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
    
    public devHelp: DeveloperHelpService, private router: Router) { }

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
    if(!this.newProduct.categoryId){
      this.toastr.info("Set product category")
      return;
    }
    if(!this.newProduct.name){
      this.toastr.info("Set product name")
      return;
    }
    console.log(this.newProduct)
    this.helperService.addProduct(this.newProduct).subscribe((id) => {
      this.isSuccessfullyAdded = true;
      this.toastr.success("Product successfully added!");
      this.router.navigateByUrl('/admin/edit-product?id=' + id);
    })
  }

}
