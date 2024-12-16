// auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private apiUrl = 'localhost:7099/api/Usuarios'; // Altere para sua URL de API

    constructor(private http: HttpClient, private router: Router) { }

    createUser(newUser: { username: string; password: string, nome: string }): Observable<any> {
        return this.http.post(`${this.apiUrl}/CreateUsuariosAsync`, newUser);
    }

}