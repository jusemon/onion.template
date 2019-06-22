import { Base } from 'src/app/shared/generics/models';
import { Roles } from '../roles/roles.models';
import { Actions } from '../actions/actions.models';

export interface Permissions extends Base {
    roleId: number;
    actionId: number;
    role: Roles;
    action: Actions;
}

