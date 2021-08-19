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
  isCategoryChoose = false;

  constructor(private categoryService: CategoryService, private router: Router) { }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories as Category[];
    })
    
  }

  openCategory(id: number){
    if(this.categories){
      this.childCategories = this.categories.find(x => x.id === id)?.childCategories;
    }
  }
}
