import { Base } from 'src/app/shared/generics/models';
import { Roles } from '../roles/roles.models';

export interface Users extends Base {
    username: string;
    email: string;
    password: string;
    roleId: string;
    role: Roles;
    token: string;
}
