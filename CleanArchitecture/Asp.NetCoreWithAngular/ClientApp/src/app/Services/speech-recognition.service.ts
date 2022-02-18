import { NgZone } from '@angular/core';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as _ from "lodash";


interface IWindow extends Window {
  webkitSpeechRecognition: any;
  SpeechRecognition: any;
}

@Injectable({
  providedIn: 'root'
})
export class SpeechRecognitionService {

  speechRecognition: any;

  constructor(private zone: NgZone) {  }

  record(): Observable<string> {
      return Observable.create((observer: { next: (arg0: string) => void; error: (arg0: any) => void; complete: () => void; }) => {
          const { webkitSpeechRecognition }: IWindow = <IWindow><unknown>window;
          /*اگر مشکلی در اینجا بود در این لینک راه حل هست*/
          //https://stackoverflow.com/questions/39784986/speechrecognition-is-not-working-in-firefox
          this.speechRecognition = new webkitSpeechRecognition();
          this.speechRecognition.continuous = true;
          //this.speechRecognition.interimResults = true;
          //this.speechRecognition.lang = 'en-us';

          this.speechRecognition.lang = 'fa';
          this.speechRecognition.maxAlternatives = 10;

          this.speechRecognition.onresult = (speech: { results: { [x: string]: any; }; resultIndex: string | number; })  => {
              let Term: string = "";
              if (speech.results) {
                  var result = speech.results[speech.resultIndex];
                  var transcript = result[0].transcript;
                  if (result.isFinal) {
                      if (result[0].confidence < 0.3 ) {
                          //console.log("متن تشخیص داده نشد - لطفا مجددا سعی کنید");
                          Term ="متن تشخیص داده نشد - لطفا مجددا سعی کنید"
                      }
                      else {
                          Term = _.trim(transcript);
                          //console.log("Did you said? -> " + term + " , If not then say something else...");
                      }
                  }
              }
              this.zone.run(() => {
                  observer.next(Term);
              });
          };

          this.speechRecognition.onerror = (error: any) => {
              observer.error(error);
          };

          this.speechRecognition.onend = () => {
              observer.complete();
          };

          this.speechRecognition.start();
          console.log("Say something - We are listening !!!");
      });
  }

  DestroySpeechObject() {
      if (this.speechRecognition)
          this.speechRecognition.stop();
  }


}
