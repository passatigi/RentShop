import { Input } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { CartItem } from 'src/app/_models/cartItem';
import { Product } from 'src/app/_models/product';
import { RealProduct } from 'src/app/_models/realProduct';
import { SelectProductService } from 'src/app/_services/select-product.service';

@Component({
  selector: 'app-real-product-card',
  templateUrl: './real-product-card.component.html',
  styleUrls: ['./real-product-card.component.css']
})
export class RealProductCardComponent implements OnInit {

  @Input() realProduct: RealProduct;
  @Input() product: Product;

  id: number;
  item: CartItem;

  constructor( private selectProductService: SelectProductService) { }

  ngOnInit(): void {
    this.loadItem();
  }

  loadItem(){
    const item: CartItem = {
      id: this.realProduct.id,
      serialNumber: this.realProduct.serialNumber,
      rentPrice: this.realProduct.rentPrice,
      condition: this.realProduct.condition,
      productId: this.product.id,
      name: this.product.name,
      vendor: this.product.vendor,
      productImgLink: this.product.productImgsLinks[0]
    };
    this.item = item;
  }

  add(realProduct: RealProduct) {
    this.selectProductService.add(this.item);
  }
}
