import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category } from '../_models/category';
import { CategoryService } from '../_services/category.service';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  isAdmin = true;

  categories?: Category[]; 
  childCategories?:  Category[]; 

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories as Category[];
    })
    
  }

  openCategory(id: number){
    if(this.categories){
      this.childCategories = this.categories.find(x => x.id === id)?.childCategories;
      console.log("openCategory " + id)
    }
  }

 



}
