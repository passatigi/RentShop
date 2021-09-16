import { Component, Input, OnInit, Self} from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Address } from 'src/app/_models/address';
import { CartItem } from 'src/app/_models/cartItem';
import { Order } from 'src/app/_models/order';
import { RealProduct } from 'src/app/_models/realProduct';
import { Segment } from 'src/app/_models/segment';
import { AccountService } from 'src/app/_services/account.service';
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
  show: boolean = true;
  days: Segment[];
  myForm: FormGroup;
  isCollapsed = true;
  error: string;
  daterangepickerModel?: Date[];
  addresses: Address[];
  selectedValue: Address;

  @Input() item: CartItem;
  constructor(private selectProductService: SelectProductService, 
    private dateService: DateService,
    private formBuilder: FormBuilder,
    private orderService: OrderService,
    private accountService: AccountService
    ) {}

  ngOnInit(): void {
    this.dateService.getProductScheduleById(this.item.id).subscribe( result => {
      this.days = result.segments;
      this.minDate = this.addDays(this.currentDate, 1);
      this.addDisabledDays();
    });

    this.myForm = this.formBuilder.group({
      shippedAddress: ['', Validators.required],
      returnAddress: ['', Validators.required],
      comments: ['', ],
      range: ['', [Validators.required, this.matchRangeValues()]]
    });

    this.getUserAddresses();
  }

  getUserAddresses(){
    let user = JSON.parse(localStorage.getItem('user'));
    this.accountService.getUserAddresses(user.email).subscribe(addresses => {
      this.addresses = addresses;
    });
  }

  matchRangeValues(): ValidatorFn{
    return (control: AbstractControl) => {
      let requiredReturnDate = new Date(control.value[1]);
      let requiredDate = new Date(control.value[0]);
      
      if (requiredDate.getDate() == requiredReturnDate.getDate()){
        this.error = 'You cannot rent an item for 0 days';
        return { invalidRangeOfDays: 'You cannot rent an item for 0 days'}
      }

      for (var i = 0; i < this.disabledDates.length; i++)
        {
          if ( new Date(this.disabledDates[i]) >= requiredDate && new Date(this.disabledDates[i]) <= requiredReturnDate)
            {
              this.error = 'You can only rent the item on available days';
              return { invalidRangeOfDays: 'You can only rent the item on available days'}
            }
        }
      this.error = undefined;
      return null;
    }
  }


  isControlInvalid(controlName: string): boolean {
    const control = this.myForm.controls[controlName];
    
    const result = control.invalid && control.touched;
    
    return result;
    }

  countDays(): number{
    let start = new Date(this.myForm.controls.range.value[0]);
    let end =  new Date(this.myForm.controls.range.value[1]);
    var diff = Math.abs(start.getTime() - end.getTime());
    return Math.ceil(diff / (1000 * 3600 * 24));
  }

  daysIsNaN(numb: number){
    return Number.isNaN(numb)
  }

  addOrder(){
    let orderProducts: RealProduct[] = [];
    let orderProduct: RealProduct = {
      id: this.item.id
    }
    orderProducts.push(orderProduct);

    let newOrder: Order = {
      orderProducts : orderProducts,
      requiredDate : this.myForm?.controls.range.value[0],
      requiredReturnDate : this.myForm?.controls.range.value[1],
      comments : this.myForm?.controls.comments?.value,
      customerId : this.myForm?.controls.shippedAddress.value.appUserId,
      shippedAddressId : this.myForm?.controls.shippedAddress.value.id,
      returnAddressId : this.myForm?.controls.returnAddress.value.id,
      totalPrice : this.item.rentPrice * this.countDays()
    }

    this.orderService.addOrder(newOrder).subscribe((res: Response) => {
      this.remove();
    },
    (error) => {
      console.log(error.error)
    });
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
