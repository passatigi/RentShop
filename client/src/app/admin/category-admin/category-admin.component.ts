import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category } from 'src/app/_models/category';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
  selector: 'app-category-admin',
  templateUrl: './category-admin.component.html',
  styleUrls: ['./category-admin.component.css']
})
export class CategoryAdminComponent implements OnInit {
  @Input() categories?: Category[]; 
  @Input() childCategories?:  Category[]; 

  isAddCategoryCollapsed = true;
  isUpdateCategoryCollapsed = true;

  addCategoryForm?: FormGroup;
  updateCategoryForm?: FormGroup;
  validationErrors: string[] = [];

  categoryForUpdate?: Category;

  constructor(private fb: FormBuilder, private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.initializeAddForm();
    this.initializeUpdateForm();
  }

  findCategory(categories: Category[], id: number){
    return categories.find(x => x.id === id);
  }

  initializeAddForm() {
    this.addCategoryForm = this.fb.group({
      name: ['', Validators.required],
      parentCategoryId: ['', Validators.required],
      imgLink: ['', Validators.required],
    })
  }

  initializeUpdateForm() {
    this.categoryForUpdate = <Category>{ };

    this.updateCategoryForm = this.fb.group({
      name: ['', Validators.required],
      imgLink: [''],
      parentCategoryId: ['', Validators.required],
      childCategoryId: [''],
    });

    this.updateCategoryForm.controls.parentCategoryId.valueChanges.subscribe((value) => {
      if(this.categories){
        let category = this.updateForm(value, this.categories);
        this.childCategories = category?.childCategories;
      }
        
    })
    this.updateCategoryForm.controls.childCategoryId.valueChanges.subscribe((value) => {
      if(this.childCategories){
        this.updateForm(value, this.childCategories);
      }
    })
  }

  updateForm(id: number, categoriesToSearch: Category[]){
    if(this.categoryForUpdate && this.updateCategoryForm){
        this.categoryForUpdate.id = id;
        let category = this.findCategory(categoriesToSearch, id);
        this.updateCategoryForm.controls.name.setValue(category?.name);
        this.updateCategoryForm.controls.imgLink.setValue(category?.imgLink);
        this.categoryForUpdate.name = category?.name;
        this.categoryForUpdate.imgLink = category?.imgLink;
        return category;
    }
    return undefined;
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
  

}
