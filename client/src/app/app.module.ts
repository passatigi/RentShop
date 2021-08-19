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
    NotFoundComponent
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
    NgxGalleryModule
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
