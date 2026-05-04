import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-profile-settings',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './profile-settings.component.html',
  styleUrl: './profile-settings.component.css'
})
export class ProfileSettingsComponent {

    userName: string | null = null;
    
    email: string | null = null;

    createdAt: Date | null = null;

   constructor(private api: UserService, private router: Router) {}

   ngOnInit() {
    console.log('ENTROU NO MY LIST'); // 

    this.api.getUser().subscribe((data: any) => {
        this.userName = data.userName;
        this.email = data.email;
        this.createdAt = data.createdAt;
        console.log(data);
      });
  }

  updateUser(){

    if(this.userName == null){
      console.error('Erro ao tentar atualizar1');
    }else{
      this.api.updateUser(this.userName).subscribe({
      next: (res) => {
        console.log(res);
        alert("Deu Bom");

        this.router.navigate(['/profile']);
      },
      error: (err) => {
        console.error('Erro ao tentar atualizar', err);
      }
    });
    }
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']).then(() => {
      window.location.reload();
    });
    console.log("Sai")
  }
}
