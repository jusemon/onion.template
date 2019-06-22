export interface UserLogin {
    username: string;
    password: string;
}

export interface UserLoginToken {
    id?: number;
    username?: string;
    email?: string;
    token?: string;
}
