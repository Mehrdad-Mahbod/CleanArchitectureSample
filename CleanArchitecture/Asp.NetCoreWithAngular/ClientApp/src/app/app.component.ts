import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './Services/authentication.service';
import { DataTransferService } from './Services/datatransfer.service';
import { ServerurlService } from './Services/serverurl.service';

import { UserRole } from './Models/UserRole';

import * as MyPublicMethod from '../PublicMethod';

import * as $ from "jquery";
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';
  public JwtHelper = new JwtHelperService();
  public UserRole = new UserRole();


  constructor(public router: Router, private http: HttpClient,
    /*private route: ActivatedRoute, 
    private ngZone: NgZone,*/
    private authenticationService: AuthenticationService,
    private Serverurl: ServerurlService, private Dt: DataTransferService) {
    //this.Dt.GetData().subscribe((Message) => {
    //  $("#UserName").html(Message);
    //});
  }


  GotoLink(St: string): void {
    /*this.ngZone.run(() => {
      /*if(St=="AddSingleVisa")
      {
        window.location.reload();
      }      
      this.router.navigate(['/' + St + '']);
    });*/

    console.log("GotoLink");
    this.router.navigateByUrl("./");
    //this.router.navigate(['/Home']);
    this.router.navigate(['/' + St + '']);
  }

  private InfoTokenData: any;


  ngOnInit() {
    var Self = this;


    MyPublicMethod.MainFaunction();
    MyPublicMethod.OnlyNumberWithSeparatingThreeDigits();
    MyPublicMethod.OnlyNumber();

    if (this.IsLoggedIn()) {

      //alert('شما وارد شده اید');
      const LocalToken = localStorage.getItem('token');
      const TokenData = this.JwtHelper.urlBase64Decode(LocalToken!.split('.')[1]);
      this.InfoTokenData = JSON.parse(TokenData);
      this.UserRole.UserId = this.InfoTokenData.UserID;
      this.UserRole.RoleId = this.InfoTokenData.RoleID;

      this.http.post<any>(this.Serverurl.Url + '/Menu/GetAllUserRoleMenu', this.UserRole).subscribe(Data => {
        var Self = this;
        var ul = $("<ul class='sidebar-nav' style='top: 30%;'></ul>");
        ul.attr("id", "MyCollapse");

        var source = MyPublicMethod.CreateNestedData(Data);
        MyPublicMethod.CreateUL(ul, source);
        $(ul).appendTo("#sidebar-wrapper").insertAfter("#StaticMenu");
        MyPublicMethod.MyCollapsechildrenHide();
        MyPublicMethod.OpenMenuDataBase();

        /*رویداد کلیک  برای منوهای دیتا بیس*/
        $("a.Link").click(function () {
          //e.preventDefault();
          var UrArray = $(this).attr('data-url')!.split("/");
          var Component = UrArray[0];
          var Ur = "/" + $(this).attr('data-url');
          var ParentID = $(this).attr('data-ParentId');
          //console.log('Con:' + Component + "|||" + 'Ur:' + Ur + "|||" + "ParentID:" + ParentID);
          //alert('Con:' + Component + "|||" + 'Ur:' + Ur + "|||" + "ParentID:" + ParentID);          
          console.log(typeof (Component) + "//" + Component);
          if (Component != 'null') { //اگر زیر منو باشد
            Self.GotoLink(Component);
            $("#MyCollapse li").children('ul').hide();
            /*سایدبار را می بندد*/
            if (!$("#wrapper").hasClass('DivWrapper')) {
              $("#wrapper").addClass("DivWrapper");
            }
            /*برای وقتی هست صفحه به حالت کولاپس قرار دارد*/
            if ($("#wrapper").hasClass('toggled')) {
              $("#wrapper").removeClass("toggled");
            }
          }
        });

      },
        (error) => { alert(JSON.stringify(error)) }
      );

      this.Dt.GetData().subscribe((Message) => {
        console.log("From DataTransfer:" + Message);
        //$("#UserName").html(Message);
      });




      //this.LogIn();


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
          //ساید بار را باز و بسته می کند
          //چون اینجا فعلا به درد نمی خورد کامنت کردم
          //$("#wrapper").toggleClass("toggled");
        }
      })


      const LocalToken = localStorage.getItem('token');
      if (LocalToken != null) {
        const TokenData = Self.JwtHelper.urlBase64Decode(LocalToken.split('.')[1]);
        this.InfoTokenData != JSON.parse(TokenData);
      }

      /*نمایش نام کاربر*/
      if (this.InfoTokenData != undefined) {

        $("#UserName").html(" " + this.InfoTokenData.family_name + " ");
      }
    });

  }

  LogIn() {
    var Self = this;
    //var InfoTokenData :any;
    //alert('شما وارد شده اید');     

    const LocalToken = localStorage.getItem('token');
    const TokenData = this.JwtHelper.urlBase64Decode(LocalToken!.split('.')[1]);
    this.InfoTokenData = JSON.parse(TokenData);

    this.UserRole.UserId = this.InfoTokenData.UserID;
    this.UserRole.RoleId = this.InfoTokenData.RoleID;


    this.http.post<any>(this.Serverurl.Url + '/Menu/GetAllUserRoleMenu', this.UserRole).subscribe(Data => {
      //var Self = this;
      var ul = $("<ul class='sidebar-nav' style='top: 30%;'></ul>");
      ul.attr("id", "MyCollapse");

      var source = MyPublicMethod.CreateNestedData(Data);
      MyPublicMethod.CreateUL(ul, source);
      $(ul).appendTo("#sidebar-wrapper").insertAfter("#StaticMenu");
      MyPublicMethod.MyCollapsechildrenHide();
      MyPublicMethod.OpenMenuDataBase();

      /*رویداد کلیک  برای منوهای دیتا بیس*/
      $("a.Link").click(function () {
        //e.preventDefault();
        var UrArray = $(this).attr('data-url')!.split("/");
        var Component = UrArray![0];
        var Ur = "/" + $(this).attr('data-url');
        var ParentID = $(this).attr('data-ParentId');
        //console.log('Con:' + Component + "|||" + 'Ur:' + Ur + "|||" + "ParentID:" + ParentID);
        //alert('Con:' + Component + "|||" + 'Ur:' + Ur + "|||" + "ParentID:" + ParentID);          
        console.log(typeof (Component) + "//" + Component);
        if (Component != 'null') { //اگر زیر منو باشد
          Self.GotoLink(Component);
          $("#MyCollapse li").children('ul').hide();
          /*سایدبار را می بندد*/
          if (!$("#wrapper").hasClass('DivWrapper')) {
            $("#wrapper").addClass("DivWrapper");
          }
          /*برای وقتی هست صفحه به حالت کولاپس قرار دارد*/
          if ($("#wrapper").hasClass('toggled')) {
            $("#wrapper").removeClass("toggled");
          }
        }
      });
    },
      (error) => { alert(JSON.stringify(error)) }
    );

    this.Dt.GetData().subscribe((Message) => {
      console.log("From DataTransfer:" + Message);
      //$("#UserName").html(Message);
    });
  }

  Logout() {
    $("#UserName").html('');
    this.authenticationService.Logout().subscribe(Data => {
      console.log(Data);
      //alert(Data[1].valueOf());
      //alert(Data.valueOf());
    },
      (error) => alert(error));
    this.router.navigate(['/']);

    $("#MyCollapse").remove();
  }
  IsLoggedIn() {
    return this.authenticationService.IsLoggedIn();
  }


}