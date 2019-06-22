import { DataSource } from '@angular/cdk/collections';
import { MatPaginator, MatSort } from '@angular/material';
import { tap, finalize } from 'rxjs/operators';
import { Observable, merge, BehaviorSubject, Subscription } from 'rxjs';
import { Page } from '../generics/models';


/**
 * Data source for tables. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
export class ServerSideDataSource<TEntity> extends DataSource<TEntity> {
  private dataSource: (params: { [x: string]: any; }) => Observable<Page<TEntity>>;
  private changesSub: Subscription = new Subscription();
  private itemsSubject = new BehaviorSubject<TEntity[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  loading$ = this.loadingSubject.asObservable();

  constructor(private paginator: MatPaginator, private sort: MatSort) {
    super();
  }

  setDataSource(getPaginated: (params: { [x: string]: any; }) => Observable<Page<TEntity>>): void {
    this.dataSource = getPaginated;
    this.changesSub.unsubscribe();
    this.changesSub = merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        tap(() => this.updateData())
      )
      .subscribe();
    this.updateData();
  }

  updateData() {
    this.loadingSubject.next(true);
    const pageSize = this.paginator.pageSize || 5;
    this.dataSource(
      {
        pageIndex: this.paginator.pageIndex,
        direction: this.sort.direction,
        sortBy: this.sort.active,
        pageSize
      })
      .pipe(
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe(page => {
        this.paginator.length = page.totalItems;
        this.itemsSubject.next(page.items);
      });
  }

  getData(): TEntity[] {
    return this.itemsSubject.getValue();
  }

  connect(): Observable<TEntity[]> {
    return this.itemsSubject.asObservable();
  }

  disconnect() {
    this.itemsSubject.complete();
    this.loadingSubject.complete();
    this.changesSub.unsubscribe();
  }
}
