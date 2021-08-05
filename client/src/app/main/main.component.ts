import { Component, OnInit } from '@angular/core';
import { Category } from '../_models/category';
import { CategoryService } from '../_services/category.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  isCollapsed = true;
  categories?: Category[]; 
  childCategories?:  Category[]; 

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories as Category[];
      console.log(this.categories)
    })
  }

  openCategory(id: number){
    if(this.categories){
      this.childCategories = this.categories.find(x => x.id === id)?.childCategories;
    }
  }

  addCategory(){

  }

}
