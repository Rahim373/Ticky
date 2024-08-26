import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../core/models/login-request';
import { ApiRoutes } from '../core/constants/routes';
import { TokenResponse, User } from '../core/models/tokenResponse';
import { ErrorResponse } from '../core/models/errorResponse';
import { Router } from '@angular/router';
import { RoleEnum } from '../core/models/role';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private Token_Key: string = "token";
  private _authenticatedUser: User | undefined;

  constructor(private httpClient: HttpClient, private router: Router) { }

  login(request: LoginRequest): void {
    this.httpClient.post<TokenResponse | ErrorResponse>(ApiRoutes.Auth.Token, request).
      subscribe((response: any) => {
        if (response.accessToken) {
          const token = response as TokenResponse;
          this.storeToken(token);
          this._authenticatedUser = token.user;
          this.router.navigate(["app"]);
        }
      });
  }

  getAccessToken(): string | null {
    const token = this.retrieveToken();
    return token?.accessToken ?? null;
  }

  isLoggedIn(): boolean {
    const token = this.retrieveToken();
    return (token?.accessToken && new Date(token?.accessTokenExpiration) > new Date()) || false;
  }

  logout() {
    this.removeToken();
  }

  getUserRoles(): RoleEnum[] | null {
    if (!this._authenticatedUser) {
      this.retrieveToken();
    }

    if (!this._authenticatedUser) {
      this.logout();
      return null;
    }

    return this._authenticatedUser.roles;
  }

  isGranted(role: RoleEnum) : boolean {
    return this.getUserRoles()?.includes(role) || false;
  }

  private storeToken(token: TokenResponse) {
    localStorage.setItem(this.Token_Key, JSON.stringify(token));
  }

  private retrieveToken(): TokenResponse | null {
    const tokenString: string | null = localStorage.getItem(this.Token_Key);
    try {
      if (tokenString) {
        const token: TokenResponse = JSON.parse(tokenString);

        if (!this._authenticatedUser) {
          this._authenticatedUser = token.user;
        }

        return token;
      }
      return null;
    } catch {
      return null;
    }
  }

  private removeToken(): void {
    localStorage.clear();
    this.router.navigate(['login']);
  }
}
