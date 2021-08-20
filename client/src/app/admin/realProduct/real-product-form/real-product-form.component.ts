import { Component, Input, OnInit } from '@angular/core';
import { AdminRealProduct } from 'src/app/_models/adminModels/adminRealProduct';


@Component({
  selector: 'app-real-product-form',
  templateUrl: './real-product-form.component.html',
  styleUrls: ['./real-product-form.component.css']
})
export class RealProductFormComponent implements OnInit {
  @Input() realProduct: AdminRealProduct;

  constructor() { }

  ngOnInit(): void {
  }

}
