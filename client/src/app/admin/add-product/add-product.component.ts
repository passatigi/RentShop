import { Component, OnInit } from '@angular/core';
import { AdminProduct, ProductFeature } from 'src/app/_models/adminProduct';
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
 addProductFeature: ProductFeature = {};

 categories: Category[] = [];
 features: ProductFeature[] = [];
  constructor(private helperService: AdminHelperService) { }

  ngOnInit(): void {
    this.newProduct.productFeatures = [];
    this.helperService.getCategories().subscribe(categories => {
      this.categories = categories as Category[];
    })
  }


  
  onSelectCategory(event:any){
    console.log(this.newProduct.categoryId)
    if(this.newProduct?.categoryId)
        this.helperService.getFeatures(this.newProduct.categoryId).subscribe(features => {
          this.features = features as ProductFeature[];
        })
  }

  addFeature(){
    console.log(this.addProductFeature)
    if(this.newProduct.productFeatures?.includes(this.addProductFeature) 
        || !this.addProductFeature.name 
        || !this.addProductFeature.value) return;

    this.newProduct.productFeatures?.push(this.addProductFeature)
    //this.addProductFeature = {};
  }

  deleteFeature(feature?: ProductFeature){
    if(this.newProduct.productFeatures && feature)
    this.newProduct.productFeatures = this.newProduct.productFeatures.filter(x => x !== feature)
    
  }

}
