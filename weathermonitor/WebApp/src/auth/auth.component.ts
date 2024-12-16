// auth.component.ts
import { Component } from '@angular/core';
import { AuthService } from './auth.services';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './auth.component.html',
    styleUrls: ['./auth.component.css']
})
export class AuthComponent {
    username: string = '';
    password: string = '';

    constructor(private authService: AuthService, private router: Router) { }

    onSubmit() {
        const credentials = { username: this.username, password: this.password };
        this.authService.login(credentials).subscribe(
            response => {
                localStorage.setItem('token', response.token);
                this.router.navigate(['/dashboard']);
            },
            error => {
                console.error(error);
                alert('Login falhou!');
            }
        );
    }
}