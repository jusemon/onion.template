import { Users } from 'src/app/security/users/users.models';

export interface Base {
    id: number;
    createdAt: Date;
    createdBy: number;
    lastUpdatedAt?: Date;
    lastUpdatedBy?: number;
    createdByUser?: Users;
    lastUpdatedByUser?: Users;
}

export interface Response<TEntity> {
    exceptionMessage: string;
    exceptionType: string;
    result: TEntity;
    isSuccess: boolean;
}

export interface Page<TEntity> {
    pageIndex: number;
    pageSize: number;
    totalItems: number;
    items: TEntity[];
}

export interface Dict<TEntity> {
    [key: string]: TEntity;
}

export interface PageRequest {
    pageIndex: number;
    isAsc: boolean;
    sortBy: string;
    pageSize: number;
}