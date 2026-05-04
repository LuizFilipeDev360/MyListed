import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MediaService {

  private apiMediaUrl = 'https://localhost:7219/Media';

  constructor(private http: HttpClient) { }

  getMedia(){
    return this.http.get(this.apiMediaUrl);
  }

  getMediaByString(term: string) {
    return this.http.get(this.apiMediaUrl+`?mediaString=${term}`);
  }

  getMediaById(id: number){
    return this.http.get(this.apiMediaUrl+`/${id}`)
  }

  postMedia(media: any){
    return this.http.post<any>(this.apiMediaUrl, media);
  }

}
