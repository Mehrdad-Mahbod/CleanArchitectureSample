import { Injectable, Inject } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ServerurlService {

  constructor(@Inject('BASE_URL') private baseUrl: string) { }

  public Url = this.baseUrl + "api";
  //public Url = "http://www.birolax.com:5000/api";
  //public Url = "http://localhost:5000/api";
}
