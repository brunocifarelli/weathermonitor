// app.component.ts
import { Component } from '@angular/core';
import { AuthService } from './auth/auth.services';  
import { Router } from '@angular/router'; 

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    title = 'WeatherMonitor'; 
    constructor(private authService: AuthService, private router: Router) { }

    // Método para logout
    logout() {
        this.authService.logout();  
        this.router.navigate(['/login']);  
    }

    // Verifica se o usuário está autenticado
    isAuthenticated(): boolean {
        return this.authService.isAuthenticated();
    }
}