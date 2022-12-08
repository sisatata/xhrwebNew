import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventEmitterService {

  // logout event
  private logoutEventSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(null);
  logoutEvent: Observable<boolean> = this.logoutEventSubject.asObservable();

  constructor() { }

  emitLogoutEvent(eventData: boolean) {
    this.logoutEventSubject.next(eventData);
  }

}
