import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { Product } from 'src/app/_models/product';
import { ProductsService } from 'src/app/_services/products.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  id: number; 
  products: Product[] = [];

  constructor(private productService: ProductsService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe( params => {
      this.id = params['categoryid'];
    })
    this.loadProducts(this.id);
  }

  loadProducts(id: number){
    this.productService.getProductsByCaregoryId(id).subscribe(products => {
      this.products = products;
    })
  }
}
