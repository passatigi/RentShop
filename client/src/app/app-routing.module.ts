import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddProductComponent } from './admin/product/add-product/add-product.component';
import { AdminComponent } from './admin/admin/admin.component';
import { LoginComponent } from './login_register/login/login.component';
import { RegistrationComponent } from './login_register/registration/registration.component';
import { MainComponent } from './main/main.component';
import { AdminGuard } from './_guards/admin.guard';
import { ProductDetailComponent } from './_products/product-detail/product-detail.component';
import { ProductListComponent } from './_products/product-list/product-list.component';
import { EditProductComponent } from './admin/product/edit-product/edit-product.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { CartDetaisComponent } from './_cart/cart-detais/cart-detais.component';
import { OrdersListComponent } from './_orders/orders-list/orders-list.component';
import { DeliverymanGuard } from './_guards/deliveryman.guard';
import { MonthDeliverymanScheduleComponent } from './deliveryman/deliveryman-schedule/month-deliveryman-schedule/month-deliveryman-schedule.component';
import { DeliveryListComponent } from './deliveryman/delivery-schedule/delivery-list/delivery-list.component';
import { ProfileComponent } from './profile/profile.component';
import { EditProfileComponent } from './profile/edit-profile/edit-profile.component';

const routes: Routes = [
  {path: '', component: MainComponent},
  {path: 'product', component: ProductDetailComponent},
  {path: 'category', component: ProductListComponent},
  {path: 'orders', component: OrdersListComponent},
  {path: 'cart', component: CartDetaisComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegistrationComponent},
  {path: 'admin', component: RegistrationComponent},
  {path: 'profile', component: ProfileComponent},
  {path: 'editProfile', component: EditProfileComponent},
  {
    path: 'admin', 
    runGuardsAndResolvers: 'always',
    canActivate: [AdminGuard],
    children: [
      {path: 'main', component: AdminComponent},
      {path: 'add-product', component: AddProductComponent},
      {path: 'edit-product', component: EditProductComponent},
    ]
  },
  {
    path: 'delivery', 
    runGuardsAndResolvers: 'always',
    canActivate: [DeliverymanGuard],
    children: [
      // {path: 'main', component: AdminComponent},
      {path: 'man-schedule', component: MonthDeliverymanScheduleComponent},
      {path: 'delivery-list', component: DeliveryListComponent}
    ]
  },
  {path: 'not-found', component: NotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
