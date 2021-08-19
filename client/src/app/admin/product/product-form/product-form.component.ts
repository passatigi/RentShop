import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AdminProduct } from 'src/app/_models/adminModels/adminProduct';
import { AdminProductFeature } from 'src/app/_models/adminModels/adminProductFeature';
import { DeveloperHelpService } from 'src/app/_services/developer-help.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {
  @Input() product?: AdminProduct;
  @Input() features: AdminProductFeature[] = [];

  addProductFeature: AdminProductFeature = {};

  constructor( 
    private toastr: ToastrService,
    public devHelp: DeveloperHelpService
    ) { }

  ngOnInit(): void {
  }

  addFeatureToProduct(){
    if(this.product){
      if(this.product.productFeatures?.includes(this.addProductFeature))
        this.toastr.error("Feature already added");
      else if(!this.addProductFeature.name)
        this.toastr.error("Please, choose feature name");
      else if(!this.addProductFeature.value)
        this.toastr.error("Please, write value");
      else
        this.product.productFeatures?.push(this.addProductFeature)
    }
  }

  deleteFeature(feature?: AdminProductFeature){
    if(this.product?.productFeatures && feature)
      this.product.productFeatures = this.product.productFeatures.filter(x => x !== feature)
  }

}
