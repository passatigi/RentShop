import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AdminPhoto } from 'src/app/_models/adminModels/adminPhoto';
import { AdminProduct } from 'src/app/_models/adminModels/adminProduct';
import { AdminHelperService } from 'src/app/_services/admin-helper.service';

@Component({
  selector: 'app-product-photos',
  templateUrl: './product-photos.component.html',
  styleUrls: ['./product-photos.component.css']
})
export class ProductPhotosComponent implements OnInit {
  @Input() product: AdminProduct;
  uploadPath?: string;

  photos: AdminPhoto[] = [];

  constructor(private helpService: AdminHelperService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.uploadPath = 'photo/add-photo/' + this.product.id;
    this.helpService.getDetailedPhotos(this.product.id).subscribe((photos) => {
      this.photos = <AdminPhoto[]>photos;
    });
  }

  deletePhoto(id: number){
    this.helpService.deletePhoto(id).subscribe(() => {
      this.toastr.success("Successfully deleted");
      this.photos = this.photos.filter(p => p.id !== id);
    })
  }

}
