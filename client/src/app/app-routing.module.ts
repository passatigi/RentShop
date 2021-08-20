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
import { CartDetaisComponent } from './_cart/cart-detais/cart-detais.component';

const routes: Routes = [
  {path: '', component: MainComponent},
  {path: 'product', component: ProductDetailComponent},
  {path: 'category', component: ProductListComponent},
  {path: 'cart', component: CartDetaisComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegistrationComponent},
  {path: 'admin', component: RegistrationComponent},
  {
    path: 'admin', 
    runGuardsAndResolvers: 'always',
    canActivate: [AdminGuard],
    children: [
      {path: 'main', component: AdminComponent},
      {path: 'add-product', component: AddProductComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
