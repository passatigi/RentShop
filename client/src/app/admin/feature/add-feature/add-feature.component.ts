import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AdminProductFeature } from 'src/app/_models/adminModels/adminProductFeature';

import { Category } from 'src/app/_models/category';
import { AdminHelperService } from 'src/app/_services/admin-helper.service';
import { DeveloperHelpService } from 'src/app/_services/developer-help.service';

@Component({
  selector: 'app-add-feature',
  templateUrl: './add-feature.component.html',
  styleUrls: ['./add-feature.component.css']
})
export class AddFeatureComponent implements OnInit {
  @Input() category?: Category;
  @Input() features: AdminProductFeature[] = [];

  newCategoryFeature: AdminProductFeature = {};

  constructor(private helperService: AdminHelperService, private toastr: ToastrService,
    
    public devHelp: DeveloperHelpService) { }

  ngOnInit(): void {
  }

  addNewFeature(){
    console.log(this.category)

    this.newCategoryFeature.categoryId = this.category?.id;
    console.log(this.newCategoryFeature)
    if(!this.newCategoryFeature.explanation) this.newCategoryFeature.explanation = ''

    if(this.isFull(this.newCategoryFeature))
      this.helperService.addFeature(this.newCategoryFeature).subscribe((feature) => {
        this.newCategoryFeature = {};
        this.features.push(<AdminProductFeature>feature)
        this.toastr.success("Successfully added")
      });
    else{
      this.toastr.info("New feature should be filled")
    }
  }

  isFull(object: any){
    return  !Object.values(object).includes(undefined);
  }

}
