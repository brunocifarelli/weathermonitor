import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './create-user.services';  // Serviço de autenticação

@Component({
    selector: 'app-register',
    templateUrl: './create-user.component.html',
    styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent {
    username: string = '';
    password: string = '';
    nome: string = '';
    confirmPassword: string = '';
    errorMessage: string = '';
    successMessage: string = '';

    constructor(private userService: UserService, private router: Router) { }

    // Método para registrar um novo usuário
    register(): void {
        if (this.password !== this.confirmPassword) {
            this.errorMessage = 'As senhas não coincidem!';
            return;
        }

        const newUser = { username: this.username, password: this.password, nome: this.nome };

        // Chama o método register do AuthService
        this.userService.createUser(newUser).subscribe(
            (response) => {
                this.successMessage = 'Usuário cadastrado com sucesso!';
                this.router.navigate(['/login']);  // Redireciona para a tela de login após o sucesso
            },
            (error) => {
                this.errorMessage = 'Erro ao cadastrar. Tente novamente.';
            }
        );
    }
}