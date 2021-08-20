import { Component, Input, OnInit } from '@angular/core';
import { CartItem } from 'src/app/_models/cartItem';
import { SelectProductService } from 'src/app/_services/select-product.service';

@Component({
  selector: 'app-card-item',
  templateUrl: './card-item.component.html',
  styleUrls: ['./card-item.component.css']
})
export class CardItemComponent implements OnInit {


  public show: boolean = true;

  @Input() item: CartItem;

  constructor(private selectProductService: SelectProductService) { }

  ngOnInit(): void {
  }

  remove(){
    this.selectProductService.remove(this.item);
    this.show = false;
  }




}
