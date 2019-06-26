import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTable, MatDialog, MatSnackBar } from '@angular/material';
import { ServerSideDataSource } from 'src/app/shared/utils/server-side-datasource';
import { Users } from '../users.models';
import { UserService } from '../services/user.service';
import { ConfirmComponent } from 'src/app/shared/dialogs/confirm/confirm.component';
import { take, map, tap, finalize } from 'rxjs/operators';
import * as XLSX from 'xlsx';
import { detailExpand } from 'src/app/shared/utils/animations';
import { LoadingService } from 'src/app/shared/services/loading/loading.service';
@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss'],
  animations: [
    detailExpand
  ],
})
export class UsersListComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<any>;

  dataSource: ServerSideDataSource<Users>;
  displayedColumns = ['username', 'email', 'roleId', 'actions'];
  expandedElement: Users;
  isExpansionDetailRow = (i: number, row: any) => row.hasOwnProperty('detailRow');

  constructor(
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private loading: LoadingService,
    private userService: UserService) {
  }

  ngOnInit() {
    this.dataSource = new ServerSideDataSource(this.paginator, this.sort, true);
    this.dataSource.setDataSource((params: { [x: string]: any }) => this.userService.getPaged(params));
  }

  refresh() {
    this.dataSource.updateData();
  }

  delete(user: Users) {
    const dialogRef = this.dialog.open(ConfirmComponent, { width: '250px', data: { content: 'Are you sure you want to delete it?' } });
    dialogRef.afterClosed().pipe(take(1)).subscribe(result => {
      if (result) {
        this.userService.delete(user.id).pipe(take(1)).subscribe(() => {
          this.refresh();
          this.snackBar.open(`User "${user.username}" has been deleted.`, 'Dismiss', { duration: 3000 });
        });
      }
    });
  }

  export() {
    this.loading.show();
    this.userService.getAll().pipe(
      take(1),
      tap(users => {
        const data = users.map(u => ({
          Username: u.username,
          Email: u.email,
          Role: u.roleId
        }));
        const workSheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);
        const workBook: XLSX.WorkBook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(workBook, workSheet, 'Users');
        XLSX.writeFile(workBook, 'users.xlsx', { bookType: 'xlsx', type: 'buffer' });
      }),
      finalize(() => this.loading.hide())
    ).subscribe();
  }
}
