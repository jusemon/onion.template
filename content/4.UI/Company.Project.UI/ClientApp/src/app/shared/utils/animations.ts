import { trigger, state, style, transition, animate } from '@angular/animations';

export const detailExpand = trigger('detailExpand', [
    state('collapsed', style({ height: '0px', minHeight: '0' })),
    state('expanded', style({ height: '*' })),
    transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
]);

export const openClose = trigger('openClose', [
    state('open', style({ transform: 'rotate(0)' })),
    state('close', style({ transform: 'rotate(180deg)' })),
    transition('open => close', [animate('.2s')]),
    transition('close => open', [animate('.2s')])
]);
