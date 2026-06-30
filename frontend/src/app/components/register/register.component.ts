import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  email = '';
  password = '';
  confirmPassword = '';

  constructor(private auth: AuthService, private router: Router) {}

   hasRegisterError = false;
   passwordMismatch = false;

  register() {
    if (this.password !== this.confirmPassword) {
    alert('As senhas não coincidem');
    this.passwordMismatch = true;
    return;
  }


    this.auth.register(this.email, this.password, this.confirmPassword).subscribe({
      next: (res) => {
        console.log(res);

        this.hasRegisterError = false;
        this.passwordMismatch = false;
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error('Erro no register', err);
        alert("Cadastro Inválido!")
        this.hasRegisterError = true;
      }
    });
  } 
}

