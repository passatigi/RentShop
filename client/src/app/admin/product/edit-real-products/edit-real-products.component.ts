import { Component, Input, OnInit } from '@angular/core';
import { AdminProduct } from 'src/app/_models/adminModels/adminProduct';

@Component({
  selector: 'app-edit-real-products',
  templateUrl: './edit-real-products.component.html',
  styleUrls: ['./edit-real-products.component.css']
})
export class EditRealProductsComponent implements OnInit {
  @Input() product: AdminProduct;
  constructor() { }

  ngOnInit(): void {
  }

}
