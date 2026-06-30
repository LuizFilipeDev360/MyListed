import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserMediaService {

  private apiUserMediaUrl = 'https://localhost:7219/UserMedia';

  constructor(private http: HttpClient) { }

  getUserMedia() {
    const token = localStorage.getItem('token');

    return this.http.get(this.apiUserMediaUrl, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  getUserMediaById(id: number) {
    const token = localStorage.getItem('token');

    return this.http.get(this.apiUserMediaUrl+`/${id}`, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  postUserMedia(mediaId: any) {
    return this.http.post(this.apiUserMediaUrl, mediaId);
  }

  patchUserMedia(mediaId:number, mediaUser: any) {
    return this.http.patch(this.apiUserMediaUrl+`/${mediaId}`, mediaUser);
  }

  deleteUserMedia(mediaId: number){
    return this.http.delete(this.apiUserMediaUrl+`/${mediaId}`);
  }
}
