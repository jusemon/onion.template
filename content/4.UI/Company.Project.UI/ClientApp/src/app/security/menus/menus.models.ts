import { Base } from 'src/app/shared/generics/models';
import { Actions } from '../actions/actions.models';

export interface Menus extends Base {
    name: string;
    icon: string;
    position: number;
    actionId: number;
    action: Actions;
}
