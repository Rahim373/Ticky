import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../core/models/login-request';
import { ApiRoutes } from '../core/constants/routes';
import { TokenResponse } from '../core/models/tokenResponse';
import { ErrorResponse } from '../core/models/errorResponse';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private Token_Key: string = "token";

  constructor(private httpClient: HttpClient, private router: Router) { }

  login(request: LoginRequest): void {
    this.httpClient.post<TokenResponse | ErrorResponse>(ApiRoutes.getToken, request).
      subscribe((response: any) => {
        if (response.accessToken) {
          this.storeToken(response);
          this.router.navigate(["/dashboard"]);
        }
      });
  }

  isLoggedIn(): boolean {
    const token = this.retrieveToken();
    return (token?.accessToken && new Date(token?.accessTokenExpiration) > new Date()) || false;
  }

  storeToken(token: TokenResponse) {
    localStorage.setItem(this.Token_Key, JSON.stringify(token));
  }

  retrieveToken(): TokenResponse | null {
    const tokenString: string | null = localStorage.getItem(this.Token_Key);
    try {
      if (tokenString) {
        const token = JSON.parse(tokenString);
        return token;
      }
      return null;
    } catch {
      return null;
    }
  }
}
