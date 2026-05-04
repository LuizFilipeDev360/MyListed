import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { jwtDecode } from 'jwt-decode';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule, FormsModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  isLogged = false;

  userName: string | null = null;

  searchTerm = '';

  role = '';

  constructor(private router: Router) {}

  ngOnInit() {

    const token = localStorage.getItem('token');

    this.isLogged = !!localStorage.getItem('token');

    if(token){
      const decoded: any = jwtDecode(token);
      this.userName = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
      this.role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    }

    console.log(localStorage.getItem('token'));
  }

  search(term: string) {
    this.router.navigate(['/results'], {
      queryParams: { search_query: term }
    });
  }
}
