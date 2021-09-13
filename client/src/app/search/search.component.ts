import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category } from '../_models/category';
import { Product } from '../_models/product';
import { SearchService } from '../_services/search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  searchString?: string;
  categories: Category[];
  products: Product[];
  resultLoaded = true;
  

  constructor(private route: ActivatedRoute,
    private searchService: SearchService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe( params => {
      this.searchString = params['string'];
      this.search(this.searchString);
    })
  }
  search(searchString: string){
    this.searchService.getSearchResult(searchString).subscribe((result) => {
      this.products = result.products;
      this.categories = result.categories;
      this.resultLoaded = true;
      console.log(result)
    })
  }

}
