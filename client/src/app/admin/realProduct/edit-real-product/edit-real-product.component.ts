import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AdminProduct } from 'src/app/_models/adminModels/adminProduct';
import { AdminRealProduct } from 'src/app/_models/adminModels/adminRealProduct';
import { AdminHelperService } from 'src/app/_services/admin-helper.service';

@Component({
  selector: 'app-edit-real-product',
  templateUrl: './edit-real-product.component.html',
  styleUrls: ['./edit-real-product.component.css']
})
export class EditRealProductComponent implements OnInit {
  @Input() realProduct: AdminRealProduct;
  
  constructor(private helperService:AdminHelperService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  updateRealProduct(){
    this.helperService.updateRealProduct(this.realProduct).subscribe(() => {
      this.toastr.success("Successfully updated");
      
    })
  }

}
