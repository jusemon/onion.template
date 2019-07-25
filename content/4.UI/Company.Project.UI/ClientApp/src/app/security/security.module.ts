import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule } from '@angular/router';

const routes: Route[] = [
  { path: 'users', loadChildren: './users/users.module#UsersModule' },
  { path: 'actions', loadChildren: './actions/actions.module#ActionsModule' }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class SecurityModule { }
