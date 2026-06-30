import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tick } from '@angular/core/testing';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

private apiGenreUrl = 'https://localhost:7219/Genre';

  constructor(private http: HttpClient) { }

  getGenre(){
  console.log("Chamando Genre");

    return this.http.get(this.apiGenreUrl);
  }

  getGenreById(id: number){
    return this.http.get(this.apiGenreUrl + `/${id}`);
  }

  postGenre(genre: any){
    return this.http.post<any>(this.apiGenreUrl, genre);
  }

  putGenre(genre: any, id: number){
    return this.http.put<any>(this.apiGenreUrl + `/${id}`, genre);
  }
  
  deleteGenre(id: number){
    return this.http.delete(this.apiGenreUrl + `/${id}`);
  }
}
