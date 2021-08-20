import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AdminProduct } from 'src/app/_models/adminModels/adminProduct';
import { AdminRealProduct } from 'src/app/_models/adminModels/adminRealProduct';
import { AdminHelperService } from 'src/app/_services/admin-helper.service';


@Component({
  selector: 'app-add-real-product',
  templateUrl: './add-real-product.component.html',
  styleUrls: ['./add-real-product.component.css']
})
export class AddRealProductComponent implements OnInit {
  @Input() product: AdminProduct;

  newRealProduct: AdminRealProduct = {};
  constructor(private helpService: AdminHelperService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.newRealProduct.productId = this.product.id;
  }

  addRealProduct(){
    console.log(this.newRealProduct);
    if(this.newRealProduct.serialNumber === undefined || this.newRealProduct.serialNumber === "")
      this.toastr.error("Serial number should be filled")
    else if(this.newRealProduct.rentPrice === undefined || this.newRealProduct.rentPrice === null)
      this.toastr.error("Rent price should be filled")
    else
      this.helpService.addRealProduct(this.newRealProduct).subscribe((id) => {
        this.newRealProduct.id = <number>id;
        this.product.realProducts.push(this.newRealProduct);
        this.newRealProduct = {};
        this.newRealProduct.productId = this.product.id;
      });
  }

}
