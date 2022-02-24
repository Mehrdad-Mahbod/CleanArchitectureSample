import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HttpConfigInterceptor } from './interceptor/httpconfig.interceptor';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthenticationService } from './Services/authentication.service';
import { AuthguardService } from './Services/authguard.service';
import { ServerurlService } from './Services/serverurl.service';
import { SharedModule } from './shared/shared.module';
import { GeneralofficeComponent } from './generaloffice/generaloffice.component';
import { DefiniteofficeComponent } from './definiteoffice/definiteoffice.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'Home', component: HomeComponent },
  { path: 'Register', component: RegisterComponent,/*canActivate: [AuthGuardService]*/ },
  { path: 'Login', component: LoginComponent },
  { path: 'GeneralOffice', component: GeneralofficeComponent, canActivate: [AuthguardService] },
  { path: 'DefiniteOffice', component: DefiniteofficeComponent, canActivate: [AuthguardService] },
]



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    GeneralofficeComponent,
    DefiniteofficeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    RouterModule.forRoot(routes),
    SharedModule.forRoot(),

  ],
  providers: [
    AuthenticationService,
    { provide: 'BASE_URL', useFactory: getBaseUrl },
    AuthguardService,
    //DataTransferService,
    //PublicHttpService,
    ServerurlService,
    { provide: HTTP_INTERCEPTORS, useClass: HttpConfigInterceptor, multi: true },
    //SpinnerService

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
//Add By Mehrdad
export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}
