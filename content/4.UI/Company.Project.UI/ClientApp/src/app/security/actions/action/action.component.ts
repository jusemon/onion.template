import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { FormComponent } from 'src/app/shared/components/form/form.component';
import { FormConfig } from 'src/app/shared/components/form/form.models';
import { Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { LoadingService } from 'src/app/shared/services/loading/loading.service';
import { ActionService } from '../services/action.service';
import { take, finalize } from 'rxjs/operators';
import { Actions } from '../actions.models';

@Component({
  selector: 'app-action',
  templateUrl: './action.component.html',
  styleUrls: ['./action.component.scss']
})
export class ActionComponent implements OnInit, OnDestroy {
  @ViewChild('form') appForm: FormComponent;
  editMode: boolean;
  id: number;

  config: FormConfig = {
    appearance: 'standard',
    colsPerRow: 2,
    fields: [{
      name: 'name',
      label: 'Name',
      type: 'text',
      validators: {
        required: {
          validator: Validators.required,
          message: 'Name is <strong>required</strong>'
        }
      }
    }, {
      name: 'description',
      label: 'Description',
      type: 'text',
      validators: {
        maxlength: {
          validator: Validators.maxLength(50),
          message: 'The desctiption can\'t have more than <strong>50</strong> characters'
        }
      }
    }]
  };

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar,
    private loading: LoadingService,
    private actionService: ActionService
  ) { }

  ngOnInit() {
    this.route.params.pipe(take(1)).subscribe((data) => {
      this.editMode = typeof (data.id) !== 'undefined';
      if (this.editMode) {
        this.id = data.id;
        this.get(data.id);
      }
    });
  }

  get(id: number) {
    this.actionService.get(id).pipe(take(1)).subscribe((action) => {
      this.appForm.form.setValue({
        name: action.name,
        description: action.description
      });
    });
  }

  onSubmit(action: Actions) {
    this.loading.show();
    const finalizeFunction = finalize(() => this.loading.hide());
    if (this.editMode) {
      action.id = this.id;
      this.actionService.update(action).pipe(take(1), finalizeFunction).subscribe(() => {
        this.snackBar.open(`Action "${action.name}" has been updated.`, 'Dismiss', { duration: 3000 });
        this.goBack();
      });
    } else {
      this.actionService.create(action).pipe(take(1), finalizeFunction).subscribe(() => {
        this.snackBar.open(`Action "${action.name}" has been created.`, 'Dismiss', { duration: 3000 });
        this.goBack();
      });
    }
  }

  goBack() {
    this.router.navigate(['security/actions']);
  }

  ngOnDestroy(): void { }
}
