import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

import { AuthenticationService } from './Services/authentication.service';
import { DataTransferService } from './Services/datatransfer.service';
import { ServerurlService } from './Services/serverurl.service';
import { LoadmenusService } from './Services/loadmenus.service';

import { UserRole } from './ViewModels/UserRole';
import * as MyPublicMethod from '../PublicMethod';
import * as $ from "jquery";
import { GetCurrentUserToken } from './GetCurrentUserToken';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';
  public JwtHelper = new JwtHelperService();
  public UserRole = new UserRole();

  public UserToken:GetCurrentUserToken =new GetCurrentUserToken();

  constructor(public router: Router, private http: HttpClient,
    private authenticationService: AuthenticationService,
    private Serverurl: ServerurlService, private Dt: DataTransferService, private Lm:LoadmenusService) {
  }

  ngOnInit() {
    var Self = this;

    MyPublicMethod.MainFaunction();
    MyPublicMethod.OnlyNumberWithSeparatingThreeDigits();
    MyPublicMethod.OnlyNumber();

    if (this.IsLoggedIn()) {
      //alert('شما وارد شده اید');
      this.UserRole.UserId = this.UserToken.GetUserToken().UserID;
      this.UserRole.RoleId = this.UserToken.GetUserToken().RoleID;

      this.Lm.GetAllUserMenusWithUserIdAndRoleId(this.UserRole);
    }
    /**********Start Jquery***********/
    $("document").ready(() => {   /*function () { نسخه قدیم به این شکل بود   */      
      /*Enter حرکت با */
      $(document).on('keydown', 'input,select', function (e) {
        if (e.which == 13) {
          e.preventDefault();
          // Get all focusable elements on the page
          var $canfocus = $('input,select');
          var index = $canfocus.index(this) + 1;
          if (index >= $canfocus.length) index = 0;
          $canfocus.eq(index).focus();
        }
      });

      /*با کلیک بر روی هر یک از عناصر ناوبار حالت کلاپس اتفاق می افتد*/
      $('.nav-link').on('click', function () {
        if ($('.navbar-collapse.collapse.show').hasClass('show')) {
          $('.navbar-collapse.collapse.show').removeClass('show');
        }
      })

      /*نمایش نام کاربر*/
      if (this.UserToken.GetUserToken() != undefined) {
        $("#UserName").html(" " + this.UserToken.GetUserToken().family_name + " ");
      }
    });

  }

  LogIn() {
    var Self = this;  
    this.UserRole.UserId = this.UserToken.GetUserToken().UserID;
    this.UserRole.RoleId = this.UserToken.GetUserToken().RoleID;
    this.Lm.GetAllUserMenusWithUserIdAndRoleId(this.UserRole);
  }

  Logout() {
    $("#UserName").html('');
    this.authenticationService.Logout().subscribe(Data => {
    },
      (error) => alert(error));
    this.router.navigate(['/']);

    $("#MyCollapse").remove();
  }

  IsLoggedIn() {
    return this.authenticationService.IsLoggedIn();
  }

}