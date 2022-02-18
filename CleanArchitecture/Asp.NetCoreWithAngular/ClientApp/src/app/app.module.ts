import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpConfigInterceptor } from './interceptor/httpconfig.interceptor';
import { AuthenticationService } from './Services/authentication.service';
import { AuthguardService } from './Services/authguard.service';
import { ServerurlService } from './Services/serverurl.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule
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
