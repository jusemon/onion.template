import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorMessage } from '../generics/models';

export function handleResponse<TEntity>(snackBar?: MatSnackBar) {
    return catchError((err: HttpErrorResponse, _: Observable<TEntity>) => {
        const response = err.error as ErrorMessage;
        if (snackBar) {
            snackBar.open(response.exceptionMessage, 'Dismiss', {
                duration: 3000,
            });
        }
        throw new Error(response.exceptionMessage);
    });
}
