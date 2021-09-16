import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { Category } from 'src/app/_models/category';
import { Product } from 'src/app/_models/product';
import { CategoryService } from 'src/app/_services/category.service';
import { ProductsService } from 'src/app/_services/products.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit { 
  category: Category;
  @Input() products: Product[] = [];

  constructor(private productService: ProductsService,
  private categoryService: CategoryService,
     private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe( params => {
      let id = parseInt(params['categoryid']);
      this.getCategory(id);
    })
    
  }
  getCategory(categoryId: number){
    this.categoryService.getCategory(categoryId).subscribe((category) => {
      this.category = category;
      this.loadProducts(category.id);
    }) 
  }

  loadProducts(categoryId: number){
    this.productService.getProductsByCaregoryId(categoryId).subscribe(products => {
      this.products = products;
      console.log(products)
    })
    
  }
}
