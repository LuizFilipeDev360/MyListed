import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  user = '';
  password = '';

  constructor(private auth: AuthService, private router: Router) {}

  login() {
    this.auth.login(this.user, this.password).subscribe({
      next: (res) => {
        console.log(res);

        localStorage.setItem('token', res.accessToken);

        this.router.navigate(['/mylist']).then(() => {
          window.location.reload();
        });
      },
      error: (err) => {
        console.error('Erro no login', err);
      }
    });
  }
}
