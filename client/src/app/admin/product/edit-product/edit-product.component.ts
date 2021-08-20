import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AdminProduct } from 'src/app/_models/adminModels/adminProduct';
import { AdminProductFeature } from 'src/app/_models/adminModels/adminProductFeature';
import { ProductFeature } from 'src/app/_models/productFeature';
import { AdminHelperService } from 'src/app/_services/admin-helper.service';
import { ProductsService } from 'src/app/_services/products.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {
  product?: AdminProduct;
  features: AdminProductFeature[] = [];
  photoLink?: string;

  

  constructor(
    private route: ActivatedRoute, 
    private productService: ProductsService, 
    private helperService: AdminHelperService,
    private toastr: ToastrService
    ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.productService.getProductById(Number.parseInt(params.id)).subscribe((product) => {
        if(product){
          this.product = <AdminProduct>product;
          
          console.log(this.product)
          this.getFeatures();
          this.setPhoto();
        }
        else{
          console.error("aboba");
        }
      })
    })
  }

  getFeatures(){
    this.helperService.getFeatures(this.product.categoryId).subscribe((features) => {
      this.features = <ProductFeature[]>features;
      this.crossFeatures();
    })
  }

  crossFeatures(){
    this.features.forEach(f => {
      this.product.productFeatures.forEach(pf => {
        if(f.featureId === pf.featureId){
          this.features[this.features.indexOf(f)] = pf;
        }
      });
    });
  }

  updateProduct(){
    this.helperService.updateProduct(this.product).subscribe(() => {
      this.toastr.success("Successfully updated");
    })
  }

  setPhoto(){
    if(this.product?.productImgsLinks && this.product?.productImgsLinks.length > 0)
      this.photoLink = this.product.productImgsLinks[0];
  }
}
