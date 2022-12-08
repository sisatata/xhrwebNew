import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  showSidebarCollapsed: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  
  messageSource = new BehaviorSubject("");
  loggoutMessage = this.messageSource.asObservable();
  loggedOutMessageSource = new BehaviorSubject(false);
  isLoggedout = this.loggedOutMessageSource.asObservable();
  
  constructor() { }

  getGenders() {
    var genders = [];
    genders.push({ value: 0, text: 'Others' });
    genders.push({ value: 1, text: 'Female' });
    genders.push({ value: 2, text: 'Male' });
    return genders;
  }

  getAllCompanyStatuses() {
    var statuses = [];
    statuses.push({ value: 1, text: 'Pending' });
    statuses.push({ value: 2, text: 'InProgress' });
    statuses.push({ value: 3, text: 'Approved' });
    statuses.push({ value: 9, text: 'Declined' });
    return statuses;
  }

  getPaginationByPageNumbers() {
    var pageNumbers = [{ text: 5, value: 5 }, { text: 10, value: 10 },
    { text: 25, value: 25 }, { text: 50, value: 50 },
    { text: 100, value: 100 }, { text: 500, value: 500 },];
    return pageNumbers;
  }
}
