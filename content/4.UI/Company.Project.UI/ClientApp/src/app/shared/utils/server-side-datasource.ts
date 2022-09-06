import { DataSource} from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { tap, finalize, map } from 'rxjs/operators';
import { Observable, merge, BehaviorSubject, Subscription } from 'rxjs';
import { Page, PageRequest } from '../generics/models';


/**
 * Data source for tables. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
export class ServerSideDataSource<TEntity> extends DataSource<TEntity> {
  private dataSource?: (params: PageRequest) => Observable<Page<TEntity>>;
  private changesSub: Subscription = new Subscription();
  private itemsSubject = new BehaviorSubject<TEntity[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(true);
  public paginator!: MatPaginator;
  public sort!: MatSort;
  loading$ = this.loadingSubject.asObservable();

  constructor(private hasDetail = false) {
    super();
  }

  setDataSource(getPaginated: (params: PageRequest) => Observable<Page<TEntity>>): void {
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
    this.dataSource!(
      {
        pageIndex: this.paginator.pageIndex,
        isAsc: this.sort.direction === 'asc',
        sortBy: this.sort.active,
        pageSize
      })
      .pipe(
        map(r => {
          if (this.hasDetail) {
            const items: Array<any> = [];
            r.items.forEach(i => items.push(i, { detailRow: true, detail: i }));
            r.items = items;
          }
          return r;
        }),
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
