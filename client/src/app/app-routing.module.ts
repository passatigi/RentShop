import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddProductComponent } from './admin/add-product/add-product.component';
import { AdminComponent } from './admin/admin/admin.component';
import { LoginComponent } from './login_register/login/login.component';
import { RegistrationComponent } from './login_register/registration/registration.component';
import { MainComponent } from './main/main.component';
import { AdminGuard } from './_guards/admin.guard';

const routes: Routes = [
  {path: '', component: MainComponent},
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
