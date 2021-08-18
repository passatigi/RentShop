import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AdminProduct, ProductFeature } from 'src/app/_models/adminProduct';
import { Category } from 'src/app/_models/category';
import { NewFeature } from 'src/app/_models/newFeature';
import { AdminHelperService } from 'src/app/_services/admin-helper.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  isAddNewFeatureCollapsed =  true;
toString(object:any){
  return JSON.stringify(object)
}

 newProduct: AdminProduct = {};
 addProductFeature: ProductFeature = {};

 categories: Category[] = [];
 features: ProductFeature[] = [];

 selectedCategory?: Category;
 newCategoryFeature: NewFeature = {};

  constructor(private helperService: AdminHelperService, private toastr: ToastrService) { }

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
          this.features = features as ProductFeature[];
        })
  }

  addFeatureToProduct(){
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

  addNewFeature(){
    console.log(this.selectedCategory)
    this.newCategoryFeature.categoryId = this.selectedCategory?.id;

    if(this.isFull(this.newCategoryFeature))
    this.helperService.addFeature(this.newCategoryFeature).subscribe((feature) => {
      this.newCategoryFeature = {};
      this.toastr.success("Successfully added")
      this.features.push(<ProductFeature>feature)
    });
    else{
      this.toastr.info("New feature should be filled")
    }
  }

  addNewProduct(){
    console.log(this.newProduct)
    this.helperService.addProduct(this.newProduct).subscribe((product) => {
      console.log(product)
    })
  }

  isFull(object: any){
    return !Object.values(this.newCategoryFeature).every(o => o === undefined);
  }

}
