import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbCarouselConfig, NgbPaginationConfig } from '@ng-bootstrap/ng-bootstrap';
import { User } from '../Models/User';
import { AuthenticationService } from '../Services/authentication.service';
import { PublichttpService } from '../Services/publichttp.service';
import { ServerurlService } from '../Services/serverurl.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [NgbCarouselConfig]  // add NgbCarouselConfig to the component providers
})
export class HomeComponent implements OnInit {

  public Images: string[];
  constructor(config: NgbCarouselConfig, configNgbPagination: NgbPaginationConfig, private HttpService: PublichttpService,
    private ServerUrl: ServerurlService, private authenticationService: AuthenticationService, public activatedRoute: ActivatedRoute,
    public router: Router) {
    this.Images = [1, 2, 3, 4].map(() => `https://picsum.photos/900/500?random&t=${Math.random()}`);
    // customize default values of carousels used by this component tree
    config.showNavigationArrows = true;
    config.showNavigationIndicators = true;
    config.interval = 5000;
    config.wrap = false;
    /*config.keyboard = false;
    config.pauseOnHover = false;*/
    /*NgbPaginationConfig*************************** */
    configNgbPagination.size = 'sm'
  }

  public page = 1;
  public pageSize = 10;

  public UserList: User[] = [];
  public Itemslength = 150;

  ngOnInit() {
    //this.items = Array(this.Itemslength).fill(0).map((x, i) => ({ id: (i + 1), name: `Item ${i + 1}`}));
    let Au = {} as User;
    Au.fromPage = 0;
    Au.toPage = 9;

    /*
    this.HttpService.HttpGetWithObject(this.ServerUrl.Url + "/User/GetListDoctorsForPagination", Au).subscribe(
      Data => {
        /*this.ngbTypeaheadMainGroupDoctorList = Data.slice();
        let M = {} as MainGroupDoctor;
        M.id = 0;
        M.name = "سر گروه";
        //M.userId = 0;
        this.ngbTypeaheadMainGroupDoctorList.unshift(M); /*push(D);/*
        this.UserList = Data;
        //console.log(Data);
      },
      (error) => {
        alert(JSON.stringify(error));
      },
      () => {
      });

      */
  }

  public onPageChange(pageNum: number): void {
    //this.items = Array(this.Itemslength).fill(pageNum * this.pageSize).map((x, i) => ({ id: (i + 1), name: `Item ${i + 1}`}));

    let Au = {} as User;
    Au.fromPage = ((pageNum - 1) * this.pageSize);
    Au.toPage = (pageNum * this.pageSize) - 1;

    this.HttpService.HttpGetWithObject(this.ServerUrl.Url + "/User/GetListDoctorsForPagination", Au).subscribe(
      Data => {
        /*this.ngbTypeaheadMainGroupDoctorList = Data.slice();
        let M = {} as MainGroupDoctor;
        M.id = 0;
        M.name = "سر گروه";
        //M.userId = 0;
        this.ngbTypeaheadMainGroupDoctorList.unshift(M); /*push(D);*/
        this.UserList = Data;
        console.log(Data);
      },
      (error) => {
        alert(JSON.stringify(error));
      },
      () => {
      });
  }

  public TurnTaking(U: User) {
    this.router.navigate(['/TurnTaking'], { state: { User: U } });
  }


}
