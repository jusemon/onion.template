import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Base, Page } from '../generics/models';
import { AuthService } from 'src/app/auth/auth.service';
import { handleResponse } from './rx-pipes';

/**
 * A base for the services
 *
 * @export
 */
export class BaseService<TEntity extends Base> {

    constructor(
        protected endpoint: string,
        protected http: HttpClient,
        protected snackBar: MatSnackBar,
        protected auth: AuthService
    ) {}

    /**
     * Get the options of the request
     *
     * @param [params] Dynamic params
     * @returns Return the options of the request
     */
    protected getOptions(params?: { [x: string]: any }) {
        return {
            headers: {
                Authorization: `Bearer ${this.auth.getToken()}`
            },
            params: params || {}
        };
    }

    /**
     * Get all the entities
     *
     * @param [urlController] The url controller
     * @returns A observable with a array of entities
     */
    public getAll(urlController?: string): Observable<TEntity[]> {
        const endpoint = typeof (urlController) !== 'undefined' ? urlController : this.endpoint;
        return this.http.get<TEntity[]>(`${endpoint}/`, this.getOptions())
            .pipe(handleResponse(this.snackBar));
    }

    /**
     * Get all the entities
     *
     * @param [urlController] The url controller
     * @returns A observable with a array of entities
     */
    public getPaged(params: { [x: string]: any }, urlController?: string): Observable<Page<TEntity>> {
        const endpoint = typeof (urlController) !== 'undefined' ? urlController : `${this.endpoint}/paged`;
        return this.http.get<Page<TEntity>>(`${endpoint}/`, this.getOptions(params))
            .pipe(handleResponse(this.snackBar));
    }

    /**
     * Get the entity by id
     *
     * @param id The entity id
     * @param [urlController] The url controller
     * @returns A observable with the entity
     */
    public get(id: number, urlController?: string): Observable<TEntity> {
        const endpoint = typeof (urlController) !== 'undefined' ? urlController : this.endpoint;
        return this.http.get<TEntity>(`${endpoint}/${id}`, this.getOptions())
            .pipe(handleResponse(this.snackBar));
    }

    /**
     * Create a entity
     *
     * @param entity The entity to create
     * @param [urlController] The url controller
     * @returns A observable with the entity
     */
    public create(entity: TEntity, urlController?: string): Observable<TEntity> {
        const endpoint = typeof (urlController) !== 'undefined' ? urlController : this.endpoint;
        return this.http.post<TEntity>(`${endpoint}/`, entity, this.getOptions())
            .pipe(handleResponse(this.snackBar));
    }

    /**
     * Update a entity
     *
     * @param entity The entity to update
     * @param [urlController] The url controller
     * @returns A observable with the entity
     */
    public update(entity: TEntity, urlController?: string): Observable<TEntity | any> {
        const endpoint = typeof (urlController) !== 'undefined' ? urlController : this.endpoint;
        return this.http.put<TEntity>(`${endpoint}/`, entity, this.getOptions())
            .pipe(handleResponse(this.snackBar));
    }

    /**
     * Delete a entity
     *
     * @param id The entity id
     * @param [urlController] The url controller
     * @returns A observable with the entity
     */
    public delete(id: number, urlController?: string): Observable<TEntity> {
        const endpoint = typeof (urlController) !== 'undefined' ? urlController : this.endpoint;
        return this.http.delete<TEntity>(`${endpoint}/${id}`, this.getOptions())
            .pipe(handleResponse(this.snackBar));
    }
}
