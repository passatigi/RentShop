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
  isAddCategoryCollapsed = true;
  isUpdateCategoryCollapsed = true;
  categories?: Category[]; 
  childCategories?:  Category[]; 

  addCategoryForm?: FormGroup;
  updateCategoryForm?: FormGroup;
  validationErrors: string[] = [];
  categoriesNames?: string[] = [];


  constructor(private categoryService: CategoryService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories as Category[];
      //console.log(this.categories)
      this.initializeAddForm();
      this.initializeUpdateForm();
    })
    
  }

  openCategory(id: number){
    if(this.categories){
      this.childCategories = this.categories.find(x => x.id === id)?.childCategories;
      console.log("openCategory " + id)
    }
  }

  findCategory(categories: Category[], id: number){
    return categories.find(x => x.id === id);
  }


  initializeAddForm() {
    this.categoriesNames = this.categories?.map(c => c.name);
    console.log(this.categoriesNames);
    this.addCategoryForm = this.fb.group({
      name: ['', Validators.required],
      parentCategoryId: ['', Validators.required],
      imgLink: ['', Validators.required],
    })
  }
  initializeUpdateForm() {
    this.categoryForUpdate = <Category>{ };
    this.categoriesNames = this.categories?.map(c => c.name);
    console.log(this.categoriesNames);
    this.updateCategoryForm = this.fb.group({
      name: ['', Validators.required],
      imgLink: [''],
      parentCategoryId: ['', Validators.required],
      childCategoryId: [''],
    })
    this.updateCategoryForm.controls.parentCategoryId.valueChanges.subscribe((value) => {
      if(this.categoryForUpdate && this.updateCategoryForm && this.categories){
        console.log(value)
        this.categoryForUpdate.id = value;
        let category = this.findCategory(this.categories, value);
        console.log(category);
        this.childCategories = category?.childCategories;
        this.updateCategoryForm.controls.name.setValue(category?.name);
        this.updateCategoryForm.controls.imgLink.setValue(category?.imgLink);
      }
        
    })
    this.updateCategoryForm.controls.childCategoryId.valueChanges.subscribe((value) => {
      if(this.categoryForUpdate &&  this.updateCategoryForm && this.childCategories){
        this.categoryForUpdate.id = value;
        let category = this.findCategory(this.childCategories, value);
        console.log(category);
        
        this.updateCategoryForm.controls.name.setValue(category?.name);
        this.updateCategoryForm.controls.imgLink.setValue(category?.imgLink);
      }
        


    })
  }

  addCategory(){
    this.categoryService.addCategory(this.addCategoryForm?.value)
        .subscribe(response => {
          let category = response as Category;
          console.log(category);
          this.isAddCategoryCollapsed = true;

          let parentId = this.addCategoryForm?.value['parentCategoryId'] as number;
          this.categories?.find(x => x.id === parentId)?.childCategories.push(category);

          this.addCategoryForm?.reset()
        })
    console.log("ff")
  }

  updateCategory(){
    if(this.updateCategoryForm && this.categoryForUpdate){
      this.categoryForUpdate.name = this.updateCategoryForm.controls.name.value;
      this.categoryForUpdate.imgLink = this.updateCategoryForm.controls.imgLink.value;
      if(this.categoryForUpdate.imgLink === '') this.categoryForUpdate.imgLink = undefined;
      console.log(this.categoryForUpdate);
      this.categoryService.updateCategory(this.categoryForUpdate).subscribe(() => {

        this.updateCategoryForm?.reset();
        window.location.reload();
      })
    }
   
  }
  categoryForUpdate?: Category;



}
