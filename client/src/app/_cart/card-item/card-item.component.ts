import { Component, Input, OnInit, Self} from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, NgControl, ValidatorFn, Validators } from '@angular/forms';
import { CartItem } from 'src/app/_models/cartItem';
import { CreateOrder } from 'src/app/_models/createOrder';
import { OrderProduct } from 'src/app/_models/orderProduct';
import { Segment } from 'src/app/_models/segment';
import { DateService } from 'src/app/_services/date.service';
import { OrderService } from 'src/app/_services/order.service';
import { SelectProductService } from 'src/app/_services/select-product.service';

@Component({
  selector: 'app-card-item',
  templateUrl: './card-item.component.html',
  styleUrls: ['./card-item.component.css']
})
export class CardItemComponent implements OnInit {

  currentDate = new Date();
  disabledDates: Date[] = [];
  minDate: Date;
  tomorrow: Date;
  public show: boolean = true;
  days: Segment[];
  myForm: FormGroup;
  isCollapsed = true;
  error: string;

  @Input() item: CartItem;

  constructor(private selectProductService: SelectProductService, 
    private dateService: DateService,
    private formBuilder: FormBuilder,
    private orderService: OrderService
    ) {}

  ngOnInit(): void {
    this.dateService.getProductScheduleById(this.item.id).subscribe( result => {
      this.days = result.segments;
      this.minDate = this.addDays(this.currentDate, 1);
      this.addDisabledDays();
    });

    this.myForm = this.formBuilder.group({
      requiredDate: [null, [Validators.required, this.matchValues("requiredReturnDate")]],
      requiredReturnDate: [null, [Validators.required, this.matchValues("requiredDate")]],
      shippedAdress: ['', Validators.required],
      returnAdress: ['', Validators.required],
      comments: ['', ]
    });
  }

  // add subscribe
  addOrder(){
    
    let user = JSON.parse(localStorage.getItem('user'));
    let orderProducts: OrderProduct[] = [];
    let orderProduct: OrderProduct = {
      realProductId: this.item.id
    }
    orderProducts.push(orderProduct);

    let newOrder: CreateOrder = {
      orderProducts : orderProducts,
      requiredDate : this.myForm?.controls.requiredDate.value,
      requiredReturnDate : this.myForm?.controls.requiredReturnDate.value,
      comments : this.myForm?.controls.comments?.value,
      customeId : user.id,
      shippedAdress : this.myForm?.controls.shippedAdress.value,
      returnAdress : this.myForm?.controls.returnAdress.value
    }

    this.orderService.addOrder(newOrder);

    this.remove();
  }


  matchValues(matchTo: string): ValidatorFn{
    return (control: AbstractControl) => {
      let requiredReturnDate = new Date();
      let requiredDate = new Date();

      if ( matchTo == 'requiredDate')
      {
        requiredReturnDate = new Date(control?.value);
        requiredDate = new Date(control?.parent?.controls[matchTo].value);
      }
      else {
        requiredReturnDate = new Date(control?.parent?.controls[matchTo].value);
        requiredDate = new Date(control?.value);
      }

      for (var i = 0; i < this.disabledDates.length; i++)
        {
          if ( new Date(this.disabledDates[i]) >= requiredDate && new Date(this.disabledDates[i]) <= requiredReturnDate)
            {
              this.error = 'You can only rent the item on available days';
              return { invalidRangeOfDays: 'You can only rent the item on available days'}
            }
        }
      if (requiredDate >= requiredReturnDate){
        this.error = 'start after end';
        return { invalidRangeOfDays: 'start after end'}
      }
      this.error = undefined;
      return null;
    }
  }

  addDisabledDays(){
    for (var i = 0; i < this.days.length; i++) {
      if (new Date(this.days[i].rentEnd) >= this.minDate) {
        for (var j = new Date(this.days[i].rentStart); j <= new Date(this.days[i].rentEnd); j = new Date(this.addDays(j, 1))){
          this.disabledDates.push(j);
        }
      }
    }
  }

  addDays(date: Date, count: number): Date{
    date = new Date(date.getTime() + (1000 * 60 * 60 * 24) * count);
    return date;
  }

  remove(){
    this.selectProductService.remove(this.item);
    this.show = false;
  }
}
