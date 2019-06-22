import { Base } from 'src/app/shared/generics/models';

export interface Roles extends Base {
    name: string;
    isAdmin: boolean;
}
