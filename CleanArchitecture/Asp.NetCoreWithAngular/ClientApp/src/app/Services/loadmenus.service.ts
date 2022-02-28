import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import { DataTransferService } from './datatransfer.service';
import { ServerurlService } from './serverurl.service';

import { UserRole } from '../ViewModels/UserRole';

import * as MyPublicMethod from '../../PublicMethod';

@Injectable({
  providedIn: 'root'
})
export class LoadmenusService {


  constructor(public router: Router, private http: HttpClient,
    private authenticationService: AuthenticationService,
    private Serverurl: ServerurlService, private Dt: DataTransferService) {
  }



  GetAllUserMenusWithUserIdAndRoleId(UserRole: UserRole) {
    this.http.post<any>(this.Serverurl.Url + '/Menu/GetAllUserMenusWithUserIdAndRoleId', UserRole).subscribe(Data => {
      var Self = this;
      var ul = $("<ul class='sidebar-nav' style='top: 30%;'></ul>");
      ul.attr("id", "MyCollapse");

      var source = MyPublicMethod.CreateNestedData(Data);
      MyPublicMethod.CreateUL(ul, source);
      $(ul).appendTo("#SidebarWrapper").insertAfter("#BetWeenMenu");


      console.log(Data);

      MyPublicMethod.MyCollapsechildrenHide();
      MyPublicMethod.OpenMenuDataBase();

      /*رویداد کلیک  برای منوهای دیتا بیس*/
      $("a.Link").click(function () {
        //e.preventDefault();
        var UrArray = $(this).attr('data-url')!.split("/");
        var Component = UrArray[0];
        var Ur = "/" + $(this).attr('data-url');
        var ParentID = $(this).attr('data-ParentId');
        if (Component != 'null') { //اگر زیر منو باشد
          Self.GotoLink(Component);
          $("#MyCollapse li").children('ul').hide();
        }
      });

    },
      (error) => { alert(JSON.stringify(error)) }
    );
  }

  GotoLink(St: string): void {
    this.router.navigateByUrl("./");
    //this.router.navigate(['/Home']);
    this.router.navigate(['/' + St + '']);
  }



}
