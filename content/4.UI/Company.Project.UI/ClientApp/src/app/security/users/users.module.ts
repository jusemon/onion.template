import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersComponent } from './users.component';
import { UserComponent } from './user/user.component';
import { UsersListComponent } from './users-list/users-list.component';
import { Route, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

const routes: Route[] = [
  { path: '', component: UsersComponent },
  { path: 'create', component: UserComponent },
  { path: 'update/:id', component: UserComponent }
];

@NgModule({
  declarations: [UsersComponent, UserComponent, UsersListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class UsersModule { }
