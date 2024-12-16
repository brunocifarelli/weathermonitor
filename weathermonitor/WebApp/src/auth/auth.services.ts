// auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private apiUrl = 'localhost:7099/api/Auth'; // Altere para sua URL de API

    constructor(private http: HttpClient, private router: Router) { }

    login(credentials: { username: string; password: string }): Observable<any> {
        return this.http.post(`${this.apiUrl}/login`, credentials);
    }

    logout(): void {
        localStorage.removeItem('token');
        this.router.navigate(['/login']);
    }

    getToken(): string | null {
        return localStorage.getItem('token');
    }

    isAuthenticated(): boolean {
        const token = this.getToken();
        return !!token;
    }
}