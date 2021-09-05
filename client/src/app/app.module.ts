import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainComponent } from './main/main.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { CategoryAdminComponent } from './admin/category-admin/category-admin.component';
import { NavComponent } from './nav/nav.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { RegistrationComponent } from './login_register/registration/registration.component';
import { LoginComponent } from './login_register/login/login.component';
import { AdminComponent } from './admin/admin/admin.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HasRoleDirective } from './_directives/has-role.directive';
import { AddProductComponent } from './admin/product/add-product/add-product.component';
import { AddFeatureComponent } from './admin/feature/add-feature/add-feature.component';
import { FeatureFormComponent } from './admin/feature/feature-form/feature-form.component';
import { ProductFormComponent } from './admin/product/product-form/product-form.component';
import { ProductDetailComponent } from './_products/product-detail/product-detail.component';
import { ProductListComponent } from './_products/product-list/product-list.component';
import { ProductCardComponent } from './_products/product-card/product-card.component';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { CategoryCardComponent } from './_categories/category-card/category-card.component';
import { EditProductComponent } from './admin/product/edit-product/edit-product.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { AddRealProductComponent } from './admin/realProduct/add-real-product/add-real-product.component';
import { RealProductFormComponent } from './admin/realProduct/real-product-form/real-product-form.component';
import { EditRealProductsComponent } from './admin/product/edit-real-products/edit-real-products.component';
import { EditRealProductComponent } from './admin/realProduct/edit-real-product/edit-real-product.component';
import { ShoppingCartComponent } from './_cart/shopping-cart/shopping-cart.component';
import { CartDetaisComponent } from './_cart/cart-detais/cart-detais.component';
import { CardItemComponent } from './_cart/card-item/card-item.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { CalendarComponent } from './_products/calendar/calendar.component';
import { RealProductCardComponent } from './_products/real-product-card/real-product-card.component';
import { OrdersListComponent } from './_orders/orders-list/orders-list.component';
import { OrderCardComponent } from './_orders/order-card/order-card.component';
import { PhotoUploadComponent } from './admin/photo/photo-upload/photo-upload.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FileUploadModule } from 'ng2-file-upload';
import { ProductPhotosComponent } from './admin/product/product-photos/product-photos.component';
import { DeliverymanScheduleFormComponent } from './deliveryman/deliveryman-schedule/deliveryman-schedule-form/deliveryman-schedule-form.component';
import { MonthDeliverymanScheduleComponent } from './deliveryman/deliveryman-schedule/month-deliveryman-schedule/month-deliveryman-schedule.component';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { MonthPickerComponent } from './deliveryman/month-picker/month-picker.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FixedPositionatedComponent } from './auxiliary-components/fixed-positionated/fixed-positionated.component';
import { NgxDraggableDomModule } from 'ngx-draggable-dom';
import { DeliverymanEditDayScheduleComponent } from './deliveryman/deliveryman-schedule/deliveryman-edit-day-schedule/deliveryman-edit-day-schedule.component';
import { DeliveryListComponent } from './deliveryman/delivery-schedule/delivery-list/delivery-list.component';
import { ProfileComponent } from './profile/profile.component';
import { EditProfileComponent } from './profile/edit-profile/edit-profile.component';
import { AddressEditComponent } from './profile/address-edit/address-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    TextInputComponent,
    CategoryAdminComponent,
    NavComponent,
    RegistrationComponent,
    LoginComponent,
    AdminComponent,
    HasRoleDirective,
    AddProductComponent,
    AddFeatureComponent,
    FeatureFormComponent,
    ProductFormComponent,
    ProductDetailComponent,
    ProductListComponent,
    ProductCardComponent,
    CategoryCardComponent,
    EditProductComponent,
    NotFoundComponent,
    AddRealProductComponent,
    RealProductFormComponent,
    EditRealProductsComponent,
    EditRealProductComponent,
    ShoppingCartComponent,
    CartDetaisComponent,
    CardItemComponent,
    CalendarComponent,
    RealProductCardComponent,
    OrdersListComponent,
    OrderCardComponent,
    PhotoUploadComponent,
    ProductPhotosComponent,
    DeliverymanScheduleFormComponent,
    MonthDeliverymanScheduleComponent,
    MonthPickerComponent,
    FixedPositionatedComponent,
    DeliverymanEditDayScheduleComponent,
    DeliveryListComponent,
    ProfileComponent,
    AddressEditComponent,
    EditProfileComponent,
    DeliveryListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right' 
    }),
    FontAwesomeModule,
    NgxGalleryModule,
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
    FileUploadModule,
    TimepickerModule.forRoot(),
    ModalModule.forRoot(),
    NgxDraggableDomModule
  ],
  exports: [
    NgxGalleryModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
