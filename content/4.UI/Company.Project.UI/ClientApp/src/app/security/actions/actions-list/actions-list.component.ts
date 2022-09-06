import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable} from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { take, tap, finalize } from 'rxjs/operators';
import { Actions } from '../actions.models';
import { ActionService } from '../services/action.service';
import { ConfirmComponent } from 'src/app/shared/dialogs/confirm/confirm.component';
import { detailExpand } from 'src/app/shared/utils/animations';
import { LoadingService } from 'src/app/shared/services/loading/loading.service';
import { ServerSideDataSource } from 'src/app/shared/utils/server-side-datasource';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-actions-list',
  templateUrl: './actions-list.component.html',
  styleUrls: ['./actions-list.component.scss'],
  animations: [
    detailExpand
  ],
})
export class ActionsListComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table?: MatTable<any>;

  dataSource!: ServerSideDataSource<Actions>;
  displayedColumns = ['name', 'description', 'actions'];
  expandedElement?: Actions;
  isExpansionDetailRow = (i: number, row: any) => row.hasOwnProperty('detailRow');

  constructor(
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private loading: LoadingService,
    private actionService: ActionService) {
  }

  ngOnInit(): void {
    this.dataSource = new ServerSideDataSource(true);
  }
  
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.dataSource.setDataSource((params) => this.actionService.getPaged(params));
  }

  refresh() {
    this.dataSource.updateData();
  }

  delete(action: Actions) {
    const dialogRef = this.dialog.open(ConfirmComponent, { width: '250px', data: { content: 'Are you sure you want to delete it?' } });
    dialogRef.afterClosed().pipe(take(1)).subscribe(result => {
      if (result) {
        this.actionService.delete(action.id).pipe(take(1)).subscribe(() => {
          this.refresh();
          this.snackBar.open(`Action "${action.name}" has been deleted.`, 'Dismiss', { duration: 3000 });
        });
      }
    });
  }

  export() {
    this.loading.show();
    this.actionService.getAll().pipe(
      take(1),
      tap(actions => {
        const data = actions.map(u => ({
          Name: u.name,
          Description: u.description
        }));
        const workSheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);
        const workBook: XLSX.WorkBook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(workBook, workSheet, 'Actions');
        XLSX.writeFile(workBook, 'actions.xlsx', { bookType: 'xlsx', type: 'buffer' });
      }),
      finalize(() => this.loading.hide())
    ).subscribe();
  }

}
