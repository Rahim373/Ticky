import { RoleEnum } from "./role";

export interface TokenResponse {
    accessToken: string;
    refreshToken: string;
    accessTokenExpiration: Date;
    user: User,
    roles: RoleEnum
}

export interface User {
    id: string;
    email: string;
    roles: RoleEnum[]
}

export interface Role {
    id: string;
    name: string;
    extends: string | null
}