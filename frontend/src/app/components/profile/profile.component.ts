import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {

    userName: string | null = null;
    
    email: string | null = null;

    createdAt: Date | null = null;



    constructor(private api: UserService) {}

    ngOnInit() {
    console.log('ENTROU NO MY LIST'); // 

    this.api.getUser().subscribe((data: any) => {
        this.userName = data.userName;
        this.email = data.email;
        this.createdAt = data.createdAt;
        console.log(data);
      });
  }
}
