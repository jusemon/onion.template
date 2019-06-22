import { tap } from 'rxjs/operators';
import { Response } from '../generics/models';
import { MatSnackBar } from '@angular/material';

export function catchApiError<TEntity>(snackBar?: MatSnackBar) {
    return tap((response: Response<TEntity>) => {
        if (!response.isSuccess) {
            if (snackBar) {
                snackBar.open(response.exceptionMessage, 'Dismiss', { duration: 3000 });
            }
            throw new Error(response.exceptionMessage);
        }
    });
}
