// auth.service.ts
import { Injectable, Inject,PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginRequest, LoginResponse, AuthUser, RegisterRequest } from '../../core/models/auth';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly API_URL = '/api';
  private readonly TOKEN_KEY = 'jwt_token';
  private jwtHelper = new JwtHelperService();
  private platformId!: Object;
  
  //behavior subject to hold the current user state. This obj is ideal for maintaining state
  private currentUserSubject = new BehaviorSubject<AuthUser | null>(null);
  //this prevents other components from directly modifying the current user state but still
  //able to subscribe to changes
  public currentUser$ = this.currentUserSubject.asObservable();
  
  constructor(private http: HttpClient, @Inject(PLATFORM_ID) platformId: Object,) {
    // Check for existing token on service initialization
    this.platformId = platformId;
    this.loadUserFromToken();
  }

  register(credentials: RegisterRequest){
    return this.http.post(`${this.API_URL}/auth/register`, credentials);
  }
  
  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.API_URL}/auth/login`, credentials)
      .pipe(
        tap({
          next: response => {
            console.log('Login response received:', response);
            
            if (response && response.token) {
              console.log('Token found in response, storing...');
              if (isPlatformBrowser(this.platformId)) {
                console.log('Running in browser context.');
                // Only access localStorage if running in the browser
                this.setToken(response.token);
                this.currentUserSubject.next(this.parseUserFromToken(response.token));
                console.log('User parsed from token:', this.currentUserSubject.value);
              }
              // this.setToken(response.token);
              // this.currentUserSubject.next(response.user);
            } else {
              console.error('No token in login response:', response);
            }
          },
          error: error => {
            console.error('Login request failed:', error);
          }
        })
      );
  }
  
  logout(): void {
    this.removeToken();
    this.currentUserSubject.next(null);
  }
  
  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }
  
  isAuthenticated(): boolean {
    if (!isPlatformBrowser(this.platformId)) {
      const token = this.getToken();

      //return token if true and false if not 
      return token ? !this.jwtHelper.isTokenExpired(token) : false;
    }
    return false;
  }
  
  hasRole(role: string): boolean {
    const user = this.currentUserSubject.value;
    return user?.roles.includes(role) || false;
  }
  
  private setToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }
  
  private removeToken(): void {
    localStorage.removeItem(this.TOKEN_KEY);
  }
  
  private loadUserFromToken(): void {
    const token = this.getToken();
    //if token exists and is not expired, parse user and update subject
    if (token && !this.isTokenExpired(token)) {
      const user = this.parseUserFromToken(token);
      this.currentUserSubject.next(user);
    }
  }
  
  private isTokenExpired(token: string): boolean {
    try {
      const payload = this.jwtHelper.decodeToken(token);
      //if the payload is null or has no expiry, consider it expired
      if (!payload || !payload.exp) return true;
      
      const expiry = payload.exp * 1000; // Convert to milliseconds
      return Date.now() > expiry;
    } catch {
      return true;
    }
  }
  
  private parseUserFromToken(token: string): AuthUser | null {
    try {
      const payload = this.jwtHelper.decodeToken(token);
      return {
        //For now only having the username and roles, i don't really like having the id
        //in the front end. I'll do it later for indexing purposes maybe.

        // id: payload[ClaimTypes.NameIdentifier] || payload.sub,
        username: payload[ClaimTypes.Name] || payload.name,
        // email: payload[ClaimTypes.Email] || payload.email,
        roles: this.extractRoles(payload[ClaimTypes.Role] || payload.role || [])
      };
    } catch {
      return null;
    }
  }

  private extractRoles(roleData: any): string[] {
    if (Array.isArray(roleData)) {
      return roleData;
    }
    if (typeof roleData === 'string') {
      return [roleData];
    }
    return [];
  }
}

// Helper constants to match your .NET ClaimTypes
const ClaimTypes = {
  NameIdentifier: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier',
  Name: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name',
  Email: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress',
  Role: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
};