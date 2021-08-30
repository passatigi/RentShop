import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { ToastrService } from 'ngx-toastr';
import { AdminPhoto } from 'src/app/_models/adminModels/adminPhoto';
import { AdminProduct } from 'src/app/_models/adminModels/adminProduct';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-upload',
  templateUrl: './photo-upload.component.html',
  styleUrls: ['./photo-upload.component.css']
})
export class PhotoUploadComponent implements OnInit {

  @Input() uploadPath: string;
  @Input() photos: AdminPhoto[];
 
  uploader?: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  
  constructor(private toastr:ToastrService) { }

  ngOnInit(): void {
    this.initializeUploader();

  }

  fileOverBase(e: any){
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + this.uploadPath,
      //authToken: 'Bearer ' + this.user?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024*1024,
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if(response) {
        console.log(response)
        this.toastr.success("Uploaded successfully")
        this.photos.push(JSON.parse(response)); 
        // const photo: Photo = JSON.parse(response);
        // this.member?.photos.push(photo);
        // if(photo.isMain &&  this.user && this.member) {
        //   this.user.photoUrl= photo.url;
        //   this.member.photoUrl = photo.url;
        //   this.accountService.setCurrentUser(this.user);
        // }
      }
    }
  }
}
