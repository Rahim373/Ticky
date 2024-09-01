import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../core/models/login-request';
import { ApiRoutes } from '../core/constants/routes';
import { TokenResponse, User } from '../core/models/tokenResponse';
import { ErrorResponse } from '../core/models/errorResponse';
import { Router } from '@angular/router';
import { RoleEnum } from '../core/models/role';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private Token_Key: string = "token";
  private _authenticatedUser: User | undefined;

  constructor(private httpClient: HttpClient, private router: Router) { }

  /**
   * Get auth token from server
   * @param request username and password
   * @returns returns nothing. Ig the token is successfully retrived, the user is logged in to the app
   */
  async login(request: LoginRequest): Promise<boolean> {
    try {
      var response = await this.getAuthToken(request);
      if ("accessToken" in response) {
        const token = response as TokenResponse;
        this.storeToken(token);
        this._authenticatedUser = token.user;
        this.router.navigate(["app"]);
        return true;
      }
      return false;
    }
    catch (err) {
      return false;
    }
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
    this.router.navigate(['login']);
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

  isGranted(role: RoleEnum): boolean {
    return this.getUserRoles()?.includes(role) || false;
  }

  /**
   * 
   * @param request Gets auth token from server
   * @returns Return auth information
   */
  private getAuthToken(request: LoginRequest) {
    return lastValueFrom(this.httpClient.post<TokenResponse | ErrorResponse>(ApiRoutes.Auth.Token, request));
  }

  /**
   * Saves auth token into local storage
   * @param token 
   */
  private storeToken(token: TokenResponse) {
    localStorage.setItem(this.Token_Key, JSON.stringify(token));
  }

  /**
   * Retrives token from localstorage
   * @returns Returns token information
   */
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

  /**
   * Removes token from localstorage
   */
  private removeToken(): void {
    localStorage.clear();
    this.router.navigate(['login']);
  }
}
