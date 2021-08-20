import { Component, Input, OnInit } from '@angular/core';
import { AdminProduct } from 'src/app/_models/adminModels/adminProduct';
import { AdminRealProduct } from 'src/app/_models/adminModels/adminRealProduct';
import { RealProduct } from 'src/app/_models/realProduct';

@Component({
  selector: 'app-edit-real-products',
  templateUrl: './edit-real-products.component.html',
  styleUrls: ['./edit-real-products.component.css']
})
export class EditRealProductsComponent implements OnInit {
  @Input() product: AdminProduct;

  realProductToEdit?: AdminRealProduct;
  constructor() { }

  ngOnInit(): void {
  }

  updateRealProduct(realProduct: AdminRealProduct){
    this.realProductToEdit = realProduct;
  }

}
