import { tap, map } from 'rxjs/operators';
import { Response } from '../generics/models';
import { MatSnackBar } from '@angular/material';

export function handleResponse<TEntity>(snackBar?: MatSnackBar) {
    return map((response: Response<TEntity>) => {
        if (!response.isSuccess) {
            if (snackBar) {
                snackBar.open(response.exceptionMessage, 'Dismiss', { duration: 3000 });
            }
            throw new Error(response.exceptionMessage);
        }
        return response.result;
    });
}
