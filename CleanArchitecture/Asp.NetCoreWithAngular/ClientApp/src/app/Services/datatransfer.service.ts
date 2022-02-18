import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataTransferService {

  constructor() { }

  public Sb: Subject<any> = new Subject();

  public SendDate(Message: string): void {
    this.Sb.next(Message)
  }
  public GetData():Observable<any> {
    return this.Sb.asObservable();
  }
}
