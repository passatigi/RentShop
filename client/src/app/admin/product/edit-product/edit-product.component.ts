import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminProduct } from 'src/app/_models/adminModels/adminProduct';
import { ProductFeature } from 'src/app/_models/productFeature';
import { ProductsService } from 'src/app/_services/products.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {
  product?: AdminProduct;
  features: ProductFeature[] = [];

  constructor(private route: ActivatedRoute, private productService: ProductsService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.productService.getProductById(Number.parseInt(params.id)).subscribe((product) => {
        this.product = <AdminProduct>product;
        console.log(this.product);
      })
    })
  }

}
