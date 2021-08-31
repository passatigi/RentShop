import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Observable } from 'rxjs';
import { CartItem } from 'src/app/_models/cartItem';
import { Product } from 'src/app/_models/product';
import { RealProduct } from 'src/app/_models/realProduct';
import { ProductsService } from 'src/app/_services/products.service';
import { SelectProductService } from 'src/app/_services/select-product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {

  @Input() realProduct: RealProduct;

  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  id: number;
  product: Product;
  items: CartItem[];
  item: CartItem;



  constructor(private productService: ProductsService, 
    private route: ActivatedRoute, 
    private selectProductService: SelectProductService) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe( params => {
      this.id = params['id'];
    })

    this.loadProduct(this.id);

    this.galleryOptions = [
      {
        width: '100%',
        height: '600px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ]
  }

  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    for (const photoUrl of this.product.productImgsLinks){
      imageUrls.push({
        small: photoUrl,
        medium: photoUrl,
        big: photoUrl
      })
    }
    return imageUrls;
  }

  loadProduct(id: number){
    this.productService.getProductById(id).subscribe(product =>{
      this.product = product;
      this.galleryImages = this.getImages();
    });
  }


  add(realProduct: RealProduct) {

    const item: CartItem = {
      id: realProduct.id,
      serialNumber: realProduct.serialNumber,
      rentPrice: realProduct.rentPrice,
      condition: realProduct.condition,
      productId: this.product.id,
      name: this.product.name,
      vendor: this.product.vendor,
      productImgLink: this.product.productImgsLinks[0]
    };

    this.selectProductService.add(item);
  }

  // get count() {
  //   return this.selectProductService.items.filter(i => i.id == this.realProduct.id).length;
  // }


  // remove() {
  //   if (this.count > 0) {
  //     this.selectProductService.remove(this.realProduct)
  //   }
  // }
}
