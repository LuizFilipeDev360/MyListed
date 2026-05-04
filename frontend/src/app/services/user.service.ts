import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUserUrl = 'https://localhost:7219/User';

  constructor(private http: HttpClient) { }

  getUser(){
    const token = localStorage.getItem('token');

    return this.http.get(this.apiUserUrl+`/Profile`, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  updateUser(userName: string) {
  return this.http.put<any>(
    'https://localhost:7219/User/change-username',
    {},
    {
      params: { newUsername: userName }
    }
  );
}

}
