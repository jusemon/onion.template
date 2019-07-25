import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActionsComponent } from './actions.component';
import { ActionComponent } from './action/action.component';
import { ActionsListComponent } from './actions-list/actions-list.component';
import { Route, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

const routes: Route[] = [
  { path: '', component: ActionsComponent },
  { path: 'create', component: ActionComponent },
  { path: 'update/:id', component: ActionComponent }
];

@NgModule({
  declarations: [ActionsComponent, ActionComponent, ActionsListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class ActionsModule { }
