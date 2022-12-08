import { Injectable, Injector } from '@angular/core';
import { SharedService } from '../services/shared.service';

@Injectable({
  providedIn: 'root'
})
export class PaginatorBase {
  currentPage: number = 1;
  currentPageChild: number = 1;
  pageSize:number =10;
  pageSizeChild: number = 10;
  expandedIndex: number = -1;
  totalItems: number = 0;
  totalItemsForChild: number = 0;
  paginationPageNumbers: any[];
  paginationPageNumbersForChild: any[];
  totalItemsText: string = "";
  totalItemsTextForChild: string = "";
  child:boolean = false;
  sharedService: SharedService = new SharedService();

  firstItem: number = 0;
  lastItem: number = 0;

  firstItemForChild: number = 0;
  lastItemForChild: number = 0;
  constructor(injector: Injector) {
    this.paginationPageNumbers = this.sharedService.getPaginationByPageNumbers();
    this.paginationPageNumbersForChild = this.sharedService.getPaginationByPageNumbers();
  }

  pageChanged(event) {
    this.currentPage = event;
    this.generateTotalItemsText();
    this.child = false;
  }
  pageChangedForChild(event){
    this.currentPageChild = event;
    this.generateTotalItemsTextForChild();
  }
  itemCount() {
  
    this.firstItem = (this.currentPage - 1) * this.pageSize + 1;
    this.lastItem = (this.currentPage) * this.pageSize;
   
    //console.log(this.firstItem, this.lastItem);
  }
  itemCountForChild() {
    this.firstItemForChild = (this.currentPageChild - 1) * this.pageSizeChild + 1;
    this.lastItemForChild = (this.currentPageChild) * this.pageSizeChild;
  }

  onChangePaginationPerPageForChild() {
    this.currentPageChild = 1;
    this.pageSizeChild = this.pageSizeChild;
    this.child = true;
    this.generateTotalItemsTextForChild();
  }

  onChangePaginationPerPage() {
    this.currentPage = 1;
    this.pageSize = this.pageSize;
   // console.log( this.pageSize)

    this.generateTotalItemsText();
  }


  generateTotalItemsText() {
    if (this.totalItems == 0) {
      this.totalItemsText = "No record found";
    }
    else {

      this.itemCount();
      this.totalItemsText = this.firstItem + " to " + 
      (this.lastItem > this.totalItems? this.totalItems:this.lastItem) + " of " + this.totalItems + " records";
    }
  }
  generateTotalItemsTextForChild(){
    if (this.totalItemsForChild == 0) {
      this.totalItemsTextForChild = "No record found";
    }
    else {

      this.itemCountForChild();
      this.totalItemsTextForChild = this.firstItemForChild + " to " + 
      (this.lastItemForChild > this.totalItemsForChild? this.totalItemsForChild:this.lastItemForChild) + " of " + this.totalItemsForChild + " items";
    }
  }
}
