import { Component, OnInit, Input, SimpleChanges, Injector } from '@angular/core';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { Division } from '../../shared/models';
import { CreateDivisionComponent } from './create-division/create-division.component';
import { DivisionService } from '../services';

@Component({
  selector: 'app-divisions',
  templateUrl: './divisions.component.html',
  styleUrls: ['./divisions.component.css']
})

export class DivisionsComponent implements OnInit {
  userFullName: string;
  userId: number;
  divisions: Division[];
  expandedIndex: number = -1;
  isSystemAdmin = false;


  constructor(
    private dialog: MatDialog,
    private divisionService: DivisionService,
    private injector: Injector
  ) {  }

  ngOnInit() {
       this.getDivisions();
  }

  ngOnChanges(changes: SimpleChanges) {
    
  }

  createDivision() {
    const createDivisionDialogConfig = new MatDialogConfig();

    // Setting different dialog configurations
    createDivisionDialogConfig.disableClose = true;
    createDivisionDialogConfig.autoFocus = true;
    createDivisionDialogConfig.panelClass = "xtra-dialog";

    var division = new Division();
    //division.userId = this.userId;
    createDivisionDialogConfig.data = division;

    const createDivisionDialog = this.dialog.open(CreateDivisionComponent, createDivisionDialogConfig);

    //const successfullCreate = createDivisionDialog.componentInstance.onCreateDivisionEvent.subscribe((data) => {
    //  this.getDivisions();
    //});

    createDivisionDialog.afterClosed().subscribe(() => {
    });


  }

  getDivisions() {
    this.divisionService.getAllDivisions().subscribe(result => {
        this.divisions = result;
      });
  }

}

