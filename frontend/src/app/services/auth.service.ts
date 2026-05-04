import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  login(user: string, password: string) {
    return this.http.post<any>('https://localhost:7219/auth/login', {
      user,
      password
    });
  }

  register(email: string, password: string, confirmPassword: string){
    return this.http.post<any>('https://localhost:7219/auth/register', {
      email,
      password,
      confirmPassword
    });
  }

  getRole() {
    const token = localStorage.getItem('token');
    if (!token) return null;

    const decoded: any = jwtDecode(token);

    return decoded["role"] ||
           decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  }

  hasRole(roles: string[]) {
  const role = this.getRole();
  return roles.includes(role);
}


}
