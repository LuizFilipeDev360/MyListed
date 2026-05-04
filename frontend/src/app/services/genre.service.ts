import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

private apiGenreUrl = 'https://localhost:7219/Genre';

  constructor(private http: HttpClient) { }

  getGenre(){
    return this.http.get(this.apiGenreUrl);
  }
}
