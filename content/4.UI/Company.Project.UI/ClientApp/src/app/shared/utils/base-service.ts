import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MatSnackBar } from '@angular/material';
import { Base, Response, Page } from '../generics/models';
import { AuthService } from 'src/app/auth/auth.service';
import { handleResponse } from './rx-pipes';

/**
 * A base for the services
 *
 * @export
 */
export class BaseService<TEntity extends Base> {
    api: string;

    constructor(
        protected http: HttpClient,
        protected urlController: string,
        protected snackBar: MatSnackBar,
        protected auth: AuthService
    ) {
        this.api = environment.api;
    }

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
        const controller = typeof (urlController) !== 'undefined' ? urlController : this.urlController;
        return this.http.get<Response<TEntity[]>>(`${this.api}/${controller}/`, this.getOptions())
            .pipe(handleResponse<TEntity[]>());
    }

    /**
     * Get all the entities
     *
     * @param [urlController] The url controller
     * @returns A observable with a array of entities
     */
    public getPaged(params: { [x: string]: any }, urlController?: string): Observable<Page<TEntity>> {
        const controller = typeof (urlController) !== 'undefined' ? urlController : `${this.urlController}/paged`;
        return this.http.get<Response<Page<TEntity>>>(`${this.api}/${controller}/`, this.getOptions(params))
            .pipe(handleResponse());
    }

    /**
     * Get the entity by id
     *
     * @param id The entity id
     * @param [urlController] The url controller
     * @returns A observable with the entity
     */
    public get(id: number, urlController?: string): Observable<TEntity> {
        const controller = typeof (urlController) !== 'undefined' ? urlController : this.urlController;
        return this.http.get<Response<TEntity>>(`${this.api}/${controller}/${id}`, this.getOptions())
            .pipe(handleResponse());
    }

    /**
     * Create a entity
     *
     * @param entity The entity to create
     * @param [urlController] The url controller
     * @returns A observable with the entity
     */
    public create(entity: TEntity, urlController?: string): Observable<TEntity> {
        const controller = typeof (urlController) !== 'undefined' ? urlController : this.urlController;
        return this.http.post<Response<TEntity>>(`${this.api}/${controller}/`, entity, this.getOptions())
            .pipe(handleResponse());
    }

    /**
     * Update a entity
     *
     * @param entity The entity to update
     * @param [urlController] The url controller
     * @returns A observable with the entity
     */
    public update(entity: TEntity, urlController?: string): Observable<TEntity | any> {
        const controller = typeof (urlController) !== 'undefined' ? urlController : this.urlController;
        return this.http.put<Response<TEntity>>(`${this.api}/${controller}/`, entity, this.getOptions())
            .pipe(handleResponse());
    }

    /**
     * Delete a entity
     *
     * @param id The entity id
     * @param [urlController] The url controller
     * @returns A observable with the entity
     */
    public delete(id: number, urlController?: string): Observable<TEntity> {
        const controller = typeof (urlController) !== 'undefined' ? urlController : this.urlController;
        return this.http.delete<Response<TEntity>>(`${this.api}/${controller}/${id}`, this.getOptions())
            .pipe(handleResponse());
    }
}
