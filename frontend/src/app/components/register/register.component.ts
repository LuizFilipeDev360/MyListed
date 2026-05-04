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

  register() {
    if (this.password !== this.confirmPassword) {
    alert('As senhas não coincidem');
    return;
  }


    this.auth.register(this.email, this.password, this.confirmPassword).subscribe({
      next: (res) => {
        console.log(res);

        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error('Erro no register', err);
      }
    });
  } 
}

