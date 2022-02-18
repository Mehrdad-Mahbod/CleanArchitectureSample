import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl, Validator } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

import * as $ from "jquery"

import { User } from '../Models/User';

import { AuthenticationService } from '../Services/authentication.service';
import { ServerurlService } from '../Services/serverurl.service';
import { DataTransferService } from '../Services/datatransfer.service';

import { AppComponent } from '../app.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  public InputForm!: FormGroup;
  public Username: string | undefined;
  public Password: string | undefined;

  public helper = new JwtHelperService();

  constructor(public http: HttpClient, private authenticationService: AuthenticationService, private router: Router,
    private Serverurl: ServerurlService, private Dt: DataTransferService

  ) { }

  ngOnInit() {
    this.InputForm = new FormGroup({
      'UserName': new FormControl(null),
      'Password': new FormControl(null)
    });
  }

  get GetControl() {
    return this.InputForm.controls;
  }

  Login() {

    console.log(this.InputForm);
    if (this.InputForm.valid) {
      let userInfo: User = Object.assign({}, this.InputForm.value);
      var Message: String;
      this.authenticationService.Login(userInfo).
        subscribe(token => {
          if (token == 0) {
            //alert("چنین کاربری در سیستم تعریف نشده");
            Message = "چنین کاربری در سیستم تعریف نشده";
          }
          else {
            console.log(JSON.stringify(token))
            Message = "شما به سیستم ورود کرده اید";
            this.ReciveToken(token);
          }
        },
          error => this.ErrorManagement(error), () => this.CompleteLogin(Message));
    }
  }

  ReciveToken(token: { token: string; expiration: string; }) {
    localStorage.setItem('token', token.token);
    localStorage.setItem('tokenExpiration', token.expiration);
    //const localStorageToken = localStorage.getItem('token');
    const LocalToken = this.helper.urlBase64Decode(token.token.split('.')[1]);
    var TokenData = JSON.parse(LocalToken);
    alert(JSON.stringify("UserID:" + TokenData.UserID + "RoleID:" + TokenData.RoleID + "***" + TokenData.unique_name + "***" + TokenData.family_name));
    let A: AppComponent = new AppComponent(this.router, this.http, this.authenticationService, this.Serverurl, this.Dt);
    A.LogIn();
    /***********این قسمت جالب نیست بعدا عوض کن************** */
    $(document).ready(function () {
      $('#UserName').html(" " + TokenData.family_name + " ");
    });
    this.Dt.SendDate(TokenData.family_name);
    this.router.navigate(["/"]);
    //window.location.reload();
  }

  ErrorManagement(Error: { error: { [x: string]: any; }; }) {
    if (Error && Error.error) {
      alert(Error.error[""]);
    }
  }

  CompleteLogin(Str: String) {
    alert(Str);
  }

}
