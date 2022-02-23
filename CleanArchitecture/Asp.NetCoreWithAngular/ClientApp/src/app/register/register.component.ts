import { Component, OnInit } from '@angular/core';

import { FormBuilder,FormGroup,FormControl, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterViewModel } from '../ViewModels/Authentication/RegisterViewModel';
import { AuthenticationService } from '../Services/authentication.service'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private fb: FormBuilder, private authenticationService: AuthenticationService, private router: Router) { }

  public formGroup:FormGroup | undefined;

  ngOnInit() {
    this.formGroup = this.fb.group({
      'ParentId': null,
      'Gender': '1',
      'Name': new FormControl(null),
      'Family': new FormControl(null),
      'UserName': new FormControl(null),
      'PhoneNumber':new FormControl(null),
      'Email': new FormControl(null),
      'Password': new FormControl(null) ,
    }); 
  }

  Register(Form1: NgForm) {
    //alert(JSON.stringify(Form1.value));
    //let userInfo: IUser = Object.assign({}, this.formGroup.value);
    let RegisterViewModel: RegisterViewModel = Object.assign({}, Form1.value);
    this.authenticationService.Register(RegisterViewModel).
      subscribe(Data => alert(Data),/*token => this.recibirToken(token),*/
      error => this.ErrorManagement(error),
      ()=> this.Complete()
    );
  }
  recibirToken(token:any)
  {
    localStorage.setItem('token',token.token);
    localStorage.setItem('tokenExpiration',token.expiration);
    this.router.navigate(["/"]);
  }
  Complete() {
    //alert("اطلاعات کاربر ثبت گردید");
    this.router.navigate(["/"]);
  }
  ErrorManagement(Error:any) {
    if (Error && Error.error) {
      alert(JSON.stringify(Error.error));
    }
  }

}
