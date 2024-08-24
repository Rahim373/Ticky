export interface TokenResponse {
    userId: string;
    email: string;
    accessToken: string;
    refreshToken: string;
    accessTokenExpiration: Date;
}
