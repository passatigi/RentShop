import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { Category } from '../_models/category';
import { CategoryService } from '../_services/category.service';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  isAdmin = true;
  id: number ;
  categories?: Category[]; 
  childCategories?:  Category[]; 
  allChildCategories: Category[] = []; 
  selectedParentCategry?: Category; 
  isCategoryChoose = false;

  constructor(private categoryService: CategoryService, private router: Router) { }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(categories => {
      console.log(categories)
      this.categories = categories as Category[];
      this.getRandomCategories(this.categories);
    })
    
  }

  getRandomCategories(categories: Category[]){
    let allChildCategories = [];
      for (const category of categories) {
        allChildCategories = allChildCategories.concat(category.childCategories);
      }
    this.allChildCategories = allChildCategories;
  }

  openCategory(id: number){
    if(this.categories){
      this.selectedParentCategry = this.categories.find(x => x.id === id);
      this.childCategories = this.selectedParentCategry?.childCategories;
    }
  }
}
